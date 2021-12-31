using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class TabControlEx : TabControl
    {
        private Bitmap downimg, upimg;

        public TabControlEx()
        {
            InitializeComponent();

            var imgname = Helper.GetDefinitionFileName(@"\panel\stash\stash_tabs");
            var img = Helper.Sprite2Png(imgname);
            downimg = Helper.GetImageByFrame(img, 2, 1);
            upimg = Helper.GetImageByFrame(img, 2, 0);

            this.ItemSize = new Size(downimg.Width, downimg.Height);
            //this.DrawMode = TabDrawMode.Normal;//.OwnerDrawFixed;
            //this.SizeMode = TabSizeMode.Fixed;

            this.DrawItem += TabControlEx_DrawItem;

            //stackoverflow: 3115321, drawitem not fired, userpaint=true
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);

            Graphics g = pevent.Graphics;
            g.Clear(Color.Black);

            foreach (TabPage tp in this.TabPages)
            {
                var tabRect = this.GetTabRect(this.TabPages.IndexOf(tp));
                g.DrawImage(upimg, tabRect);
                g.DrawString(tp.Text, this.Font, Brushes.White, 4, 4);
            }
        }
        private void TabControlEx_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            var tabRect = this.GetTabRect(e.Index);
            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.DrawImage(downimg, tabRect);
            }
            else
            {
                e.Graphics.DrawImage(upimg, tabRect);
            }
            e.Graphics.DrawString(this.TabPages[e.Index].Text, this.Font, Brushes.White, tabRect.X + 4, tabRect.Y + 4);
        }

        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    Graphics g = pevent.Graphics;
        //    g.Clear(Color.Transparent);

        //    foreach (TabPage tp in this.TabPages)
        //    {
        //        var tabRect = this.GetTabRect(this.TabPages.IndexOf(tp));
        //        g.DrawImage(upimg, tabRect);
        //        g.DrawString(tp.Text, this.Font, Brushes.White, 4, 4);
        //    }
        //}

        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


        }
    }
}
