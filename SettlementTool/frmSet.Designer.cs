namespace SettlementTool
{
    partial class frmSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSet));
            this.cmbBaud = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbPort = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnet = new DevComponents.DotNetBar.ButtonX();
            this.btnTestConnet = new DevComponents.DotNetBar.ButtonX();
            this.btnGetDBName = new DevComponents.DotNetBar.ButtonX();
            this.cbbDBName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMSSQLUserPass = new System.Windows.Forms.TextBox();
            this.lblMSSQLUserPass = new System.Windows.Forms.Label();
            this.txtMSSQLUserName = new System.Windows.Forms.TextBox();
            this.lblMSSQLUserName = new System.Windows.Forms.Label();
            this.rbMSSQLSQL = new System.Windows.Forms.RadioButton();
            this.rbMSSQLWindowsNT = new System.Windows.Forms.RadioButton();
            this.lblMSSQLVerify = new System.Windows.Forms.Label();
            this.txtMSSQLServer = new System.Windows.Forms.TextBox();
            this.lblMSSQLServer = new System.Windows.Forms.Label();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.btnUpMacSN = new DevComponents.DotNetBar.ButtonX();
            this.txtMacNomber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel4 = new DevComponents.DotNetBar.TabControlPanel();
            this.btnTimeSet = new DevComponents.DotNetBar.ButtonX();
            this.chkAutoXF = new System.Windows.Forms.CheckBox();
            this.btnGetParamInfo = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEndTime4 = new System.Windows.Forms.MaskedTextBox();
            this.txtBeginTime4 = new System.Windows.Forms.MaskedTextBox();
            this.txtEndTime3 = new System.Windows.Forms.MaskedTextBox();
            this.txtBeginTime3 = new System.Windows.Forms.MaskedTextBox();
            this.txtEndTime2 = new System.Windows.Forms.MaskedTextBox();
            this.txtBeginTime2 = new System.Windows.Forms.MaskedTextBox();
            this.txtEndTime1 = new System.Windows.Forms.MaskedTextBox();
            this.txtBeginTime1 = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblBegin = new System.Windows.Forms.Label();
            this.lblMealType = new System.Windows.Forms.Label();
            this.cbbMealType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAutoPeriod = new System.Windows.Forms.CheckBox();
            this.chkBTBalance = new System.Windows.Forms.CheckBox();
            this.tabItem4 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.chkPowerOn = new System.Windows.Forms.CheckBox();
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnCloses = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbBaud
            // 
            this.cmbBaud.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaud.ForeColor = System.Drawing.Color.Black;
            this.cmbBaud.FormattingEnabled = true;
            this.cmbBaud.ItemHeight = 21;
            this.cmbBaud.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaud.Location = new System.Drawing.Point(108, 85);
            this.cmbBaud.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBaud.Name = "cmbBaud";
            this.cmbBaud.Size = new System.Drawing.Size(124, 27);
            this.cmbBaud.TabIndex = 3;
            // 
            // cmbPort
            // 
            this.cmbPort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPort.ForeColor = System.Drawing.Color.Black;
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.ItemHeight = 21;
            this.cmbPort.Location = new System.Drawing.Point(108, 27);
            this.cmbPort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(124, 27);
            this.cmbPort.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(29, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(29, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "串  口：";
            // 
            // btnConnet
            // 
            this.btnConnet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConnet.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnet.Location = new System.Drawing.Point(423, 5);
            this.btnConnet.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnet.Name = "btnConnet";
            this.btnConnet.Size = new System.Drawing.Size(113, 54);
            this.btnConnet.TabIndex = 4;
            this.btnConnet.Text = "打开";
            this.btnConnet.Click += new System.EventHandler(this.btnConnet_Click);
            // 
            // btnTestConnet
            // 
            this.btnTestConnet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTestConnet.Location = new System.Drawing.Point(485, 330);
            this.btnTestConnet.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestConnet.Name = "btnTestConnet";
            this.btnTestConnet.Size = new System.Drawing.Size(147, 41);
            this.btnTestConnet.TabIndex = 6;
            this.btnTestConnet.Text = "测试连接";
            this.btnTestConnet.Click += new System.EventHandler(this.btnTestConnet_Click);
            // 
            // btnGetDBName
            // 
            this.btnGetDBName.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetDBName.Location = new System.Drawing.Point(485, 214);
            this.btnGetDBName.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetDBName.Name = "btnGetDBName";
            this.btnGetDBName.Size = new System.Drawing.Size(147, 41);
            this.btnGetDBName.TabIndex = 13;
            this.btnGetDBName.Text = "获取账套名称列表";
            this.btnGetDBName.Click += new System.EventHandler(this.btnGetDBName_Click);
            // 
            // cbbDBName
            // 
            this.cbbDBName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbDBName.ForeColor = System.Drawing.Color.Black;
            this.cbbDBName.FormattingEnabled = true;
            this.cbbDBName.ItemHeight = 21;
            this.cbbDBName.Location = new System.Drawing.Point(167, 221);
            this.cbbDBName.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDBName.Name = "cbbDBName";
            this.cbbDBName.Size = new System.Drawing.Size(263, 27);
            this.cbbDBName.TabIndex = 26;
            this.cbbDBName.SelectedIndexChanged += new System.EventHandler(this.cbbDBName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(44, 228);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "账套名称：";
            // 
            // txtMSSQLUserPass
            // 
            this.txtMSSQLUserPass.Location = new System.Drawing.Point(499, 160);
            this.txtMSSQLUserPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtMSSQLUserPass.MaxLength = 50;
            this.txtMSSQLUserPass.Name = "txtMSSQLUserPass";
            this.txtMSSQLUserPass.Size = new System.Drawing.Size(132, 26);
            this.txtMSSQLUserPass.TabIndex = 24;
            this.txtMSSQLUserPass.UseSystemPasswordChar = true;
            // 
            // lblMSSQLUserPass
            // 
            this.lblMSSQLUserPass.AutoSize = true;
            this.lblMSSQLUserPass.BackColor = System.Drawing.Color.Transparent;
            this.lblMSSQLUserPass.Location = new System.Drawing.Point(406, 165);
            this.lblMSSQLUserPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMSSQLUserPass.Name = "lblMSSQLUserPass";
            this.lblMSSQLUserPass.Size = new System.Drawing.Size(88, 16);
            this.lblMSSQLUserPass.TabIndex = 23;
            this.lblMSSQLUserPass.Text = "用户密码：";
            // 
            // txtMSSQLUserName
            // 
            this.txtMSSQLUserName.Location = new System.Drawing.Point(260, 160);
            this.txtMSSQLUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtMSSQLUserName.MaxLength = 50;
            this.txtMSSQLUserName.Name = "txtMSSQLUserName";
            this.txtMSSQLUserName.Size = new System.Drawing.Size(132, 26);
            this.txtMSSQLUserName.TabIndex = 22;
            // 
            // lblMSSQLUserName
            // 
            this.lblMSSQLUserName.AutoSize = true;
            this.lblMSSQLUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblMSSQLUserName.Location = new System.Drawing.Point(167, 165);
            this.lblMSSQLUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMSSQLUserName.Name = "lblMSSQLUserName";
            this.lblMSSQLUserName.Size = new System.Drawing.Size(88, 16);
            this.lblMSSQLUserName.TabIndex = 21;
            this.lblMSSQLUserName.Text = "用户名称：";
            // 
            // rbMSSQLSQL
            // 
            this.rbMSSQLSQL.AutoSize = true;
            this.rbMSSQLSQL.BackColor = System.Drawing.Color.Transparent;
            this.rbMSSQLSQL.Location = new System.Drawing.Point(167, 120);
            this.rbMSSQLSQL.Margin = new System.Windows.Forms.Padding(4);
            this.rbMSSQLSQL.Name = "rbMSSQLSQL";
            this.rbMSSQLSQL.Size = new System.Drawing.Size(218, 20);
            this.rbMSSQLSQL.TabIndex = 20;
            this.rbMSSQLSQL.TabStop = true;
            this.rbMSSQLSQL.Text = "使用指定的用户名称和密码";
            this.rbMSSQLSQL.UseVisualStyleBackColor = false;
            // 
            // rbMSSQLWindowsNT
            // 
            this.rbMSSQLWindowsNT.AutoSize = true;
            this.rbMSSQLWindowsNT.BackColor = System.Drawing.Color.Transparent;
            this.rbMSSQLWindowsNT.Location = new System.Drawing.Point(167, 80);
            this.rbMSSQLWindowsNT.Margin = new System.Windows.Forms.Padding(4);
            this.rbMSSQLWindowsNT.Name = "rbMSSQLWindowsNT";
            this.rbMSSQLWindowsNT.Size = new System.Drawing.Size(234, 20);
            this.rbMSSQLWindowsNT.TabIndex = 19;
            this.rbMSSQLWindowsNT.TabStop = true;
            this.rbMSSQLWindowsNT.Text = "使用Windos NT 集成安全设置";
            this.rbMSSQLWindowsNT.UseVisualStyleBackColor = false;
            // 
            // lblMSSQLVerify
            // 
            this.lblMSSQLVerify.AutoSize = true;
            this.lblMSSQLVerify.BackColor = System.Drawing.Color.Transparent;
            this.lblMSSQLVerify.Location = new System.Drawing.Point(47, 85);
            this.lblMSSQLVerify.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMSSQLVerify.Name = "lblMSSQLVerify";
            this.lblMSSQLVerify.Size = new System.Drawing.Size(88, 16);
            this.lblMSSQLVerify.TabIndex = 18;
            this.lblMSSQLVerify.Text = "验证信息：";
            // 
            // txtMSSQLServer
            // 
            this.txtMSSQLServer.Location = new System.Drawing.Point(167, 40);
            this.txtMSSQLServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtMSSQLServer.MaxLength = 100;
            this.txtMSSQLServer.Name = "txtMSSQLServer";
            this.txtMSSQLServer.Size = new System.Drawing.Size(265, 26);
            this.txtMSSQLServer.TabIndex = 17;
            // 
            // lblMSSQLServer
            // 
            this.lblMSSQLServer.AutoSize = true;
            this.lblMSSQLServer.BackColor = System.Drawing.Color.Transparent;
            this.lblMSSQLServer.Location = new System.Drawing.Point(47, 44);
            this.lblMSSQLServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMSSQLServer.Name = "lblMSSQLServer";
            this.lblMSSQLServer.Size = new System.Drawing.Size(104, 16);
            this.lblMSSQLServer.TabIndex = 16;
            this.lblMSSQLServer.Text = "服务器名称：";
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.Gray;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Controls.Add(this.tabControlPanel4);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ForeColor = System.Drawing.Color.Black;
            this.tabControl1.Location = new System.Drawing.Point(1, 43);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.SelectedTabIndex = 1;
            this.tabControl1.Size = new System.Drawing.Size(692, 416);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Metro;
            this.tabControl1.TabIndex = 18;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem3);
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem4);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.btnUpMacSN);
            this.tabControlPanel1.Controls.Add(this.txtMacNomber);
            this.tabControlPanel1.Controls.Add(this.btnGetDBName);
            this.tabControlPanel1.Controls.Add(this.txtMSSQLServer);
            this.tabControlPanel1.Controls.Add(this.btnTestConnet);
            this.tabControlPanel1.Controls.Add(this.cbbDBName);
            this.tabControlPanel1.Controls.Add(this.lblMSSQLServer);
            this.tabControlPanel1.Controls.Add(this.label3);
            this.tabControlPanel1.Controls.Add(this.lblMSSQLVerify);
            this.tabControlPanel1.Controls.Add(this.txtMSSQLUserPass);
            this.tabControlPanel1.Controls.Add(this.rbMSSQLWindowsNT);
            this.tabControlPanel1.Controls.Add(this.lblMSSQLUserPass);
            this.tabControlPanel1.Controls.Add(this.rbMSSQLSQL);
            this.tabControlPanel1.Controls.Add(this.txtMSSQLUserName);
            this.tabControlPanel1.Controls.Add(this.lblMSSQLUserName);
            this.tabControlPanel1.Controls.Add(this.label10);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 38);
            this.tabControlPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(692, 378);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // btnUpMacSN
            // 
            this.btnUpMacSN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpMacSN.Location = new System.Drawing.Point(485, 272);
            this.btnUpMacSN.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpMacSN.Name = "btnUpMacSN";
            this.btnUpMacSN.Size = new System.Drawing.Size(147, 41);
            this.btnUpMacSN.TabIndex = 30;
            this.btnUpMacSN.Text = "更改机器号";
            this.btnUpMacSN.Click += new System.EventHandler(this.btnUpMacSN_Click);
            // 
            // txtMacNomber
            // 
            this.txtMacNomber.Location = new System.Drawing.Point(164, 281);
            this.txtMacNomber.Margin = new System.Windows.Forms.Padding(4);
            this.txtMacNomber.MaxLength = 5;
            this.txtMacNomber.Name = "txtMacNomber";
            this.txtMacNomber.Size = new System.Drawing.Size(265, 26);
            this.txtMacNomber.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(44, 285);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 28;
            this.label10.Text = "机器号：";
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "账套配置";
            // 
            // tabControlPanel4
            // 
            this.tabControlPanel4.Controls.Add(this.btnTimeSet);
            this.tabControlPanel4.Controls.Add(this.chkAutoXF);
            this.tabControlPanel4.Controls.Add(this.btnGetParamInfo);
            this.tabControlPanel4.Controls.Add(this.groupBox1);
            this.tabControlPanel4.Controls.Add(this.cbbMealType);
            this.tabControlPanel4.Controls.Add(this.label5);
            this.tabControlPanel4.Controls.Add(this.chkAutoPeriod);
            this.tabControlPanel4.Controls.Add(this.chkBTBalance);
            this.tabControlPanel4.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel4.Location = new System.Drawing.Point(0, 38);
            this.tabControlPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlPanel4.Name = "tabControlPanel4";
            this.tabControlPanel4.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel4.Size = new System.Drawing.Size(692, 378);
            this.tabControlPanel4.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabControlPanel4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel4.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.tabControlPanel4.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel4.Style.GradientAngle = 90;
            this.tabControlPanel4.TabIndex = 15;
            this.tabControlPanel4.TabItem = this.tabItem4;
            // 
            // btnTimeSet
            // 
            this.btnTimeSet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTimeSet.Location = new System.Drawing.Point(423, 327);
            this.btnTimeSet.Margin = new System.Windows.Forms.Padding(4);
            this.btnTimeSet.Name = "btnTimeSet";
            this.btnTimeSet.Size = new System.Drawing.Size(165, 45);
            this.btnTimeSet.TabIndex = 8;
            this.btnTimeSet.Text = "保存时段设置";
            this.btnTimeSet.Click += new System.EventHandler(this.btnTimeSet_Click);
            // 
            // chkAutoXF
            // 
            this.chkAutoXF.AutoSize = true;
            this.chkAutoXF.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoXF.Location = new System.Drawing.Point(25, 344);
            this.chkAutoXF.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoXF.Name = "chkAutoXF";
            this.chkAutoXF.Size = new System.Drawing.Size(123, 20);
            this.chkAutoXF.TabIndex = 7;
            this.chkAutoXF.Text = "放卡自动扣款";
            this.chkAutoXF.UseVisualStyleBackColor = false;
            // 
            // btnGetParamInfo
            // 
            this.btnGetParamInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetParamInfo.Location = new System.Drawing.Point(423, 265);
            this.btnGetParamInfo.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetParamInfo.Name = "btnGetParamInfo";
            this.btnGetParamInfo.Size = new System.Drawing.Size(165, 45);
            this.btnGetParamInfo.TabIndex = 6;
            this.btnGetParamInfo.Text = "获取账套时段设置";
            this.btnGetParamInfo.Click += new System.EventHandler(this.btnGetParamInfo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtEndTime4);
            this.groupBox1.Controls.Add(this.txtBeginTime4);
            this.groupBox1.Controls.Add(this.txtEndTime3);
            this.groupBox1.Controls.Add(this.txtBeginTime3);
            this.groupBox1.Controls.Add(this.txtEndTime2);
            this.groupBox1.Controls.Add(this.txtBeginTime2);
            this.groupBox1.Controls.Add(this.txtEndTime1);
            this.groupBox1.Controls.Add(this.txtBeginTime1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.lblBegin);
            this.groupBox1.Controls.Add(this.lblMealType);
            this.groupBox1.Location = new System.Drawing.Point(25, 131);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(320, 201);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时段设置：";
            // 
            // txtEndTime4
            // 
            this.txtEndTime4.Location = new System.Drawing.Point(200, 157);
            this.txtEndTime4.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndTime4.Mask = "90:00";
            this.txtEndTime4.Name = "txtEndTime4";
            this.txtEndTime4.PromptChar = ' ';
            this.txtEndTime4.Size = new System.Drawing.Size(92, 26);
            this.txtEndTime4.TabIndex = 37;
            // 
            // txtBeginTime4
            // 
            this.txtBeginTime4.Location = new System.Drawing.Point(93, 157);
            this.txtBeginTime4.Margin = new System.Windows.Forms.Padding(4);
            this.txtBeginTime4.Mask = "90:00";
            this.txtBeginTime4.Name = "txtBeginTime4";
            this.txtBeginTime4.PromptChar = ' ';
            this.txtBeginTime4.Size = new System.Drawing.Size(92, 26);
            this.txtBeginTime4.TabIndex = 36;
            // 
            // txtEndTime3
            // 
            this.txtEndTime3.Location = new System.Drawing.Point(200, 124);
            this.txtEndTime3.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndTime3.Mask = "90:00";
            this.txtEndTime3.Name = "txtEndTime3";
            this.txtEndTime3.PromptChar = ' ';
            this.txtEndTime3.Size = new System.Drawing.Size(92, 26);
            this.txtEndTime3.TabIndex = 35;
            // 
            // txtBeginTime3
            // 
            this.txtBeginTime3.Location = new System.Drawing.Point(93, 124);
            this.txtBeginTime3.Margin = new System.Windows.Forms.Padding(4);
            this.txtBeginTime3.Mask = "90:00";
            this.txtBeginTime3.Name = "txtBeginTime3";
            this.txtBeginTime3.PromptChar = ' ';
            this.txtBeginTime3.Size = new System.Drawing.Size(92, 26);
            this.txtBeginTime3.TabIndex = 34;
            // 
            // txtEndTime2
            // 
            this.txtEndTime2.Location = new System.Drawing.Point(200, 91);
            this.txtEndTime2.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndTime2.Mask = "90:00";
            this.txtEndTime2.Name = "txtEndTime2";
            this.txtEndTime2.PromptChar = ' ';
            this.txtEndTime2.Size = new System.Drawing.Size(92, 26);
            this.txtEndTime2.TabIndex = 33;
            // 
            // txtBeginTime2
            // 
            this.txtBeginTime2.Location = new System.Drawing.Point(93, 91);
            this.txtBeginTime2.Margin = new System.Windows.Forms.Padding(4);
            this.txtBeginTime2.Mask = "90:00";
            this.txtBeginTime2.Name = "txtBeginTime2";
            this.txtBeginTime2.PromptChar = ' ';
            this.txtBeginTime2.Size = new System.Drawing.Size(92, 26);
            this.txtBeginTime2.TabIndex = 32;
            // 
            // txtEndTime1
            // 
            this.txtEndTime1.Location = new System.Drawing.Point(200, 57);
            this.txtEndTime1.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndTime1.Mask = "90:00";
            this.txtEndTime1.Name = "txtEndTime1";
            this.txtEndTime1.PromptChar = ' ';
            this.txtEndTime1.Size = new System.Drawing.Size(92, 26);
            this.txtEndTime1.TabIndex = 31;
            // 
            // txtBeginTime1
            // 
            this.txtBeginTime1.Location = new System.Drawing.Point(93, 57);
            this.txtBeginTime1.Margin = new System.Windows.Forms.Padding(4);
            this.txtBeginTime1.Mask = "90:00";
            this.txtBeginTime1.Name = "txtBeginTime1";
            this.txtBeginTime1.PromptChar = ' ';
            this.txtBeginTime1.Size = new System.Drawing.Size(92, 26);
            this.txtBeginTime1.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 163);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 44;
            this.label9.Tag = "Snack";
            this.label9.Text = "夜宵";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 129);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 43;
            this.label8.Tag = "Dinner";
            this.label8.Text = "晚餐";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 96);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 42;
            this.label7.Tag = "Lunch";
            this.label7.Text = "午餐";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 63);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 41;
            this.label6.Tag = "Breakfast";
            this.label6.Text = "早餐";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(200, 31);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(40, 16);
            this.lblEnd.TabIndex = 40;
            this.lblEnd.Text = "结束";
            // 
            // lblBegin
            // 
            this.lblBegin.AutoSize = true;
            this.lblBegin.Location = new System.Drawing.Point(93, 31);
            this.lblBegin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBegin.Name = "lblBegin";
            this.lblBegin.Size = new System.Drawing.Size(40, 16);
            this.lblBegin.TabIndex = 39;
            this.lblBegin.Text = "开始";
            // 
            // lblMealType
            // 
            this.lblMealType.AutoSize = true;
            this.lblMealType.Location = new System.Drawing.Point(27, 31);
            this.lblMealType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMealType.Name = "lblMealType";
            this.lblMealType.Size = new System.Drawing.Size(40, 16);
            this.lblMealType.TabIndex = 38;
            this.lblMealType.Text = "餐类";
            // 
            // cbbMealType
            // 
            this.cbbMealType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMealType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMealType.ForeColor = System.Drawing.Color.Black;
            this.cbbMealType.FormattingEnabled = true;
            this.cbbMealType.ItemHeight = 21;
            this.cbbMealType.Location = new System.Drawing.Point(117, 47);
            this.cbbMealType.Margin = new System.Windows.Forms.Padding(4);
            this.cbbMealType.Name = "cbbMealType";
            this.cbbMealType.Size = new System.Drawing.Size(147, 27);
            this.cbbMealType.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(23, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "选择时段：";
            // 
            // chkAutoPeriod
            // 
            this.chkAutoPeriod.AutoSize = true;
            this.chkAutoPeriod.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoPeriod.Location = new System.Drawing.Point(25, 95);
            this.chkAutoPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoPeriod.Name = "chkAutoPeriod";
            this.chkAutoPeriod.Size = new System.Drawing.Size(155, 20);
            this.chkAutoPeriod.TabIndex = 1;
            this.chkAutoPeriod.Text = "自动获取账套时段";
            this.chkAutoPeriod.UseVisualStyleBackColor = false;
            // 
            // chkBTBalance
            // 
            this.chkBTBalance.AutoSize = true;
            this.chkBTBalance.BackColor = System.Drawing.Color.Transparent;
            this.chkBTBalance.Location = new System.Drawing.Point(25, 12);
            this.chkBTBalance.Margin = new System.Windows.Forms.Padding(4);
            this.chkBTBalance.Name = "chkBTBalance";
            this.chkBTBalance.Size = new System.Drawing.Size(91, 20);
            this.chkBTBalance.TabIndex = 0;
            this.chkBTBalance.Text = "使用补贴";
            this.chkBTBalance.UseVisualStyleBackColor = false;
            // 
            // tabItem4
            // 
            this.tabItem4.AttachedControl = this.tabControlPanel4;
            this.tabItem4.Name = "tabItem4";
            this.tabItem4.Text = "支付配置";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.CanvasColor = System.Drawing.Color.DimGray;
            this.tabControlPanel3.Controls.Add(this.chkPrint);
            this.tabControlPanel3.Controls.Add(this.chkPowerOn);
            this.tabControlPanel3.Controls.Add(this.cmbBaud);
            this.tabControlPanel3.Controls.Add(this.label2);
            this.tabControlPanel3.Controls.Add(this.cmbPort);
            this.tabControlPanel3.Controls.Add(this.label1);
            this.tabControlPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 38);
            this.tabControlPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(692, 378);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 9;
            this.tabControlPanel3.TabItem = this.tabItem3;
            // 
            // chkPowerOn
            // 
            this.chkPowerOn.AutoSize = true;
            this.chkPowerOn.BackColor = System.Drawing.Color.Transparent;
            this.chkPowerOn.Location = new System.Drawing.Point(32, 149);
            this.chkPowerOn.Margin = new System.Windows.Forms.Padding(4);
            this.chkPowerOn.Name = "chkPowerOn";
            this.chkPowerOn.Size = new System.Drawing.Size(123, 20);
            this.chkPowerOn.TabIndex = 4;
            this.chkPowerOn.Text = "开启开机自启";
            this.chkPowerOn.UseVisualStyleBackColor = false;
            // 
            // tabItem3
            // 
            this.tabItem3.AttachedControl = this.tabControlPanel3;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "基本配置";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(564, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(113, 54);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.Color.Transparent;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnCloses);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(1, 1);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(692, 42);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx1.Style.BorderWidth = 0;
            this.panelEx1.Style.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Style.ForeColor.Color = System.Drawing.Color.White;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 23;
            this.panelEx1.Text = "系统配置";
            // 
            // btnCloses
            // 
            this.btnCloses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.btnCloses.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnCloses.Location = new System.Drawing.Point(643, 5);
            this.btnCloses.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloses.Name = "btnCloses";
            this.btnCloses.Size = new System.Drawing.Size(47, 33);
            this.btnCloses.Symbol = "57676";
            this.btnCloses.SymbolColor = System.Drawing.Color.White;
            this.btnCloses.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnCloses.TabIndex = 0;
            this.btnCloses.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnCloses.Click += new System.EventHandler(this.btnCloses_Click);
            this.btnCloses.MouseEnter += new System.EventHandler(this.btnCloses_MouseEnter);
            this.btnCloses.MouseLeave += new System.EventHandler(this.btnCloses_MouseLeave);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.Color.Transparent;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnConnet);
            this.panelEx2.Controls.Add(this.btnClose);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(1, 459);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(692, 63);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.White;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx2.Style.BorderWidth = 0;
            this.panelEx2.Style.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx2.Style.ForeColor.Color = System.Drawing.Color.White;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 30;
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.BackColor = System.Drawing.Color.Transparent;
            this.chkPrint.Location = new System.Drawing.Point(32, 190);
            this.chkPrint.Margin = new System.Windows.Forms.Padding(4);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(91, 20);
            this.chkPrint.TabIndex = 5;
            this.chkPrint.Text = "开启打印";
            this.chkPrint.UseVisualStyleBackColor = false;
            // 
            // frmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(694, 523);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panelEx2);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSet";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSet_FormClosed);
            this.Load += new System.EventHandler(this.frmSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.tabControlPanel4.ResumeLayout(false);
            this.tabControlPanel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControlPanel3.ResumeLayout(false);
            this.tabControlPanel3.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnConnet;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbBaud;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btnTestConnet;
        private DevComponents.DotNetBar.ButtonX btnGetDBName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbDBName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMSSQLUserPass;
        private System.Windows.Forms.Label lblMSSQLUserPass;
        private System.Windows.Forms.TextBox txtMSSQLUserName;
        private System.Windows.Forms.Label lblMSSQLUserName;
        private System.Windows.Forms.RadioButton rbMSSQLSQL;
        private System.Windows.Forms.RadioButton rbMSSQLWindowsNT;
        private System.Windows.Forms.Label lblMSSQLVerify;
        private System.Windows.Forms.TextBox txtMSSQLServer;
        private System.Windows.Forms.Label lblMSSQLServer;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel4;
        private System.Windows.Forms.CheckBox chkBTBalance;
        private DevComponents.DotNetBar.TabItem tabItem4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox txtEndTime4;
        private System.Windows.Forms.MaskedTextBox txtBeginTime4;
        private System.Windows.Forms.MaskedTextBox txtEndTime3;
        private System.Windows.Forms.MaskedTextBox txtBeginTime3;
        private System.Windows.Forms.MaskedTextBox txtEndTime2;
        private System.Windows.Forms.MaskedTextBox txtBeginTime2;
        private System.Windows.Forms.MaskedTextBox txtEndTime1;
        private System.Windows.Forms.MaskedTextBox txtBeginTime1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblBegin;
        private System.Windows.Forms.Label lblMealType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMealType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAutoPeriod;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX btnCloses;
        private System.Windows.Forms.CheckBox chkAutoXF;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.CheckBox chkPowerOn;
        private DevComponents.DotNetBar.ButtonX btnTimeSet;
        private DevComponents.DotNetBar.ButtonX btnGetParamInfo;
        private DevComponents.DotNetBar.ButtonX btnUpMacSN;
        private System.Windows.Forms.TextBox txtMacNomber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkPrint;
    }
}