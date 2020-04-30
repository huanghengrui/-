using DevComponents.DotNetBar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SettlementTool
{
    public partial class frmMain : frmBase
    {
        public bool beginMove = false;
        public int currentXPosition = 0;
        public int currentYPosition = 0;
        
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 17;
        public delegate void OutDelegate(string text);
        private List<string> cardList = new List<string>();
        private DataTable dtData = new DataTable();
        public frmTite frmtite = null;
        public frmSPay frmSpay = null;

        private int[] ID = new int[10];
        private int[] Num = new int[10];
        private Double[] Price = new Double[10];
        private string[] ProductInfo = new string[10];
        private int printX = 0;
        private int printY = 0;
        private byte ProdCategory = 1;
        private int SettlementType = 0;
        private double Moneys = 0.00;
        private HSUNFK.TCardPubData pubData = new HSUNFK.TCardPubData();
        private HSUNFK.TCardSFData sfData = new HSUNFK.TCardSFData(); 
        private SFSoftParamInfo paramInfo = new SFSoftParamInfo("");
        private bool OkContinue = true;
        private bool IsfrmtiteClose = true;
        private static bool IsShowPhoto = true;
        private static string SFDataTime = "";
        private static string ZWPayType = "";

        public DataTable dtEmp = new DataTable();
        public DataTable dtEmpHistoryCard = new DataTable();
        public IntPtr mainHwnd;

        public const string MESSAGE = "提示";
        public const string UNABLETOGETDATABASEPERSONNELINFO = "无法获取到数据库的人员信息,请先添加人员!";
        public const string ONLYONECATEGORYISALLOWED = "只允许放一种类别,请重新放置卡片!";
        public const string OPENPAY = "请到设置开启扫码支付!";
        public const string SELECTPAY = "没有找到支付账号,请到设置进行配置!";
        public const string PAY = "扫码支付";
        public const string FIRSTUSE = "初次使用,请先正确的配置系统参数!";

        /// <summary>
        /// 设置可以改变窗体大小
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero;//默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            msgGrid.Columns.Clear();
            AddColumn(msgGrid, 0, "varieties", false, false, 1, msgGrid.Width / 2 - 10);
            AddColumn(msgGrid, 0, "money", false, false, 1, msgGrid.Width / 2 - 10);
            AddColumn(msgGrid, 0, "varietiesid", true, false, 1, 100);
            AddColumn(msgGrid, 0, "category", true, false, 1, 100);
            AddColumn(msgGrid, 0, "cardNo", true, false, 0, 100);

            dtData.Columns.Add("varieties", typeof(string));
            dtData.Columns.Add("money", typeof(string));
            dtData.Columns.Add("varietiesid", typeof(string));
            dtData.Columns.Add("category", typeof(string));
            dtData.Columns.Add("cardNo", typeof(string));

            msgGrid.DataSource = dtData;
            txtMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
            lbMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
            mainHwnd = this.Handle;
            MaxForm();

            if (!SystemInfo.ini.ExistINIFile())
            {
                IsShowPhoto = false;
                MessageBoxEx.Show(FIRSTUSE, MESSAGE);
                return;
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(ShowPhoto));

            connetDB();//连接数据库
           
            frmSet frm = new frmSet();
            frm.Connet();
           
            if (SystemInfo.isOpened)
            {
                SystemInfo.threadStop = false;
                SystemInfo.threadSendStop = false;
                SystemInfo.threadSend = true;
                dtEmpHistoryCard = db.GetDataTable("SELECT * FROM VRS_EmpHistoryCard");

                dtEmp = db.GetDataTable("SELECT EmpNo,EmpName, CardSectorNo, CardStatusID,CardPhysicsNo10 FROM VRS_Emp");
                if (dtEmp != null)
                {
                    if (dtEmp.Rows.Count == 0)
                    {
                        MessageBoxEx.Show(UNABLETOGETDATABASEPERSONNELINFO, MESSAGE);
                    }
                }
                else
                {
                    MessageBoxEx.Show(UNABLETOGETDATABASEPERSONNELINFO, MESSAGE);
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadRead), 1);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadWrite), 1);
                if (SystemInfo.IsAutoXF)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(AutoXF), 1);
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadReadCard), 1);
                }
            }
            else
            {
                dtData.Rows.Clear();
                txtName.Tag = "";
                txtName.Text = "";
                txtCard.Text = "";
                txtMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
                lbMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
                lbCardCount.Text = "0";
                panelConsumptionType.TextX = "IC CARD";
            }
        }

        /// <summary>
        /// 测试连接数据库
        /// </summary>
        private void connetDB()
        {
            string MSSQLServer = ".";
            bool WindowsNT = true;
            string MSSQLUserName = "";
            string MSSQLUserPass = "";
            string DBName = "";
            int index = 0;
            OleDbConnection oleConn = null;
            SqlConnection sqlConn  = null;
            switch (SystemInfo.DBType)
            {
                case 1:
                    MSSQLServer = SystemInfo.ini.IniReadValue("MSSQL", "Server", ".");
                    WindowsNT = Convert.ToBoolean(SystemInfo.ini.IniReadValue("MSSQL", "WindowsNT", "true"));

                    MSSQLUserName = SystemInfo.ini.IniReadValue("MSSQL", "UserName", "");
                    MSSQLUserPass = SystemInfo.ini.IniReadValue("MSSQL", "UserPass", "");
                    DBName = SystemInfo.ini.IniReadValue("MSSQL", "DBName", "");
                    break;
                case 2:
                    MSSQLServer = SystemInfo.ini.IniReadValue("MSDE", "Server", ".");
                    WindowsNT = Convert.ToBoolean(SystemInfo.ini.IniReadValue("MSDE", "WindowsNT", "true"));
                    MSSQLUserName = SystemInfo.ini.IniReadValue("MSDE", "UserName", "");
                    MSSQLUserPass = SystemInfo.ini.IniReadValue("MSDE", "UserPass", "");
                    DBName = SystemInfo.ini.IniReadValue("MSDE", "DBName", "");
                    break;
            }

            SystemInfo.ConnStr = GetMSSQLConnStr(MSSQLServer, WindowsNT, MSSQLUserName, MSSQLUserPass, DBName);
            while (true)
            {
                Thread.Sleep(1000);
                
                try
                {
                    if (SystemInfo.DBType == 1)
                    {
                        using (sqlConn = new SqlConnection(SystemInfo.ConnStr))
                        {
                            sqlConn.Open();
                            Thread.Sleep(1000);
                            IsShowPhoto = false;
                        }
                        return;
                    }
                    else
                    {
                        using (oleConn = new OleDbConnection(SystemInfo.ConnStr))
                        {
                            oleConn.Open();
                            Thread.Sleep(1000);
                            IsShowPhoto = false;
                        }
                        return;
                    }
                }
                catch
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
                    index++;
                    if (index > 3)
                    {
                        IsShowPhoto = false;
                        return;
                    } 
                    Thread.Sleep(5000);
                    continue;
                }
                finally
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
            }
        }
        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="WindowsNT"></param>
        /// <param name="UserName"></param>
        /// <param name="UserPass"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public string GetMSSQLConnStr(string ServerName, bool WindowsNT, string UserName, string UserPass, string DBName)
        {
            if (WindowsNT)
                return string.Format("Trusted_Connection=true;Server={0};Database={1};Pooling=False", ServerName, DBName);
            else
                return string.Format("Trusted_Connection=false;Server={0};Database={1};uid={2};pwd={3};Pooling=False",
                  ServerName, DBName, UserName, UserPass);
        }

        /// <summary>
        /// 显示初始画面
        /// </summary>
        /// <param name="state"></param>
        private void ShowPhoto(object state)
        {
            frmPhoto frm = new frmPhoto();
            IsShowPhoto = true;
            frm.Show();
            Application.DoEvents();
            Thread.Sleep(5000);
            while(true)
            {
                if (!IsShowPhoto) break;
                Thread.Sleep(1000);
            }
            frm.Close();
            Application.DoEvents();
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object>(s =>
                {
                    this.TopMost = true;
                    Thread.Sleep(1000);
                    this.TopMost = false;
                }), 0);
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="obj"></param>
        private void ThreadWrite(object obj)
        {

            while (SystemInfo.serialPort.IsOpen)
            {
                if (SystemInfo.IsWorking || !IsfrmtiteClose)
                {
                    Thread.Sleep(100);
                    continue;
                }
                if (SystemInfo.threadSendStop)
                {
                    SystemInfo.threadSendStop = false;
                    break;
                }

                if (SystemInfo.threadSend)
                {
                    string cmdStr = "D1 89 31 06 36 56";
                    byte[] sendData = HexStringToByteArray(cmdStr);
                    SystemInfo.isHand = true;
                    SystemInfo.serialPort.Write(sendData, 0, sendData.Length);
                    Thread.Sleep(500);
                }
            }

        }

        /// <summary>
        /// 读取字节数据
        /// </summary>
        private void ThreadRead(Object obj)
        {
            string category = "";
            string varieties = "";
            //string money = "";
            try
            {
                while (SystemInfo.serialPort.IsOpen)
                {
                    if (SystemInfo.IsWorking || !IsfrmtiteClose)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    Thread.Sleep(100);

                    string readData = "";
                    if (!SystemInfo.serialPort.IsOpen)
                        break;
                    int index = SystemInfo.serialPort.BytesToRead;

                    if (SystemInfo.threadStop && index == 0)
                    {
                        SystemInfo.threadStop = false;
                        break;
                    }

                    #region 字节读取

                    if (index > 0)
                    {
                        SystemInfo.readCardCount = 0;
                        byte[] readBuffer = new byte[index];
                        SystemInfo.serialPort.Read(readBuffer, 0, index);
                        readData = byteToHexStr(readBuffer);
                        if (readData.IndexOf("5AA58000") >= 0)
                        {
                            //MessageBoxEx.Show("接受指令通讯包分析错误！", "提示");
                            string m = "接受指令通讯包分析错误！";
                            if (panelConsumptionType.InvokeRequired)
                            {
                                panelConsumptionType.Invoke(new Action<String>(s => {

                                    panelConsumptionType.TextX = s;

                                }), m);
                            }
                            else
                            {
                                panelConsumptionType.TextX = m;
                            }
                            return;
                        }
                        else if (readData.IndexOf("5AA58100") >= 0)
                        {
                            //MessageBoxEx.Show("连接设备错误！", "提示");
                            string m = "连接设备错误！";
                            if (panelConsumptionType.InvokeRequired)
                            {
                                panelConsumptionType.Invoke(new Action<String>(s => {

                                    panelConsumptionType.TextX = s;

                                }), m);
                            }
                            else
                            {
                                panelConsumptionType.TextX = m;
                            }
                            return;
                        }
                        else if (readData.IndexOf("5AA58200") >= 0)
                        {
                            //MessageBoxEx.Show("固件和软件版本错误！", "提示");
                            string m = "固件和软件版本错误！";
                            if (panelConsumptionType.InvokeRequired)
                            {
                                panelConsumptionType.Invoke(new Action<String>(s => {

                                    panelConsumptionType.TextX = s;

                                }), m);
                            }
                            else
                            {
                                panelConsumptionType.TextX = m;
                            }
                            return;
                        }

                        if (SystemInfo.isHand && readData.Length > 3)
                        {
                            if (readData.IndexOf("02") != 0)
                                continue;
                            SystemInfo.readCardNumber = readData.Substring(2, 2);
                            if (SystemInfo.readCardNumber == "00")
                            {
                                outText("2," + "0");
                                continue;
                            }
                            SystemInfo.readCardIndex = Convert.ToInt32(SystemInfo.readCardNumber, 16);
                            SystemInfo.cardData = "";
                            SystemInfo.threadSend = false;
                            SystemInfo.isHand = false;
                        }

                        SystemInfo.cardData = SystemInfo.cardData + readData;
                        if (SystemInfo.cardData.Length > SystemInfo.readCardIndex * 40 && SystemInfo.cardData.Substring(SystemInfo.cardData.Length - 2, 2) == "03")
                        {
                            SystemInfo.threadSend = true;
                            for (int i = 0; i < SystemInfo.readCardIndex; i++)
                            {
                                outText("2," + SystemInfo.readCardIndex);
                                string cardNo = "";
                                cardNo = SystemInfo.cardData.Substring(6 + 40 * i, 16);
                                cardNo = getCardNo(cardNo);
                                // cardList.Add(cardNo);
                                //outText(cardNo + ",1");
                                varieties = SystemInfo.cardData.Substring(6 + 16 + 40 * i, 8);
                                category = SystemInfo.cardData.Substring(6 + 24 + 40 * i, 8);
                                // string money = cardData.Substring(6 + 36 + 40 * i, 4);
                                // int num = Convert.ToInt32(money, 16);
                                // double moneys = (double)num / 100;
                                int Cat = IsNum(category);
                                int Var = IsNum(varieties);

                                if (!SystemInfo.isWrite)
                                    outText("1," + Var + "," + Cat + "," + cardNo);
                            }
                        }
                    }
                    else
                    {
                        SystemInfo.threadSend = true;
                    }
                    #endregion
                }
            }
            catch(Exception E)
            {
                MessageBoxEx.Show(E.Message);
            }
        
        }
        /// <summary>
        /// 转换数字字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int IsNum(string num)
        {
            int ret = 0;
            try
            {
                ret = int.Parse(num, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        /// <summary>
        /// 委托加数据到DATAGRID
        /// </summary>
        /// <param name="text"></param>
        private void outText(string text)
        {
            if (msgGrid.InvokeRequired)
            {
                OutDelegate outDelegate = new OutDelegate(outText);
                this.BeginInvoke(outDelegate, new object[] { text });
                return;
            }
            string[] tmp = new string[0];
            tmp = text.Split(',');
            switch (tmp[0])
            {
                case "1":
                    RefreshMacMsg(text);
                    break;
                case "2":
                    if (Convert.ToInt32(tmp[1]) < dtData.Rows.Count)
                    {
                        dtData.Rows.Clear();
                        Thread.Sleep(100);
                        lbMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
                        lbCardCount.Text = 0.ToString();
                    }
                    break;
            }

        }
        /// <summary>
        /// 添加数据到列表
        /// </summary>
        /// <param name="msg"></param>
        private void RefreshMacMsg(string msg)
        {
            Moneys = 0.00;
           
            if (msg != "")
            {
                string[] tmp = new string[0];
                tmp = msg.Split(',');
                //msgGrid.Rows.Add();
                //msgGrid[0, msgGrid.RowCount - 1].Value = msg;
                int category = 0;
                int varietie = 0;
                int.TryParse(tmp[2], out category);
                int.TryParse(tmp[1], out varietie);
                DataRow[] dataRow = dtData.Select("category='" + category + "' and varietiesid='" + varietie + "' and cardNo='" + tmp[3] + "'");
                if (dataRow.Length == 0)
                {
                    string sql = "select * from [SF_MacProducts] where [CategoryID]=" + category + " and [ProductsID]=" + varietie;
                    DataTableReader dr = db.GetDataReader(sql);
                    if (dr.Read())
                    {
                        string varieties = dr["ProductsName"].ToString();
                        string money = SystemInfo.moneyStr + (Convert.ToDouble(dr["ProductsPrice"])).ToString("0.00");

                        dtData.Rows.Add(new object[] { varieties, money, varietie, category, tmp[3] });

                        msgGrid.Rows[msgGrid.RowCount - 1].Selected = true;
                        msgGrid.CurrentCell = msgGrid.Rows[msgGrid.RowCount - 1].Cells[0];
                        
                    }
                }

            }
            for (int i = 0; i < msgGrid.RowCount; i++)
            {
                Moneys += Convert.ToDouble(msgGrid[1, i].Value.ToString().Substring(SystemInfo.moneyStr.Length));
            }
            lbMoney.Text = SystemInfo.moneyStr + Moneys.ToString("0.00");
            lbCardCount.Text = msgGrid.Rows.Count.ToString();
           
        }
        /// <summary>
        /// 定时器，用于定时释放内存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy/MM/dd  hh:mm:ss");
            if (SystemInfo.IsthreadSend)
            {
                SystemInfo.ReThreadCount++;
                if (SystemInfo.ReThreadCount >= 240)
                {
                    if (SystemInfo.ReThreadCount == 240)
                    {
                        SystemInfo.threadStop = true;
                        SystemInfo.threadSendStop = true;
                        SystemInfo.threadSend = false;
                    }
                    if (!SystemInfo.threadSendStop && !SystemInfo.threadStop)
                    {
                        RemoveAllCache();
                        SystemInfo.threadSend = true;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadRead), 1);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadWrite), 1);
                        SystemInfo.ReThreadCount = 0;
                    }
                }
                if (SystemInfo.IsAutoPeriod)
                {
                    int t = 0;
                    int t1 = 0;
                    int t2 = 0;
                    DateTime tt;
                    for (int i = 0; i < 3; i++)
                    {
                        t = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
                        tt = Convert.ToDateTime(paramInfo.BeginTime[i]);
                        t1 = tt.Hour * 60 + tt.Minute;
                        tt = Convert.ToDateTime(paramInfo.EndTime[i]);
                        t2 = tt.Hour * 60 + tt.Minute;
                        if ((t >= t1) && (t < t2))
                        {
                            SystemInfo.MealType = SystemInfo.MealTypeList[i];
                            break;
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 窗体大小发生改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Resize(object sender, EventArgs e)
        {
            resize();
        }
        /// <summary>
        /// 自适应控件
        /// </summary>
        private void resize()
        {
            labelX6.Font = new Font("Times New Roman", this.Width / 50F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            labelX1.Font = new Font("Times New Roman", this.Width / 70F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            labelX4.Font = new Font("Times New Roman", this.Width / 70F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            labelX5.Font = new Font("Times New Roman", this.Width / 70F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));

            txtName.Font = new Font("Times New Roman", this.Width / 80F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            txtCard.Font = new Font("Times New Roman", this.Width / 80F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            txtMoney.Font = new Font("Times New Roman", this.Width / 80F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));

            panelConsumptionType.Font = new Font("Times New Roman", this.Width / 80F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            lbMoney.Font = new Font("Times New Roman", this.Width / 40F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            labelX2.Font = new Font("Times New Roman", this.Width / 40F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));

            lbCardCount.Font = new Font("Times New Roman", this.Width / 40F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            labelX3.Font = new Font("Times New Roman", this.Width / 40F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));

            lbTime.Font = new Font("Times New Roman", this.Width / 50F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            PanelInfo.Font = new Font("Times New Roman", this.Width / 80F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));

            
            exPanelCount.Font = new Font("Times New Roman", this.Width / 60F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            exPanelMoney.Font = new Font("Times New Roman", this.Width / 60F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));

            btnExit.Width = this.Width / 12;
            btnExit.Height = this.Height / 12;
            btnSet.Width = this.Width / 12;
            btnSet.Height = this.Height / 12;
            btnCardPay.Width = this.Width / 12;
            btnCardPay.Height = this.Height / 12;

            btnWPay.Width = this.Width / 12;
            btnWPay.Height = this.Height / 12;

            picTime.Width = this.Width / 12;
            picTime.Height = this.Height / 12;
            PanelInfo.Width = this.Width / 5 * 2;
            PanelInfo.Height = this.Height / 5 * 3;
            panelConsumptionType.Width = this.Width / 12 * 3;
            panelConsumptionType.Height = this.Height / 12 * 3;

            labelX1.Width = this.Width / 50 * 3;
            labelX1.Height = this.Height / 68 * 3;

            labelX4.Width = this.Width / 50 * 3;
            labelX4.Height = this.Height / 68 * 3;

            labelX5.Width = this.Width / 50 * 3;
            labelX5.Height = this.Height / 68 * 3;

            labelX5.Width = this.Width / 50 * 3;
            labelX5.Height = this.Height / 68 * 3;

            txtName.Width = this.Width / 17 * 3;
            txtName.Height = this.Height / 68 * 3;

            txtCard.Width = this.Width / 17 * 3;
            txtCard.Height = this.Height / 68 * 3;

            txtMoney.Width = this.Width / 17 * 3;
            txtMoney.Height = this.Height / 68 * 3;

            exPanelCount.Width = this.Width / 12 * 3;
            exPanelCount.Height = this.Height / 10 * 3;
            exPanelMoney.Width = this.Width / 12 * 3;
            exPanelMoney.Height = this.Height / 10 * 3;
            exPanelMoney.HeadHeight = exPanelMoney.Height / 3;
            exPanelCount.HeadHeight = exPanelCount.Height / 3;

            lbMoney.Location = new Point(panelConsumptionType.Width / 8, panelConsumptionType.Height / 2);
            labelX2.Location = new Point(panelConsumptionType.Width / 8 * 5, panelConsumptionType.Height / 2);

            lbCardCount.Location = new Point(panelConsumptionType.Width / 8, panelConsumptionType.Height / 2);
            labelX3.Location = new Point(panelConsumptionType.Width / 8 * 5, panelConsumptionType.Height / 2);

            PanelInfo.Location = new Point(this.Width / 40, this.Height / 5);
            panelConsumptionType.Location = new Point(this.Width - panelConsumptionType.Width - panelConsumptionType.Width / 10, this.Height / 5);
            exPanelCount.Location = new Point(this.Width - panelConsumptionType.Width - panelConsumptionType.Width / 10, this.Height / 16 * 8);
            exPanelMoney.Location = new Point(this.Width - 2 * panelConsumptionType.Width - 2 * panelConsumptionType.Width / 10, this.Height / 16 * 8);

            labelX1.Location = new Point(this.Width - 2 * panelConsumptionType.Width - 2 * panelConsumptionType.Width / 10, this.Height / 5 + panelConsumptionType.Height / 14);
            labelX4.Location = new Point(this.Width - 2 * panelConsumptionType.Width - 2 * panelConsumptionType.Width / 10, this.Height / 5 + panelConsumptionType.Height / 14 * 6);
            labelX5.Location = new Point(this.Width - 2 * panelConsumptionType.Width - 2 * panelConsumptionType.Width / 10, this.Height / 5 + panelConsumptionType.Height / 14 * 11);

            txtName.Location = new Point(this.Width - 21 * panelConsumptionType.Width / 11, this.Height / 5 + panelConsumptionType.Height / 14);
            txtCard.Location = new Point(this.Width - 21 * panelConsumptionType.Width / 11, this.Height / 5 + panelConsumptionType.Height / 14 * 6);
            txtMoney.Location = new Point(this.Width - 21 * panelConsumptionType.Width / 11, this.Height / 5 + panelConsumptionType.Height / 14 * 11);

            labelX6.Location = new Point(this.Width / 40 + PanelInfo.Width / 2 - labelX6.Width / 2, this.Height - this.Height / 30 * 5);

            picTime.Location = new Point(this.Width / 3, this.Height / 30);
            lbTime.Location = new Point(this.Width / 3 + picTime.Width, this.Height / 30 + picTime.Height / 5);
            btnExit.Location = new Point(this.Width - this.Width / 12, this.Height / 30);
            btnSet.Location = new Point(this.Width - this.Width / 12 * 2, this.Height / 30);
            btnCardPay.Location = new Point(this.Width - this.Width / 12 * 2, this.Height - this.Height / 30 * 5);

            btnWPay.Location = new Point(this.Width - this.Width / 12 * 3, this.Height - this.Height / 30 * 5);

            msgGrid.RowTemplate.Height = msgGrid.Height / 8;
            picTitle.Height = this.Height / 16;
            if(msgGrid.Columns.Count>3)
            {
                msgGrid.Columns[1].Width = msgGrid.Width / 2 - 10;
                msgGrid.Columns[3].Width = msgGrid.Width / 2 - 10;
            }
            Application.DoEvents();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
        /// <summary>
        /// 移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void panTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove && e.Button == MouseButtons.Left)
            {
                int lx = MousePosition.X - currentXPosition;
                int ty = MousePosition.Y - currentYPosition;
                if (Math.Abs(lx) > 10 || Math.Abs(ty) > 10)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }
                    this.Left += lx;//根据鼠标x坐标确定窗体的左边坐标x
                    this.Top += ty;//根据鼠标的y坐标窗体的顶部，即Y坐标
                    currentXPosition = MousePosition.X;
                    currentYPosition = MousePosition.Y;
                }

            }
        }

        private void panTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }
        }

        private void picTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MaxForm();
        }
        /// <summary>
        /// 窗体最大化
        /// </summary>
        private void MaxForm()
        {
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            this.Left = rect.Left;
            this.Top = rect.Top;
            this.Width = rect.Width;
            this.Height = rect.Height;
        }
        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            dtData.Rows.Clear();
            cardList.Clear();
            SystemInfo.threadStop = true;
            SystemInfo.threadSendStop = true;
            SystemInfo.threadSend = false;
            
            frmSet frm = new frmSet();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                if (SystemInfo.isOpened)
                {
                    SystemInfo.threadStop = false;
                    SystemInfo.threadSendStop = false;
                    SystemInfo.threadSend = true;
                    dtEmpHistoryCard = db.GetDataTable("SELECT * FROM VRS_EmpHistoryCard");

                    dtEmp = db.GetDataTable("SELECT EmpNo,EmpName, CardSectorNo, CardStatusID,CardPhysicsNo10 FROM VRS_Emp");
                    if (dtEmp != null)
                    {
                        if (dtEmp.Rows.Count == 0)
                        {
                            MessageBoxEx.Show(UNABLETOGETDATABASEPERSONNELINFO, MESSAGE);
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show(UNABLETOGETDATABASEPERSONNELINFO, MESSAGE);
                    }
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadRead), 1);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadWrite), 1);
                    if(SystemInfo.IsAutoXF)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(AutoXF), 1);
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadReadCard), 1);
                    }
                }
                else
                {
                    dtData.Rows.Clear();
                    txtName.Tag = "";
                    txtName.Text = "";
                    txtCard.Text = "";
                    txtMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
                    lbMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
                    lbCardCount.Text = "0";
                     panelConsumptionType.TextX = "IC CARD";
                }
            }
            SystemInfo.IsWorking = false;
            IsfrmtiteClose = true;
        }

        private void msgGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="obj"></param>
        private void ThreadReadCard(object obj)
        {
            string CardData10 = "";
            while (SystemInfo.serialPort.IsOpen)
            {
                if (SystemInfo.IsWorking || !IsfrmtiteClose) 
                    continue;
                if(dtEmp!=null)
                {
                    if(dtEmp.Rows.Count>0)
                        ReadCard(ref CardData10);
                }  
            }

        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="obj"></param>
        private void ThreadFindCard(object obj)
        {
            string CardData10 = "";
            string CardDataH = "";
            string CardData8 = "";
            while (SystemInfo.serialPort.IsOpen)
            {
                if (IsfrmtiteClose)
                {
                    return;
                }
                if (SystemInfo.IsWorking) continue;
                CheckCardExists(ref CardData10, ref CardDataH, ref CardData8);
            }

        }
        /// <summary>
        /// 获取品种信息字符串
        /// </summary>
        /// <returns></returns>
        private string GetProduct()
        {
            string ret = "";
            ProductInfo = new string[10];
            ID = new int[10];
            Num = new int[10];
            Price = new Double[10];

            for (int i = 0; i < msgGrid.RowCount; i++)
            {
                if (i == 0)
                {
                    ProdCategory = Convert.ToByte(msgGrid[3, 0].Value.ToString());
                }
                if (ProdCategory != Convert.ToByte(msgGrid[3, i].Value.ToString()))
                {
                    MessageBoxEx.Show(ONLYONECATEGORYISALLOWED, MESSAGE);
                    return ret;
                }

                for (int j = 0; j < 10; j++)
                {
                    if (ID[j] == 0)
                    {
                        ID[j] = Convert.ToInt32(msgGrid[2, i].Value);
                        Price[j] = Convert.ToDouble(msgGrid[1, i].Value.ToString().Substring(SystemInfo.moneyStr.Length));
                        ProductInfo[j] = msgGrid[0, i].Value.ToString();
                        Num[j]++;
                        break;
                    }
                    else if (ID[j] == Convert.ToInt32(msgGrid[2, i].Value))
                    {
                        Num[j]++;
                        break;
                    }
                }

            }

            ret = ProdCategory.ToString("0000");
            for (int i = 0; i < 10; i++)
            {
                string[] msg = Num[i].ToString("0.00").Split('.');
                string[] tmp = Price[i].ToString("0.00").Split('.');
                ret += ID[i].ToString("0000") + Convert.ToInt32(msg[0]).ToString("000000") + Convert.ToInt32(msg[1]).ToString("00") + Convert.ToInt32(tmp[0]).ToString("00000") + "." + Convert.ToInt32(tmp[1]).ToString("00");
            }
            return ret;
        }
        /// <summary>
        /// 刷卡消费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCardPay_Click(object sender, EventArgs e)
        {
            string CardData10 = "";
            string CardDataH = "";
            string CardData8 = "";
            if (SystemInfo.IsWorking || !IsfrmtiteClose) return;
            SystemInfo.IsWorking = true;
            if (CheckCardExists(ref CardData10, ref CardDataH, ref CardData8))
            {
                WriteCard(CardData10,CardDataH, CardData8);
            } 
            SystemInfo.IsWorking = false;
        }
        /// <summary>
        /// 写卡
        /// </summary>
        /// <param name="CardData10"></param>
        /// <param name="CardDataH"></param>
        /// <param name="CardData8"></param>
        private void WriteCard(string CardData10,string CardDataH,string CardData8)
        {
            btnCardPay.Enabled = false;
            btnWPay.Enabled = false;
            double money = 0;
            double mmCZ = 0;
            double mmBT = 0;
           
            try
            {
                //DateTime start = DateTime.Now;
                //string ExecTimes = "";
                if (!ReadCard(CardDataH, CardData8, ref CardData10))
                {
                    return;
                }
                //ExecTimes += "    " + GetDateDiffTimes(start, DateTime.Now);
                //start = DateTime.Now;
                string Product = GetProduct();
                if (Product == "") return;
                double.TryParse(CurrencyToStringEx(lbMoney.Text), out money);
                if (money <= 0)
                {
                    string m = "消费金额要大于0";
                    if (panelConsumptionType.InvokeRequired)
                    {
                        panelConsumptionType.Invoke(new Action<String>(s => {

                            panelConsumptionType.TextX = s;

                        }), m);
                    }
                    else
                    {
                        panelConsumptionType.TextX = m;
                    }
                    return;
                }
                DateTime dt = new DateTime();
                if (!db.GetServerDate(this.Text, ref dt)) return;
                // if (!CheckUseDate(dt, sfData.UseDate)) return;
                ClearCardLimitInfo(dt, ref sfData);
                string OpterStartDate = "";
                string OpterEndDate = "";
                DateTime StartDt = new DateTime();
                DateTime EndDt = new DateTime();
                DataRow[] dataRow = dtEmpHistoryCard.Select("CardSectorNo = '" + pubData.CardNo + "'");

                for (int x = 0; x < dataRow.Length; x++)
                {
                    OpterStartDate = "";
                    OpterEndDate = "";
                    OpterStartDate = dataRow[x]["OpterStartDate"].ToString();
                    OpterEndDate = dataRow[x]["OpterEndDate"].ToString();
                    if (OpterStartDate != "")
                    {
                        StartDt = Convert.ToDateTime(OpterStartDate);
                    }
                    if (OpterEndDate != "")
                    {
                        EndDt = Convert.ToDateTime(OpterEndDate);
                    }
                    if (OpterStartDate != "" && OpterEndDate != "")
                    {
                        if (dt > StartDt && dt < EndDt)
                        {
                            break;
                        }
                    }
                    else if (OpterStartDate != "" && OpterEndDate == "")
                    {
                        if (dt > StartDt)
                        {
                            OpterEndDate = dt.ToString(SystemInfo.SQLDateTimeFMT);
                            break;
                        }
                    }

                }
                if (OpterStartDate != "")
                    OpterStartDate = Convert.ToDateTime(OpterStartDate).ToString(SystemInfo.SQLDateTimeFMT);
                if (OpterEndDate != "")
                    OpterEndDate = Convert.ToDateTime(OpterEndDate).ToString(SystemInfo.SQLDateTimeFMT);

                double ShowBTMoney = db.GetBTMoney(sfData.BtDate, sfData.BtMonery);
                double AllBalance = sfData.Balance + ShowBTMoney;

                txtMoney.Text = (db.GetBTMoney(sfData.BtDate, sfData.BtMonery) + sfData.Balance).ToString(SystemInfo.moneyStr + "0.00");
                string sql = "";

                string cap = "结算台刷卡消费";
                string SFType = "3";
                byte flag = 0;
                if (SFType == "2") flag = 4;
                string MealType = SystemInfo.MealType;
                if (MealType == "" || MealType == null) MealType = "NULL";
                bool DecMoney = (SFType == "1") || (SFType == "3");
                if (DecMoney)
                {
                    if (AllBalance < money)
                    {
                        string m = "卡内余额不足！";
                        if (panelConsumptionType.InvokeRequired)
                        {
                            panelConsumptionType.Invoke(new Action<String>(s => {

                                panelConsumptionType.TextX = s;

                            }), m);
                        }
                        else
                        {
                            panelConsumptionType.TextX = m;
                        }
                        return;
                    }
                }
                sfData.UseDate = dt;
                #region
                //if ((ShowBTMoney == 0) && (sfData.BtMonery > 0))
                //{
                //    sfData.UseTimes += 1;
                //    double m = -sfData.BtMonery;
                //    sql.Add("EXEC PSF_SFDataInsert '" + pubData.CardNo + "'," + "0" + ",'" + dt.ToString(SystemInfo.SQLDateTimeFMT) + "'," +
                //              "0" + "," + sfData.Balance.ToString() + "," + "0" + ",'" + MealType + "'," + "0" + "," +
                //              sfData.UseTimes.ToString() + ",'" + OprtInfo.OprtSysID + "','" + Product + "'," + "1" + ",NULL" + ",'" +
                //              CardData10 + "','" + pubData.MacTAG + "','" + "" + "'," + m.ToString() + "," +
                //              "" + "0" + "," + "0" + ",'" + OpterStartDate + "','" + OpterEndDate + "',NULL,NULL,NULL");
                //    sfData.BtMonery = 0;
                //}
                //else
                //{
                //    if (DecMoney)
                //    {
                //        if (SystemInfo.IsUseBTBalance)
                //        {
                //            sfData.UseTimes += 1;
                //            double oldBT = sfData.BtMonery;
                //            sfData.BtMonery -= money;
                //            mmBT = -money;
                //            if (sfData.BtMonery < 0)
                //            {
                //                sfData.Balance += sfData.BtMonery;
                //                mmBT = -oldBT;
                //                mmCZ = -(money - oldBT);
                //                sfData.BtMonery = 0;
                //            }

                //        }
                //        else
                //        {
                //            sfData.UseTimes += 1;
                //            double oldCZ = sfData.Balance;
                //            sfData.Balance -= money;
                //            mmCZ = -money;
                //            if (sfData.Balance < 0)
                //            {
                //                sfData.BtMonery += sfData.Balance;
                //                mmCZ = -oldCZ;
                //                mmBT = -(money - oldCZ);
                //                sfData.Balance = 0;
                //            }
                //        }
                //        AllBalance -= money;
                //    }
                //    else if (flag == 4)
                //    {
                //        mmCZ = -money;
                //    }
                //}
                #endregion
                if (DecMoney)
                {
                    if (SystemInfo.IsUseBTBalance)
                    {
                        sfData.UseTimes += 1;
                        double oldBT = sfData.BtMonery;
                        sfData.BtMonery -= money;
                        mmBT = -money;
                        if (sfData.BtMonery < 0)
                        {
                            sfData.Balance += sfData.BtMonery;
                            mmBT = -oldBT;
                            mmCZ = -(money - oldBT);
                            sfData.BtMonery = 0;
                        }

                    }
                    else
                    {
                        sfData.UseTimes += 1;
                        double oldCZ = sfData.Balance;
                        sfData.Balance -= money;
                        mmCZ = -money;
                        if (sfData.Balance < 0)
                        {
                            sfData.BtMonery += sfData.Balance;
                            mmCZ = -oldCZ;
                            mmBT = -(money - oldCZ);
                            sfData.Balance = 0;
                        }
                    }
                    AllBalance -= money;
                }
                else if (flag == 4)
                {
                    mmCZ = -money;
                }
                sql = "EXEC PSF_SFDataInsert '" + pubData.CardNo + "'," + SFType + ",'" + dt.ToString(SystemInfo.SQLDateTimeFMT) + "'," +
                              mmCZ.ToString() + "," + sfData.Balance.ToString() + "," + MealType + ",'" + SystemInfo.DBMacSN + "'," + "0" + "," +
                             sfData.UseTimes.ToString() + ",'" + OprtInfo.OprtSysID + "','" + Product + "'," + "1" + ",NULL" + ",'" +
                             CardData10 + "','" + pubData.MacTAG + "','" + "" + "'," + mmBT.ToString() + "," +
                             "" + sfData.BtMonery + "," + "0" + ",'" + OpterStartDate + "','" + OpterEndDate + "',NULL,NULL,NULL";
                string Title = cap;
                double Amount = money;
                double ReceivablesAmount = money;
                double CardBalance = AllBalance;
                if (db.ExecSQL(sql) < 1) return;
                int cardRet = 0;
                string CardNo10 = "";
                string CardNoH = "";
                string CardNo8 = "";
                bool IsSFError = false;
                string msg = txtName.Text + ": " + txtCard.Text;
            ContinueSF:
                if (IsSFError)
                {
                    if (!CheckCardExists(ref CardNo10, ref CardNoH, ref CardNo8, false))
                    {
                        string m = "读取卡片内容失败，请检查卡片是否放好";
                        if (panelConsumptionType.InvokeRequired)
                        {
                            panelConsumptionType.Invoke(new Action<String>(s => {

                                panelConsumptionType.TextX = s;

                            }), m);
                        }
                        else
                        {
                            panelConsumptionType.TextX = m;
                        }
                        goto ContinueSF;
                    }
                    if (CardNo10 != CardData10)
                    {
                        if (OkContinue)
                        {
                            MessageBoxEx.Show("请放同一张卡继续？" + "\r\n\r\n检查发卡机连接、正确放好卡片后，点[确定]继续。", MESSAGE);

                            goto ContinueSF;
                        }
                        else
                        {
                            if (db.MessageBoxShowQuestion("请放同一张卡继续？"))
                                return;
                            else
                                goto ContinueSF;
                        }
                    }
                    IsSFError = false;
                }

                cardRet = WriteCardInfo(sfData);
                if (cardRet != 0)
                {
                    if (OkContinue)
                    {
                        MessageBoxEx.Show(GetCardMsg(cardRet) + "\r\n\r\n检查发卡机连接、正确放好卡片后，点[确定]继续。", MESSAGE);

                        IsSFError = true;
                        goto ContinueSF;
                    }
                    else
                    {
                        if (db.MessageBoxShowQuestion(GetCardMsg(cardRet) + "是否继续？"))
                            return;
                        else
                        {
                            IsSFError = true;
                            goto ContinueSF;
                        }
                    }
                }

                double temp = sfData.Balance + db.GetBTMoney(sfData.BtDate, sfData.BtMonery);
                ThreadPool.QueueUserWorkItem(new WaitCallback(SoundPlay), money);

                txtMoney.Text = SystemInfo.moneyStr + temp.ToString("0.00");
                msg = msg + string.Format("[{0:f2},{1:f2}]", Amount, temp);
                db.WriteSYLog(this.Text, "结算台刷卡消费", msg);
                CardBuzzer();
                DispExtScreen(Amount, temp, 1, flag);
                //ExecTimes += "    " + GetDateDiffTimes(start, DateTime.Now);
                SFDataTime = dt.ToString(SystemInfo.SQLDateTimeFMT);
                SettlementType = (int)PayType.CardType;
                PrintCardBill();

                IsfrmtiteClose = false;

                string Name = "姓名：" + txtName.Text + "";
                string Card = "卡号：" + txtCard.Text + "";
                string Money = "" + SystemInfo.moneyStr + money.ToString("0.00") + "";
                string Balance = "余额：" + SystemInfo.moneyStr + temp.ToString("0.00");

                frmtite = new frmTite(Name, Card, Money, Balance);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadFindCard), 1);  //启动线程检测卡是否拿开
                frmtite.ShowDialog();
            }
            catch
            {

            }
            finally
            {
                btnCardPay.Enabled = true;
                btnWPay.Enabled = true;
            }
        }

        /// <summary>
        /// 打印机
        /// </summary>
        private void PrintCardBill()
        {
            //this.printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 200, 300);
            ////将写好的格式给打印预览控件以便预览
            //printPreviewDialog.Document = printDocument;
            ////显示打印预览
            //DialogResult result = printPreviewDialog.ShowDialog();
            //if (result == DialogResult.OK)
            if (SystemInfo.IsPrint && CheckPrinter())
            {
                printDocument.DefaultPageSettings.PaperSize = null;
                printDocument.Print();
            }     
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            StringBuilder name = new StringBuilder();

            #region 取流水号
            string dayRunNum = SystemInfo.ini.IniReadValue("Public", "dayRunNum", "1@1");

            string[] RunNum = dayRunNum.Split('@');

            string day = DateTime.Now.Day.ToString();
            if (day == RunNum[0])
            {
                RunNum[1] = (Convert.ToInt32(RunNum[1]) + 1).ToString();
            }
            else
            {
                RunNum[1] = "1";
            }

            SystemInfo.ini.IniWriteValue("Public", "dayRunNum", day + "@" + RunNum[1]);
            #endregion


            /*如果需要改变自己 可以在new Font(new FontFamily("黑体"),11）中的“黑体”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
            System.Drawing.Brushes.Blue , 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置 */
            printX = 0;
            printY = 10;
            e.Graphics.DrawString("******************************", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
            printX += 30;
            printY = 50;
            e.Graphics.DrawString("结算台消费  " + "["+ Convert.ToInt32(RunNum[1])+"]", new Font(new FontFamily("黑体"), 11), System.Drawing.Brushes.Black, printY, printX);
            printX += 30;
            printY = 10;
            switch (SettlementType)
            {
                case (int)PayType.CardType:
                    e.Graphics.DrawString("人员编号:   " + txtName.Tag, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
                    printX += 20;
                    e.Graphics.DrawString("人员姓名:   " + txtName.Text, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
                    printX += 20;
                    e.Graphics.DrawString("卡    号:   " + txtCard.Text, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
                    printX += 20;
                    e.Graphics.DrawString("金    额:   " + lbMoney.Text, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
                    printX += 20;
                    e.Graphics.DrawString("余    额:   " + txtMoney.Text, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
                    break;

                case (int)PayType.SpayType:
                    e.Graphics.DrawString("类    型:   " + ZWPayType, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
                    printX += 20;
                    e.Graphics.DrawString("金    额:   " + lbMoney.Text, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
                    break;
            }

          
            printX += 30;
            printY = 50;
            e.Graphics.DrawString("菜  品", new Font(new FontFamily("黑体"), 11), System.Drawing.Brushes.Black, printY, printX);
            printX += 30;
            printY = 10;

            e.Graphics.DrawString("名 称", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, printX);
            e.Graphics.DrawString("数 量", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 80, printX);
            e.Graphics.DrawString("单 价", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 130, printX);
            for (int i = 0; i < ProductInfo.Length; i++)
            {
                if(ProductInfo[i] != null)
                {
                    printX += 20;
                    name.Remove(0,name.Length);
                    name.Append(ProductInfo[i]);
                    e.Graphics.DrawString(Num[i].ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 90, printX);
                    e.Graphics.DrawString(SystemInfo.moneyStr + Price[i].ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 130, printX);
                    if (name.Length > 5)
                    {
                        e.Graphics.DrawString(name.ToString().Substring(0,5), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, printX);
                        printX += 20;
                        e.Graphics.DrawString(name.ToString().Substring(5, name.Length - 5), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, printX);
                    }
                    else
                    {
                        e.Graphics.DrawString(name.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 10, printX);
                    }
                   
                    
                }
            }

            printX += 30;
            e.Graphics.DrawString(SFDataTime, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
            printX += 20;
            e.Graphics.DrawString("备    注：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
            printX += 20;
            e.Graphics.DrawString("******************************", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
            printX += 60;
            e.Graphics.DrawString("-", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, printY, printX);
            printX += 20;

            //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 200, printX);
        }
        /// 判断是否连接打印机
        /// </summary>
        public bool CheckPrinter()
        {
            //取得默认打印机名
            PrintDocument pdoc = new PrintDocument();
            string printerName1 = pdoc.PrinterSettings.PrinterName;

            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString().ToLower();
                if (printerName.IndexOf(printerName1.ToLower()) > -1)
                {

                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {
                        return false;
                    }
                    else
                    {
                      
                        return true;
                    }
                }
            }
            return false;
        }

      
        public string GetDateDiffTimes(System.DateTime StartDate, System.DateTime EndDate)
        {
            long Milliseconds = DateDiff(DateInterval.Milliseconds, StartDate, EndDate);
            string ret = "";
            long sec = Milliseconds / 1000;
            long hour = sec / 3600;
            sec = sec % 3600;
            long minute = sec / 60;
            sec = sec % 60;
            Milliseconds = Milliseconds % 1000;
            ret = string.Format("{0}:{1}:{2}.{3}", hour, minute, sec, Milliseconds);
            return ret;
        }
        public enum DateInterval
        {
            Milliseconds, Second, Minute, Hour, Day, Week, Month, Quarter, Year
        }
        public long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case DateInterval.Milliseconds:
                    lngDateDiffValue = (long)TS.TotalMilliseconds;
                    break;
                case DateInterval.Second:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case DateInterval.Day:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case DateInterval.Week:
                    lngDateDiffValue = (long)(TS.Days / 7);
                    break;
                case DateInterval.Month:
                    lngDateDiffValue = (long)(TS.Days / 30);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (long)((TS.Days / 30) / 3);
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (long)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);
        }

        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="money"></param>
        public void SoundPlay(object money)
        {
            try
            {
                SoundPlayer sp = new SoundPlayer();
                sp.SoundLocation = Application.StartupPath + @"\sound\xf.wav";
                sp.Play();
                Thread.Sleep(1000);
                string moneyStr = ((Double)money).ToString("0.00");
                string[] moneyChar = moneyStr.Split('.');
                int index = moneyChar[0].Length;
                char[] c = new char[0];
                switch (index)
                {
                    case 1:
                        sp.SoundLocation = Application.StartupPath + @"\sound\num"+ moneyChar[0] + ".wav";
                        sp.Play();
                        break;
                    case 2:
                        c = moneyChar[0].ToCharArray();
                        sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[0] + ".wav";
                        sp.Play();
                        Thread.Sleep(500);
                        sp.SoundLocation = Application.StartupPath + @"\sound\num_shi.wav";
                        sp.Play();
                        if(int.Parse(c[1].ToString())!=0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                        }
                        break;
                    case 3:
                        c = moneyChar[0].ToCharArray();
                        sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[0] + ".wav";
                        sp.Play();
                        Thread.Sleep(500);
                        sp.SoundLocation = Application.StartupPath + @"\sound\num_bai.wav";
                        sp.Play();
                      
                        if (int.Parse(c[1].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_shi.wav";
                            sp.Play();
                        }
                        else if (int.Parse(c[2].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                        }
                        if (int.Parse(c[2].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[2] + ".wav";
                            sp.Play();
                        }
                       
                        break;
                    case 4:
                        c = moneyChar[0].ToCharArray();
                        sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[0] + ".wav";
                        sp.Play();
                        Thread.Sleep(500);
                        sp.SoundLocation = Application.StartupPath + @"\sound\num_qian.wav";
                        sp.Play();
                      
                        if (int.Parse(c[1].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_bai.wav";
                            sp.Play();
                        }
                        else if (int.Parse(c[2].ToString()) != 0 || int.Parse(c[3].ToString()) != 0 )
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                        }
                        if (int.Parse(c[2].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[2] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_shi.wav";
                            sp.Play();
                        }
                        else if (int.Parse(c[3].ToString()) != 0 && int.Parse(c[1].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[2] + ".wav";
                            sp.Play();
                        }
                        if (int.Parse(c[3].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[3] + ".wav";
                            sp.Play();
                        }
                       
                        break;
                    case 5:
                        c = moneyChar[0].ToCharArray();
                        sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[0] + ".wav";
                        sp.Play();
                        Thread.Sleep(500);
                        sp.SoundLocation = Application.StartupPath + @"\sound\num_wan.wav";
                        sp.Play();
                       
                        if (int.Parse(c[1].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_qian.wav";
                            sp.Play();
                        }
                        else if(int.Parse(c[2].ToString()) != 0|| int.Parse(c[3].ToString()) != 0|| int.Parse(c[4].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                        }

                        if (int.Parse(c[2].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[2] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_bai.wav";
                            sp.Play();
                        }
                        else if (int.Parse(c[1].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[2] + ".wav";
                            sp.Play();
                        }

                        if (int.Parse(c[3].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[3] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_shi.wav";
                            sp.Play();
                        }
                        else if (int.Parse(c[4].ToString()) != 0 && int.Parse(c[2].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[3] + ".wav";
                            sp.Play();
                        }
                        if (int.Parse(c[4].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[4] + ".wav";
                            sp.Play();
                        }
                        break;
                    case 6:
                        c = moneyChar[0].ToCharArray();
                        sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[0] + ".wav";
                        sp.Play();
                        Thread.Sleep(500);
                        sp.SoundLocation = Application.StartupPath + @"\sound\num_shi.wav";
                        sp.Play();
                        if (int.Parse(c[1].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                            sp.Play();
                        }
                           
                        Thread.Sleep(500);
                        sp.SoundLocation = Application.StartupPath + @"\sound\num_wan.wav";
                        sp.Play();
                        
                        if (int.Parse(c[2].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[2] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_qian.wav";
                            sp.Play();
                        }
                        else if (int.Parse(c[3].ToString()) != 0 || int.Parse(c[4].ToString()) != 0 || int.Parse(c[5].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[2] + ".wav";
                            sp.Play();
                        }

                        if (int.Parse(c[3].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[3] + ".wav";
                            sp.Play();
                          
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_bai.wav";
                            sp.Play();
                           
                        }
                        else if (int.Parse(c[2].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[3] + ".wav";
                            sp.Play();
                        }

                        if (int.Parse(c[4].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[4] + ".wav";
                            sp.Play();
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num_shi.wav";
                            sp.Play();
                          
                        }
                        else if(int.Parse(c[5].ToString()) != 0 && int.Parse(c[3].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[4] + ".wav";
                            sp.Play();
                        }
                     
                        if (int.Parse(c[5].ToString()) != 0)
                        {
                            Thread.Sleep(500);
                            sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[5] + ".wav";
                            sp.Play();
                        }
                        break;
                }

               
                Thread.Sleep(500);
                sp.SoundLocation = Application.StartupPath + @"\sound\yuan.wav";
                sp.Play();
                index = moneyChar[1].Length;
                c = new char[1];
                c = moneyChar[1].ToCharArray();
                if(int.Parse(c[0].ToString()) != 0)
                {
                    Thread.Sleep(500);
                    sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[0] + ".wav";
                    sp.Play();
                    Thread.Sleep(500);
                    sp.SoundLocation = Application.StartupPath + @"\sound\jiao.wav";
                    sp.Play();
                }
                if (int.Parse(c[1].ToString()) != 0)
                {
                    Thread.Sleep(500);
                    sp.SoundLocation = Application.StartupPath + @"\sound\num" + c[1] + ".wav";
                    sp.Play();
                    Thread.Sleep(500);
                    sp.SoundLocation = Application.StartupPath + @"\sound\fen.wav";
                    sp.Play();
                }

            }
            catch(Exception E)
            {
                MessageBoxEx.Show(E.Message, "提示");
            }  
        }

        public void CardBuzzer()
        {
            string CardData10 = "";
            string CardDataH = "";
            string CardData8 = "";
            if (!DeviceObject.objCPIC.Buzzer(1))
            {
                DeviceObject.objCPIC.GetCardData(ref CardData10, ref CardDataH, ref CardData8);
            }
        }
        protected void DispExtScreen(double Amount, double Balance, byte Mark, byte Flag)
        {
            if (SystemInfo.AllowExtScreen) DeviceObject.objApp.DispMoney(Amount, Balance, Mark, Flag);
        }
        public int WriteCardInfo(HSUNFK.TCardSFData sfData)
        {
            return DeviceObject.objCPIC.WriteCardInfoSF(ref sfData);
        }
        /// <summary>
        /// 自动检测刷卡
        /// </summary>
        /// <param name="j"></param>
        public void AutoXF(object j)
        {
            string CardData10 = "";
            string CardDataH = "";
            string CardData8 = "";
            while (SystemInfo.serialPort.IsOpen)
            {
                if (SystemInfo.IsWorking || !IsfrmtiteClose) continue;

                if (CheckCardExists(ref CardData10, ref CardDataH, ref CardData8))
                {
                    if (panelConsumptionType.InvokeRequired)
                    {
                        panelConsumptionType.Invoke(new Action<String>(s => {

                            SystemInfo.IsWorking = true;
                            WriteCard(CardData10, CardDataH, CardData8);
                            SystemInfo.IsWorking = false;
                        }), "");
                    }
                    else
                    {
                        SystemInfo.IsWorking = true;
                        WriteCard(CardData10, CardDataH, CardData8);
                        SystemInfo.IsWorking = false;
                    }
                 
                }
            }
        }

        private bool ReadCard(ref string CardData10)
        {
            CardData10 = "";
            string CardDataH = "";
            string CardData8 = "";

            if (!CheckCardExists(ref CardData10, ref CardDataH, ref CardData8)) return false;

            if (!IsfrmtiteClose) return false;
            
            return ReadCard(CardDataH, CardData8, ref CardData10);
        }
        private bool ReadCard(string CardDataH, string CardData8, ref string CardData10)
        {
            pubData = new HSUNFK.TCardPubData();
            sfData = new HSUNFK.TCardSFData();

            if (!ReadCardInfo(ref pubData, ref sfData)) return false;

            if (!CheckCardExists(pubData.CardNo, CardData10)) return false;
            DateTime dt = new DateTime();
            if (!db.GetServerDate(this.Text, ref dt)) return false;
            if (!CheckCardValidDate(dt, pubData.CardBeginDate, pubData.CardEndDate)) return false;

            bool IsOk = false;
            try
            {
                DataRow[] dataRow = dtEmp.Select("CardSectorNo = '" + pubData.CardNo + "'");
                if (dataRow.Length > 0)
                {
                    if (Convert.ToInt32(dataRow[0]["CardStatusID"]) != 20)
                    {
                        string m = "非正常卡片，不允许消费！";
                        if (panelConsumptionType.InvokeRequired)
                        {
                            panelConsumptionType.Invoke(new Action<String>(s => {

                                panelConsumptionType.TextX = s;

                            }), m);
                        }
                        else
                        {
                            panelConsumptionType.TextX = m;
                        }
                       
                    }
                    else
                    {
                        string no = dataRow[0]["EmpNo"].ToString();
                        string name = dataRow[0]["EmpName"].ToString();
                        string card = dataRow[0]["CardSectorNo"].ToString();
                        if (panelConsumptionType.InvokeRequired)
                        {
                            panelConsumptionType.Invoke(new Action<String>(s => {
                                txtName.Tag = no;
                                txtName.Text = s;
                                txtCard.Text = card;
                                txtMoney.Text = SystemInfo.moneyStr + (sfData.Balance + sfData.BtMonery).ToString("0.00");
                                panelConsumptionType.TextX = "IC CARD";
                            }), name);
                        }
                        else
                        {
                            txtName.Tag = no;
                            txtName.Text = name;
                            txtCard.Text = card;
                            txtMoney.Text = SystemInfo.moneyStr + (sfData.Balance + sfData.BtMonery).ToString("0.00");
                            panelConsumptionType.TextX = "IC CARD";
                        }
                        IsOk = true;
                    }
                }
                else
                {
                    string m = "非法卡，不允许操作！";
                    if (panelConsumptionType.InvokeRequired)
                    {
                        panelConsumptionType.Invoke(new Action<String>(s => {

                            panelConsumptionType.TextX = s;

                        }), m);
                    }
                    else
                    {
                        panelConsumptionType.TextX = m;
                    }
                  
                }
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message, MESSAGE);
            }
            return IsOk;
        }
        public bool CheckCardExists(ref string CardData10, ref string CardDataH, ref string CardData8)
        {
            return CheckCardExists(ref CardData10, ref CardDataH, ref CardData8, true);
        }

        public bool CheckCardExists(ref string CardData10, ref string CardDataH, ref string CardData8, bool ShowError)
        {
            CardData10 = "";
            CardDataH = "";
            CardData8 = "";
            if (!DeviceObject.objCPIC.IsOnline())
            {
                if (ShowError)
                {
                    string m = "连接读卡器失败！";
                    if (panelConsumptionType.InvokeRequired)
                    {
                        panelConsumptionType.Invoke(new Action<String>(s => {

                             panelConsumptionType.TextX = s;
                            
                            if(frmtite!=null)
                                frmtite.Close();
                            IsfrmtiteClose = true;
                        }),m);
                    }
                    else
                    {
                         panelConsumptionType.TextX = m;
                    }
                }
                   
                return false;
            }
            if (!DeviceObject.objCPIC.GetCardData(ref CardData10, ref CardDataH, ref CardData8))
            {
               
                if (ShowError)
                {
                    string m = "读取卡片内容失败，请检查卡片是否放好!";
                    if (panelConsumptionType.InvokeRequired)
                    {
                        panelConsumptionType.Invoke(new Action<String>(s => {
                            if (!SystemInfo.IsWorking && IsfrmtiteClose)
                            {
                                txtName.Tag = "";
                                txtName.Text = "";
                                txtCard.Text = "";
                                txtMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
                                panelConsumptionType.TextX = s;
                            }
                          
                            if (frmtite != null)
                                frmtite.Close();
                            IsfrmtiteClose = true;
                        }), m);
                    }
                    else
                    {
                        if (!SystemInfo.IsWorking && IsfrmtiteClose)
                        {
                            txtName.Tag = "";
                            txtName.Text = "";
                            txtCard.Text = "";
                            txtMoney.Text = SystemInfo.moneyStr + 0.ToString("0.00");
                            panelConsumptionType.TextX = m;
                        }

                        if (frmtite != null)
                            frmtite.Close();
                        IsfrmtiteClose = true;
                    }
                }
                 
                return false;
            }

            return true;
        }

        public bool CheckCardExists(string CardSectorNo, string CardData10)
        {
            if (!IsNumeric(CardSectorNo) || (Convert.ToInt64(CardSectorNo) <= 0))
            {
                string m = "非法卡，不允许操作！";
                if (panelConsumptionType.InvokeRequired)
                {
                    panelConsumptionType.Invoke(new Action<String>(s => {

                         panelConsumptionType.TextX = s;

                    }),m);
                }
                else
                {
                     panelConsumptionType.TextX = m;
                }
              
                return false;
            }
            DataRow[] dataRow = null;
            DataTableReader dr = null;
            bool ret = false;
            try
            {
                dataRow = dtEmp.Select("CardSectorNo='" + CardSectorNo + "'  AND CardPhysicsNo10='" + CardData10 + "'");
                if (dataRow.Length > 0) ret = true;
            }
            catch (Exception E)
            {
                MessageBoxEx.Show(E.Message, "提示");
            }
            finally
            {
                if (dr != null) dr.Close();
                dr = null;
            }
            if (!ret)
            {
                string m = "非法卡，不允许操作！";
                if (panelConsumptionType.InvokeRequired)
                {
                    panelConsumptionType.Invoke(new Action<String>(s => {

                         panelConsumptionType.TextX = s;

                    }),m);
                }
                else
                {
                     panelConsumptionType.TextX = m;
                }
               
            }
           
            return ret;
        }

        public bool CheckCardValidDate(DateTime dt, DateTime bt, DateTime et)
        {
            string msg = string.Format("卡片有效期为：{0} 至 {1}。", bt, et);
            DateTime d1 = new DateTime(dt.Year, dt.Month, dt.Day);
            DateTime d2 = bt;
            DateTime d3 = et;
            if (d1 < d2)
            {
                string m = msg + "\r\n\r\n" + "卡片未启用。";
                if (panelConsumptionType.InvokeRequired)
                {
                    panelConsumptionType.Invoke(new Action<String>(s => {

                         panelConsumptionType.TextX = s;

                    }),m);
                }
                else
                {
                     panelConsumptionType.TextX = m;
                }
              
                return false;
            }
            if (d1 > d3)
            {
                string m = msg + "\r\n\r\n" + "卡片已经过期。";
                if (panelConsumptionType.InvokeRequired)
                {
                    panelConsumptionType.Invoke(new Action<String>(s => {

                         panelConsumptionType.TextX = m;

                    }),m);
                }
                else
                {
                     panelConsumptionType.TextX = m;
                }
               
                return false;
            }
            return true;
        }
        public bool ReadCardInfo(ref HSUNFK.TCardPubData pubData, ref HSUNFK.TCardSFData sfData)
        {
            if (SystemInfo.CardKey == "")
            {
                string m = "授权卡信息为空，请到[系统选项]读取授权卡信息。";
                if (panelConsumptionType.InvokeRequired)
                {
                    panelConsumptionType.Invoke(new Action<String>(s => {

                         panelConsumptionType.TextX = s;

                    }),m);
                }
                else
                {
                     panelConsumptionType.TextX = m;
                }
               
                return false;
            }
            byte DataFlag = 0;
            HSUNFK.TCardSKData skData = new HSUNFK.TCardSKData();
            int cardRet = DeviceObject.objCPIC.ReadCardInfo(SystemInfo.IsLongEmpID, ref DataFlag, ref pubData,
              ref sfData, ref skData, 0);
            if (cardRet != 0)
            {
                string m = GetCardMsg(cardRet);
                if (panelConsumptionType.InvokeRequired)
                {
                    panelConsumptionType.Invoke(new Action<String>(s => {

                         panelConsumptionType.TextX = s;

                    }), m);
                }
                else
                {
                     panelConsumptionType.TextX = m;
                }
                return false;
            }
            if ((DataFlag != 1) && (DataFlag != 2) && (DataFlag != 3))
            {
                string m = "卡信息已乱，请修复卡片或者换卡！";
                if (panelConsumptionType.InvokeRequired)
                {
                    panelConsumptionType.Invoke(new Action<String>(s => {

                         panelConsumptionType.TextX = s;

                    }),m);
                }
                else
                {
                     panelConsumptionType.TextX = m;
                }
              
                return false;
            }
            if (SystemInfo.AntiDuplication)
            {
                string BlockData = "";
                int Sector = DeviceObject.objCPIC.GetSectorData(16, "FFFFFFFFFFFF", ref BlockData);
                if (Sector != 0)
                {
                    string m = "卡扇区错误，请使用防复制卡片";
                    if (panelConsumptionType.InvokeRequired)
                    {
                        panelConsumptionType.Invoke(new Action<String>(s => {

                             panelConsumptionType.TextX = s;

                        }),m);
                    }
                    else
                    {
                         panelConsumptionType.TextX = m;
                    }
                  
                    return false;
                }
            }

            return true;
        }
        public string GetCardMsg(int cardRet)
        {
            string ret = "";
            switch(cardRet)
            {
                case 0:
                    ret = "连接读卡器成功。";
                    break;
                case 1:
                    ret = "连接读卡器失败！";
                    break;
                case 2:
                    ret = "卡扇区密码错误！";
                    break;
                case 3:
                    ret = "读取卡片内容失败，请检查卡片是否放好";
                    break;
                case 4:
                    ret = "卡扇区错误，请使用防复制卡片";
                    break;
                default:
                    ret = "读取卡片内容失败，请检查卡片是否放好";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 扫码支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWPay_Click(object sender, EventArgs e)
        {
            SystemInfo.IsWorking = true;
            panelConsumptionType.TextX = PAY;
            Application.DoEvents();
            LanWritePay(true);
            SystemInfo.IsWorking = false;
        }

        private void LanWritePay(bool IsWPay)
        {
            btnCardPay.Enabled = false;
            btnWPay.Enabled = false;
            double money = 0;
            string SFType = "0";
            string MealType = SystemInfo.MealType;
            double Fact = 0;
            string mobileStr = "";
         
            try
            {
                DataTableReader dr = db.GetDataReader("SELECT * FROM SF_MacInfo WHERE MacSN='" + SystemInfo.DBMacSN + "'");
                if (dr.Read())
                {
                    mobileStr = dr["MobileInfo"].ToString();
                }
                TMobileInfo MobileInfo = new TMobileInfo(mobileStr);
                if (MobileInfo.XJLName == "")
                {
                    MessageBoxEx.Show(SELECTPAY, MESSAGE);
                    return;
                }
                DeviceObject.objCard.MobileInit(MobileInfo.MobTyp, MobileInfo.MercID, MobileInfo.TrmNo, MobileInfo.PWD, MobileInfo.XJLName, MobileInfo.XJLPWD);
               
                string Product = GetProduct();
                if (Product == "") return;
                Product = "";
                double.TryParse(CurrencyToStringEx(lbMoney.Text), out money);
                if (money <= 0)
                {
                    string m = "消费金额要大于0";
                    if (panelConsumptionType.InvokeRequired)
                    {
                        panelConsumptionType.Invoke(new Action<String>(s => {

                            panelConsumptionType.TextX = s;

                        }), m);
                    }
                    else
                    {
                        panelConsumptionType.TextX = m;
                    }
                    return;
                }

                double MobileMoney = money;
                money -= MobileInfo.RateMoney(IsWPay, MobileMoney);
                Fact = money;
                DateTime dt = new DateTime();
                if (!db.GetServerDate(this.Text, ref dt)) return;


                int h = mainHwnd.ToInt32();
               
                bool IsPayM = false;
                string ErrMsg = "";
                bool ret = false;
                string TradeNo = "";
               
                //扫码支付
                DeviceObject.objCard.MobilePayCodeSet("");
                ret = DeviceObject.objCard.MobilePayCode(h, true, null, null, MobileMoney, ref IsWPay, ref IsPayM, true, ref TradeNo, ref ErrMsg);

                //ret = DeviceObject.objCard.MobileShow(h, null, null, MobileMoney, ref IsWPay, ref IsPayM, true, ref TradeNo, ref ErrMsg);
                if (!ret)
                {
                    MessageBoxEx.Show(ErrMsg, MESSAGE);
                    return;
                }
                SFType = IsWPay ? "0" : "1";
                ZWPayType = IsWPay ? "微信消费" : "支付宝消费";
               
                if (MealType == "" || MealType == null) MealType = "NULL";

                string sql = "EXEC PSF_SFDataMobileInsert " + SFType + ",'" + dt.ToString(SystemInfo.SQLDateTimeFMT) + "'," + money + ",'" +
                             SystemInfo.DBMacSN + "','" + Product + "','" + TradeNo + "'," + 0 + "," + 0 + ",'" + 0 + "'";

                double Amount = money;
                if (db.ExecSQL(sql) < 1) return;

                ThreadPool.QueueUserWorkItem(new WaitCallback(SoundPlay), money);
                string title = "订单号：" + TradeNo + " " + "消费类型：" + ZWPayType + " " + "消费金额：" + SystemInfo.moneyStr + money.ToString("0.00") + "";

                db.WriteSYLog(this.Text, "结算台扫码消费", title);
                SettlementType = (int)PayType.SpayType;
                SFDataTime = dt.ToString(SystemInfo.SQLDateTimeFMT);
                PrintCardBill();
                IsfrmtiteClose = false;
                string xfType = "消费类型：" + ZWPayType + "";
                string Money = SystemInfo.moneyStr + money.ToString("0.00");

                frmSpay = new frmSPay(ZWPayType, Money);

                ThreadPool.QueueUserWorkItem(new WaitCallback(CloseFrmSPay));
                frmSpay.ShowDialog();
                IsfrmtiteClose = true;
            }
            catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, MESSAGE);
            }
            finally
            {
                btnCardPay.Enabled = true;
                btnWPay.Enabled = true;
            }
        }

        private void CloseFrmSPay(object state)
        {
            int index = 0;
            while(true)
            {
                Thread.Sleep(1000);
                index++;
                if (index > 3 )
                {
                    if(this.InvokeRequired)
                    {
                        this.Invoke(new Action<int>(s => {
                            if (frmSpay != null)
                            {
                                frmSpay.Close();
                                frmSpay = null;
                            }
                        }),0);
                    }
                    else
                    {
                        if (frmSpay != null)
                        {
                            frmSpay.Close();
                            frmSpay = null;
                        }
                    }
                    break;
                }
            }
        }
    }
    public class TMobileInfo
    {
        private string _ip = "180.96.69.219";
        private int _port = 8180;
        private string _keyID = "BB33F1074E40847A2F8FC5FCFB2781B5";
        private string _mercID = "";
        private string _trmNo = "";
        private string _pwd = "";
        private byte _rate11 = 2;
        private int _rate12 = 0;
        private byte _rate21 = 2;
        private int _rate22 = 0;
        private byte _mobTyp = 0;
        private string _xjlName = "";
        private string _xjlPWD = "";
        private const string ENCRY = "quzhengyu";

        private MemoryStream _wx = null;
        private MemoryStream _ali = null;

        public TMobileInfo(string info)
        {
            _wx = new MemoryStream();
            _ali = new MemoryStream();

            string[] tmp = info.Split('@');
            if (tmp.Length == 6 || tmp.Length == 10 || tmp.Length == 13)
            {
                _ip = tmp[0];
                int.TryParse(tmp[1], out _port);
                _keyID = GetOprtDecrypt(tmp[2]);
                _mercID = GetOprtDecrypt(tmp[3]);
                _trmNo = GetOprtDecrypt(tmp[4]);
                _pwd = GetOprtDecrypt(tmp[5]);
                if (tmp.Length == 10 || tmp.Length == 13)
                {
                    byte.TryParse(tmp[6], out _rate11);
                    int.TryParse(tmp[7], out _rate12);
                    byte.TryParse(tmp[8], out _rate21);
                    int.TryParse(tmp[9], out _rate22);
                }
                if (tmp.Length == 13)
                {
                    byte.TryParse(tmp[10], out _mobTyp);
                    _xjlName = GetOprtDecrypt(tmp[11]);
                    _xjlPWD = GetOprtDecrypt(tmp[12]);
                }
            }
          
            if (_keyID == "") _keyID = "000";
        }

        public string IP
        {
            get { return _ip; }
        }

        public int Port
        {
            get { return _port; }
        }

        public string KeyID
        {
            get { return _keyID; }
        }

        public string MercID
        {
            get { return _mercID; }
        }

        public string TrmNo
        {
            get { return _trmNo; }
        }

        public string PWD
        {
            get { return _pwd; }
        }

        public byte Rate11
        {
            get { return _rate11; }
        }

        public int Rate12
        {
            get { return _rate12; }
        }

        public byte Rate21
        {
            get { return _rate21; }
        }

        public int Rate22
        {
            get { return _rate22; }
        }

        public byte MobTyp
        {
            get { return _mobTyp; }
        }

        public string XJLName
        {
            get { return _xjlName; }
        }

        public string XJLPWD
        {
            get { return _xjlPWD; }
        }

        public MemoryStream WeiXin
        {
            get { return _wx; }
        }

        public MemoryStream AliPay
        {
            get { return _ali; }
        }
        public string GetOprtDecrypt(string src)
        {
            string ret = "";
            if (src != "")
            {
                ret = DeviceObject.objAES.AesDecrypt(src, ENCRY);
                if (ret == null) ret = "";
            }
            return ret;
        }
        public double RateMoney(bool IsWX, double money)
        {
            byte Rate1 = IsWX ? _rate11 : _rate21;
            int Rate2 = IsWX ? _rate12 : _rate22;
            double ret = 0;
            if (Rate2 > 0)
            {
                double v = money * Rate2;
                if (Rate1 == 0) v /= 100;
                if (Rate1 == 1) v /= 1000;
                if (Rate1 == 2) v /= 10000;
                ret = v;
            }
            return ret;
        }
    }

    public enum PayType
    {
        CardType = 0,       //刷卡  
        SpayType = 1       //扫码
    }
}
