using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class ListViewEx : ListView
    {
        public ListViewEx()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.SetHeight(256);
            this.OwnerDraw = true;

            this.DrawItem += ListViewEx_DrawItem;
            this.DrawColumnHeader += ListViewEx_DrawColumnHeader;
        }

        private void ListViewEx_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        private void ListViewEx_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.ListView.View == View.Details) return;

            ListViewItem item = e.Item;
            e.Graphics.DrawRectangle(Pens.Blue, e.Bounds);
            e.Graphics.DrawString(item.Text, this.Font, Brushes.Red, e.Bounds.X + 4, e.Bounds.Y + 4);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(Color.Black);
            //base.OnPaintBackground(pevent);
        }

        private void SetHeight(int height)
        {
            ImageList imglist = new ImageList();
            imglist.ImageSize = new Size(1, height);
            this.SmallImageList = imglist;
        }
    }
}
