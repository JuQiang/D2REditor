using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class PanelEx : Panel
    {
        public PanelEx()
        {
            InitializeComponent();
        }

        public new void SetStyle(ControlStyles flag, bool value)
        {
            base.SetStyle(flag, value);
        }

        public new void UpdateStyles()
        {
            base.UpdateStyles();
        }
    }
}
