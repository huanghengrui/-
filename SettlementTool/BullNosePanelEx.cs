using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace SettlementTool
{
    public partial class BullNosePanelEx : Panel
    {
        public BullNosePanelEx()
        {
            InitializeComponent();
        }

        public BullNosePanelEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region 属性
     
        private Color headForeColor = Color.White;

        [Category("自定义控件"), Description("标题字体颜色")]
        public Color HeadForeColor
        {
            get { return headForeColor; }
            set
            {
                headForeColor = value;
                this.Invalidate();
            }
        }

        private string textX="";

        [Category("自定义控件"), Description("内容")]
        public string TextX
        {
            get { return textX; }
            set
            {
                textX = value;
                this.Invalidate();
            }
        }
    
        #endregion

        #region
        private Graphics g;
        private Pen p;
        private SolidBrush sb;
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //自定义绘制过程

            //获取画布
            g = e.Graphics;

            //设置画布
            SetGraphics();

            //绘制过程
            //绘制圆角
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(
                new Point[] {
            new Point(0, this.Height / 20),
            new Point(this.Width / 20, 0),
            new Point(this.Width - this.Width / 20, 0),
            new Point(this.Width, this.Height / 20),
            new Point(this.Width, this.Height - this.Height / 20),
            new Point(this.Width - this.Width / 20, this.Height),
            new Point(this.Width / 20, this.Height),
            new Point(0, this.Height - this.Height / 20) },
                (float)0.1);

            this.Region = new Region(oPath);

            //绘制名称
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            using(SolidBrush sb = new SolidBrush(this.headForeColor))
            {
                g.DrawString(this.textX, this.Font, sb, new RectangleF(0, 0, this.Width, this.Height), sf);
            }

         
        }

        private void SetGraphics()
        {
            //设置画布的属性
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
    }
}
