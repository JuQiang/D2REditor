using D2REditor.Controls;
using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormCreateNewCharactor : Form
    {
        Bitmap backbmp, closebmp;
        int curclass = 0;
        string[] classes = new string[] { "amazonicon", "sorceressicon", "necromancericon", "paladinicon", "barbarianicon", "druidicon", "assassinicon" };
        string[] descriptions = new string[] { Utils.AllJsons["Amazon"], Utils.AllJsons["Sorceress"], Utils.AllJsons["Necromancer"], Utils.AllJsons["Paladin"], Utils.AllJsons["Barbarian"], Utils.AllJsons["druidstr"], Utils.AllJsons["assassinstr"] };
        string[] descriptions2 = new string[] { Utils.AllJsons["strAmazonDesc"], Utils.AllJsons["strSorcDesc"], Utils.AllJsons["strNecroDesc"], Utils.AllJsons["strPalDesc"], Utils.AllJsons["strBarbDesc"], Utils.AllJsons["strDruDesc"], Utils.AllJsons["strAssDesc"] };
        Button[] btnClasses = new Button[7];
        Bitmap[] classesbmp = new Bitmap[7];
        TextBox tbName;
        ButtonEx btnCreateNewCharactor;
        List<byte[]> charpack = new List<byte[]>();

        public FormCreateNewCharactor()
        {
            InitializeComponent();
        }

        private void CreateNewCharactorForm_Load(object sender, EventArgs e)
        {
            this.Paint += CreateNewCharactorForm_Paint;

            var back = Helper.GetDefinitionFileName(@"\panel\hireling\hireablepanel\hireables_bg");
            backbmp = Helper.Sprite2Png(back);

            var close = Helper.GetDefinitionFileName(@"\lobby\friendslist\friendslist_rejectinvite_button");
            closebmp = Helper.GetImageByFrame(Helper.Sprite2Png(close), 3, 0);

            var lines = File.ReadAllLines(Helper.CacheFolder + @"\assets\charpack.txt");

            for (int i = 0; i < classes.Length; i++)
            {
                classesbmp[i] = Helper.Sprite2Png(Helper.GetDefinitionFileName(@"\hireables\" + classes[i]));
                charpack.Add(Convert.FromBase64String(lines[i]));
                lbCharacterList.Items.Add(i);
            }

            tbName = new TextBox();
            tbName.Text = Utils.AllJsons["inpurt_hero_name_here"];
            tbName.Location = new Point((int)(80*Helper.DisplayRatio), (int)(560 * Helper.DisplayRatio));
            tbName.Width = (int)(420 * Helper.DisplayRatio);
            this.Controls.Add(tbName);

            btnCreateNewCharactor = new ButtonEx();
            btnCreateNewCharactor.Text = Utils.AllJsons["strlaunchcreatenewcharexp"];
            btnCreateNewCharactor.Location = new Point((int)(195*Helper.DisplayRatio), (int)(625 * Helper.DisplayRatio));
            btnCreateNewCharactor.Size = new Size((int)(190 * Helper.DisplayRatio), (int)(60 * Helper.DisplayRatio));
            btnCreateNewCharactor.Click += BtnCreateNewCharactor_Click;
            this.Controls.Add(btnCreateNewCharactor);

            this.MouseUp += CreateNewCharactorForm_MouseUp;
        }

        private void BtnCreateNewCharactor_Click(object sender, EventArgs e)
        {
            if (curclass < 0) return;

            string name = tbName.Text;
            if ((!char.IsLetter(name[0])) || (Encoding.Default.GetBytes(name).Length > 15) || (Encoding.Default.GetBytes(name).Length < 2))
            {
                MessageBox.Show(Utils.AllJsons["strBnetAccountBlank"]);
                return;
            }

            var fname = Helper.DefaultD2RFolder + tbName.Text + ".d2s";
            if (File.Exists(fname))
            {
                MessageBox.Show(Utils.AllJsons["strBnetAccountBadChars"]);
                return;
            }
            var d2s = D2S.Read(charpack[curclass]);

            d2s.LastPlayed = (uint)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks / 10000000);
            File.WriteAllBytes(fname, Core.WriteD2S(d2s));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CreateNewCharactorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X >=  this.Width - closebmp.Width && e.X < this.Width && e.Y >= 0 && e.Y < closebmp.Height)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            for (int i = 0; i < classes.Length; i++)
            {
                if (e.X >= 140*Helper.DisplayRatio && e.X < 500 * Helper.DisplayRatio && e.Y >= (90+ 65 * i) * Helper.DisplayRatio && e.Y < (90 + 65 * i + 65) * Helper.DisplayRatio)
                {
                    //System.Diagnostics.Debug.WriteLine("Class=" + i.ToString());
                    curclass = i;
                    btnClasses[i].PerformClick();
                    btnClasses[i].Focus();
                    break;
                }
            }
        }

        private void lbCharacterList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var g = e.Graphics;


            //g.Clear(Color.Black);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
                //g.DrawRectangle(Pens.White, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds);
            }

            g.DrawImage(classesbmp[e.Index], e.Bounds.X, e.Bounds.Y + 3);


            g.DrawString(descriptions[e.Index], this.Font, Helper.TextBrush, e.Bounds.X + classesbmp[e.Index].Width, e.Bounds.Y + 3);
            using (Font f = new Font(Helper.CurrentFontFamily, 9))
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;

                    g.DrawString(descriptions2[e.Index], f, Brushes.White, new Rectangle(e.Bounds.X + classesbmp[e.Index].Width, e.Bounds.Y + (int)(20 * Helper.DisplayRatio), this.Width - (int)(220 * Helper.DisplayRatio), (int)(50*Helper.DisplayRatio)), sf);
                }
            }
        }

        private void lbCharacterList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)(66 * Helper.DisplayRatio);
        }

        private void lbCharacterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCharacterList.SelectedIndex >= 0) curclass = lbCharacterList.SelectedIndex;
        }

        private void CreateNewCharactorForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g2 = e.Graphics;
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(backbmp, 0, 0);
            var title = Utils.AllJsons["strSelectHeroClass"];
            var size = g.MeasureString(title, this.Font);
            g.DrawString(title, this.Font, Helper.TextBrush, (this.Width - size.Width) / 2, 40 * Helper.DisplayRatio);
            g.DrawImage(closebmp, this.Width - closebmp.Width, 0);

            g2.DrawImage(bmp, 0, 0);
        }
    }
}
