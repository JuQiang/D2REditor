using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class CheckBoxEx : UserControl
    {
        private bool _checked = false;
        private Bitmap backbmp, checkbmp;
        private bool enter;

        public CheckBoxEx()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                var back = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_button_disabled");
                backbmp = Helper.GetImageByFrame(Helper.Sprite2Png(back), 2, 0);

                var check = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_button_active");
                checkbmp = Helper.GetImageByFrame(Helper.Sprite2Png(check), 3, 0);
            }
            enter = false;

            this.SizeChanged += CheckBoxEx_SizeChanged;
            this.MouseEnter += CheckBoxEx_MouseEnter;
            this.MouseLeave += CheckBoxEx_MouseLeave;
            this.MouseUp += CheckBoxEx_MouseUp;
            this.Paint += CheckBoxEx_Paint;

            this.Invalidate();
        }

        private void CheckBoxEx_MouseUp(object sender, MouseEventArgs e)
        {
            this._checked = !this._checked;
            this.Invalidate();
        }

        private void CheckBoxEx_MouseLeave(object sender, EventArgs e)
        {
            enter = false;
            this.Invalidate();
        }

        private void CheckBoxEx_MouseEnter(object sender, EventArgs e)
        {
            enter = true;
            this.Invalidate();
        }

        private void CheckBoxEx_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if ((!this.DesignMode) && (LicenseManager.UsageMode != LicenseUsageMode.Designtime))
            {
                var r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                if (this._checked)
                {
                    g.DrawImage(checkbmp, r, new Rectangle(0, 0, checkbmp.Width, checkbmp.Height), GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(backbmp, r, new Rectangle(0, 0, backbmp.Width, backbmp.Height), GraphicsUnit.Pixel);
                }

                if (enter) g.DrawRectangle(Pens.Wheat, r);
            }

            using (var brush = new SolidBrush(this.ForeColor))
            {

                var sf = g.MeasureString(this.Text, this.Font);
                g.DrawString(this.Text, this.Font, brush, 80, (this.Height - sf.Height) / 2 + 2);
            }


        }

        private void CheckBoxEx_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text { get; set; }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Checked
        {
            get
            {
                return this._checked;
            }
            set
            {
                this._checked = value;
                this.Invalidate();
            }
        }
    }
}
