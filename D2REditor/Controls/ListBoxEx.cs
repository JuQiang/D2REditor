using D2SLib.Model.Save;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class ListBoxEx : ListBox
    {
        Bitmap titlebmp = null;
        Bitmap borderbmp = null;
        int width = 226;
        int height = 70;
        Brush brush = new SolidBrush(Color.FromArgb(255, 199, 179, 119));

        public ListBoxEx()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.Black;

            var border = Helper.GetDefinitionFileName(@"\controller\hoverimages\characterlistselection_hover");
            borderbmp = Helper.Sprite2Png(border);
            var title = Helper.GetDefinitionFileName(@"\frontend\hd\final\frontend_charactertile");
            titlebmp = Helper.GetImageByFrame(Helper.Sprite2Png(title), 3, 0);

            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.DoubleBuffered = true;
        }

        private void ListBoxEx_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var cha = this.Items[e.Index] as D2S;

            Bitmap bmp = new Bitmap(e.Bounds.Width, e.Bounds.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(titlebmp, new Rectangle(0, 0, width, height));

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                g.DrawImage(borderbmp, new Rectangle(0, 0, width, height));
            }

            using (Font f = new Font(Helper.CurrentFontFamily, 9, FontStyle.Bold))
            {
                g.DrawString(cha.Title, f, brush, 8, 8);
                g.DrawString(cha.FileName, f, Brushes.White, 8, 26);
                g.DrawString(String.Format("等级{0} {1}", cha.Level, cha.ClassName), f, brush, 8, 44);
                g.DrawString(cha.LastPlayedString, f, brush, 154, 44);
            }

            e.Graphics.DrawImage(bmp, e.Bounds.X, e.Bounds.Y);
        }

        private void ListBoxEx_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemWidth = this.width;
            e.ItemHeight = this.height;
        }
    }
}
