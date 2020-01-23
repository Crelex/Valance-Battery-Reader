using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.CompilerServices;

namespace BatteryDialog
{
    public class Communication
    {
        private const int Mod_InfoFrashWrite = 128;

        private object mvarm_addr;

        private object mvarm_volt1;

        private object mvarm_volt2;

        private object mvarm_volt3;

        private object mvarm_volt4;

        private object mvarm_volt5;

        private object mvarm_volt6;

        private object mvarm_volts;

        private object mvarm_min_volt_sample;

        private object mvarm_max_volt_sample;

        private object mvarm_temppcb;

        private object mvarm_temp1;

        private object mvarm_temp2;

        private object mvarm_temp3;

        private object mvarm_temp4;

        private object mvarm_temp5;

        private object mvarm_temp6;

        private object mvarm_sn;

        private object mvarm_soc;

        private object mvarm_current;

        private object mvarm_mode;

        private object mvarm_model;

        private object mvarm_calibration_mode;

        private object mvarm_NSOC;

        private object mvarm_ExceedHTLimit;

        private object mvarm_ExceedLTLimit;

        private object mvarm_NoHighOutput;

        private object mvarm_MaxDschgCurr;

        private object mvarm_MaxChrgCurr;

        private object mvarm_IntraModBalCount;

        private object mvarm_InterModBalCount;

        private object mvarm_CalCorrection;

        private object mvarm_ChrgCutoff;

        private object mvarm_DischrgCutoff;

        private object mvarm_CommErrors;

        private object mvarm_MaxCellVolt;

        private object mvarm_MinCellVolt;

        private object mvarm_MaxBattLifeTemp;

        private object mvarm_MinBattLifeTemp;

        private object mvarm_ChrgDischrgCycle;

        private object mvarm_hdwCalibrationCycle;

        private object mvarm_isRevision2;

        private object mvarm_WattHour;

        private object mvarm_fault_ut_s;

        private object mvarm_fault_ot_s;

        private object mvarm_fault_occ_s;

        private object mvarm_fault_ocd_s;

        private object mvarm_fault_ov_s;

        private object mvarm_fault_uv_s;

        private object mvarm_fault_osc_count;

        private object mvarm_fault_mem_acc_count;

        private object mvarm_reset_counter;

        private byte[] CRC;

        private object mvarm_vrefp;

        private object mvarm_vrefn;

        private object mvarm_gainp;

        private object mvarm_gainn;

        private object mvarm_current0A;

        private object mvarm_current2A;

        private object mvarm_current3A;

        private object mvarm_current5A;

        private object mvarm_current8A;

        private object mvarm_current10A;

        private object mvarm_current20A;

        private object mvarm_current100A;

        private object mvarm_current150A;

        private object mvarm_current200A;

        private object mvarm_version;

        private object mvarm_revision;

        private object mvarm_fwtime;

        private object mvarm_batt_status;

        private object mvarm_balance_bank_status;

        private object mvarm_comments;

        private int IDtemp;

        public bool fault_OV4v0
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 1), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_OV4v5
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 16), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_OTcell60c
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 4), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_OTcell65c
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 64), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_OTpcba80c
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 8), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_OTpcba100c
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 128), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_UV2v3
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 16384), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_UV2v5
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 2), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool Balancing
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 256), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_BatteryFailure
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 512), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_NMIFailure
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 32768), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public bool fault_WeakCellBank
        {
            get
            {
                return Operators.ConditionalCompareObjectGreater(Operators.AndObject(mvarm_batt_status, 32), 0, TextCompare: false);
            }
            set
            {
            }
        }

        public int HardwareRevision
        {
            get
            {
                return Conversions.ToInteger(mvarm_isRevision2);
            }
            set
            {
                mvarm_isRevision2 = value;
            }
        }

        public int NumHardwareCalibration
        {
            get
            {
                return Conversions.ToInteger(mvarm_hdwCalibrationCycle);
            }
            set
            {
                mvarm_hdwCalibrationCycle = value;
            }
        }

        public int NumChargeDischargeCycle
        {
            get
            {
                return Conversions.ToInteger(mvarm_ChrgDischrgCycle);
            }
            set
            {
                mvarm_ChrgDischrgCycle = value;
            }
        }

        public int MinBattLifeTemperature
        {
            get
            {
                return Conversions.ToInteger(mvarm_MinBattLifeTemp);
            }
            set
            {
                mvarm_MinBattLifeTemp = value;
            }
        }

        public int MaxBattLifeTemperature
        {
            get
            {
                return Conversions.ToInteger(mvarm_MaxBattLifeTemp);
            }
            set
            {
                mvarm_MaxBattLifeTemp = value;
            }
        }

        public int MinimumCellVolt
        {
            get
            {
                return Conversions.ToInteger(mvarm_MinCellVolt);
            }
            set
            {
                mvarm_MinCellVolt = value;
            }
        }

        public int MaximumCellVolt
        {
            get
            {
                return Conversions.ToInteger(mvarm_MaxCellVolt);
            }
            set
            {
                mvarm_MaxCellVolt = value;
            }
        }

        public int ExceedHighOutput
        {
            get
            {
                return Conversions.ToInteger(mvarm_NoHighOutput);
            }
            set
            {
                mvarm_NoHighOutput = value;
            }
        }

        public int CommunicationErrors
        {
            get
            {
                return Conversions.ToInteger(mvarm_CommErrors);
            }
            set
            {
                mvarm_CommErrors = value;
            }
        }

        public int DischargeCutoff
        {
            get
            {
                return Conversions.ToInteger(mvarm_DischrgCutoff);
            }
            set
            {
                mvarm_DischrgCutoff = value;
            }
        }

        public int ChargeCutoff
        {
            get
            {
                return Conversions.ToInteger(mvarm_ChrgCutoff);
            }
            set
            {
                mvarm_ChrgCutoff = value;
            }
        }

        public int CalibrationCorrection
        {
            get
            {
                return Conversions.ToInteger(mvarm_CalCorrection);
            }
            set
            {
                mvarm_CalCorrection = value;
            }
        }

        public int InterModuleBalanceCount
        {
            get
            {
                return Conversions.ToInteger(mvarm_InterModBalCount);
            }
            set
            {
                mvarm_InterModBalCount = value;
            }
        }

        public int IntraModuleBalanceCount
        {
            get
            {
                return Conversions.ToInteger(mvarm_IntraModBalCount);
            }
            set
            {
                mvarm_IntraModBalCount = value;
            }
        }

        public double MaxChrgCurrent
        {
            get
            {
                return Conversions.ToDouble(mvarm_MaxChrgCurr);
            }
            set
            {
                mvarm_MaxChrgCurr = value;
            }
        }

        public double MaxDschrgCurrent
        {
            get
            {
                return Conversions.ToDouble(mvarm_MaxDschgCurr);
            }
            set
            {
                mvarm_MaxDschgCurr = value;
            }
        }

        public int LTLimit
        {
            get
            {
                return Conversions.ToInteger(mvarm_ExceedLTLimit);
            }
            set
            {
                mvarm_ExceedLTLimit = value;
            }
        }

        public int HTLimit
        {
            get
            {
                return Conversions.ToInteger(mvarm_ExceedHTLimit);
            }
            set
            {
                mvarm_ExceedHTLimit = value;
            }
        }

        public int NSOC
        {
            get
            {
                return Conversions.ToInteger(mvarm_NSOC);
            }
            set
            {
                mvarm_NSOC = value;
            }
        }

        public int VOLT1
        {
            get
            {
                return Conversions.ToInteger(mvarm_volt1);
            }
            set
            {
                mvarm_volt1 = value;
            }
        }

        public int VOLT2
        {
            get
            {
                return Conversions.ToInteger(mvarm_volt2);
            }
            set
            {
                mvarm_volt2 = value;
            }
        }

        public int VOLT3
        {
            get
            {
                return Conversions.ToInteger(mvarm_volt3);
            }
            set
            {
                mvarm_volt3 = value;
            }
        }

        public int VOLT4
        {
            get
            {
                return Conversions.ToInteger(mvarm_volt4);
            }
            set
            {
                mvarm_volt4 = value;
            }
        }

        public int VOLT5
        {
            get
            {
                return Conversions.ToInteger(mvarm_volt5);
            }
            set
            {
                mvarm_volt5 = value;
            }
        }

        public int VOLT6
        {
            get
            {
                return Conversions.ToInteger(mvarm_volt6);
            }
            set
            {
                mvarm_volt6 = value;
            }
        }

        public int VOLTS
        {
            get
            {
                return Conversions.ToInteger(mvarm_volts);
            }
            set
            {
                mvarm_volts = value;
            }
        }

        public int TEMP1
        {
            get
            {
                return Conversions.ToInteger(mvarm_temp1);
            }
            set
            {
                mvarm_temp1 = value;
            }
        }

        public int TEMP2
        {
            get
            {
                return Conversions.ToInteger(mvarm_temp2);
            }
            set
            {
                mvarm_temp2 = value;
            }
        }

        public int TEMP3
        {
            get
            {
                return Conversions.ToInteger(mvarm_temp3);
            }
            set
            {
                mvarm_temp3 = value;
            }
        }

        public int TEMP4
        {
            get
            {
                return Conversions.ToInteger(mvarm_temp4);
            }
            set
            {
                mvarm_temp4 = value;
            }
        }

        public int TEMP5
        {
            get
            {
                return Conversions.ToInteger(mvarm_temp5);
            }
            set
            {
                mvarm_temp5 = value;
            }
        }

        public int TEMP6
        {
            get
            {
                return Conversions.ToInteger(mvarm_temp6);
            }
            set
            {
                mvarm_temp6 = value;
            }
        }

        public int TEMPPCB
        {
            get
            {
                return Conversions.ToInteger(mvarm_temppcb);
            }
            set
            {
                mvarm_temppcb = value;
            }
        }

        public double SOC
        {
            get
            {
                return Conversions.ToDouble(mvarm_soc);
            }
            set
            {
                mvarm_soc = value;
            }
        }

        public string SN
        {
            get
            {
                return Conversions.ToString(mvarm_sn);
            }
            set
            {
                mvarm_sn = value;
            }
        }

        public string VerfP
        {
            get
            {
                return Conversions.ToString(mvarm_vrefp);
            }
            set
            {
                mvarm_vrefp = value;
            }
        }

        public string VerfN
        {
            get
            {
                return Conversions.ToString(mvarm_vrefn);
            }
            set
            {
                mvarm_vrefn = value;
            }
        }

        public string GainP
        {
            get
            {
                return Conversions.ToString(mvarm_gainp);
            }
            set
            {
                mvarm_gainp = value;
            }
        }

        public string GainN
        {
            get
            {
                return Conversions.ToString(mvarm_gainn);
            }
            set
            {
                mvarm_gainn = value;
            }
        }

        public int Current0A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current0A);
            }
            set
            {
                mvarm_current0A = value;
            }
        }

        public int Current2A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current2A);
            }
            set
            {
                mvarm_current2A = value;
            }
        }

        public int Current3A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current3A);
            }
            set
            {
                mvarm_current3A = value;
            }
        }

        public int Current5A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current5A);
            }
            set
            {
                mvarm_current5A = value;
            }
        }

        public int Current8A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current8A);
            }
            set
            {
                mvarm_current8A = value;
            }
        }

        public int Current10A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current10A);
            }
            set
            {
                mvarm_current10A = value;
            }
        }

        public int Current20A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current20A);
            }
            set
            {
                mvarm_current20A = value;
            }
        }

        public int Current100A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current100A);
            }
            set
            {
                mvarm_current100A = value;
            }
        }

        public int Current150A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current150A);
            }
            set
            {
                mvarm_current150A = value;
            }
        }

        public int Current200A
        {
            get
            {
                return Conversions.ToInteger(mvarm_current200A);
            }
            set
            {
                mvarm_current200A = value;
            }
        }

        public string Version
        {
            get
            {
                return Conversions.ToString(mvarm_version);
            }
            set
            {
                mvarm_version = value;
            }
        }

        public string Revision
        {
            get
            {
                return Conversions.ToString(mvarm_revision);
            }
            set
            {
                mvarm_revision = value;
            }
        }

        public string BuildRevision
        {
            get
            {
                return Conversions.ToString(mvarm_fwtime);
            }
            set
            {
                mvarm_fwtime = value;
            }
        }

        public int ADDRESS
        {
            get
            {
                return Conversions.ToInteger(mvarm_addr);
            }
            set
            {
                mvarm_addr = value;
            }
        }

        public string MODEL => Conversions.ToString(mvarm_model);

        public string MODE
        {
            get
            {
                return Conversions.ToString(mvarm_mode);
            }
            set
            {
                mvarm_mode = value;
            }
        }

        public string Comments
        {
            get
            {
                return Conversions.ToString(mvarm_comments);
            }
            set
            {
                mvarm_comments = value;
            }
        }

        public string CURRENT
        {
            get
            {
                return Conversions.ToString(mvarm_current);
            }
            set
            {
                mvarm_current = value;
            }
        }

        public int WattHour
        {
            get
            {
                return Conversions.ToInteger(mvarm_WattHour);
            }
            set
            {
                mvarm_WattHour = value;
            }
        }

        public int Fault_UT_S
        {
            get
            {
                return Conversions.ToInteger(mvarm_fault_ut_s);
            }
            set
            {
                mvarm_fault_ut_s = value;
            }
        }

        public int Fault_OT_S
        {
            get
            {
                return Conversions.ToInteger(mvarm_fault_ot_s);
            }
            set
            {
                mvarm_fault_ot_s = value;
            }
        }

        public int Fault_OCC_S
        {
            get
            {
                return Conversions.ToInteger(mvarm_fault_occ_s);
            }
            set
            {
                mvarm_fault_occ_s = value;
            }
        }

        public int Fault_OCD_S
        {
            get
            {
                return Conversions.ToInteger(mvarm_fault_ocd_s);
            }
            set
            {
                mvarm_fault_ocd_s = value;
            }
        }

        public int Fault_OV_S
        {
            get
            {
                return Conversions.ToInteger(mvarm_fault_ov_s);
            }
            set
            {
                mvarm_fault_ov_s = value;
            }
        }

        public int Fault_UV_S
        {
            get
            {
                return Conversions.ToInteger(mvarm_fault_uv_s);
            }
            set
            {
                mvarm_fault_uv_s = value;
            }
        }

        public bool Bal_Cell_1
        {
            get
            {
                if (Conversions.ToBoolean(Operators.AndObject(mvarm_balance_bank_status, 1)))
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    mvarm_balance_bank_status = Operators.NotObject(Operators.AndObject(mvarm_balance_bank_status, 1));
                }
                else
                {
                    mvarm_balance_bank_status = Operators.OrObject(mvarm_balance_bank_status, 1);
                }
            }
        }

        public bool Bal_Cell_2
        {
            get
            {
                if (Conversions.ToBoolean(Operators.AndObject(mvarm_balance_bank_status, 2)))
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    mvarm_balance_bank_status = Operators.NotObject(Operators.AndObject(mvarm_balance_bank_status, 2));
                }
                else
                {
                    mvarm_balance_bank_status = Operators.OrObject(mvarm_balance_bank_status, 2);
                }
            }
        }

        public bool Bal_Cell_3
        {
            get
            {
                if (Conversions.ToBoolean(Operators.AndObject(mvarm_balance_bank_status, 4)))
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    mvarm_balance_bank_status = Operators.NotObject(Operators.AndObject(mvarm_balance_bank_status, 4));
                }
                else
                {
                    mvarm_balance_bank_status = Operators.OrObject(mvarm_balance_bank_status, 4);
                }
            }
        }

        public bool Bal_Cell_4
        {
            get
            {
                if (Conversions.ToBoolean(Operators.AndObject(mvarm_balance_bank_status, 8)))
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    mvarm_balance_bank_status = Operators.NotObject(Operators.AndObject(mvarm_balance_bank_status, 8));
                }
                else
                {
                    mvarm_balance_bank_status = Operators.OrObject(mvarm_balance_bank_status, 8);
                }
            }
        }

        public bool Bal_Cell_5
        {
            get
            {
                if (Conversions.ToBoolean(Operators.AndObject(mvarm_balance_bank_status, 16)))
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    mvarm_balance_bank_status = Operators.NotObject(Operators.AndObject(mvarm_balance_bank_status, 16));
                }
                else
                {
                    mvarm_balance_bank_status = Operators.OrObject(mvarm_balance_bank_status, 16);
                }
            }
        }

        public bool Bal_Cell_6
        {
            get
            {
                if (Conversions.ToBoolean(Operators.AndObject(mvarm_balance_bank_status, 32)))
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    mvarm_balance_bank_status = Operators.NotObject(Operators.AndObject(mvarm_balance_bank_status, 32));
                }
                else
                {
                    mvarm_balance_bank_status = Operators.OrObject(mvarm_balance_bank_status, 32);
                }
            }
        }

        public bool Calibration_Mode
        {
            get
            {
                return Conversions.ToBoolean(mvarm_calibration_mode);
            }
            set
            {
            }
        }

        public int CellVoltage_Max
        {
            get
            {
                return Conversions.ToInteger(mvarm_max_volt_sample);
            }
            set
            {
            }
        }

        public int CellVoltage_Min
        {
            get
            {
                return Conversions.ToInteger(mvarm_min_volt_sample);
            }
            set
            {
            }
        }

        public int FaultOscillatorCount => Conversions.ToInteger(mvarm_fault_osc_count);

        public int FaultMemAccessCount => Conversions.ToInteger(mvarm_fault_mem_acc_count);

        public int ResetCount => Conversions.ToInteger(mvarm_reset_counter);

        public bool ModelReadReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            if (array[1] != 3)
            {
                return false;
            }
            try
            {
                int num;
                if (array[4] == 0)
                {
                    num = 0;
                    switch (array[3])
                    {
                        case 49:
                            mvarm_model = "U1-12XP Rev. 1";
                            break;
                        case 52:
                            mvarm_model = "U24-12XP Rev. 1";
                            break;
                        case 55:
                            mvarm_model = "U27-12XP Rev. 1";
                            break;
                        case 86:
                            mvarm_model = "UEV-18XP Rev. 1";
                            break;
                        default:
                            mvarm_model = "";
                            break;
                    }
                }
                else
                {
                    num = 2;
                    switch (array[3])
                    {
                        case 49:
                            mvarm_model = "U1-12XP Rev. 2";
                            break;
                        case 52:
                            mvarm_model = "U24-12XP Rev. 2";
                            break;
                        case 55:
                            mvarm_model = "U27-12XP Rev. 2";
                            break;
                        case 86:
                            mvarm_model = "UEV-18XP Rev. 2";
                            break;
                        default:
                            mvarm_model = "";
                            break;
                    }
                }
                mvarm_isRevision2 = num;
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] ModelReadSend()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(ADDRESS + 1)),
                3,
                0,
                238,
                0,
                1,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public byte[] CRC16(byte[] data, int datalen)
        {
            byte b = byte.MaxValue;
            byte b2 = byte.MaxValue;
            byte b3 = 1;
            byte b4 = 160;
            checked
            {
                int num = datalen - 1;
                for (int i = 0; i <= num; i++)
                {
                    b = unchecked((byte)(b ^ data[i]));
                    int num2 = 0;
                    do
                    {
                        byte b5 = b2;
                        byte b6 = b;
                        b2 = (byte)unchecked((int)b2 / 2);
                        b = (byte)unchecked((int)b / 2);
                        if ((b5 & 1) == 1)
                        {
                            b = (byte)(b | 0x80);
                        }
                        unchecked
                        {
                            if ((b6 & 1) == 1)
                            {
                                b2 = (byte)(b2 ^ b4);
                                b = (byte)(b ^ b3);
                            }
                        }
                        num2++;
                    }
                    while (num2 <= 7);
                }
                return new byte[2]
                {
                    b2,
                    b
                };
            }
        }

        public byte[] SNSOCReadSend()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                57,
                0,
                10,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool SNSOCReadReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    if (HardwareRevision == 2)
                    {
                        short num = (short)((array[3] & 0xF0) >> 4);
                        short num2 = (short)(array[4] & 0x3F);
                        short num3 = (short)(((array[4] & 0xC0) >> 6) + ((array[3] & 0xF) << 2));
                        mvarm_sn = Strings.Format(num3, "00") + Strings.Format(num2, "00") + Strings.Format(unchecked((int)array[5]) * 256 + unchecked((int)array[6]), "00000");
                    }
                    else
                    {
                        mvarm_sn = Strings.Format(unchecked((int)array[3]) * 256 + unchecked((int)array[4]), "00000");
                    }
                    mvarm_vrefn = Strings.Trim(Conversions.ToString(Conversions.ToDouble(Conversion.Str(unchecked((int)array[7]) * 256 + unchecked((int)array[8]))) * 2.0));
                    mvarm_vrefp = Strings.Trim(Conversions.ToString(Conversions.ToDouble(Conversion.Str(unchecked((int)array[9]) * 256 + unchecked((int)array[10]))) * 2.0));
                    mvarm_gainn = Strings.Trim(Conversion.Str(unchecked((int)array[11]) * 256 + unchecked((int)array[12])));
                    mvarm_gainp = Strings.Trim(Conversion.Str(unchecked((int)array[13]) * 256 + unchecked((int)array[14])));
                    if (array[16] == 0)
                    {
                        mvarm_soc = "00.0000";
                    }
                    else
                    {
                        mvarm_soc = Strings.Format((double)(100 * unchecked((int)array[16])) / 255.0, "##.####");
                    }
                    mvarm_balance_bank_status = array[15];
                    if (array[17] > 128)
                    {
                        mvarm_current = Strings.Trim("-" + Conversion.Str((unchecked(Conversion.Val((byte)(~array[17])) * 256.0 + Conversion.Val((byte)(~array[18]))) + 1.0) / 100.0));
                    }
                    else
                    {
                        mvarm_current = Strings.Trim(Conversion.Str((double)(unchecked((int)array[17]) * 256 + unchecked((int)array[18])) / 100.0));
                    }
                    mvarm_volts = Strings.Trim(Conversion.Str(unchecked((int)array[21]) * 256 + unchecked((int)array[22])));
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] BalanceReadSend()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                90,
                0,
                1,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool BalanceReadReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (((unchecked((int)array[3]) * 256 + unchecked((int)array[4])) & 0x10) == 16)
                    {
                        return false;
                    }
                    if (((unchecked((int)array[3]) * 256 + unchecked((int)array[4])) & 0x10) == 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] VoltRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                69,
                0,
                9,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool VoltReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_max_volt_sample = Strings.Trim(Conversion.Str(unchecked((int)array[3]) * 256 + unchecked((int)array[4])));
                    mvarm_min_volt_sample = Strings.Trim(Conversion.Str(unchecked((int)array[5]) * 256 + unchecked((int)array[6])));
                    mvarm_volt1 = Strings.Trim(Conversion.Str(unchecked((int)array[9]) * 256 + unchecked((int)array[10])));
                    mvarm_volt2 = Strings.Trim(Conversion.Str(unchecked((int)array[11]) * 256 + unchecked((int)array[12])));
                    mvarm_volt3 = Strings.Trim(Conversion.Str(unchecked((int)array[13]) * 256 + unchecked((int)array[14])));
                    mvarm_volt4 = Strings.Trim(Conversion.Str(unchecked((int)array[15]) * 256 + unchecked((int)array[16])));
                    if (Conversion.Val(MODE) == 6.0)
                    {
                        mvarm_volt5 = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                        mvarm_volt6 = Strings.Trim(Conversion.Str(unchecked((int)array[19]) * 256 + unchecked((int)array[20])));
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] TempRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                80,
                0,
                7,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool TempReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    int num = unchecked((int)array[3]) * 256 + unchecked((int)array[4]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_temppcb = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_temppcb = Strings.Trim(Conversions.ToString(num));
                    }
                    num = unchecked((int)array[5]) * 256 + unchecked((int)array[6]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_temp1 = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_temp1 = Strings.Trim(Conversions.ToString(num));
                    }
                    num = unchecked((int)array[7]) * 256 + unchecked((int)array[8]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_temp2 = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_temp2 = Strings.Trim(Conversions.ToString(num));
                    }
                    num = unchecked((int)array[9]) * 256 + unchecked((int)array[10]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_temp3 = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_temp3 = Strings.Trim(Conversions.ToString(num));
                    }
                    num = unchecked((int)array[11]) * 256 + unchecked((int)array[12]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_temp4 = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_temp4 = Strings.Trim(Conversions.ToString(num));
                    }
                    num = unchecked((int)array[13]) * 256 + unchecked((int)array[14]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_temp5 = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_temp5 = Strings.Trim(Conversions.ToString(num));
                    }
                    num = unchecked((int)array[15]) * 256 + unchecked((int)array[16]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_temp6 = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_temp6 = Strings.Trim(Conversions.ToString(num));
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] Wakeup()
        {
            byte[] array = new byte[10]
            {
                0,
                0,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 4);
            array[4] = array2[1];
            array[5] = array2[0];
            array[6] = 13;
            array[7] = 10;
            return array;
        }

        public byte[] EnterCalibrationMode()
        {
            byte[] array = new byte[9]
            {
                checked((byte)(1 + ADDRESS)),
                5,
                1,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public byte[] SendTempData(float Temp)
        {
            float[] array = new float[8];
            byte[] array2 = new byte[25];
            byte b = 0;
            checked
            {
                do
                {
                    array[b] = Temp;
                    b = (byte)unchecked((uint)(b + 1));
                }
                while (unchecked((uint)b) <= 6u);
                array2[0] = (byte)(ADDRESS + 1);
                array2[1] = 16;
                array2[2] = 0;
                array2[3] = 92;
                array2[4] = 0;
                array2[5] = 7;
                array2[6] = 14;
                b = 0;
                b = 0;
                do
                {
                    array2[7 + 2 * unchecked((int)b)] = (byte)Math.Round(Conversion.Int(array[b] / 256f));
                    array2[8 + 2 * unchecked((int)b)] = (byte)Math.Round(array[b] % 256f);
                    b = (byte)unchecked((uint)(b + 1));
                }
                while (unchecked((uint)b) <= 6u);
                byte[] array3 = CRC16(array2, 21);
                array2[21] = array3[1];
                array2[22] = array3[0];
                array2[23] = 13;
                array2[24] = 10;
                return array2;
            }
        }

        public byte[] CalibrateTemp()
        {
            byte[] array = checked(new byte[9]
            {
                (byte)(ADDRESS + 1),
                5,
                1,
                (byte)Math.Round(Conversion.Int(8.0)),
                0,
                0,
                0,
                0,
                0
            });
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public byte[] ExitCalibrationMode()
        {
            byte[] array = new byte[9]
            {
                checked((byte)(1 + ADDRESS)),
                5,
                0,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public byte[] Calibraton0ARead()
        {
            byte[] array = new byte[9]
            {
                1,
                5,
                1,
                1,
                0,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool Calibraton0AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] Calibraton10ARead()
        {
            byte[] array = new byte[9]
            {
                1,
                5,
                1,
                2,
                0,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool Calibraton10AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] current10ARead()
        {
            byte[] array = new byte[10]
            {
                1,
                3,
                0,
                57,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool current10AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current10A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current10A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] Calibraton100ARead()
        {
            byte[] array = new byte[9]
            {
                1,
                5,
                1,
                4,
                0,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool Calibraton100AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] current100ARead()
        {
            byte[] array = new byte[10]
            {
                1,
                3,
                0,
                57,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool current100AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current100A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] StartCalibratonRead()
        {
            byte[] array = new byte[9]
            {
                1,
                5,
                1,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool StartCalibratonReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] CloseCalibratonRead()
        {
            byte[] array = new byte[9]
            {
                1,
                5,
                1,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool CloseCalibratonReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] VerifyRead()
        {
            byte[] array = new byte[10]
            {
                1,
                3,
                0,
                57,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool Verify0AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current0A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current0A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool Verify2AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current2A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current2A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool Verify3AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current3A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current3A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool Verify5AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current5A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current5A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool Verify8AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current8A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current8A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool Verify20AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current20A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current20A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool Verify150AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current150A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current150A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool Verify200AReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_current200A = Strings.Trim(Conversion.Str(unchecked((int)array[17]) * 256 + unchecked((int)array[18])));
                    mvarm_current = RuntimeHelpers.GetObjectValue(mvarm_current200A);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] writetoflashRead()
        {
            byte[] array = new byte[9]
            {
                1,
                5,
                1,
                0,
                128,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool writetoflashReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] BatteryStatusRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                30,
                0,
                1,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool BatteryStatusReturn(object buffer)
        {
            byte[] array = (byte[])buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    int num = (int)Math.Round(unchecked((double)(int)array[3] * 256.0 + (double)(int)array[4]));
                    mvarm_batt_status = num;
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] BatteryWHRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                33,
                0,
                2,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool BatteryWHReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    long num = (long)Math.Round(unchecked((double)(int)array[3] * 16777216.0 + (double)(int)array[4] * 65536.0 + (double)(int)array[5] * 256.0 + (double)(int)array[6]));
                    mvarm_WattHour = num;
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] BatteryVersionRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                224,
                0,
                22,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool BatteryVersionReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    string str = "";
                    string str2 = "";
                    int num = 3;
                    do
                    {
                        str2 += Conversions.ToString(Strings.ChrW(array[num]));
                        num++;
                    }
                    while (num <= 21);
                    if (Operators.ConditionalCompareObjectEqual(mvarm_isRevision2, 2, TextCompare: false))
                    {
                        num = 21;
                        do
                        {
                            str += Conversions.ToString(Strings.ChrW(array[num]));
                            num++;
                        }
                        while (num <= 30);
                    }
                    else
                    {
                        num = 21;
                        do
                        {
                            str += Conversions.ToString(Strings.ChrW(array[num]));
                            num++;
                        }
                        while (num <= 32);
                    }
                    mvarm_revision = str;
                    int num2 = Conversions.ToInteger(Strings.Trim(Conversion.Str(unchecked((int)array[37]) * 256 + unchecked((int)array[38]))));
                    DateTime dateTime = DateAndTime.DateAdd("d", num2, "2005-01-01");
                    mvarm_version = Strings.Format(dateTime, "MM/dd/yyyy");
                    DateTime dateTime2 = new DateTime(unchecked((int)array[41]) * 100 + unchecked((int)array[42]), array[39], array[40], array[43], array[44], 0);
                    mvarm_fwtime = Strings.Format(dateTime2, "G");
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] StopbalancingRead()
        {
            byte[] array = new byte[9]
            {
                1,
                5,
                1,
                0,
                16,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool StopbalancingReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] CheckmoduleRead()
        {
            byte[] array = new byte[10]
            {
                1,
                3,
                0,
                238,
                0,
                1,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool CheckmoduleReturn(object Buffer, string productmodel)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] == 3)
                {
                    int num;
                    switch (productmodel)
                    {
                        case "U1-12XP":
                            num = 49;
                            break;
                        case "U24-12XP":
                            num = 52;
                            break;
                        case "U27-12XP":
                            num = 55;
                            break;
                        case "UEV-18XP":
                            num = 86;
                            break;
                        default:
                            num = 0;
                            break;
                    }
                    try
                    {
                        if (array[3] != num)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ProjectData.SetProjectError(ex);
                        Exception ex2 = ex;
                        bool result = false;
                        ProjectData.ClearProjectError();
                        return result;
                    }
                }
                return false;
            }
            catch (Exception ex3)
            {
                ProjectData.SetProjectError(ex3);
                Exception ex4 = ex3;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] SetID(string NewID)
        {
            byte[] array = checked(new byte[13]
            {
                (byte)(ADDRESS + 1),
                16,
                0,
                0,
                0,
                1,
                2,
                (byte)Math.Round(Conversion.Int(Conversion.Val(NewID) / 256.0)),
                (byte)Math.Round(Conversion.Val(NewID) % 256.0),
                0,
                0,
                0,
                0
            });
            byte[] array2 = CRC16(array, 9);
            array[9] = array2[1];
            array[10] = array2[0];
            array[11] = 13;
            array[12] = 10;
            byte[] result = array;
            IDtemp = Conversions.ToInteger(NewID);
            return result;
        }

        public bool VerifySetID(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (!((array[1] == 16) | (array[2] == 16)))
                {
                    return false;
                }
                bool result = true;
                ADDRESS = checked(IDtemp - 1);
                return result;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] OpenbalancingRead()
        {
            byte[] array = new byte[9]
            {
                checked((byte)(1 + ADDRESS)),
                5,
                0,
                0,
                16,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public bool OpenbalancingReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 5)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] CheckCalibrationMode()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(ADDRESS + 1)),
                3,
                0,
                90,
                0,
                1,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool ReadCalibrationMode(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 3)
                {
                    return false;
                }
                if (array[4] == 8)
                {
                    mvarm_calibration_mode = true;
                }
                else
                {
                    mvarm_calibration_mode = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] EnterCalibrationMode(int paramid)
        {
            byte[] array = new byte[9]
            {
                checked((byte)(1 + paramid)),
                5,
                1,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public byte[] SendTempData(float Temp, int paramid)
        {
            float[] array = new float[8];
            byte[] array2 = new byte[25];
            byte b = 0;
            checked
            {
                do
                {
                    array[b] = Temp;
                    b = (byte)unchecked((uint)(b + 1));
                }
                while (unchecked((uint)b) <= 6u);
                array2[0] = (byte)(1 + paramid);
                array2[1] = 16;
                array2[2] = 0;
                array2[3] = 92;
                array2[4] = 0;
                array2[5] = 7;
                array2[6] = 14;
                b = 0;
                b = 0;
                do
                {
                    array2[7 + 2 * unchecked((int)b)] = (byte)Math.Round(Conversion.Int(array[b] / 256f));
                    array2[8 + 2 * unchecked((int)b)] = (byte)Math.Round(array[b] % 256f);
                    b = (byte)unchecked((uint)(b + 1));
                }
                while (unchecked((uint)b) <= 6u);
                byte[] array3 = CRC16(array2, 21);
                array2[21] = array3[1];
                array2[22] = array3[0];
                array2[23] = 13;
                array2[24] = 10;
                return array2;
            }
        }

        public byte[] CalibrateTemp(int paramid)
        {
            byte[] array = checked(new byte[9]
            {
                (byte)(1 + paramid),
                5,
                1,
                (byte)Math.Round(Conversion.Int(8.0)),
                0,
                0,
                0,
                0,
                0
            });
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public byte[] ExitCalibrationMode(int paramid)
        {
            byte[] array = new byte[9]
            {
                checked((byte)(1 + paramid)),
                5,
                0,
                0,
                8,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 5);
            array[5] = array2[1];
            array[6] = array2[0];
            array[7] = 13;
            array[8] = 10;
            return array;
        }

        public byte[] EventLogRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                106,
                0,
                12,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public byte[] EventLog2Read()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                130,
                0,
                18,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool EventLogReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_NSOC = unchecked((int)array[3]) * 256 + unchecked((int)array[4]);
                    mvarm_ExceedHTLimit = unchecked((int)array[5]) * 256 + unchecked((int)array[6]);
                    mvarm_ExceedLTLimit = unchecked((int)array[7]) * 256 + unchecked((int)array[8]);
                    mvarm_NoHighOutput = unchecked((int)array[9]) * 256 + unchecked((int)array[10]);
                    mvarm_MaxDschgCurr = unchecked((int)array[11]) * 256 + unchecked((int)array[12]);
                    mvarm_MaxChrgCurr = unchecked((int)array[13]) * 256 + unchecked((int)array[14]);
                    mvarm_IntraModBalCount = unchecked((int)array[15]) * 256 + unchecked((int)array[16]);
                    mvarm_InterModBalCount = unchecked((int)array[17]) * 256 + unchecked((int)array[18]);
                    mvarm_CalCorrection = unchecked((int)array[19]) * 256 + unchecked((int)array[20]);
                    mvarm_ChrgCutoff = unchecked((int)array[21]) * 256 + unchecked((int)array[22]);
                    mvarm_DischrgCutoff = unchecked((int)array[23]) * 256 + unchecked((int)array[24]);
                    mvarm_CommErrors = unchecked((int)array[25]) * 256 + unchecked((int)array[26]);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public bool EventLog2Return(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 3)
                {
                    return false;
                }
                mvarm_fault_ut_s = (double)(int)array[5] * 16777216.0 + (double)(int)array[6] * 65536.0 + (double)(int)array[3] * 256.0 + (double)(int)array[4];
                mvarm_fault_ot_s = (double)(int)array[9] * 16777216.0 + (double)(int)array[10] * 65536.0 + (double)(int)array[7] * 256.0 + (double)(int)array[8];
                mvarm_fault_occ_s = (double)(int)array[13] * 16777216.0 + (double)(int)array[14] * 65536.0 + (double)(int)array[11] * 256.0 + (double)(int)array[12];
                mvarm_fault_ocd_s = (double)(int)array[17] * 16777216.0 + (double)(int)array[18] * 65536.0 + (double)(int)array[15] * 256.0 + (double)(int)array[16];
                mvarm_fault_uv_s = (double)(int)array[21] * 16777216.0 + (double)(int)array[22] * 65536.0 + (double)(int)array[19] * 256.0 + (double)(int)array[20];
                mvarm_fault_ov_s = (double)(int)array[25] * 16777216.0 + (double)(int)array[26] * 65536.0 + (double)(int)array[23] * 256.0 + (double)(int)array[24];
                checked
                {
                    mvarm_reset_counter = unchecked((int)array[27]) * 255 + unchecked((int)array[28]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }

        public byte[] BatteryMaximumRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                1,
                0,
                7,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool BatteryMaximumReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    int num = unchecked((int)array[3]) * 256 + unchecked((int)array[4]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_MaxBattLifeTemp = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_MaxBattLifeTemp = Strings.Trim(Conversions.ToString(num));
                    }
                    num = unchecked((int)array[5]) * 256 + unchecked((int)array[6]);
                    if ((num & 0x8000) != 0)
                    {
                        mvarm_MinBattLifeTemp = Strings.Trim(Conversions.ToString(-(num ^ 0xFFFF)));
                    }
                    else
                    {
                        mvarm_MinBattLifeTemp = Strings.Trim(Conversions.ToString(num));
                    }
                    mvarm_MaxCellVolt = unchecked((int)array[7]) * 256 + unchecked((int)array[8]);
                    mvarm_MinCellVolt = unchecked((int)array[9]) * 256 + unchecked((int)array[10]);
                    mvarm_fault_osc_count = unchecked((int)array[13]) * 256 + unchecked((int)array[14]);
                    mvarm_fault_mem_acc_count = unchecked((int)array[15]) * 256 + unchecked((int)array[16]);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] BatteryCycleRead()
        {
            byte[] array = new byte[10]
            {
                checked((byte)(1 + ADDRESS)),
                3,
                0,
                18,
                0,
                2,
                0,
                0,
                0,
                0
            };
            byte[] array2 = CRC16(array, 6);
            array[6] = array2[1];
            array[7] = array2[0];
            array[8] = 13;
            array[9] = 10;
            return array;
        }

        public bool BatteryCycleReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            checked
            {
                try
                {
                    if (array[1] != 3)
                    {
                        return false;
                    }
                    mvarm_hdwCalibrationCycle = unchecked((int)array[3]) * 256 + unchecked((int)array[4]);
                    mvarm_ChrgDischrgCycle = unchecked((int)array[5]) * 256 + unchecked((int)array[6]);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    bool result = false;
                    ProjectData.ClearProjectError();
                    return result;
                }
            }
        }

        public byte[] BatteryBalanceSend(ushort BalanceSelection)
        {
            checked
            {
                byte[] array = new byte[11]
                {
                    (byte)(1 + ADDRESS),
                    16,
                    0,
                    32,
                    2,
                    (byte)Math.Round((double)unchecked((int)BalanceSelection) / 256.0),
                    (byte)(BalanceSelection & 0xFF),
                    0,
                    0,
                    0,
                    0
                };
                byte[] array2 = CRC16(array, 7);
                array[7] = array2[1];
                array[8] = array2[0];
                array[9] = 13;
                array[10] = 10;
                return array;
            }
        }

        public bool BatteryBalanceReturn(object Buffer)
        {
            byte[] array = (byte[])Buffer;
            try
            {
                if (array[1] != 16)
                {
                    return false;
                }
                if (array[3] == 32)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                bool result = false;
                ProjectData.ClearProjectError();
                return result;
            }
        }
    }
}
