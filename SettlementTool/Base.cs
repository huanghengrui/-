using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SettlementTool
{
    public class SystemInfo
    {
        public static System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        public static System.IO.Ports.SerialPort serialPort = new System.IO.Ports.SerialPort(components);

        public static  IniFiles ini = new IniFiles(Application.StartupPath + @"\Config.ini");
        public static  string cBaud = "19200";
        public static  string cPort = "COM1";
        public static  Cmd cmd_buffer = new Cmd();
        public static  bool isWrite = false;
        public static  bool isHand = false;
        public static  bool threadStop = false;
        public static  bool threadSendStop = false;
        public static  bool threadSend = false;
        public static  bool IsthreadSend = false;
        public static  bool isOpened = false;
        public static  int ThreadCount = 0;
        public static  int ReThreadCount = 0;
        public static  string readCardNumber = "";
        public static  int readCardIndex = 0;
        public static  int readCardCount = 0;
        public static  string cardData = "";
        public static  List<string> cardList = new List<string>();
        public static  List<string> threadCardList = new List<string>();
        public static  int delayIndex = 500;
        public static  string inipath;
        public static int DBType = 1;
        public static string ConnStr = "";
        public static bool IsBigMacAdd = false;
        public static int MaxSN603 = 254;
        public static int MaxSN603Ex = 65534;
        public static TRealSocket socket;
        public static string moneyStr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + " ";
        public static string SQLDateTimeFMT = "yyyy-MM-dd HH:mm:ss";
        public static string YMFormatCard = "yyyyMM";
        public static byte SFBtBagFlag = 0;
        public static byte SFBtBagDate = 0;
        public static string CardKey = "";
        public static bool IsLongEmpID = false;
        public static bool AntiDuplication = false;
        public static string ComputerName = "";
        public static byte PubCardSector = 1;
        public static byte SFCardSector = 2;
        public static byte SKCardSector = 4;
        public static int CustomersCode = 0;
        public static string DealersCode = "";
        public static bool AllowCardWarn = false;
        public static int CardTypeCount = 8;
        public static bool IsUseBTBalance = false;
        public static bool IsAutoPeriod = false;
        public static bool AllowExtScreen = false;
        public static bool AllDevAllowance = false;
        public static string MealType = "";
        public static string[] MealTypeList = new string[4];
        public static bool IsAutoXF = false;
        public static bool IsWorking = false;
        public static bool IsPrint = true;
        public static string DBMacSN = "0";
    }
    public struct MacConnTypeString
    {
        public const string USB = "USB";
        public const string Comm = "RS232/485";
        public const string LAN = "LAN";
        public const string GPRS = "GPRS";
    }

    public struct DeviceObject
    {
        public static QHKS.KS objKS = null;
        public static QHKS.MJ objMJ = null;
        public static QHKS.App objApp = null;
        public static HSUNFK.AES objAES = null;
        public static HSUNFK.DES objDES = null;
        public static HSUNFK.CPIC objCPIC = null;
        public static HSUNFK.Card objCard = null;
        // public static HSFAPI.IKS objIkS = null;
    }

    public class Database
    {
        private SqlConnection sqlConn = null;
        private OleDbConnection oleConn = null;
        private string ConnStr = "";
        private int _DBType = 0;
        const int CommandTimeout = 36000;
        public Database(string ConnectionString)
        {
            ConnStr = ConnectionString;
            _DBType = SystemInfo.DBType;
        }

        public void Open()
        {
            Open(ConnStr);
        }

        public void Open(string ConnectionString)
        {
            ConnStr = ConnectionString;
            Open(_DBType, ConnStr);
        }

        public void Open(int DBType, string ConnectionString)
        {
            _DBType = DBType;
            ConnStr = ConnectionString;
            Close();
            switch (_DBType)
            {
                case 1:
                case 2:
                    sqlConn = new SqlConnection(ConnStr);
                    sqlConn.Open();
                    break;
                case 255:
                    oleConn = new OleDbConnection(ConnStr);
                    oleConn.Open();
                    break;
            }
        }

        public void SetConnStr(string ConnectionString)
        {
            ConnStr = ConnectionString;
        }

        public void Close()
        {
            if (sqlConn != null)
            {
                sqlConn.Close();
                sqlConn = null;
            }
            if (oleConn != null)
            {
                oleConn.Close();
                oleConn = null;
            }
        }

        public bool IsOpen
        {
            get
            {
                return ((sqlConn != null) && (sqlConn.State == ConnectionState.Open)) ||
                  ((oleConn != null) && (oleConn.State == ConnectionState.Open));
            }
        }

        public int ExecSQL(string SQLQuery)
        {
            SQLQuery = SQLQuery.Trim();
            int ret = 0;
            switch (_DBType)
            {
                case 1:
                case 2:
                    using (SqlConnection sqlConn = new SqlConnection(SystemInfo.ConnStr))
                    {
                        sqlConn.Open();
                        SqlCommand sqlCmd = new SqlCommand(SQLQuery, sqlConn);
                        sqlCmd.CommandTimeout = CommandTimeout;
                        ret = sqlCmd.ExecuteNonQuery();
                    }
                    break;
                case 255:
                    using (OleDbConnection oleConn = new OleDbConnection(SystemInfo.ConnStr))
                    {
                        oleConn.Open();
                        OleDbCommand oleCmd = new OleDbCommand(SQLQuery, oleConn);
                        oleCmd.CommandTimeout = CommandTimeout;
                        ret = oleCmd.ExecuteNonQuery();
                    }
                    break;
            }
            return ret;
        }

        public int ExecSQL(List<string> SQLQuery)
        {
            int ret = 0;
            string sql = "";
            try
            {
                switch (_DBType)
                {
                    case 1:
                    case 2:
                        using (SqlConnection sqlConn = new SqlConnection(SystemInfo.ConnStr))
                        {
                            sqlConn.Open();
                            SqlCommand sqlCmd;
                            SqlTransaction sqlTran = sqlConn.BeginTransaction();
                            try
                            {
                                for (int i = 0; i < SQLQuery.Count; i++)
                                {
                                    sql = SQLQuery[i].Trim();
                                    if (sql == "") continue;
                                    sqlCmd = new SqlCommand(sql, sqlConn);
                                    sqlCmd.CommandTimeout = CommandTimeout;
                                    sqlCmd.Transaction = sqlTran;
                                    sqlCmd.ExecuteNonQuery();
                                }
                                sqlTran.Commit();
                            }
                            catch (Exception E)
                            {
                                ret = 1;
                                MessageBoxEx.Show(E.Message + "\r\n" + sql);
                                sqlTran.Rollback();
                            }
                        }
                        break;
                    case 255:
                        using (OleDbConnection oleConn = new OleDbConnection(SystemInfo.ConnStr))
                        {
                            oleConn.Open();
                            OleDbCommand oleCmd;
                            OleDbTransaction oleTran = oleConn.BeginTransaction();
                            try
                            {
                                for (int i = 0; i < SQLQuery.Count; i++)
                                {
                                    sql = SQLQuery[i].Trim();
                                    if (sql == "") continue;
                                    oleCmd = new OleDbCommand(sql, oleConn);
                                    oleCmd.CommandTimeout = CommandTimeout;
                                    oleCmd.Transaction = oleTran;
                                    oleCmd.ExecuteNonQuery();
                                }
                                oleTran.Commit();
                            }
                            catch (Exception E)
                            {
                                ret = 1;
                                MessageBoxEx.Show(E.Message + "\r\n" + sql);
                                oleTran.Rollback();
                            }
                        }
                        break;
                }
            }
            catch (Exception E)
            {
                ret = 1;
                MessageBoxEx.Show(E.Message);
            }
            return ret;
        }

        public int ExecSQL(string SQLQuery, bool HideError)
        {
            int ret = 0;
            if (HideError)
            {
                try
                {
                    ret = ExecSQL(SQLQuery);
                }
                catch { }
            }
            else
            {
                ret = ExecSQL(SQLQuery);
            }
            return ret;
        }

        public string ExecSQLMsg(string SQLQuery)
        {
            string ret = "";
            DataTableReader dr = GetDataReader(SQLQuery);
            if (dr.Read()) ret = dr[0].ToString();
            dr.Close();
            return ret.Trim();
        }

        public DataTableReader GetDataReader(string SQLQuery)
        {
            //lock (sign)
            {
                DataSet ds = GetDataSet(SQLQuery);
                return ds.CreateDataReader();
            }

        }

        public DataSet GetDataSet(string SQLQuery)
        {
            DataSet ds = new DataSet();
            if (SQLQuery == "")
            {
                ds = null;
            }
            else
            {
                switch (_DBType)
                {
                    case 1:
                    case 2:
                        using (SqlConnection sqlConn = new SqlConnection(SystemInfo.ConnStr))
                        {
                            sqlConn.Open();
                            SqlDataAdapter sqlDA = new SqlDataAdapter(SQLQuery, sqlConn);
                            if (sqlDA.SelectCommand != null) sqlDA.SelectCommand.CommandTimeout = CommandTimeout;
                            if (sqlDA.DeleteCommand != null) sqlDA.DeleteCommand.CommandTimeout = CommandTimeout;
                            if (sqlDA.UpdateCommand != null) sqlDA.UpdateCommand.CommandTimeout = CommandTimeout;
                            sqlDA.Fill(ds, "DataSource");
                            sqlDA.Dispose();
                            sqlDA = null;
                        }

                        break;
                    case 255:
                        using (OleDbConnection oleConn = new OleDbConnection(SystemInfo.ConnStr))
                        {
                            oleConn.Open();
                            OleDbDataAdapter oleDA = new OleDbDataAdapter(SQLQuery, oleConn);
                            if (oleDA.SelectCommand != null) oleDA.SelectCommand.CommandTimeout = CommandTimeout;
                            if (oleDA.DeleteCommand != null) oleDA.DeleteCommand.CommandTimeout = CommandTimeout;
                            if (oleDA.UpdateCommand != null) oleDA.UpdateCommand.CommandTimeout = CommandTimeout;
                            oleDA.Fill(ds, "DataSource");
                            oleDA.Dispose();
                            oleDA = null;
                        }

                        break;
                }
            }
            return ds;
        }
      
        public DataTable GetDataTable(string SQLQuery)
        {
            DataTable dt = new DataTable();
            if (SQLQuery == "")
            {
                dt = null;
            }
            else
            {
                switch (_DBType)
                {
                    case 1:
                    case 2:
                        using (SqlConnection sqlConn = new SqlConnection(SystemInfo.ConnStr))
                        {
                            sqlConn.Open();
                            SqlDataAdapter sqlDA = new SqlDataAdapter(SQLQuery, sqlConn);
                            if (sqlDA.SelectCommand != null) sqlDA.SelectCommand.CommandTimeout = CommandTimeout;
                            if (sqlDA.DeleteCommand != null) sqlDA.DeleteCommand.CommandTimeout = CommandTimeout;
                            if (sqlDA.UpdateCommand != null) sqlDA.UpdateCommand.CommandTimeout = CommandTimeout;
                            sqlDA.Fill(dt);
                            sqlDA.Dispose();
                            sqlDA = null;
                        }
                        break;
                    case 255:
                        using (OleDbConnection oleConn = new OleDbConnection(SystemInfo.ConnStr))
                        {
                            oleConn.Open();
                            OleDbDataAdapter oleDA = new OleDbDataAdapter(SQLQuery, oleConn);
                            if (oleDA.SelectCommand != null) oleDA.SelectCommand.CommandTimeout = CommandTimeout;
                            if (oleDA.DeleteCommand != null) oleDA.DeleteCommand.CommandTimeout = CommandTimeout;
                            if (oleDA.UpdateCommand != null) oleDA.UpdateCommand.CommandTimeout = CommandTimeout;
                            oleDA.Fill(dt);
                            oleDA.Dispose();
                            oleDA = null;
                        }
                        break;
                }
            }
            return dt;
        }

        public DataTable GetDataTableList()
        {
            DataTable dt = new DataTable();
            if (!IsOpen) Open();
            switch (_DBType)
            {
                case 1:
                case 2:
                    dt = sqlConn.GetSchema("Tables");
                    break;
                case 255:
                    dt = oleConn.GetSchema("Tables");
                    break;
            }
            return dt;
        }

        public string AttachDB(string Path, string DBName)
        {
            string ret = "";
            try
            {
                switch (_DBType)
                {
                    case 1:
                    case 2:
                        ExecSQL("EXEC sp_attach_db '" + DBName + "','" + Path + DBName + ".mdf','" + Path + DBName + ".ldf'");
                        break;
                }
            }
            catch (Exception E)
            {
                ret = E.Source + "\r\n\r\n" + E.Message;
            }
            return ret;
        }

        public bool CompactDatabase()
        {
            bool ret = false;
            DataTableReader dr = null;
            try
            {
                switch (_DBType)
                {
                    case 1:
                    case 2:
                        dr = GetDataReader("SELECT * FROM sysfiles");
                        while (dr.Read())
                        {
                            ExecSQL("DBCC SHRINKFILE('" + dr["name"].ToString().Trim() + "')");
                        }
                        ret = true;
                        break;
                }
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

        public bool CreateDatabase(string Path, string DBName)
        {
            bool ret = false;
            string sql;
            try
            {
                switch (_DBType)
                {
                    case 1:
                    case 2:
                        sql = "CREATE DATABASE [" + DBName + "] ON(NAME='" + DBName + "_Data', FILENAME='" + Path +
                          DBName + ".mdf') LOG ON(NAME='" + DBName + "_Log',FILENAME='" + Path + DBName + ".ldf')";
                        ExecSQL(sql);
                        ret = true;
                        break;
                }
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message);
            }
            return ret;
        }

        public bool DeleteDatabase(string DBName, bool HideError)
        {
            bool ret = false;
            DataTableReader dr = null;
            try
            {
                switch (_DBType)
                {
                    case 1:
                    case 2:
                        dr = GetDataReader("SELECT spid FROM master..sysprocesses WHERE dbid=db_id('" + DBName + "')");
                        if (dr.Read()) ExecSQL("KILL " + dr[0].ToString().Trim());
                        ExecSQL("DROP DATABASE [" + DBName + "]");
                        ret = true;
                        break;
                }
            }
            catch (Exception E)
            {
                if (!HideError) MessageBoxEx.Show(E.Message);
            }
            finally
            {
                if (dr != null) dr.Close();
                dr = null;
            }
            return ret;
        }
        public bool GetServerDate(string title, ref DateTime ServerDate)
        {

            ServerDate = new DateTime();
            bool ret = false;
            DataTableReader dr = null;
            try
            {
                dr = GetDataReader("SELECT getdate() as ServerDate");
                if (dr.Read())
                {
                    ServerDate = Convert.ToDateTime(dr[0]);
                    ret = true;
                    if (ServerDate.Date != DateTime.Now.Date)
                    {
                        string msg = "服务器时间不等于电脑时间";
                        msg = string.Format(msg, ServerDate.Date, DateTime.Now.Date, title);
                        if (MessageBoxShowQuestion(msg)) ret = false;
                    }
                }
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
        public bool MessageBoxShowQuestion(string Msg)
        {
            return MessageBoxShowQuestion(Msg, MessageBoxIcon.Question);
        }

        public bool MessageBoxShowQuestion(string Msg, MessageBoxIcon Icon)
        {
            return MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, Icon,
              MessageBoxDefaultButton.Button2) == DialogResult.No;
        }

        public double GetBTMoney(string BTDate, double BTMoney)
        {   
            DateTime dt = new DateTime();
            double ret = BTMoney;
            if (BTDate == "000000") return ret;
            int y = 2000 + Convert.ToInt32(BTDate.Substring(0, 2), 16);
            int m = Convert.ToInt32(BTDate.Substring(2, 2), 16);
            int d = Convert.ToInt32(BTDate.Substring(4, 2), 16);
            dt = new DateTime(y, m, d);
            int year = DateTime.Now.Date.Year;
            byte month = (byte)DateTime.Now.Date.Month;
            byte day = (byte)DateTime.Now.Date.Day;
            byte maxDays = GetMonthDays(year, month);
            switch (SystemInfo.SFBtBagFlag)
            {
                case 0:
                    if (SystemInfo.SFBtBagDate != 0)
                    {
                        dt = dt.AddDays(SystemInfo.SFBtBagDate);
                        if (DateTime.Now.Date > dt) ret = 0;
                    }
                    break;
                case 1:
                    if (SystemInfo.SFBtBagDate != 0)
                    {
                        if ((month == 1) && (dt.Month == 12) && (year == dt.Year + 1))
                        {
                            if (day >= SystemInfo.SFBtBagDate) ret = 0;
                        }
                        else if ((year == dt.Year) && (month == dt.Month + 1))
                        {
                            if (day >= SystemInfo.SFBtBagDate)
                                ret = 0;
                            else if (SystemInfo.SFBtBagDate >= maxDays)
                                ret = 0;
                        }
                    }
                    break;
                case 2:
                    if ((month == 1) && (dt.Month == 12) && (year == dt.Year + 1) && (day >= 1))
                        ret = 0;
                    else if ((year == dt.Year) && (month == dt.Month + 1) && (day >= 1))
                        ret = 0;
                    break;
            }
            return ret;
        }

        public byte GetMonthDays(int Year, byte Month)
        {
            byte ret = 0;
            switch (Month)
            {
                case 2:
                    if (Year % 400 == 0)
                        ret = 29;
                    else if ((Year % 4 == 0) && (Year % 100 != 0))
                        ret = 29;
                    else
                        ret = 28;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    ret = 30;
                    break;
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    ret = 31;
                    break;
            }
            return ret;
        }
        public void WriteSYLog(string Title, string Tool, string Detail)
        {
            Detail = Detail.Replace("'", "");
                string sql = "INSERT INTO SY_Log(GUID,OprtTime,OprtModule,OprtTool,OprtDetail,OprtNoName,OprtComputerName) " +
                                  "VALUES(newid(),getdate(),'" + Title + "','" + Tool + "','" + Detail + "','顺盘结算工具','" + SystemInfo.ComputerName + "')";
                ExecSQL(sql, true);
        }
    }

    public struct OprtInfo
    {
        public static string OprtSysID = "";
        public static string OprtNo = "";
        public static bool OprtIsSys = false;
        public static string DepartPower = "";
        public static string DepartPowerSysID = "";
        public static string OprtNoAndName = "";
    }
    public class TCommonType
    {
        private string _sysid;
        private string _id;
        private string _name;
        private bool _hideID;

        public TCommonType(string sysID, string id, string name)
        {
            _sysid = sysID;
            _id = id;
            _name = name;
        }

        public TCommonType(string sysID, string id, string name, bool HideID)
        {
            _sysid = sysID;
            _id = id;
            _name = name;
            _hideID = HideID;
        }

        public string sysID
        {
            get { return _sysid; }
            set { _sysid = value; }
        }

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override string ToString()
        {
            if (_hideID || (_id == ""))
                return _name;
            else
                return "[" + _id + "]" + _name;
        }
    }
    public class TRealSocket
    {
        public delegate void ReadSocketData(string SocketData);
        private Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private bool RunningFlag = false;
        private ReadSocketData ReadSocket;
        private bool IsSend = false;

        public TRealSocket(int Port, ReadSocketData readData)
        {
            ReadSocket = readData;
            IPAddress UdpIP = null;
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipa in ips)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    UdpIP = ipa;
                    break;
                }
            }
            IPEndPoint ipLocalPoint;
            if (UdpIP == null)
                ipLocalPoint = new IPEndPoint(IPAddress.Any, Port);
            else
                ipLocalPoint = new IPEndPoint(UdpIP, Port);
            sock.Bind(ipLocalPoint);
            RunningFlag = true;
        }

        ~TRealSocket()
        {
            Stop();
        }

        public void Start()
        {
            ReceiveHandle();
        }

        public void Stop()
        {
            RunningFlag = false;
            if (sock != null) sock.Close();
            sock = null;
        }

        public void Send(string data)
        {
            try
            {
                int size = data.Length / 2;
                byte[] s = new byte[size];
                for (int i = 0; i < size; i++)
                {
                    s[i] = Convert.ToByte(data.Substring(i * 2, 2), 16);
                }
                sock.Send(s);
            }
            catch
            {
            }
        }

        private string ByteToHex(byte byt)
        {
            string ret = Convert.ToString(byt, 16);
            while (ret.Length < 2)
            {
                ret = "0" + ret;
            }
            return ret.ToUpper();
        }

        private void ReceiveHandle()
        {
            string msg;
            byte[] data = new byte[1024];
            while (RunningFlag)
            {
                try
                {
                    if (sock == null || sock.Available < 1)
                    {
                        Application.DoEvents();
                        continue;
                    }
                    if (IsSend)
                    {
                        Application.DoEvents();
                        continue;
                    }
                    IsSend = true;
                    int len = sock.Receive(data);
                    msg = "";
                    for (int i = 0; i < len; i++)
                    {
                        msg += ByteToHex(data[i]);
                    }
                    ReadSocket(msg);
                    IsSend = false;
                }
                catch
                {
                    IsSend = false;
                }
            }
        }
       
    }
    public class SFSoftParamInfo
    {
        private string[] _BeginTime = new string[4] { "00:01", "09:01", "13:01", "21:01" };
        private string[] _EndTime = new string[4] { "09:00", "13:00", "21:00", "23:59" };

        public SFSoftParamInfo(string ParamString)
        {
            string[] tmp = ParamString.Split('@');
            if (tmp.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    string[] tmp1 = tmp[i].Split('#');
                    if (tmp1.Length != 2) break;
                    _BeginTime[i] = ValidatTime(tmp1[0]);
                    _EndTime[i] = ValidatTime(tmp1[1]);
                }
            }
        }

        public string[] BeginTime
        {
            get { return _BeginTime; }
        }

        public string[] EndTime
        {
            get { return _EndTime; }
        }
        public string ValidatTime(string str)
        {
            string ret = "";
            string[] tmp = str.Split(':');
            if (tmp.Length >= 2)
            {
                if (tmp[0].Trim() == "")
                    tmp[0] = "00";
                else if (tmp[0].Trim().Length == 1)
                    tmp[0] = tmp[0].Trim() + "0";
                if (tmp[1].Trim() == "")
                    tmp[1] = "00";
                else if (tmp[1].Trim().Length == 1)
                    tmp[1] = tmp[1].Trim() + "0";
                ret = tmp[0] + ":" + tmp[1];
            }
            else
                ret = "00:00";
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(ret, out dt)) ret = "00:00";
            return ret;
        }
    }

}
