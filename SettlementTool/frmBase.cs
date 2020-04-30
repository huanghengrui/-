using DevComponents.DotNetBar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace SettlementTool
{
    public partial class frmBase : Form
    {
        public Database db = new Database("");
        public frmBase()
        {
            InitializeComponent();
        }

        public bool TestConnet()
        {
            bool ret = false;
            if (SystemInfo.serialPort.IsOpen)
            {
                string cmdStr = "D1 89 31 06 36 56";
                byte[] sendData = HexStringToByteArray(cmdStr);
                SystemInfo.serialPort.Write(sendData, 0, sendData.Length);
                int intdex = 0;
                Thread.Sleep(500);
                while (true)
                {
                    Thread.Sleep(100);
                    int index = SystemInfo.serialPort.BytesToRead;

                    if (index > 0)
                    {
                        byte[] readBuffer = new byte[index];
                        int count = SystemInfo.serialPort.Read(readBuffer, 0, index);
                        ret = true;
                        continue;
                    }
                    else
                    {
                        intdex++;
                        if (intdex >= 5)
                            break;
                        continue;
                    }
                }

            }
            else
            {
                ret = false;
            }
            return ret;
        }

        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        //字节数组转16进制字符串
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        public string getCardNo(string caNo)
        {
            char[] charCard = new char[0];
            charCard = caNo.ToCharArray();
            caNo = charCard[14].ToString() + charCard[15] + charCard[12] +
                charCard[13] + charCard[10] + charCard[11] +
                charCard[8] + charCard[9] + charCard[6] +
                charCard[7] + charCard[4] + charCard[5] +
                charCard[2] + charCard[3] + charCard[0] + charCard[1];
            return caNo;
        }

        public bool FindCardList(string card)
        {
            bool ret = false;

            for (int i = 0; i < SystemInfo.cardList.Count; i++)
            {
                if (SystemInfo.cardList[i] == card)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        public bool FindZero(string data)
        {
            char[] c = data.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] != '0')
                {
                    return true;
                }
            }
            return false;
        }

        public bool FindFailChar(string data)
        {
            bool ret = false;
            char[] c = data.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if ((c[i] <= '9' && c[i] >= '0') || (c[i] <= 'Z' && c[i] >= 'A') || (c[i] <= 'z' && c[i] >= 'a' || c[i] == '\0'))
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        public bool FindThreadCardList(string card)
        {
            bool ret = false;

            for (int i = 0; i < SystemInfo.threadCardList.Count; i++)
            {
                if (SystemInfo.threadCardList[i] == card)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        public string ConvertStringToHex(string strASCII, string separator = null)
        {
            StringBuilder sbHex = new StringBuilder();
            foreach (char chr in strASCII)
            {
                sbHex.Append(String.Format("{0:X2}", Convert.ToInt32(chr)));
                sbHex.Append(separator ?? string.Empty);
            }
            return sbHex.ToString();
        }

        public string ConvertHexToString(string HexValue, string separator = null)
        {
            HexValue = string.IsNullOrEmpty(separator) ? HexValue : HexValue.Replace(string.Empty, separator);
            StringBuilder sbStrValue = new StringBuilder();
            while (HexValue.Length > 0)
            {
                sbStrValue.Append(Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString());
                HexValue = HexValue.Substring(2);
            }
            return sbStrValue.ToString();
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }

        protected void AddColumn(DataGridView grid, int colType, string Field, bool IsHide, bool HasSort,
       byte CenterFlag, int colWidth)
        {
            DataGridViewTextBoxColumn colText;
            DataGridViewCheckBoxColumn colCheck;
            DataGridViewComboBoxColumn colCombo;
            switch (colType)
            {
                case 0:
                    colText = new DataGridViewTextBoxColumn();
                    colText.DataPropertyName = Field;
                    colText.Visible = !IsHide;
                    if (!HasSort) colText.SortMode = DataGridViewColumnSortMode.NotSortable;
                    colText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    if (CenterFlag == 1)
                        colText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    else if (CenterFlag == 2)
                        colText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (colWidth > 0)
                        colText.Width = colWidth;
                    else
                        colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns.Add(colText);
                    break;
                case 1:
                    colCheck = new DataGridViewCheckBoxColumn();
                    colCheck.DataPropertyName = Field;
                    colCheck.Visible = !IsHide;
                    colCheck.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    if (CenterFlag == 1)
                        colCheck.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    else if (CenterFlag == 2)
                        colCheck.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (colWidth > 0)
                        colCheck.Width = colWidth;
                    else
                        colCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns.Add(colCheck);
                    break;
                case 2:
                    colCombo = new DataGridViewComboBoxColumn();
                    colCombo.DataPropertyName = Field;
                    colCombo.Visible = !IsHide;
                    colCombo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    if (CenterFlag == 1)
                        colCombo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    else if (CenterFlag == 2)
                        colCombo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (colWidth > 0)
                        colCombo.Width = colWidth;
                    else
                        colCombo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    colCombo.DisplayStyleForCurrentCellOnly = true;
                    grid.Columns.Add(colCombo);
                    break;
            }
        }

        public QHKS.TConnInfo ValueToConnInfo(byte IsBigMac, int MacSN, byte MacType, string MacConnType,
        string MacIPAddress, string MacPort, string MacConnPWD, string MacPhysicsAddress)
        {
            QHKS.TConnInfo connInfo = new QHKS.TConnInfo();
            connInfo.IsBigMac = SystemInfo.IsBigMacAdd ? (byte)1 : (byte)0;
            connInfo.CommPort = "";
            connInfo.NetHost = "";
            connInfo.Password = 0;
            if (MacConnType.ToUpper() == MacConnTypeString.USB)
                connInfo.ConnType = 0;
            else if (MacConnType.ToUpper() == MacConnTypeString.Comm)
            {
                connInfo.ConnType = 1;
                connInfo.CommPort = MacPort;
                connInfo.CommBaudRate = Convert.ToInt32(MacConnPWD);
            }
            else if (MacConnType.ToUpper() == MacConnTypeString.LAN)
            {
                connInfo.ConnType = 2;
                connInfo.NetHost = MacIPAddress;
                connInfo.NetPort = Convert.ToInt32(MacPort);
                if (IsNumeric(MacConnPWD)) connInfo.Password = Convert.ToInt32(MacConnPWD);
            }
            else if (MacConnType.ToUpper() == MacConnTypeString.GPRS)
            {
                connInfo.ConnType = 3;
                connInfo.NetHost = MacIPAddress;
                connInfo.NetPort = Convert.ToInt32(MacPort);
                if (IsNumeric(MacConnPWD)) connInfo.Password = Convert.ToInt32(MacConnPWD);
            }
            connInfo.MacSN = MacSN;
            connInfo.MacType = MacType;
            if ((MacType == 32) || (MacType == 33) || (MacType == 34) || (MacType == 35) || (MacType == 36))
            {
                connInfo.MacType = Convert.ToByte(MacType - 30);
            }
            connInfo.MacAddress = MacPhysicsAddress;
            return connInfo;
        }
        public bool IsNumeric(string str)
        {
            if (str == null) str = "";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return reg.IsMatch(str);
        }

        private void frmBase_Load(object sender, EventArgs e)
        {
            if (DeviceObject.objKS == null) DeviceObject.objKS = new QHKS.KS();
            if (DeviceObject.objMJ == null) DeviceObject.objMJ = new QHKS.MJ();
            if (DeviceObject.objApp == null) DeviceObject.objApp = new QHKS.App();

            if (DeviceObject.objAES == null) DeviceObject.objAES = new HSUNFK.AES();
            if (DeviceObject.objDES == null) DeviceObject.objDES = new HSUNFK.DES();
            if (DeviceObject.objCPIC == null) DeviceObject.objCPIC = new HSUNFK.CPIC();
            if (DeviceObject.objCard == null) DeviceObject.objCard = new HSUNFK.Card();
            // if (DeviceObject.objIkS == null) DeviceObject.objIkS = new HSFAPI.KSClass();
        }

        public string ReadConfig(string ID, string KeyWord, string Def)
        {
            string ret = Def;
            DataTableReader dr = null;
            try
            {
                dr = db.GetDataReader("SELECT [Value] FROM SY_Config WHERE ID='" + ID + "' AND [Key]='" + KeyWord + "'");
                if (dr.Read()) ret = dr[0].ToString();
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message);
            }
            finally
            {
                if (dr != null) dr.Close();
                dr = null;
            }
            return ret;
        }

        public int ReadConfig(string ID, string KeyWord, int Def)
        {
            string ret = ReadConfig(ID, KeyWord, Def.ToString());
            if (IsNumeric(ret))
                return Convert.ToInt32(ret);
            else
                return 0;
        }
        public byte ReadConfig(string ID, string KeyWord, byte Def)
        {
            string ret = ReadConfig(ID, KeyWord, Def.ToString());
            if (IsNumeric(ret))
                return Convert.ToByte(ret);
            else
                return 0;
        }
        public bool ReadConfig(string ID, string KeyWord, bool Def)
        {
            string ret = ReadConfig(ID, KeyWord, Convert.ToByte(Def).ToString());
            if (IsNumeric(ret))
                return Convert.ToByte(ret) == 1;
            else
                return false;
        }

        public bool WriteConfig(string ID, string KeyWord, int Value)
        {
            return WriteConfig(ID, KeyWord, Value, "", "");
        }
        public bool WriteConfig(string ID, string KeyWord, int Value, string title, string oprt)
        {
            return WriteConfig(ID, KeyWord, Value.ToString(), title, oprt);
        }

        public bool WriteConfig(string ID, string KeyWord, string Value, string title, string oprt)
        {
            bool ret = false;
            DataTableReader dr = null;
            string sql = "";
            try
            {
                dr = db.GetDataReader("SELECT [Value] FROM SY_Config WHERE ID='" + ID + "' AND [Key]='" + KeyWord + "'");
                if (dr.Read())
                    sql = "UPDATE SY_Config SET [Value]='" + Value + "' WHERE ID='" + ID + "' AND [Key]='" +
                               KeyWord + "'";
                else
                    sql = "INSERT INTO SY_Config(ID,[Key],[Value]) VALUES('" + ID + "','" + KeyWord + "','" +
                              Value + "')";
                db.ExecSQL(sql);
                ret = true;
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message);
            }
            finally
            {
                if (dr != null) dr.Close();
                dr = null;
            }
            return ret;
        }

        public void ClearCardLimitInfo(DateTime dt, ref HSUNFK.TCardSFData sfData)
        {
            DateTime d1 = new DateTime(dt.Year, dt.Month, dt.Day);
            DateTime d2 = Convert.ToDateTime(sfData.UseDate);
            DateTime d3 = new DateTime(d2.Year, d2.Month, d2.Day);
            if (d1 != d3)
            {
                sfData.LimitMoney1 = "000000";
                sfData.LimitTimes1 = 0;
                sfData.LimitMoney2 = "000000";
                sfData.LimitTimes2 = 0;
                sfData.LimitMoney3 = "000000";
                sfData.LimitTimes3 = 0;
                sfData.LimitMoney4 = "000000";
                sfData.LimitTimes4 = 0;
            }
            if (d1.ToString(SystemInfo.YMFormatCard) != d3.ToString(SystemInfo.YMFormatCard))
            {
                sfData.LimitMoneyMonth = "000000";
                sfData.LimitTimesMonth = 0;
            }
        }
        protected string CurrencyToStringEx(string src)
        {
            string ret = src;
            if (src.Length > SystemInfo.moneyStr.Length)
            {
                if (ret.Substring(0, SystemInfo.moneyStr.Length) == SystemInfo.moneyStr)
                {
                    ret = ret.Substring(SystemInfo.moneyStr.Length);
                }
                else if (ret.Substring(1, SystemInfo.moneyStr.Length) == SystemInfo.moneyStr && ret.Substring(0, 1) == "-")
                {
                    ret = "-" + ret.Substring(SystemInfo.moneyStr.Length + 1);
                }
                if (!IsNumeric(ret)) ret = "0.00";
            }
            else if (IsNumeric(src))
                ret = src;
            else
                ret = "0.00";
            return ret;
        }

        public bool CheckTextMaxLength(string LabelText, string Text, int MaxLength)
        {
            int size = GetTextLength(Text);
            bool ret = ((MaxLength == 0) || (MaxLength == 32767) ||
              ((MaxLength > 0) && (MaxLength < 32767) & (size <= MaxLength)));
            if (!ret) MessageBoxEx.Show(string.Format("内容字节长度{0}，已经大于最大允许字节长度{1}！", size, MaxLength));
            return ret;
        }

        public int GetTextLength(string Text)
        {
            int ret = 0;
            int a;
            for (int i = 0; i < Text.Length; i++)
            {
                a = Convert.ToInt32(Text[i]);
                if ((a < 0) || (a > 255)) ret += 2; else ret += 1;
            }
            return ret;
        }
    }
    public class Cmd
    {
        private string Header = "55 AA";
        private string Command = "00";
        private string Datalen = "00";
        private string Checksum = "00";
        private string Data = "00";
        private string Data_checksum = "00";

        public string Header1
        {
            get { return Header; }
            set { Header = value; }
        }

        public string Command1
        {
            get { return Command; }
            set { Command = value; }
        }

        public string Datalen1
        {
            get { return Datalen; }
            set { Datalen = value; }
        }

        public string Checksum1
        {
            get { return Checksum; }
            set { Checksum = value; }
        }

        public string Data1
        {
            get { return Data; }
            set { Data = value; }
        }

        public string Data_checksum1
        {
            get { return Data_checksum; }
            set { Data_checksum = value; }
        }

        public string Buf()
        {
            string str = Header1.Trim() + " " + Command1.Trim() + " " + Datalen1.Trim() + " " + Checksum1.Trim() + " " + Data1.Trim() + " " + Data_checksum1.Trim();
            str = str.ToUpper();
            return str;
        }


    }
}
