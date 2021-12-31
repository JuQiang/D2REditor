using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormItemTooltip : Form
    {
        public FormItemTooltip()
        {
            InitializeComponent();
        }

        private Item item;
        private Rectangle rect;
        private int level;

        public Size UpdateItem(int level, Item item)
        {
            if (item == null) return Size.Empty;

            this.level = level;
            this.item = item;

            this.BackColor = Color.FromArgb(0, 0, 0);
            this.Opacity = 0.70d;
            this.Text = "";


            return this.MeasureSizeInfo();
        }

        private void FormEditItem_Load(object sender, EventArgs e)
        {
            //var bmp = Image.FromFile("background.png") as Bitmap;
            //bmp.MakeTransparent();
            //this.BackgroundImage = bmp;

        }

        private int margin;
        private int left, top, maxw, maxh;
        private Rectangle[] rectangles = new Rectangle[4];
        private string[] tooltips = new string[4];
        private Brush[] brushes = new Brush[4];

        public Size MeasureSizeInfo()
        {
            Size ret = new Size();

            if (this.item != null)
            {
                //this.Text = this.item.Name;

                margin = 0;
                left = 0; top = 5; maxw = 0; maxh = top;
                rectangles = new Rectangle[4];
                tooltips = new string[4] { this.item.Name, item.Type,Helper.GetBasicDescription(level, item), Helper.GetEnhancedDescription(level,item)};
                brushes = new Brush[] { item.NameColor, item.NameColor, Brushes.White, item.EnhancedColor };

                using (Graphics g = this.CreateGraphics())
                {
                    using (Font f = new Font("SimSun", Helper.DefinitionInfo.TooltipFontSize, FontStyle.Bold))
                    {
                        for (int i = 0; i < tooltips.Length; i++)
                        {
                            var sf = g.MeasureString(tooltips[i], f);
                            maxw = Math.Max((int)sf.Width, maxw);
                            rectangles[i] = new Rectangle(left, maxh, 0, (int)sf.Height);
                            maxh += (int)sf.Height + margin;
                        }


                    }
                }

                this.Width = maxw + 2 * left + 20;
                this.Height = maxh + 2 * top;

                ret.Width = this.Width;
                ret.Height = this.Height;

                for (int i = 0; i < 4; i++)
                {
                    rectangles[i].Width = ret.Width;
                }
            }

            return ret;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            System.Diagnostics.Debug.WriteLine(item.Code);

            using (Font f = new Font("SimSun", Helper.DefinitionInfo.TooltipFontSize, FontStyle.Bold))
            {
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;

                    for (int i = 0; i < tooltips.Length; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(tooltips[i]);
                        if (i == 1 && tooltips[0] == tooltips[1]) continue;

                        g.DrawString(tooltips[i], f, brushes[i], rectangles[i], format);
                    }
                }
            }
        }
    }
}
