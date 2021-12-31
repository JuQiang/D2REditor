using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class ButtonEx : Button
    {
        Bitmap[] buttonImages;
        public ButtonEx()
        {
            InitializeComponent();

            //this.Size = new Size(240, 62);
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.White;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Text = this.Name;


            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;

            //if (DesignMode) return;
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            this.imageFrames = 4;
            this.buttonImages = new Bitmap[this.imageFrames];

            this.ImageFile = Helper.GeneralButtonImageFile;
            this.MouseEnter += ButtonEx_MouseEnter;
            this.MouseLeave += ButtonEx_MouseLeave;
            //this.MouseDown += ButtonEx_MouseDown;
            this.MouseUp += ButtonEx_MouseUp;
            this.SizeChanged += ButtonEx_SizeChanged;
        }

        private string imageFile = "";
        private int imageFrames = 4;
        public int ImageFrames
        {
            get
            {
                return this.imageFrames;
            }
            set
            {
                this.imageFrames = value;
                buttonImages = new Bitmap[this.imageFrames];
                RefreshImageFile();
            }
        }
        public string ImageFile
        {
            get { return imageFile; }
            set
            {
                imageFile = value;
                RefreshImageFile();
            }
        }

        private void RefreshImageFile()
        {
            if (String.IsNullOrEmpty(this.imageFile)) return;

            var back = Helper.GetDefinitionFileName(this.imageFile);
            var png = Helper.Sprite2Png(back);

            for (int i = 0; i < this.imageFrames; i++) buttonImages[i] = Helper.GetImageByFrame(png, this.imageFrames, i);

            this.BackgroundImage = buttonImages[0];
        }
        private void ButtonEx_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = buttonImages[0];
        }

        private void ButtonEx_SizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = buttonImages[0];
        }

        private void ButtonEx_MouseDown(object sender, MouseEventArgs e)
        {
            //this.BackgroundImage = buttonImages[2];
        }

        private void ButtonEx_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = buttonImages[0];
        }

        private void ButtonEx_MouseEnter(object sender, EventArgs e)
        {
            if (DesignMode) return;
            if (this.imageFrames > 1) this.BackgroundImage = buttonImages[1];
            else this.BackgroundImage = buttonImages[0];
        }
    }
}
