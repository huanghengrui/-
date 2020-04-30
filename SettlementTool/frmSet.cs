using DevComponents.DotNetBar;
using Microsoft.Win32;
using QHKS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace SettlementTool
{
    public partial class frmSet : frmBase
    {
        private Color WANAddressColor = Color.Black;
        private List<string> RealList = new List<string>();
        public string ConnStr = "";
        private string DBName = "";
        private const string ENCRY = "quzhengyu";
        public const string MESSAGE = "提示";
        public const string OPENSERIAPORT = "打开";
        public const string CLOSESERIAPORT = "关闭";

        public const string OPENSERIAPORTSUCCESS = "打开串口成功！";
        public const string OPENSERIAPORTFAILD = "打开串口失败！";

        public const string INPUTMACSN = "请输入正确的机器号";
        public const string THESAMEMACHINENUMBEREXISTS = "一卡通已经存在相同的机器号，确定修改吗?";
        public const string INPUTDBNAME = "请输入数据库名称!";

        public const string CHECKOUISNOTALLOWEDTOCONSUME = "该账套已经结账，不允许消费! 连接失败!";
        public const string CONNETDBSUCCESS = "连接数据库成功!";
        public const string CONNETDBFAILD = "连接数据库失败!";
        public const string SELECTDEVTYPE = "请选择设备类型!";
        public const string SELECTPORT = "请选择串口!";
        public const string INPUTIP = "请输入IP地址!";
        public const string INPUTPORT = "请输入端口号!";
        public const string EDITSUCCESS = "修改成功!";
        public const string INPUTXIAOJINGLINGNAME = "请输入小精灵账号!";
        public const string INPUTXIAOJINGLINGPWD = "请输入小精灵密码!";
        public const string SAVESUCCESS = "保存数据成功!";
        public const string PERIODINFOERROR = "请先正确设置餐类时段信息!";
        public const string SETTIMEINFO = "结算台设置时段信息";

        public frmSet()
        {
            InitializeComponent();
        }

        public void Connet()
        {
            frmSet_Load(null, null);
            btnConnet_Click(null, null);
        }
        //打开串口，连接数据库，初始化刷卡参数
        private void btnConnet_Click(object sender, EventArgs e)
        {
            if (!TestLink(true)) return;
            GetMacSN();
            SetMacSN();
            
            SystemInfo.cPort = cmbPort.Text;
            SystemInfo.cBaud = cmbBaud.Text;
            RealList.Clear();
            if (SystemInfo.isOpened)
            {
                try
                {
                    SystemInfo.IsthreadSend = false;
                    SystemInfo.isOpened = false;
                    SystemInfo.serialPort.Close();     //关闭串口
                    btnConnet.Text = OPENSERIAPORT;
                }
                catch
                {
                   
                }
            }
            else
            {
                
                SystemInfo.serialPort.PortName = SystemInfo.cPort;
                SystemInfo.serialPort.BaudRate = Convert.ToInt32(SystemInfo.cBaud, 10);
                SystemInfo.serialPort.Parity = Parity.Even;
                SystemInfo.serialPort.DataBits = 8;
                SystemInfo.serialPort.StopBits = StopBits.One;
                try
                {
                    SystemInfo.serialPort.Open();     //打开串口

                    if (!TestConnet())
                    {
                        SystemInfo.IsthreadSend = false;
                        SystemInfo.isOpened = false;
                        SystemInfo.serialPort.Close();     //关闭串口
                    }
                    else
                    {
                        SystemInfo.IsthreadSend = true;
                        SystemInfo.isOpened = true;
                        btnConnet.Text = CLOSESERIAPORT;
                    }
                    
                }
                catch
                {
                    
                }

                try
                {
                    //自动扣款
                    SystemInfo.IsAutoXF = chkAutoXF.Checked;
                    //使用补贴消费
                    SystemInfo.IsUseBTBalance = chkBTBalance.Checked;
                    SystemInfo.IsAutoPeriod = chkAutoPeriod.Checked;
                    //公共扇区
                    SystemInfo.PubCardSector = ReadConfig("RS_System", "PubCardSector", Convert.ToByte(1));
                    SetSFCardSector();
                    //取收费经销商代码
                    SystemInfo.DealersCode = ReadConfig("RS_System", "DealersCode", "");
                    //取收费客户代码
                    SystemInfo.CustomersCode = ReadConfig("RS_System", "CustomersCode", 0);
                    if (SystemInfo.CustomersCode > 999999) SystemInfo.CustomersCode = 999999;
                    if (SystemInfo.CustomersCode == 0)
                    {
                        SystemInfo.CustomersCode = BuildCustomersCode();
                        WriteConfig("RS_System", "CustomersCode", SystemInfo.CustomersCode);
                    }
                    //取卡密钥
                    SystemInfo.CardKey = ReadConfig("RS_System", "CardKey", "");
                    if (SystemInfo.CardKey != "")
                    {
                        SystemInfo.CardKey = DeviceObject.objCPIC.GetCardKey(SystemInfo.CardKey, "yyc120114");
                    }
                    //发卡时使用9位人员编号
                    SystemInfo.IsLongEmpID = ReadConfig("RS_System", "IsLongEmpID", true);
                    DeviceObject.objCPIC.InitCardKey(SystemInfo.CardKey, SystemInfo.DealersCode, SystemInfo.PubCardSector, SystemInfo.CustomersCode);
                    DeviceObject.objCPIC.AllowCardWarn = SystemInfo.AllowCardWarn;
                    DeviceObject.objKS.InitCard(SystemInfo.CardKey, SystemInfo.PubCardSector, SystemInfo.DealersCode, SystemInfo.CustomersCode);
                    DeviceObject.objKS.CardTypeCount = SystemInfo.CardTypeCount;
                    //收费机、充值机、计时机支持补贴功能
                    SystemInfo.AllDevAllowance = ReadConfig("RS_System", "AllDevAllowance", false);
                    SystemInfo.AllowExtScreen = ReadConfig("RS_System", "AllowExtScreen", false);

                    if(chkAutoPeriod.Checked)
                    {
                        if(txtBeginTime2.Text == "00:00" && txtBeginTime3.Text == "00:00")
                        {
                            txtBeginTime1.Focus();
                            MessageBoxEx.Show(PERIODINFOERROR, MESSAGE);
                            return;
                        }
                    }

                    SystemInfo.MealType = ((TCommonType)cbbMealType.Items[cbbMealType.SelectedIndex]).id;
                    for (int i = 0; i < 4; i++)
                    {
                        SystemInfo.MealTypeList[i] = ((TCommonType)cbbMealType.Items[i]).id;
                    }
                }
                catch
                {

                }
               
            }
            if(!SystemInfo.serialPort.IsOpen)
            {
                MessageBoxEx.Show(OPENSERIAPORTFAILD, MESSAGE);
                return;
            }
       
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 初始化餐类
        /// </summary>
        private void LoadParamInfo()
        {
            SFSoftParamInfo paramInfo = new SFSoftParamInfo("");
            DataTableReader dr = null;
            cbbMealType.Items.Clear();
            try
            {
                dr = db.GetDataReader("SELECT * FROM VSF_MealType ORDER BY MealTypeID");
                while (dr.Read())
                {
                    cbbMealType.Items.Add(new TCommonType(dr["MealTypeSysID"].ToString(), dr["MealTypeID"].ToString(),
                      dr["MealTypeName"].ToString(), true));
                }
                dr.Close();

                dr = db.GetDataReader("SELECT * FROM SY_Config WHERE ID='SFSoft' AND [Key]='ParamInfo'");
                if (dr.Read()) paramInfo = new SFSoftParamInfo(dr["Value"].ToString());
            }
            catch 
            {
               
            }
            finally
            {
                if (dr != null) dr.Close();
                dr = null;
            }
            txtBeginTime1.Text = paramInfo.BeginTime[0];
            txtBeginTime2.Text = paramInfo.BeginTime[1];
            txtBeginTime3.Text = paramInfo.BeginTime[2];
            txtBeginTime4.Text = paramInfo.BeginTime[3];
            txtEndTime1.Text = paramInfo.EndTime[0];
            txtEndTime2.Text = paramInfo.EndTime[1];
            txtEndTime3.Text = paramInfo.EndTime[2];
            txtEndTime4.Text = paramInfo.EndTime[3];
            if (cbbMealType.Items.Count > 0) cbbMealType.SelectedIndex = 0;
        }

       
        /// <summary>
        /// 设置餐类参数
        /// </summary>
        private bool SetParamInfo()
        {
            MaskedTextBox txt;
            string infoStr = "";
            bool IsOk = false;
            for (int i = 1; i <= 4; i++)
            {
                txt = (MaskedTextBox)groupBox1.Controls["txtBeginTime" + i.ToString()];
                infoStr = infoStr + txt.Text + "#";
                txt = (MaskedTextBox)groupBox1.Controls["txtEndTime" + i.ToString()];
                infoStr = infoStr + txt.Text + "@";
            }
            infoStr = infoStr.Substring(0, infoStr.Length - 1);
            DataTableReader dr = null;
            string sql = "";
            try
            {
                if (!db.IsOpen) db.Open(SystemInfo.ConnStr);
                dr = db.GetDataReader("SELECT * FROM SY_Config WHERE ID='SFSoft' AND [Key]='ParamInfo'");
                if (dr.Read())
                    sql = "UPDATE SY_Config SET [Value]='" + infoStr + "' WHERE ID='SFSoft' AND [Key]='ParamInfo'";
                else
                    sql = "INSERT INTO SY_Config(ID,[Key],[Value]) VALUES('SFSoft','ParamInfo','" + infoStr + "')";
                db.ExecSQL(sql);
                IsOk = true;
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message, MESSAGE);
            }
            finally
            {
                if (dr != null) dr.Close();
                dr = null;
            }
            if (IsOk)
            {
                db.WriteSYLog(this.Text, SETTIMEINFO, sql);
            }
            return IsOk;
        }
        /// <summary>
        /// 取收费客户代码
        /// </summary>
        /// <returns></returns>
        private int BuildCustomersCode()
        {
            string ret = "";
            Random rd = new Random();
            for (int i = 1; i <= 6; i++)
            {
                ret += rd.Next(9).ToString();
            }
            return Convert.ToInt32(ret);
        }

        /// <summary>
        /// 公共扇区
        /// </summary>
        public void SetSFCardSector()
        {
            SystemInfo.SFCardSector = Convert.ToByte(SystemInfo.PubCardSector + 1);
            SystemInfo.SKCardSector = Convert.ToByte(SystemInfo.SFCardSector + 2);
        }
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSet_Load(object sender, EventArgs e)
        {
           
            if(SystemInfo.isOpened)
            {
                SystemInfo.IsthreadSend = false;
                SystemInfo.serialPort.Close();     //关闭串口
                btnConnet.Text = OPENSERIAPORT;
                SystemInfo.isOpened = false;
            }

            string[] sSubKeys = SerialPort.GetPortNames();
            if(sSubKeys.Length > 0)
            {
                cmbPort.Items.Clear();
               
                cmbPort.Items.Clear();
                cmbPort.DataSource = sSubKeys;
               
            }
            
            if (cmbPort.Items.Count > 0)
            {
                cmbPort.SelectedIndex = 0;
              
            }

            if (SystemInfo.ini.ExistINIFile())
            {
                cmbBaud.Text = SystemInfo.ini.IniReadValue("Public", "波特率", "19200");
                cmbPort.Text = SystemInfo.ini.IniReadValue("Public", "串口", "COM1");

                bool Type = false;
                switch (SystemInfo.DBType)
                {
                    case 1:
                        txtMSSQLServer.Text = SystemInfo.ini.IniReadValue("MSSQL", "Server", ".");
                        Type = Convert.ToBoolean(SystemInfo.ini.IniReadValue("MSSQL", "WindowsNT", "true"));
                        if (Type)
                        {
                            rbMSSQLWindowsNT.Checked = true;
                        }
                        else
                        {
                            rbMSSQLSQL.Checked = true;
                        }

                        txtMSSQLUserName.Text = SystemInfo.ini.IniReadValue("MSSQL", "UserName", "");
                        txtMSSQLUserPass.Text = SystemInfo.ini.IniReadValue("MSSQL", "UserPass", "");
                        cbbDBName.Text = SystemInfo.ini.IniReadValue("MSSQL", "DBName", "");
                        break;
                    case 2:
                        txtMSSQLServer.Text = SystemInfo.ini.IniReadValue("MSDE", "Server", ".");
                        Type = Convert.ToBoolean(SystemInfo.ini.IniReadValue("MSDE", "WindowsNT", "true"));
                        if (Type)
                        {
                            rbMSSQLWindowsNT.Checked = true;
                        }
                        else
                        {
                            rbMSSQLSQL.Checked = true;
                        }

                        txtMSSQLUserName.Text = SystemInfo.ini.IniReadValue("MSDE", "UserName", "");
                        txtMSSQLUserPass.Text = SystemInfo.ini.IniReadValue("MSDE", "UserPass", "");
                        cbbDBName.Text = SystemInfo.ini.IniReadValue("MSDE", "DBName", "");
                        break;
                }
                TestLink(true);
                LoadParamInfo();
                GetMacSN();
                chkBTBalance.Checked = Convert.ToBoolean(SystemInfo.ini.IniReadValue("Public", "BTBalance", "true"));
                chkAutoPeriod.Checked = Convert.ToBoolean(SystemInfo.ini.IniReadValue("Public", "AutoPeriod", "true"));
                int index = 0;
                int.TryParse(SystemInfo.ini.IniReadValue("Public", "MealType", "0"), out index);
                cbbMealType.SelectedIndex = index;
                chkAutoXF.Checked = Convert.ToBoolean(SystemInfo.ini.IniReadValue("Public", "AutoXF", "true"));
                chkPowerOn.Checked = Convert.ToBoolean(SystemInfo.ini.IniReadValue("Public", "PowerOn", "true"));
                chkPrint.Checked = Convert.ToBoolean(SystemInfo.ini.IniReadValue("Public", "Print", "true"));
                #region
                //txtMacSN.Text = SystemInfo.ini.IniReadValue("Public", "MacSN", "1");
                //cbbCommPort.Text = SystemInfo.ini.IniReadValue("Public", "CommPort", "COM1");
                //int sIndex = 0;
                //int.TryParse(SystemInfo.ini.IniReadValue("Public", "CommPort", "3"), out sIndex);
                //if(sIndex >= 0)
                //    cbbCommBaudRate.SelectedIndex = sIndex;
                //txtLANIP.Text = SystemInfo.ini.IniReadValue("Public", "LANIP", "");
                //txtLANPort.Text = SystemInfo.ini.IniReadValue("Public", "LANPort", "6000");
                //txtWANIP.Text = SystemInfo.ini.IniReadValue("Public", "WANIP", "");
                //txtWANPort.Text = SystemInfo.ini.IniReadValue("Public", "WANPort", "6000");
                //txtWANAddress.Text = SystemInfo.ini.IniReadValue("Public", "WANAddress", "6000");
                //MacConnType = SystemInfo.ini.IniReadValue("Public", "SelectConnetType", "USB");

                //switch (MacConnType)
                //{
                //    case "USB":
                //        rbUSB.Checked = true;
                //        break;
                //    case "RS232/485":
                //        rbComm.Checked = true;
                //        break;
                //    case "LAN":
                //        rbLAN.Checked = true;
                //        break;
                //    case "GPRS":
                //        rbWAN.Checked = true;
                //        break;
                //    default:
                //        rbUSB.Checked = true;
                //        break;
                //}
                //pnlComm.Enabled = rbComm.Checked;
                //pnlLAN.Enabled = rbLAN.Checked;
                //pnlWAN.Enabled = rbWAN.Checked;


                #endregion
            }
            else
            {
                cmbBaud.Text = "19200";
                cmbPort.Text = "COM1";
                txtMSSQLServer.Text = ".";
                rbMSSQLWindowsNT.Checked = true;

                TestLink(true);
                chkBTBalance.Checked = true;
                chkAutoPeriod.Checked = true;
                chkAutoXF.Checked = true;
                txtMacNomber.Text = "301";
                SystemInfo.DBMacSN = "301";
            }
            if (SystemInfo.isOpened)
            {
                btnConnet.Text = CLOSESERIAPORT;
            }
            else
            {
                btnConnet.Text = OPENSERIAPORT;
            }
            
        }

        /// <summary>
        /// 获取机器号
        /// </summary>
        /// <returns></returns>
        private bool GetMacSN()
        {
            string getMacIni = SystemInfo.ini.IniReadValue("Public", "MacNumber", "301");
            txtMacNomber.Text = getMacIni;
            SystemInfo.DBMacSN = getMacIni;
            Application.DoEvents();
            return true;
        }

        public int GetNumber(string mac)
        {
            try
            {
                return Convert.ToInt32(mac);
            }
            catch 
            {
                return 0;
            }
        }
        /// <summary>
        /// 设置机器号
        /// </summary>
        /// <returns></returns>
        private bool SetMacSN()
        {
            string MacSN = txtMacNomber.Text.Trim();
            int macno = GetNumber(MacSN);
            if (MacSN == "" || macno == 0)
            {
                txtMacNomber.Focus();
                MessageBoxEx.Show(INPUTMACSN, MESSAGE);
                return false;
            }
           
            if (macno < 301 || macno > 600)
            {
                txtMacNomber.Focus();
                MessageBoxEx.Show("机器号的范围是301-600", MESSAGE);
                return false;
            }
            DataTableReader dr = db.GetDataReader("SELECT * FROM VSF_MacInfo WHERE MacSN='" + MacSN + "'");
            if (dr.Read())
            {
                string getMacIni = SystemInfo.ini.IniReadValue("Public", "MacNumber", "301");
                if (getMacIni == MacSN)
                {
                    return true;
                }
                if(MessageBoxEx.Show(THESAMEMACHINENUMBEREXISTS, MESSAGE,MessageBoxButtons.OKCancel)==DialogResult.OK)
                {
                    SystemInfo.ini.IniWriteValue("Public", "MacNumber", MacSN);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string sql = "INSERT INTO SF_MacInfo(MacSysID,MacSN,MacType,MacConnType,MacIpAddress,MacPort," +
                    "MacConnPWD,MacDesc,MacPhysicsAddress,MacAddressNo,Opter,IsBigMac) VALUES(newid(),'" + MacSN + "','132','USB','','','','','','','1',0)";
                if (db.ExecSQL(sql) > 0)
                {
                    SystemInfo.ini.IniWriteValue("Public", "MacNumber", MacSN);
                }
            }
            return true;
        }

        private void frmSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemInfo.ini.IniWriteValue("Public", "BTBalance", chkBTBalance.Checked.ToString());
            SystemInfo.ini.IniWriteValue("Public", "AutoPeriod", chkAutoPeriod.Checked.ToString());
            SystemInfo.ini.IniWriteValue("Public", "MealType", cbbMealType.Text);
            SystemInfo.ini.IniWriteValue("Public", "串口", cmbPort.Text);
            SystemInfo.ini.IniWriteValue("Public", "波特率",cmbBaud.Text);
            SystemInfo.ini.IniWriteValue("Public", "AutoXF", chkAutoXF.Checked.ToString());
            SystemInfo.ini.IniWriteValue("Public", "AutoPeriod", chkAutoPeriod.Checked.ToString());
            SystemInfo.ini.IniWriteValue("Public", "BTBalance", chkBTBalance.Checked.ToString());
            SystemInfo.ini.IniWriteValue("Public", "MacNumber", txtMacNomber.Text.Trim());
            SystemInfo.ini.IniWriteValue("Public", "PowerOn", chkPowerOn.Checked.ToString());
            SystemInfo.ini.IniWriteValue("Public", "Print", chkPrint.Checked.ToString());
            SetMeStart(chkPowerOn.Checked);

            SystemInfo.IsPrint = chkPrint.Checked;
        }

        private void btnTestConnet_Click(object sender, EventArgs e)
        {
            TestLink(false);
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="IsTest">是不是测试</param>
        /// <returns></returns>
        private bool TestLink(bool IsTest)
        {
            string[] dbName = cbbDBName.Text.Split('[');
            DBName = dbName[0];
            bool ret = false;
            int DBType = SystemInfo.DBType;
            string ServerName = "";
            bool WindowsNT = true;
            string UserName = "";
            string UserPass = "";
            switch (DBType)
            {
                case 1:
                case 2:
                    ServerName = txtMSSQLServer.Text.Trim();
                    WindowsNT = rbMSSQLWindowsNT.Checked;
                    if (!WindowsNT)
                    {
                        UserName = txtMSSQLUserName.Text.Trim();
                        UserPass = txtMSSQLUserPass.Text.Trim();
                    }
                    break;
            }
          
            if(DBName == "")
            {
                if(!getDBName())
                {
                    MessageBoxEx.Show(INPUTDBNAME, MESSAGE);
                    return ret;
                }
                else
                {
                    dbName = cbbDBName.Text.Split('[');
                    DBName = dbName[0];
                }
            }
            SystemInfo.ConnStr = GetMSSQLConnStr(ServerName, WindowsNT, UserName, UserPass, DBName);

            try
            {
                db.Open(DBType, SystemInfo.ConnStr);

                DataTableReader dr = db.GetDataReader("SELECT * FROM master..Hs_Account Where DBName='"+ DBName + "'");
                if(dr.Read())
                {
                    if(dr["IsForward"].ToString()=="Y")
                    {
                        MessageBoxEx.Show(CHECKOUISNOTALLOWEDTOCONSUME, MESSAGE);
                        ret = false;
                        return ret;
                    }
                }
                ret = true;
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message);
            }
            finally
            {
                db.Close();
            }
            if (ret)
            {
                switch (DBType)
                {
                    case 1:
                        SystemInfo.ini.IniWriteValue("MSSQL", "Server", ServerName);
                        SystemInfo.ini.IniWriteValue("MSSQL", "WindowsNT", WindowsNT.ToString());
                        SystemInfo.ini.IniWriteValue("MSSQL", "UserName", UserName);
                        SystemInfo.ini.IniWriteValue("MSSQL", "UserPass", UserPass);
                        SystemInfo.ini.IniWriteValue("MSSQL", "DBName", DBName);
                        break;
                    case 2:
                        SystemInfo.ini.IniWriteValue("MSDE", "Server", ServerName);
                        SystemInfo.ini.IniWriteValue("MSDE", "WindowsNT", WindowsNT.ToString());
                        SystemInfo.ini.IniWriteValue("MSDE", "UserName", UserName);
                        SystemInfo.ini.IniWriteValue("MSDE", "UserPass", UserPass);
                        SystemInfo.ini.IniWriteValue("MSDE", "DBName", DBName);
                        break;
                }
                if(!IsTest)
                {
                    MessageBoxEx.Show(CONNETDBSUCCESS, MESSAGE);
                }
            }
            else
            {
                MessageBoxEx.Show(CONNETDBFAILD, MESSAGE);
            }
            return ret;
        }
        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns></returns>
        public bool getDBName()
        {
            string DBName = cbbDBName.Text;
            bool ret = false;
            int DBType = SystemInfo.DBType;
            string ServerName = "";
            bool WindowsNT = true;
            string UserName = "";
            string UserPass = "";
            string IsForward = "";
            switch (DBType)
            {
                case 1:
                case 2:
                    ServerName = txtMSSQLServer.Text.Trim();
                    WindowsNT = rbMSSQLWindowsNT.Checked;
                    if (!WindowsNT)
                    {
                        UserName = txtMSSQLUserName.Text.Trim();
                        UserPass = txtMSSQLUserPass.Text.Trim();
                    }
                    break;
            }
            SystemInfo.ConnStr = GetMSSQLConnStr(ServerName, WindowsNT, UserName, UserPass, "master");

            try
            {
                db.Open(DBType, SystemInfo.ConnStr);
                ret = true;
               
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message);
            }
            finally
            {
                db.Close();
            }
            if(ret)
            {
                DataTableReader dr = db.GetDataReader("SELECT * FROM master..Hs_Account ORDER BY AccName");
                while (dr.Read())
                {
                    IsForward = dr["IsForward"].ToString()=="Y"?"结算":"";
                    if(IsForward!="")
                        cbbDBName.Items.Add(dr["DBName"].ToString()+"["+ IsForward + "]");
                    else
                        cbbDBName.Items.Add(dr["DBName"].ToString());
                }
                if(cbbDBName.Text == ""&& cbbDBName.Items.Count>0)
                    cbbDBName.SelectedIndex = 0;
            }

            return ret;
        }

        public string GetMSSQLConnStr(string ServerName, bool WindowsNT, string UserName, string UserPass, string DBName)
        {
            if (WindowsNT)
                return string.Format("Trusted_Connection=true;Server={0};Database={1};Pooling=False", ServerName, DBName);
            else
                return string.Format("Trusted_Connection=false;Server={0};Database={1};uid={2};pwd={3};Pooling=False",
                  ServerName, DBName, UserName, UserPass);
        }

        private void btnGetDBName_Click(object sender, EventArgs e)
        {
            getDBName();
            cbbDBName.DroppedDown = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 获取餐类信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetParamInfo_Click(object sender, EventArgs e)
        {
            TestLink(true);
            LoadParamInfo();
            if (cbbMealType.SelectedIndex < 0)
            {
                MessageBoxEx.Show(CONNETDBFAILD, MESSAGE);
                return;
            }
            SystemInfo.MealType = ((TCommonType)cbbMealType.Items[cbbMealType.SelectedIndex]).id;
            for (int i = 0; i < 4; i++)
            {
                SystemInfo.MealTypeList[i] = ((TCommonType)cbbMealType.Items[i]).id;
            }
        }

        private void btnCloses_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloses_MouseEnter(object sender, EventArgs e)
        {
            btnCloses.SymbolColor = Color.Red;
        }

        private void btnCloses_MouseLeave(object sender, EventArgs e)
        {
            btnCloses.SymbolColor = Color.White;
        }

        private void btnUpMacSN_Click(object sender, EventArgs e)
        {
            if(SetMacSN())
            {
                MessageBoxEx.Show(EDITSUCCESS, MESSAGE);
            }
        }

        private void cbbDBName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(TestLink(true))
                GetMacSN();
        }

        /// <summary>
        /// 将本程序设为开启自启
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <returns></returns>
        public bool SetMeStart(bool onOff)
        {
            bool isOk = false;
            string appName = Process.GetCurrentProcess().MainModule.ModuleName;
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            isOk = SetAutoStart(onOff, appName, appPath);
            return isOk;
        }

        /// <summary>
        /// 将应用程序设为或不设为开机启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <param name="appName">应用程序名</param>
        /// <param name="appPath">应用程序完全路径</param>
        public bool SetAutoStart(bool onOff, string appName, string appPath)
        {
            bool isOk = true;
            //如果从没有设为开机启动设置到要设为开机启动
            if (!IsExistKey(appName) && onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            //如果从设为开机启动设置到不要设为开机启动
            else if (IsExistKey(appName) && !onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            return isOk;
        }

        /// <summary>
        /// 判断注册键值对是否存在，即是否处于开机启动状态
        /// </summary>
        /// <param name="keyName">键值名</param>
        /// <returns></returns>
        private bool IsExistKey(string keyName)
        {
            try
            {
                bool _exist = false;
                RegistryKey local = Registry.LocalMachine;
                RegistryKey runs = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null)
                {
                    RegistryKey key2 = local.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName)
                {
                    if (strName.ToUpper() == keyName.ToUpper())
                    {
                        _exist = true;
                        return _exist;
                    }
                }
                return _exist;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 写入或删除注册表键值对,即设为开机启动或开机不启动
        /// </summary>
        /// <param name="isStart">是否开机启动</param>
        /// <param name="exeName">应用程序名</param>
        /// <param name="path">应用程序路径带程序名</param>
        /// <returns></returns>
        private bool SelfRunning(bool isStart, string exeName, string path)
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                //若开机自启动则添加键值对
                if (isStart)
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames)
                    {
                        if (keyName.ToUpper() == exeName.ToUpper())
                        {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                return false;
                //throw;
            }

            return true;
        }

        private void btnTimeSet_Click(object sender, EventArgs e)
        {
            if(SetParamInfo()) MessageBoxEx.Show(SAVESUCCESS, MESSAGE);
        }
    }
}
