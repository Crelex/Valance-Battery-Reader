using Microsoft.VisualBasic.CompilerServices;
using RJCP.IO.Ports;
using System;
using System.Threading;

namespace BatteryDialog
{
    public class RS232
    {
        public enum DataParity
        {
            Parity_None,
            Pariti_Odd,
            Parity_Even,
            Parity_Mark
        }

        public enum DataStopBit
        {
            StopBit_1 = 1,
            StopBit_2
        }

        private enum PurgeBuffers
        {
            RXAbort = 2,
            RXClear = 8,
            TxAbort = 1,
            TxClear = 4
        }

        private enum Lines
        {
            SetRts = 3,
            ClearRts,
            SetDtr,
            ClearDtr,
            ResetDev,
            SetBreak,
            ClearBreak
        }

        [Flags]
        public enum ModemStatusBits
        {
            ClearToSendOn = 0x10,
            DataSetReadyOn = 0x20,
            RingIndicatorOn = 0x40,
            CarrierDetect = 0x80
        }

        public enum Mode
        {
            NonOverlapped,
            Overlapped
        }

        [Flags]
        public enum EventMasks
        {
            RxChar = 0x1,
            RXFlag = 0x2,
            TxBufferEmpty = 0x4,
            ClearToSend = 0x8,
            DataSetReady = 0x10,
            ReceiveLine = 0x20,
            Break = 0x40,
            StatusError = 0x80,
            Ring = 0x100
        }

        private SerialPortStream myserial;

        private byte[] readbuffer;

        public int BaudRate
        {
            get
            {
                return myserial.BaudRate;
            }
            set
            {
                myserial.BaudRate = value;
            }
        }

        public int BufferSize
        {
            get
            {
                return myserial.ReadBufferSize;
            }
            set
            {
                readbuffer = new byte[checked(value + 1)];
                myserial.ReadBufferSize = value;
                myserial.WriteBufferSize = value;
            }
        }

        public int DataBit
        {
            get
            {
                return myserial.DataBits;
            }
            set
            {
                myserial.DataBits = value;
            }
        }

        public virtual byte[] InputStream => readbuffer;

        public string[] AvailableComPorts => SerialPortStream.GetPortNames();

        public bool IsOpen => myserial.IsOpen;

        public DataParity Parity
        {
            get
            {
                if (myserial.Parity == RJCP.IO.Ports.Parity.Even)
                {
                    return DataParity.Parity_Even;
                }
                if (myserial.Parity == RJCP.IO.Ports.Parity.Mark)
                {
                    return DataParity.Parity_Mark;
                }
                if (myserial.Parity == RJCP.IO.Ports.Parity.Odd)
                {
                    return DataParity.Pariti_Odd;
                }
                if (myserial.Parity == RJCP.IO.Ports.Parity.None)
                {
                    return DataParity.Parity_None;
                }
                return DataParity.Parity_None;
            }
            set
            {
                switch (value)
                {
                    case DataParity.Pariti_Odd:
                        myserial.Parity = RJCP.IO.Ports.Parity.Odd;
                        break;
                    case DataParity.Parity_Even:
                        myserial.Parity = RJCP.IO.Ports.Parity.Even;
                        break;
                    case DataParity.Parity_Mark:
                        myserial.Parity = RJCP.IO.Ports.Parity.Mark;
                        break;
                    case DataParity.Parity_None:
                        myserial.Parity = RJCP.IO.Ports.Parity.None;
                        break;
                    default:
                        myserial.Parity = RJCP.IO.Ports.Parity.None;
                        break;
                }
            }
        }

        public string Port
        {
            get
            {
                return myserial.PortName;
            }
            set
            {
                myserial.PortName = value;
            }
        }

        public DataStopBit StopBit
        {
            get
            {
                if (myserial.StopBits == StopBits.One)
                {
                    return DataStopBit.StopBit_1;
                }
                if (myserial.StopBits == StopBits.Two)
                {
                    return DataStopBit.StopBit_2;
                }
                return DataStopBit.StopBit_1;
            }
            set
            {
                switch (value)
                {
                    case DataStopBit.StopBit_1:
                        myserial.StopBits = StopBits.One;
                        break;
                    case DataStopBit.StopBit_2:
                        myserial.StopBits = StopBits.Two;
                        break;
                    default:
                        myserial.StopBits = StopBits.One;
                        break;
                }
            }
        }

        public virtual int Timeout
        {
            get
            {
                return myserial.ReadTimeout;
            }
            set
            {
                myserial.ReadTimeout = value;
                myserial.WriteTimeout = value;
            }
        }

        public RS232()
        {
            myserial = new SerialPortStream();
        }

        public void ClearInputBuffer()
        {
            myserial.DiscardInBuffer();
        }

        public void Close()
        {
            myserial.Close();
        }

        public void Open(string Port, int BaudRate, int DataBit, DataParity Parity, DataStopBit StopBit, int BufferSize)
        {
            myserial.PortName = Port;
            myserial.BaudRate = BaudRate;
            myserial.DataBits = DataBit;
            myserial.ParityReplace = 0;
            switch (Parity)
            {
                case DataParity.Pariti_Odd:
                    myserial.Parity = RJCP.IO.Ports.Parity.Odd;
                    break;
                case DataParity.Parity_Even:
                    myserial.Parity = RJCP.IO.Ports.Parity.Even;
                    break;
                case DataParity.Parity_Mark:
                    myserial.Parity = RJCP.IO.Ports.Parity.Mark;
                    break;
                case DataParity.Parity_None:
                    myserial.Parity = RJCP.IO.Ports.Parity.None;
                    break;
                default:
                    myserial.Parity = RJCP.IO.Ports.Parity.None;
                    break;
            }
            switch (StopBit)
            {
                case DataStopBit.StopBit_1:
                    myserial.StopBits = StopBits.One;
                    break;
                case DataStopBit.StopBit_2:
                    myserial.StopBits = StopBits.Two;
                    break;
                default:
                    myserial.StopBits = StopBits.One;
                    break;
            }
            myserial.ReadTimeout = 75;
            myserial.WriteTimeout = 75;
            myserial.ReadBufferSize = BufferSize;
            myserial.WriteBufferSize = BufferSize;
            readbuffer = new byte[checked(BufferSize + 1)];
            myserial.Open();
            myserial.DiscardInBuffer();
            myserial.DiscardOutBuffer();
            //Thread.Sleep(100);
        }

        public int Read(int Bytes2Read)
        {
            int i = 0;
            if (!myserial.IsOpen)
            {
                throw new ApplicationException("Please initialize and open port before using this method");
            }
            checked
            {
                try
                {
                    int num;
                    for (; i < Bytes2Read; i += num)
                    {
                        num = myserial.Read(readbuffer, i, Bytes2Read - i);
                    }
                    return i;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    throw new ApplicationException("Read Error: " + ex2.Message, ex2);
                }
            }
        }

        public void Write(byte[] Buffer)
        {
            if (!myserial.IsOpen)
            {
                throw new ApplicationException("Please initialize and open port before using this method");
            }
            try
            {
                myserial.DiscardInBuffer();
                myserial.Write(Buffer, 0, Buffer.Length);
                Thread.Sleep(50);
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                throw;
            }
        }
    }
}
