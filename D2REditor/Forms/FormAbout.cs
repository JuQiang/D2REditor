using D2SLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            link1.Links.Add(0, link1.Text.Length, "https://github.com/dschu012/D2SLib");
            link2.Links.Add(0, link2.Text.Length, "http://user.xmission.com/~trevin/DiabloIIv1.09_File_Format.shtml");
            link3.Links.Add(0, link3.Text.Length, "http://user.xmission.com/~trevin/DiabloIIv1.09_Item_Format.shtml");
            link4.Links.Add(0, link4.Text.Length, "https://d2mods.info/forum/viewtopic.php?t=724#p148076");
            link5.Links.Add(0, link5.Text.Length, "https://github.com/eezstreet/D2RModding-SpriteEdit");
            link6.Links.Add(0, link6.Text.Length, "https://www.github.com/juqiang");

            link1.LinkClicked += (sender2, ae) => ClickLinkLabel(ae.Link.LinkData.ToString());
            link2.LinkClicked += (sender2, ae) => ClickLinkLabel(ae.Link.LinkData.ToString());
            link3.LinkClicked += (sender2, ae) => ClickLinkLabel(ae.Link.LinkData.ToString());
            link4.LinkClicked += (sender2, ae) => ClickLinkLabel(ae.Link.LinkData.ToString());
            link5.LinkClicked += (sender2, ae) => ClickLinkLabel(ae.Link.LinkData.ToString());
            link6.LinkClicked += (sender2, ae) => ClickLinkLabel(ae.Link.LinkData.ToString());

            this.Text = Utils.AllJsons["about"];

            if (File.Exists("updatelog.txt"))
            {
                tbUpdateLog.Text = File.ReadAllText("updatelog.txt");
            }
        }
        private void ClickLinkLabel(string url)
        {
            Process.Start(url);
        }
    }
}
