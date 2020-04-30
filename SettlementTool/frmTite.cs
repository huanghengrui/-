using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SettlementTool
{
    public partial class frmTite : Form
    {
        public string title = "";
        public string Card = "";
        public string CName = "";
        public string Money = "";
        public string Balance = "";
        public frmTite(string Title)
        {
            title = Title;
            InitializeComponent();  
        }

        public frmTite(string name,string card,string money,string balance)
        {
            CName = name;
            Card = card;
            Money = money;
            Balance = balance;
            InitializeComponent();
        }

        private void frmTite_Load(object sender, EventArgs e)
        {
            lbTTT.Text = "消费成功";
            lbName.Text = "         " + CName;
            lbCard.Text = "         " + Card;
            lbBalance.Text = "         " + Balance;
            lbMoney.Text = Money;
            SystemInfo.IsWorking = false;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.BackColor = Color.Gainsboro;
            label1.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
