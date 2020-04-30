using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SettlementTool
{
    public partial class ButtonEx : Panel
    {
        Color color = new Color();
        public ButtonEx()
        {
            InitializeComponent();
            color = this.BackColor;
        }

        private void ButtonEx_MouseEnter(object sender, EventArgs e)
        {
            int cR = (int)(this.BackColor.R - this.BackColor.R * 0.1);
            int cG = (int)(this.BackColor.G - this.BackColor.G * 0.1);
            int cB = (int)(this.BackColor.B - this.BackColor.B * 0.1);
            this.BackColor = Color.FromArgb(cR, cG, cB);
        }

        private void ButtonEx_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = color;
        }

        private Color backColorEx = Color.Gainsboro;

        [Category("自定义控件"), Description("背景颜色")]
        public Color BackColorEx
        {
            get { return backColorEx; }
            set
            {
                backColorEx = value;
                color = value;
                this.BackColor = value;
            }
        }
    }
}
