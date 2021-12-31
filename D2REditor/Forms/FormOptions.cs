using D2SLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormOptions : Form
    {
        private Bitmap closebmp;
        public FormOptions()
        {
            InitializeComponent();
        }

        private void btnBrowseD2RFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (DialogResult.OK == fbd.ShowDialog())
            {
                var files = Directory.GetFiles(fbd.SelectedPath, "*.d2*");
                if (files.Length > 0)
                {
                    tbD2RFolder.Text = fbd.SelectedPath;
                }
                else
                {
                    MessageBox.Show(Utils.AllJsons["d2r_not_found"]);
                }
            }
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            lbLanguages.DataSource = Helper.LanguageMappings;
            lbLanguages.DisplayMember = "Name";
            lbLanguages.ValueMember = "Key";

            btnOk.ImageFile = Helper.GeneralButtonImageFile;
            btnCancel.ImageFile = Helper.GeneralButtonImageFile;

            Helper.RefreshSettings();

            lbLanguages.SelectedItem = Helper.LanguageMappings.Where(lang => lang.Key == Helper.CurrentLanguage).FirstOrDefault();
            tbD2RFolder.Text = Helper.DefaultD2RFolder;



            var back = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_base");
            var backbmp = Helper.Sprite2Png(back);
            this.BackgroundImage = backbmp;

            var close = Helper.GetDefinitionFileName(@"\lobby\friendslist\friendslist_rejectinvite_button");
            closebmp = Helper.GetImageByFrame(Helper.Sprite2Png(close), 3, 0);

            tcOptions.ItemSize = new Size(0, 1);
            var imgname = Helper.GetDefinitionFileName(@"\panel\stash\stash_tabs");
            var img = Helper.Sprite2Png(imgname);
            downimg = Helper.GetImageByFrame(img, 2, 1);
            upimg = Helper.GetImageByFrame(img, 2, 0);

            tbCacheLocation.Text = Helper.CacheFolder;

            RefreshLanguage();

            this.MouseUp += FormOptions_MouseUp;
            this.Paint += FormOptions_Paint;
        }

        private void FormOptions_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X >= this.Width - 24 && e.X < this.Width)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            //if(e.X>= 75 + upimg.Width * tcOptions.SelectedIndex)
            if (e.Y >= 100 && e.Y <= 100 + upimg.Height)
            {
                var index = (e.X - 75.0) / upimg.Width;
                if (index < 0 || index >= tcOptions.TabCount) return;

                tcOptions.SelectedIndex = (int)index;
                this.Invalidate();
            }
        }

        private Bitmap downimg, upimg;
        private void FormOptions_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(closebmp, this.Width - 24, 0);
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                g.DrawString(Utils.AllJsons["GameplayOptions"], this.Font, Brushes.White, new Rectangle(0, 40, this.Width, this.Height - 4), sf);
            }

            for (int i = 0; i < tcOptions.TabPages.Count; i++)
            {
                if (i == tcOptions.SelectedIndex)
                {
                    g.DrawImage(downimg, 75 + upimg.Width * i, 100);
                }
                else
                {
                    g.DrawImage(upimg, 75 + upimg.Width * i, 100);
                }

                using (Font f = new Font("SimSun", 12, FontStyle.Regular))
                {
                    using (var sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        //if(i==tcOptions.SelectedIndex)f.Style.
                        g.DrawString(tcOptions.TabPages[i].Text, f, Helper.TextBrush,
                            new Rectangle(75 + upimg.Width * i, 110, upimg.Width, upimg.Height),
                            sf);
                    }
                }
            }


            e.Graphics.DrawImage(bmp, 0, 0);

            g.Dispose();
            bmp.Dispose();
        }

        private void RefreshLanguage()
        {
            Utils.ResetAll();

            btnOk.Text = Utils.AllJsons["ok"];
            btnCancel.Text = Utils.AllJsons["cancel"];
            tpLanguage.Text = Utils.AllJsons["LanguageOptions"];
            tpFile.Text = Utils.AllJsons["file&cache"];
            btnOpenLanguageFile.Text = Utils.AllJsons["customized_language_file"];

            labelGameFile.Text = Utils.AllJsons["file_location"];
            labelCache.Text = Utils.AllJsons["cache_location"];

            this.Invalidate();
        }

        private void WriteSettings()
        {
            var lines = new List<string>();
            lines.Add("language=" + (lbLanguages.SelectedItem as LanguageMapping).Key);
            lines.Add("d2rfolder=" + tbD2RFolder.Text);

            File.WriteAllLines(Helper.CacheFolder + "\\settings.txt", lines.ToArray());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;

            Helper.CurrentLanguage = (lbLanguages.SelectedItem as LanguageMapping).Key;
            Utils.ResetAll();
            WriteSettings();
            Helper.RefreshSettings();
            this.Close();
        }

        private bool ValidateData()
        {
            if (!Directory.Exists(tbD2RFolder.Text))
            {
                MessageBox.Show(Utils.AllJsons["dir_not_found"]);
                return false;
            }

            if (!tbD2RFolder.Text.EndsWith("\\")) tbD2RFolder.Text += "\\";
            if (lbLanguages.SelectedIndex < 0) lbLanguages.SelectedIndex = 0;

            return true;
        }

        private void lbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (init) return;
            //Helper.CurrentLanguage = (lbLanguages.SelectedItem as LanguageMapping).Key;
            //RefreshLanguage();
        }

        private void tcOptions_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
        }

        private void btnOpenLanguageFile_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Helper.CacheFolder + @"\strings\fqq.json");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
