using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using BatteryDialog;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace mydaemon
{
    // attempted to make it an async service, but ... think about it "serial communication" lmfao, it had issues to say the least...
    public class DaemonService
    {
        private static System.Timers.Timer timer = new System.Timers.Timer();

        private static string[] strCellVolt = new string[4];

        private static decimal strCellVoltDiff;

        private static decimal strCellVoltMin;

        private static decimal strCellVoltMax;

        private static string[] strCellTemp = new string[6];

        private static string strModuleV;

        private static string strPCBATemp;

        private static RS232 paramcom = new RS232();

        private static ValenceBattery paramBattery = new ValenceBattery();

        private static string strVoltNO;

        private static string strCurrentID;

        private static string strCurrentCOM;

        private static string strErrorMessage;

        private static int strID;

        private static string m_model;

        private static bool boolCommnucateflag;

        private static bool boolFirstBalanceFlag;

        private static WriteApi _writeApi;

        private static int _startingBatteryModuleId;
        public DaemonService(WriteApi writeApi) { 
            _writeApi = writeApi;
            _startingBatteryModuleId = 1;
        }
        public void start()
        {
            strCurrentCOM = "COM6";
            SetTimer(3000);
        }


        private static void SetTimer(int invertal)
        {
            timer = new System.Timers.Timer(invertal);
            timer.Elapsed += CollectSensorInformation_Tick;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void CollectSensorInformation_Tick(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (_startingBatteryModuleId > 10)
                    _startingBatteryModuleId = 1;

                OpenBatteryCommunicationChannel(_startingBatteryModuleId);
                ReadSensors();

                _writeApi.WriteMeasurement("dreamstream", "spikersoft", WritePrecision.Ns, paramBattery);
                _startingBatteryModuleId++;
            }
            catch (Exception ex)
            {
                StopRead();
                SetTimer(350);
            }

        }

        private static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void StopRead()
        {
            if (paramcom.IsOpen)
            {
                paramcom.Close();
            }
            timer.Stop();
        }

        private static void OpenBatteryCommunicationChannel(int moduleID = 10)
        {
            strCurrentID = moduleID.ToString();
            moduleID = moduleID - 1;
            
            checked
            {
                try
            {
                    boolFirstBalanceFlag = true;
                    
                    if (paramcom.IsOpen)
                        {
                            paramcom.Close();
                        }
                        try
                        {
                            paramcom.Open(strCurrentCOM, 9600, 8, RS232.DataParity.Parity_Mark, RS232.DataStopBit.StopBit_1, 512);
                            paramcom.Write(paramBattery.Wakeup());
                            paramcom.Close();
                            paramcom.Open(strCurrentCOM, 115200, 8, RS232.DataParity.Parity_Mark, RS232.DataStopBit.StopBit_1, 512);
                    }
                        catch (Exception ex3)
                        {
                            WriteError("Incorrect com port selected!");
                            throw new Exception("Open communication port fail");
                        }
                            try
                            {
                                paramBattery.ADDRESS = moduleID;
                                paramcom.Write(paramBattery.ModelReadSend());
                                if (paramcom.Read(9) != -1 && paramBattery.ModelReadReturn(paramcom.InputStream))
                                {
                                    strID = (int)Math.Round((double)strID + Convert.ToDouble((moduleID + 1).ToString()));
                                    if (Convert.ToDouble(strCurrentID) == (double)strID)
                                    {
                                        m_model = paramBattery.MODEL;
                                        paramcom.Close();
                            }
                                    strID = 0;
                                }
                            }
                            catch (Exception ex5)
                            {
                                throw ex5;
                            }
                    switch (m_model)
                        {
                            case "U1-12XP Rev. 1":
                            case "U24-12XP Rev. 1":
                            case "U27-12XP Rev. 1":
                            case "U1-12XP Rev. 2":
                            case "U24-12XP Rev. 2":
                            case "U27-12XP Rev. 2":
                                strVoltNO = Convert.ToString(4);
                                break;
                            case "UEV-18XP Rev. 1":
                            case "UEV-18XP Rev. 2":
                                strVoltNO = Convert.ToString(6);
                                break;
                        }
                        paramBattery.MODE = strVoltNO;
                        paramBattery.ADDRESS = moduleID;
                         
                            if (paramBattery.HardwareRevision == 2)
                            try
                            {
                                paramcom.Open(strCurrentCOM, 9600, 8, RS232.DataParity.Parity_Mark, RS232.DataStopBit.StopBit_1, 512);
                        }
                            catch (Exception ex7)
                            {
                                throw new Exception("Open Communication Port Fail");
                            }
                            paramBattery.MODE = strVoltNO;
                            try
                            {
                                paramcom.Write(paramBattery.Wakeup());
                            }
                            catch (Exception ex)
                            {
                                //TODO log
                                throw new Exception("Wakeup Battery Fail");
                            }
                            paramcom.Close();
                            try
                            {
                                paramcom.Open(strCurrentCOM, 115200, 8, RS232.DataParity.Parity_Mark, RS232.DataStopBit.StopBit_1, 512);
                            }
                            catch (Exception ex11)
                            {
                                throw new Exception("Open communication port Fail");
                            }
                            try
                            {
                                Openbalancing(paramcom, paramBattery);
                            }
                            catch (Exception ex13)
                            {
                                throw new Exception("Pls check config:the Com port or ModuleID ");
                            }
                            paramcom.Write(paramBattery.ExitCalibrationMode());
                            paramBattery.MODE = strVoltNO;
                            strErrorMessage = "";
                            timer.Enabled = true;
                            boolCommnucateflag = true;
                }
                catch (Exception ex15)
                {
                    paramcom.Close();
                    WriteError($"Exception: { ex15.Message }");
                }
                finally
                {
                }
            }
        }

        private static void ReadSensors()
        {
            if (boolCommnucateflag)
            {
                try
                {
                    ReadBatteryBankVoltage(paramcom, paramBattery);
                    ReadBatteryTemperature(paramcom, paramBattery);
                   // ReadRev2Data(paramcom, paramBattery);

                    bool flag = paramBattery.Bal_Cell_1 | paramBattery.Bal_Cell_2 | paramBattery.Bal_Cell_3 | paramBattery.Bal_Cell_4;

                    if (Convert.ToDouble(strVoltNO) == 6.0)
                    {
                        flag = (flag | paramBattery.Bal_Cell_5 | paramBattery.Bal_Cell_6);
                    }
                    if (paramBattery.Balancing || flag)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Balance Active");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Balance Inactive");
                    }
                    if (boolFirstBalanceFlag)
                    {
                        try
                        {
                            Openbalancing(paramcom, paramBattery);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Pls check config:the Com port or ModuleID ");
                        }
                        boolFirstBalanceFlag = false;
                    }
                    try
                    {
                        ReadBatterySoc(paramcom, paramBattery);
                    }
                    catch (Exception ex3)
                    {
                        strErrorMessage = "Communication Error Reading SoC";
                    }
                    //try
                    //{
                    //    ReadBatteryVersion(paramcom, paramBattery);
                    //}
                    //catch (Exception ex5)
                    //{
                    //    strErrorMessage = "Communication Error Reading Battery Version";
                    //}
                    try
                    {
                        ReadBatteryBalanceStatus(paramcom, paramBattery);
                    }
                    catch (Exception ex7)
                    {
                        strErrorMessage = "Communication Error Reading Battery Status";
                    }
                    if (string.IsNullOrEmpty(strErrorMessage))
                    {
                     //   Console.WriteLine("No Error!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error!: {strErrorMessage.ToString()}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    strErrorMessage = "";
                }
                catch (Exception ex9)
                {
                    Exception ex10 = ex9;
                    timer.Enabled = false;
                    boolCommnucateflag = false;
                    if (paramcom.IsOpen)
                    {
                        paramcom.Close();
                    }
                }
            }
            else
            {
                try
                {
                    ReadBatterySocRefresh(paramcom, paramBattery);
                }
                catch (Exception ex11)
                {
                    throw ex11;
                }
            }
        }
        private static void ReadBatterySocRefresh(RS232 paramcom, ValenceBattery paramBattery)
        {
            try
            {
                paramcom.Write(paramBattery.SNSOCReadSend());
                if (paramcom.Read(23) == -1)
                {
                    throw new Exception("Read SOC Error on SNSOCReadSend!");
                }
                if (!paramBattery.SNSOCReadReturn(paramcom.InputStream))
                {
                    throw new Exception("Read SOC Error on SNSOCReadReturn!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        private static void ReadBatteryBalanceStatus(RS232 paramcom, ValenceBattery paramBattery)
        {
            try
            {
                paramcom.Write(paramBattery.BalanceReadSend());
                if (paramcom.Read(9) == -1)
                {
                    throw new Exception("Read SOC Error!");
                }
                if (paramBattery.BalanceReadReturn(paramcom.InputStream))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Balance: Enabled");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write("Balance: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Disabled");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private static void ReadBatteryVersion(RS232 paramcom, ValenceBattery paramBattery)
        {
            try
            {
                paramcom.Write(paramBattery.BatteryVersionRead());
                if (paramcom.Read(51) == -1)
                {
                    throw new Exception("Read Battery Version  Error!");
                }
                if (!paramBattery.BatteryVersionReturn(paramcom.InputStream))
                {
                    throw new Exception("Read Battery Version  Error!");
                }
                Console.WriteLine($"Version: {paramBattery.Version}");
                Console.WriteLine($"Firmware Date: {paramBattery.BuildRevision}");
                Console.WriteLine($"Revision: {paramBattery.Revision}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private static void ReadBatteryBankVoltage(RS232 paramcom, ValenceBattery paramBattery)
        {
            try
            {
                paramcom.Write(paramBattery.VoltRead());
                if (paramcom.Read(25) == -1)
                {
                    throw new Exception("Read Voltage Error!");
                }
                if (!paramBattery.VoltReturn(paramcom.InputStream))
                {
                    throw new Exception("Read Voltage Error!");
                }

                strCellVolt[0] = Convert.ToString(paramBattery.VOLT1);
                strCellVolt[1] = Convert.ToString(paramBattery.VOLT2 );
                strCellVolt[2] = Convert.ToString(paramBattery.VOLT3);
                strCellVolt[3] = Convert.ToString(paramBattery.VOLT4);
                strModuleV = Convert.ToString(paramBattery.VOLTS);
                strCellVoltDiff = (checked(paramBattery.CellVoltage_Max - paramBattery.CellVoltage_Min) * 0.001m);
                strCellVoltMin = paramBattery.CellVoltage_Min * 0.001m;
                strCellVoltMax = paramBattery.CellVoltage_Max * 0.001m;

                Console.WriteLine($"C1 voltage: {strCellVolt[0]}");
                Console.WriteLine($"C2 voltage: {strCellVolt[1]}");
                Console.WriteLine($"C3 voltage: {strCellVolt[2]}");
                Console.WriteLine($"C4 voltage: {strCellVolt[3]}");

                Console.WriteLine($"Voltage: {strModuleV}");
                Console.WriteLine($"Cell Voltage Difference: {Convert.ToString(strCellVoltDiff)}");
                Console.WriteLine($"Cell Voltage Min: {Convert.ToString(strCellVoltMin)}");
                Console.WriteLine($"Cell Voltage Max: {Convert.ToString(strCellVoltMax)}");

                #region TODO in other software
                // monitoring TODO: implement things like this in the visual console that reads from the dataset directly**
                //if (strCellVoltDiff > strStandardVD)
                //{
                //    //TxtStatusVD.BackColor = Color.Pink;
                //    //strVoltDiffStatus = "FAIL";
                //}
                //else
                //{
                //    //TxtStatusVD.BackColor = Color.GreenYellow;
                //    //strVoltDiffStatus = "PASS";
                //}
                //if (strCellVoltMin < strStandardCellMin)
                //{
                //    //txtCellMin.BackColor = Color.Pink;
                //    //strVoltMinStatus = "FAIL";
                //}
                //else
                //{
                //    //txtCellMin.BackColor = Color.GreenYellow;
                //    //strVoltMinStatus = "PASS";
                //}
                //if (strCellVoltMax > strStandardCellMax)
                //{
                //    //txtCellMax.BackColor = Color.Pink;
                //}
                //else
                //{
                //    //txtCellMax.BackColor = Color.GreenYellow;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                strErrorMessage = "Communication Error";
                WriteError(ex.Message.ToString());
            }
        }

        private static void ReadBatteryTemperature(RS232 paramcom, ValenceBattery paramBattery)
        {
            try
            {
                paramcom.Write(paramBattery.TempRead());
                if (paramcom.Read(21) == -1)
                {
                    throw new Exception("Read Temperature Error!");
                }
                if (!paramBattery.TempReturn(paramcom.InputStream))
                {
                    throw new Exception("Read Temperature Error!");
                }

                strCellTemp[0] = Convert.ToString(paramBattery.TEMP1);
                strCellTemp[1] = Convert.ToString(paramBattery.TEMP2);
                strCellTemp[2] = Convert.ToString(paramBattery.TEMP3);
                strCellTemp[3] = Convert.ToString(paramBattery.TEMP4);
                //strCellTemp[4] = Convert.ToString(paramBattery.TEMP5);
                //strCellTemp[5] = Convert.ToString(paramBattery.TEMP6);
                strPCBATemp = Convert.ToString(paramBattery.TEMPPCB);

                Console.WriteLine($"C1 Temp: {Convert.ToString(paramBattery.TEMP1)}");
                Console.WriteLine($"C2 Temp: {Convert.ToString(paramBattery.TEMP2)}");
                Console.WriteLine($"C3 Temp: {Convert.ToString(paramBattery.TEMP3)}");
                Console.WriteLine($"C4 Temp: {Convert.ToString(paramBattery.TEMP4)}");
                Console.WriteLine($"PCBA Temp: {Convert.ToString(paramBattery.TEMPPCB)}");
            }
            catch (Exception ex)
            {
                strErrorMessage = "Communication Error";
                WriteError(ex.Message.ToString());
            }
        }

        private static void ReadRev2Data(RS232 paramcom, ValenceBattery parambattery)
        {
            try
            {
                ReadMaximums(paramcom, parambattery);
                ReadCalCycleCount(paramcom, parambattery);
                ReadStatusByte(paramcom, parambattery);
                if (parambattery.HardwareRevision == 2)
                {
                    ReadWattHour(paramcom, parambattery);
                    ReadEventLog(paramcom, parambattery);
                    ReadEventLog2(paramcom, parambattery);
                    if (parambattery.Balancing)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Balance Active");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Balance Inactive");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine($"Battery Failure? {parambattery.fault_BatteryFailure}");
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                strErrorMessage = "Communication Error";
            }
        }

        private static void ReadEventLog2(RS232 paracom, ValenceBattery paramBattery)
        {
            try
            {
                paracom.Write(paramBattery.EventLog2Read());
                if (paracom.Read(43) == -1)
                {
                    throw new Exception("Read Event Log 2 Error");
                }
                if (!paramBattery.EventLog2Return(paracom.InputStream))
                {
                    throw new Exception("Read Event Log 2 Error");
                }
                Console.WriteLine($"Fault_UV_S: {paramBattery.Fault_UV_S.ToString()}");
                Console.WriteLine($"Fault_OV_S: {paramBattery.Fault_OV_S.ToString()}");
                Console.WriteLine($"Fault_OCC_S: {paramBattery.Fault_OCC_S.ToString()}");
                Console.WriteLine($"Fault_OCD_S: {paramBattery.Fault_OCD_S.ToString()}");
                Console.WriteLine($"Fault_UT_S: {paramBattery.Fault_UT_S.ToString()}");
                Console.WriteLine($"Fault_OT_S: {paramBattery.Fault_OT_S.ToString()}");
                Console.WriteLine($"Reset Count: {paramBattery.ResetCount.ToString()}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private static void ReadEventLog(RS232 paracom, ValenceBattery paramBattery)
        {
            try
            {
                paracom.Write(paramBattery.EventLogRead());
                if (paracom.Read(30) == -1)
                {
                    throw new Exception("Read Event Log Error!");
                }
                if (!paramBattery.EventLogReturn(paracom.InputStream))
                {
                    throw new Exception("Read Event Log Error!");
                }
                Console.WriteLine($"Max Temp: {Convert.ToString(paramBattery.HTLimit)}");
                Console.WriteLine($"Min Temp: {Convert.ToString(paramBattery.LTLimit)}");
                Console.WriteLine($"MaxDschrgCurrent: {Convert.ToString(paramBattery.MaxDschrgCurrent)}");
                Console.WriteLine($"MaxChrgCurrent: {Convert.ToString(paramBattery.MaxChrgCurrent)}");
                Console.WriteLine($"CalibrationCorrection: {Convert.ToString(paramBattery.CalibrationCorrection)}");
                Console.WriteLine($"ExceedHighOutput: {Convert.ToString(paramBattery.ExceedHighOutput)}");
                Console.WriteLine($"DischargeCutoff: {Convert.ToString(paramBattery.DischargeCutoff)}");
                Console.WriteLine($"ChargeCutoff: {Convert.ToString(paramBattery.ChargeCutoff)}");
                Console.WriteLine($"CommunicationErrors: {Convert.ToString(paramBattery.CommunicationErrors)}");
                Console.WriteLine($"IntraModuleBalanceCount: {Convert.ToString(paramBattery.IntraModuleBalanceCount)}");
                Console.WriteLine($"InterModuleBalanceCount: {Convert.ToString(paramBattery.InterModuleBalanceCount)}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private static void ReadWattHour(RS232 paramcom, ValenceBattery parambattery)
        {
            try
            {
                paramcom.Write(parambattery.BatteryWHRead());
                if (paramcom.Read(11) != -1)
                {
                    if (!parambattery.BatteryWHReturn(paramcom.InputStream))
                    {
                        throw new Exception("Read Watt Hour Error");
                    }
                    Console.WriteLine($"Watt Hours: {Convert.ToString(parambattery.WattHour)}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private static void ReadStatusByte(RS232 paramcom, ValenceBattery parambattery)
        {
            try
            {
                paramcom.Write(parambattery.BatteryStatusRead());
                if (paramcom.Read(9) != -1)
                {
                    if (!parambattery.BatteryStatusReturn(paramcom.InputStream))
                    {
                        throw new Exception("Read Battery Status Error");
                    }
                    Console.WriteLine("Battery Status Flags");
                    Console.WriteLine($"Under 2.3V: {parambattery.fault_UV2v3}");
                    Console.WriteLine($"Under 2.5V: {parambattery.fault_UV2v5}");
                    // Console.WriteLine($"Under 2.3V w/ Chg Curr: ")
                    Console.WriteLine($"Exceed 4V: {parambattery.fault_OV4v0}");
                    Console.WriteLine($"Exceed 4.5V: {parambattery.fault_OV4v5}");
                    Console.WriteLine($"Cell Over 60c: {parambattery.fault_OTcell60c}");
                    Console.WriteLine($"Cell Over 65c: {parambattery.fault_OTcell65c}");
                    Console.WriteLine($"PCBA Over 80c: {parambattery.fault_OTpcba80c}");
                    Console.WriteLine($"Cell Over 100c: {parambattery.fault_OTpcba100c}");
                    Console.WriteLine($"NMI Error: {parambattery.fault_NMIFailure}");
                    Console.WriteLine($"Battery Failure: {parambattery.fault_WeakCellBank}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private static void ReadCalCycleCount(RS232 paracom, ValenceBattery paramBattery)
        {
            try
            {
                paracom.Write(paramBattery.BatteryCycleRead());
                if (paracom.Read(11) == -1)
                {
                    throw new Exception("Read Num Cycle Error!");
                }
                if (!paramBattery.BatteryCycleReturn(paracom.InputStream))
                {
                    throw new Exception("Read Num Cycle Error!");
                }
                Console.WriteLine($"NumChargeDischargeCycle: {Convert.ToString(paramBattery.NumChargeDischargeCycle)}");
                Console.WriteLine($"NumHardwareCalibration: {Convert.ToString(paramBattery.NumHardwareCalibration)}");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ReadMaximums(RS232 paracom, ValenceBattery paramBattery)
        {
            try
            {
                paracom.Write(paramBattery.BatteryMaximumRead());
                if (paracom.Read(21) == -1)
                {
                    throw new Exception("Read Maximums Error!");
                }
                if (!paramBattery.BatteryMaximumReturn(paracom.InputStream))
                {
                    throw new Exception("Read Maximums Error!");
                }
                Console.WriteLine($"Max Temp (life)): {Convert.ToString(paramBattery.MaxBattLifeTemperature)}");
                Console.WriteLine($"Min Temp (life): {Convert.ToString(paramBattery.MinBattLifeTemperature)}");
                Console.WriteLine($"Max Cell Voltage: { Convert.ToString(paramBattery.MaximumCellVolt)}");
                Console.WriteLine($"Min Cell Voltage: { Convert.ToString(paramBattery.MinimumCellVolt)}");
                Console.WriteLine($"Fault Oscillator Count: { Convert.ToString(paramBattery.FaultOscillatorCount.ToString())}");
                Console.WriteLine($"Fault Memory Access Count: { Convert.ToString(paramBattery.FaultMemAccessCount.ToString())}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ReadBatterySoc(RS232 paramcom, ValenceBattery paramBattery)
        {
            try
            {
                paramcom.Write(paramBattery.SNSOCReadSend());
                if (paramcom.Read(23) == -1)
                {
                    throw new Exception("Read SOC Error!");
                }
                if (!paramBattery.SNSOCReadReturn(paramcom.InputStream))
                {
                    throw new Exception("Read SOC Error!");
                }
                Console.WriteLine($"VerfN: {paramBattery.VerfN}");
                Console.WriteLine($"VerfP: {paramBattery.VerfP}");
                Console.WriteLine($"GainN: {paramBattery.GainN}");
                Console.WriteLine($"GainP: {paramBattery.GainP}");
                Console.WriteLine($"SoC: {Convert.ToString(Math.Round(paramBattery.SOC, 3))}");
                Console.WriteLine($"Current: {paramBattery.CURRENT}");
                Console.WriteLine($"Model: {paramBattery.MODEL}");
                Console.WriteLine($"Serial Number: { Convert.ToInt32(paramBattery.SN).ToString("00000")}");

                if (paramBattery.Bal_Cell_1)
                {
                    //TxtStatusV1.BackColor = Color.PowderBlue;
                }
                else
                {
                    //TxtStatusV1.BackColor = Color.White;
                }
                if (paramBattery.Bal_Cell_2)
                {
                    //TxtStatusV2.BackColor = Color.PowderBlue;
                }
                else
                {
                    //TxtStatusV2.BackColor = Color.White;
                }
                if (paramBattery.Bal_Cell_3)
                {
                    //TxtStatusV3.BackColor = Color.PowderBlue;
                }
                else
                {
                    //TxtStatusV3.BackColor = Color.White;
                }
                if (paramBattery.Bal_Cell_4)
                {
                    //TxtStatusV4.BackColor = Color.PowderBlue;
                }
                else
                {
                    //TxtStatusV4.BackColor = Color.White;
                }
                if (Convert.ToDouble(paramBattery.MODE) == 6.0)
                {
                    if (paramBattery.Bal_Cell_5)
                    {
                        //TxtStatusV5.BackColor = Color.PowderBlue;
                    }
                    else
                    {
                        //TxtStatusV5.BackColor = Color.White;
                    }
                    if (paramBattery.Bal_Cell_6)
                    {
                        //TxtStatusV6.BackColor = Color.PowderBlue;
                    }
                    else
                    {
                        //TxtStatusV6.BackColor = Color.White;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void Openbalancing(RS232 paramcom, ValenceBattery paramBattery)
        {
            var errorMsg = "Open balancing Return  Error!";
            try
            {
                paramcom.Write(paramBattery.OpenbalancingRead());
                if (paramcom.Read(6) == -1)
                {
                    throw new Exception(errorMsg);
                }
                if (!paramBattery.OpenbalancingReturn(paramcom.InputStream))
                {
                    throw new Exception(errorMsg);
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message.ToString());
                throw new Exception("Error during  Open balancing check: " + ex.Message);
            }
        }
    }
}