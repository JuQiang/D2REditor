using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormEx : Form
    {
        private Bitmap closebmp;
        public FormEx()
        {
            InitializeComponent();

            var close = Helper.GetDefinitionFileName(@"\lobby\friendslist\friendslist_rejectinvite_button");
            closebmp = Helper.GetImageByFrame(Helper.Sprite2Png(close), 3, 0);

            this.MouseUp += FormEx_MouseUp;
            this.Paint += FormEx_Paint;
        }

        private void FormEx_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X >= this.Width - 55 && e.X < this.Width && e.Y >= 0 && e.Y < 55)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void FormEx_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(closebmp, this.Width - 55, 0);

            e.Graphics.DrawImage(bmp, 0, 0);

            g.Dispose(); g = null;
            bmp.Dispose(); bmp = null;
        }
    }
}
