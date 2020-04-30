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
    public partial class HeadPanelEx : Panel
    {
        public HeadPanelEx()
        {
            InitializeComponent();
        }

        public HeadPanelEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region 属性
        private Color headBackColor1 = Color.FromArgb(67, 78, 84);

        [Category("自定义控件"),Description("标题背景颜色1")]
        public Color HeadBackColor1
        {
            get { return headBackColor1; }
            set { 
                    headBackColor1 = value;
                    this.Invalidate();
                }
        }

        private Color headBackColor2 = Color.FromArgb(67, 78, 84);

        [Category("自定义控件"), Description("标题背景颜色2")]
        public Color HeadBackColor2
        {
            get { return headBackColor2; }
            set
            {
                headBackColor2 = value;
                this.Invalidate();
            }
        }

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

        private int headHeight = 30;

        [Category("自定义控件"), Description("标题高度")]
        public int HeadHeight
        {
            get { return headHeight; }
            set
            {
                headHeight = value;
                this.Invalidate();
            }
        }

        private string headText="标题名称";

        [Category("自定义控件"), Description("标题名称")]
        public string HeadText
        {
            get { return headText; }
            set
            {
                headText = value;
                this.Invalidate();
            }
        }

        private float linearScale=0.4F;

        [Category("自定义控件"), Description("渐变程度")]
        public float LinearScale
        {
            get { return linearScale; }
            set
            {
                linearScale = value;
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

            using (LinearGradientBrush brush = new LinearGradientBrush(new PointF(0, 0), new PointF(0, this.headHeight), this.headBackColor1, this.headBackColor2))
            {
                g.FillRectangle(brush, new RectangleF(0, 0, this.Width, this.headHeight));
            }
            //绘制标题名称
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            using(SolidBrush sb = new SolidBrush(this.headForeColor))
            {
                g.DrawString(this.headText, this.Font, sb, new RectangleF(0, 0, this.Width, this.headHeight), sf);
            }

            ////绘制边框
            //using (Pen p = new Pen(this.headBackColor1))
            //{
            //    g.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            //}

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
        }

        private void SetGraphics()
        {
            //设置画布的属性
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }

        private Color GetStarLinearColor(Color EndLinearColor)
        {
            return Color.FromArgb((int)(EndLinearColor.R + (255 - EndLinearColor.R) * this.linearScale), (int)(EndLinearColor.B + (255 - EndLinearColor.B) * this.linearScale), (int)(EndLinearColor.G + (255 - EndLinearColor.G) * this.linearScale));
        }
    }
}
