namespace SettlementTool
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCard = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtMoney = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lbTime = new DevComponents.DotNetBar.LabelX();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.picTime = new System.Windows.Forms.PictureBox();
            this.msgGrid = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelEx = new DevComponents.DotNetBar.PanelEx();
            this.btnWPay = new System.Windows.Forms.PictureBox();
            this.panelConsumptionType = new SettlementTool.BullNosePanelEx(this.components);
            this.PanelInfo = new SettlementTool.BullNosePanelEx(this.components);
            this.exPanelCount = new SettlementTool.HeadPanelEx(this.components);
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.lbCardCount = new DevComponents.DotNetBar.LabelX();
            this.exPanelMoney = new SettlementTool.HeadPanelEx(this.components);
            this.lbMoney = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnCardPay = new System.Windows.Forms.PictureBox();
            this.btnSet = new System.Windows.Forms.PictureBox();
            this.picTitle = new System.Windows.Forms.PictureBox();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgGrid)).BeginInit();
            this.panelEx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnWPay)).BeginInit();
            this.PanelInfo.SuspendLayout();
            this.exPanelCount.SuspendLayout();
            this.exPanelMoney.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCardPay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.DisabledBackColor = System.Drawing.Color.White;
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(569, 119);
            this.txtName.Margin = new System.Windows.Forms.Padding(20);
            this.txtName.Name = "txtName";
            this.txtName.PreventEnterBeep = true;
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(254, 46);
            this.txtName.TabIndex = 16;
            this.txtName.TabStop = false;
            // 
            // txtCard
            // 
            this.txtCard.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtCard.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCard.Border.Class = "TextBoxBorder";
            this.txtCard.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCard.DisabledBackColor = System.Drawing.Color.White;
            this.txtCard.Enabled = false;
            this.txtCard.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCard.ForeColor = System.Drawing.Color.Black;
            this.txtCard.Location = new System.Drawing.Point(569, 203);
            this.txtCard.Margin = new System.Windows.Forms.Padding(20);
            this.txtCard.Name = "txtCard";
            this.txtCard.PreventEnterBeep = true;
            this.txtCard.ReadOnly = true;
            this.txtCard.Size = new System.Drawing.Size(254, 46);
            this.txtCard.TabIndex = 18;
            this.txtCard.TabStop = false;
            // 
            // txtMoney
            // 
            this.txtMoney.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtMoney.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtMoney.Border.Class = "TextBoxBorder";
            this.txtMoney.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMoney.DisabledBackColor = System.Drawing.Color.White;
            this.txtMoney.Enabled = false;
            this.txtMoney.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMoney.ForeColor = System.Drawing.Color.Black;
            this.txtMoney.Location = new System.Drawing.Point(569, 279);
            this.txtMoney.Margin = new System.Windows.Forms.Padding(20);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.PreventEnterBeep = true;
            this.txtMoney.ReadOnly = true;
            this.txtMoney.Size = new System.Drawing.Size(254, 46);
            this.txtMoney.TabIndex = 20;
            this.txtMoney.TabStop = false;
            // 
            // labelX5
            // 
            this.labelX5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.ForeColor = System.Drawing.Color.White;
            this.labelX5.Location = new System.Drawing.Point(455, 279);
            this.labelX5.Margin = new System.Windows.Forms.Padding(20);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(100, 47);
            this.labelX5.TabIndex = 19;
            this.labelX5.Text = "余额:";
            // 
            // labelX1
            // 
            this.labelX1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(452, 119);
            this.labelX1.Margin = new System.Windows.Forms.Padding(20);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.Color.Transparent;
            this.labelX1.Size = new System.Drawing.Size(100, 47);
            this.labelX1.TabIndex = 15;
            this.labelX1.Text = "姓名:";
            // 
            // labelX4
            // 
            this.labelX4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.ForeColor = System.Drawing.Color.White;
            this.labelX4.Location = new System.Drawing.Point(452, 203);
            this.labelX4.Margin = new System.Windows.Forms.Padding(20);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(100, 47);
            this.labelX4.TabIndex = 17;
            this.labelX4.Text = "卡号:";
            // 
            // labelX6
            // 
            this.labelX6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("微软雅黑", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelX6.Location = new System.Drawing.Point(91, 572);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(210, 72);
            this.labelX6.TabIndex = 32;
            this.labelX6.Text = "请放托盘";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTime.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.White;
            this.lbTime.Location = new System.Drawing.Point(492, 31);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(365, 54);
            this.lbTime.TabIndex = 62;
            this.lbTime.Text = "2020/01/01 11:40:50";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(1187, 31);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(58, 54);
            this.btnExit.TabIndex = 82;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // picTime
            // 
            this.picTime.BackColor = System.Drawing.Color.Transparent;
            this.picTime.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picTime.BackgroundImage")));
            this.picTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picTime.Location = new System.Drawing.Point(388, 27);
            this.picTime.Name = "picTime";
            this.picTime.Size = new System.Drawing.Size(55, 58);
            this.picTime.TabIndex = 52;
            this.picTime.TabStop = false;
            // 
            // msgGrid
            // 
            this.msgGrid.AllowUserToAddRows = false;
            this.msgGrid.AllowUserToDeleteRows = false;
            this.msgGrid.AllowUserToResizeColumns = false;
            this.msgGrid.AllowUserToResizeRows = false;
            this.msgGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.msgGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msgGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.msgGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.msgGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.msgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.msgGrid.ColumnHeadersVisible = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(10);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.msgGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.msgGrid.EnableHeadersVisualStyles = false;
            this.msgGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.msgGrid.Location = new System.Drawing.Point(18, 15);
            this.msgGrid.MultiSelect = false;
            this.msgGrid.Name = "msgGrid";
            this.msgGrid.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.msgGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.msgGrid.RowHeadersVisible = false;
            this.msgGrid.RowTemplate.Height = 50;
            this.msgGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.msgGrid.Size = new System.Drawing.Size(368, 388);
            this.msgGrid.TabIndex = 5;
            this.msgGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.msgGrid_DataError);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 48);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelEx
            // 
            this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx.Controls.Add(this.btnWPay);
            this.panelEx.Controls.Add(this.panelConsumptionType);
            this.panelEx.Controls.Add(this.PanelInfo);
            this.panelEx.Controls.Add(this.exPanelCount);
            this.panelEx.Controls.Add(this.exPanelMoney);
            this.panelEx.Controls.Add(this.btnCardPay);
            this.panelEx.Controls.Add(this.btnSet);
            this.panelEx.Controls.Add(this.btnExit);
            this.panelEx.Controls.Add(this.lbTime);
            this.panelEx.Controls.Add(this.picTime);
            this.panelEx.Controls.Add(this.picTitle);
            this.panelEx.Controls.Add(this.labelX6);
            this.panelEx.Controls.Add(this.txtMoney);
            this.panelEx.Controls.Add(this.txtName);
            this.panelEx.Controls.Add(this.labelX5);
            this.panelEx.Controls.Add(this.labelX1);
            this.panelEx.Controls.Add(this.txtCard);
            this.panelEx.Controls.Add(this.labelX4);
            this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx.Location = new System.Drawing.Point(3, 3);
            this.panelEx.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx.Name = "panelEx";
            this.panelEx.Size = new System.Drawing.Size(1314, 667);
            this.panelEx.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx.Style.BackColor1.Color = System.Drawing.Color.Transparent;
            this.panelEx.Style.BackColor2.Color = System.Drawing.Color.Transparent;
            this.panelEx.Style.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelEx.Style.BackgroundImage")));
            this.panelEx.Style.BorderColor.Color = System.Drawing.Color.Transparent;
            this.panelEx.Style.BorderWidth = 0;
            this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx.Style.GradientAngle = 90;
            this.panelEx.TabIndex = 115;
            // 
            // btnWPay
            // 
            this.btnWPay.BackColor = System.Drawing.Color.Transparent;
            this.btnWPay.BackgroundImage = global::SettlementTool.Properties.Resources.SaoPay;
            this.btnWPay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWPay.Location = new System.Drawing.Point(1083, 610);
            this.btnWPay.Name = "btnWPay";
            this.btnWPay.Size = new System.Drawing.Size(58, 54);
            this.btnWPay.TabIndex = 159;
            this.btnWPay.TabStop = false;
            this.btnWPay.Click += new System.EventHandler(this.btnWPay_Click);
            // 
            // panelConsumptionType
            // 
            this.panelConsumptionType.BackColor = System.Drawing.Color.White;
            this.panelConsumptionType.HeadForeColor = System.Drawing.Color.Black;
            this.panelConsumptionType.Location = new System.Drawing.Point(869, 119);
            this.panelConsumptionType.Name = "panelConsumptionType";
            this.panelConsumptionType.Size = new System.Drawing.Size(360, 207);
            this.panelConsumptionType.TabIndex = 158;
            this.panelConsumptionType.TextX = "ID Card";
            // 
            // PanelInfo
            // 
            this.PanelInfo.BackColor = System.Drawing.Color.White;
            this.PanelInfo.Controls.Add(this.msgGrid);
            this.PanelInfo.HeadForeColor = System.Drawing.Color.White;
            this.PanelInfo.Location = new System.Drawing.Point(31, 128);
            this.PanelInfo.Name = "PanelInfo";
            this.PanelInfo.Size = new System.Drawing.Size(401, 415);
            this.PanelInfo.TabIndex = 154;
            this.PanelInfo.TextX = "";
            // 
            // exPanelCount
            // 
            this.exPanelCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.exPanelCount.Controls.Add(this.labelX3);
            this.exPanelCount.Controls.Add(this.lbCardCount);
            this.exPanelCount.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exPanelCount.HeadBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.exPanelCount.HeadBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.exPanelCount.HeadForeColor = System.Drawing.Color.White;
            this.exPanelCount.HeadHeight = 90;
            this.exPanelCount.HeadText = "数量";
            this.exPanelCount.LinearScale = 0.4F;
            this.exPanelCount.Location = new System.Drawing.Point(869, 373);
            this.exPanelCount.Name = "exPanelCount";
            this.exPanelCount.Size = new System.Drawing.Size(376, 231);
            this.exPanelCount.TabIndex = 6;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(213, 127);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(73, 66);
            this.labelX3.TabIndex = 7;
            this.labelX3.Text = "份";
            // 
            // lbCardCount
            // 
            this.lbCardCount.AutoSize = true;
            this.lbCardCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbCardCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCardCount.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardCount.Location = new System.Drawing.Point(86, 128);
            this.lbCardCount.Name = "lbCardCount";
            this.lbCardCount.Size = new System.Drawing.Size(43, 64);
            this.lbCardCount.TabIndex = 6;
            this.lbCardCount.Text = "0";
            // 
            // exPanelMoney
            // 
            this.exPanelMoney.BackColor = System.Drawing.Color.WhiteSmoke;
            this.exPanelMoney.Controls.Add(this.lbMoney);
            this.exPanelMoney.Controls.Add(this.labelX2);
            this.exPanelMoney.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exPanelMoney.HeadBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.exPanelMoney.HeadBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.exPanelMoney.HeadForeColor = System.Drawing.Color.White;
            this.exPanelMoney.HeadHeight = 90;
            this.exPanelMoney.HeadText = "本次消费金额";
            this.exPanelMoney.LinearScale = 0.4F;
            this.exPanelMoney.Location = new System.Drawing.Point(468, 373);
            this.exPanelMoney.Name = "exPanelMoney";
            this.exPanelMoney.Size = new System.Drawing.Size(355, 231);
            this.exPanelMoney.TabIndex = 6;
            // 
            // lbMoney
            // 
            this.lbMoney.AutoSize = true;
            this.lbMoney.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbMoney.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbMoney.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMoney.Location = new System.Drawing.Point(23, 127);
            this.lbMoney.Name = "lbMoney";
            this.lbMoney.Size = new System.Drawing.Size(160, 66);
            this.lbMoney.TabIndex = 4;
            this.lbMoney.Text = "￥0.00";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(220, 127);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(73, 66);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "元";
            // 
            // btnCardPay
            // 
            this.btnCardPay.BackColor = System.Drawing.Color.Transparent;
            this.btnCardPay.BackgroundImage = global::SettlementTool.Properties.Resources.CardPay;
            this.btnCardPay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCardPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCardPay.Location = new System.Drawing.Point(1187, 610);
            this.btnCardPay.Name = "btnCardPay";
            this.btnCardPay.Size = new System.Drawing.Size(58, 54);
            this.btnCardPay.TabIndex = 141;
            this.btnCardPay.TabStop = false;
            this.btnCardPay.Click += new System.EventHandler(this.btnCardPay_Click);
            // 
            // btnSet
            // 
            this.btnSet.BackColor = System.Drawing.Color.Transparent;
            this.btnSet.BackgroundImage = global::SettlementTool.Properties.Resources.Set;
            this.btnSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSet.Location = new System.Drawing.Point(1110, 31);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(58, 54);
            this.btnSet.TabIndex = 128;
            this.btnSet.TabStop = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // picTitle
            // 
            this.picTitle.BackColor = System.Drawing.Color.Transparent;
            this.picTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.picTitle.Location = new System.Drawing.Point(0, 0);
            this.picTitle.Name = "picTitle";
            this.picTitle.Size = new System.Drawing.Size(1314, 36);
            this.picTitle.TabIndex = 115;
            this.picTitle.TabStop = false;
            this.picTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picTitle_MouseDoubleClick);
            this.picTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panTitle_MouseDown);
            this.picTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panTitle_MouseMove);
            this.picTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panTitle_MouseUp);
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog1";
            this.printPreviewDialog.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1320, 673);
            this.Controls.Add(this.panelEx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 320);
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "顺盘结算软件";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgGrid)).EndInit();
            this.panelEx.ResumeLayout(false);
            this.panelEx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnWPay)).EndInit();
            this.PanelInfo.ResumeLayout(false);
            this.exPanelCount.ResumeLayout(false);
            this.exPanelCount.PerformLayout();
            this.exPanelMoney.ResumeLayout(false);
            this.exPanelMoney.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCardPay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX lbMoney;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX lbCardCount;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCard;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMoney;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.PictureBox picTime;
        private DevComponents.DotNetBar.LabelX lbTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox btnExit;
        private System.Windows.Forms.ImageList imageList;
        private DevComponents.DotNetBar.Controls.DataGridViewX msgGrid;
        private DevComponents.DotNetBar.PanelEx panelEx;
        private System.Windows.Forms.PictureBox picTitle;
        private System.Windows.Forms.PictureBox btnSet;
        private System.Windows.Forms.PictureBox btnCardPay;
        private HeadPanelEx exPanelCount;
        private HeadPanelEx exPanelMoney;
        private BullNosePanelEx PanelInfo;
        private BullNosePanelEx panelConsumptionType;
        private System.Windows.Forms.PictureBox btnWPay;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
    }
}