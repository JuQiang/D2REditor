using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormOpenD2R : Form
    {
        Bitmap back;
        private string folder;
        public FormOpenD2R()
        {
            InitializeComponent();
        }

        public FormOpenD2R(string folder)
        {
            this.folder = folder;
        }

        private void FormOpenD2R_Load(object sender, EventArgs e)
        {
            back = Image.FromFile("fileback2.png") as Bitmap;

            lbFiles.BorderStyle = BorderStyle.None;

            var files = Directory.GetFiles(this.folder, "*.d2s");
            foreach (var file in files)
            {
                lbFiles.Items.Add(file);
            }
        }

        private void lbFiles_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemWidth = back.Width;
            e.ItemHeight = 113;
        }

        private void lbFiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            e.Graphics.DrawImage(back, e.Bounds.X, e.Bounds.Y);
            e.Graphics.DrawString(lbFiles.Items[e.Index].ToString(), this.Font, Brushes.White, e.Bounds.X + 40, e.Bounds.Y + 40);
        }

    }
}
