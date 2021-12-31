using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormSelectD2R : Form
    {
        List<Bitmap> anilist = new List<Bitmap>();
        List<Bitmap> anilist2 = new List<Bitmap>();
        Timer timer;
        private Bitmap closebmp;

        int logoIndex = 0;
        string folder = "";
        Brush brush = new SolidBrush(Color.FromArgb(255, 111, 93, 77));

        void WriteLog(string msg)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(msg);
            }
        }
        public FormSelectD2R()
        {
            WriteLog("Before initialize component");
            InitializeComponent();
            WriteLog("after initialize component");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (logoIndex++ > 26) logoIndex = 0;

            pbLeftPanel.Invalidate();
        }


        private void FormSelectD2R_Load(object sender, EventArgs e)
        {
            WriteLog("Before load");
            Helper.RefreshSettings();

            InitData();
            WriteLog("after load");
        }

        private Dictionary<int, string> chamappings = new Dictionary<int, string>();
        private void InitData()
        {
            btnOptions.Text = Utils.AllJsons["OptionsHD"];
            btnAbout.Text = Utils.AllJsons["about"];
            btnEdit.Text = Utils.AllJsons["PlayTab"];
            btnCreateNew.Text = Utils.AllJsons["strLaunchCharSelectNew"];

            folder = Helper.DefaultD2RFolder;///TODO: 以后要可以选择存档目录
            if (!Directory.Exists(folder))
            {
                //btnEdit.Enabled = false;
                //btnCreateNew.Enabled = false;
                //btnDelete.Enabled = false;
                //btnRefresh.Enabled = false;
                MessageBox.Show(Utils.AllJsons["d2r_not_found"]);

                Helper.DefaultD2RFolder = Helper.CacheFolder;
                var lines = File.ReadAllLines(Helper.CacheFolder + @"\assets\charpack.txt");
                var d2s = D2S.Read(Convert.FromBase64String(lines[3]));

                d2s.LastPlayed = (uint)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks / 10000000);
                File.WriteAllBytes(Helper.CacheFolder + "\\paladin.d2s", Core.WriteD2S(d2s));

                List<byte> bytes = new List<byte>();
                for (int i = 0; i < 3; i++)
                {
                    byte[] header = new byte[0x40];
                    header[0] = 0x55; header[1] = 0xAA; header[2] = 0x55; header[3] = 0xAA;
                    header[8] = (byte)0x61;
                    header[0x0c] = 0xA0; header[0x0d] = 0x25; header[0x0e] = 0x26;

                    header[0x10] = 0x44;

                    bytes.AddRange(header);
                    bytes.AddRange(new byte[] { 0x4A, 0x4D, 0x00, 0x00 });
                }

                Helper.SharedD2IFileName = Helper.CacheFolder + "\\SharedStashSoftCoreV2.d2I";
                File.WriteAllBytes(Helper.SharedD2IFileName, bytes.ToArray());

                MessageBox.Show("点击开始游戏，来试试暴雪的亲儿子吧！");
            }
            else
            {
                //btnEdit.Enabled = true;
                //btnCreateNew.Enabled = true;
                //btnDelete.Enabled = true;
                //btnRefresh.Enabled = true;
                Helper.SharedD2IFileName = Helper.DefaultD2RFolder + "\\SharedStashSoftCoreV2.d2I";
            }

            btnDelete.ImageFile = @"\frontend\hd\final\frontend_delete";
            btnRefresh.ImageFile = @"\lobby\joingame\joingame_refreshbtn";

            btnOptions.ImageFile = Helper.GeneralButtonImageFile;
            btnAbout.ImageFile = Helper.GeneralButtonImageFile;
            btnEdit.ImageFile = Helper.GeneralButtonImageFile;
            btnCreateNew.ImageFile = Helper.GeneralButtonImageFile;

            var back = Helper.GetDefinitionFileName(@"\logoanimation\logoanimation");
            var logo = Helper.Sprite2Png(back);

            var back2 = Helper.GetDefinitionFileName(@"\logoanimation\logo_bottomflame");
            var logo2 = Helper.Sprite2Png(back2);

            var left = Helper.GetDefinitionFileName(@"\controller\switch\frontend\frontend_leftpanel");
            var leftimg = Helper.Sprite2Png(left);
            pbLeftPanel.BackgroundImage = leftimg;
            var right = Helper.GetDefinitionFileName(@"\panel\panel_bg");
            var rightimg = Helper.Sprite2Png(right);
            pbRightPanel.BackgroundImage = rightimg;

            anilist.Clear();
            anilist2.Clear();

            for (int i = 0; i < 27; i++)
            {
                anilist.Add(Helper.GetImageByFrame(logo, 27, i));
                anilist2.Add(Helper.GetImageByFrame(logo2, 27, i));
            }

            timer = new Timer();

            timer.Interval = 100;
            timer.Tick += Timer_Tick;

            timer.Enabled = true;

            InvalidateCharactors();

            var close = Helper.GetDefinitionFileName(@"\lobby\friendslist\friendslist_rejectinvite_button");
            closebmp = Helper.GetImageByFrame(Helper.Sprite2Png(close), 3, 0);

            pbRightPanel.SizeChanged += PbRightPanel_SizeChanged;
            pbRightPanel.Paint += PbRightPanel_Paint;
            pbRightPanel.MouseUp += PbRightPanel_MouseUp;

            pbRightPanel.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            pbRightPanel.UpdateStyles();
            pbLeftPanel.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            pbLeftPanel.UpdateStyles();

            //this.Width = 1029;
            //this.Height = 797;


        }

        private void PbRightPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X >= pbRightPanel.Width - 24 && e.X < pbRightPanel.Width)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void PbRightPanel_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(closebmp, pbRightPanel.Width - 24, 0);
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                g.DrawString(Utils.AllJsons["BetaWelcomeTitle"], this.Font, Brushes.White, new Rectangle(0, 40, pbRightPanel.Width, pbRightPanel.Height - 4), sf);
            }

            e.Graphics.DrawImage(bmp, 0, 0);

            g.Dispose();
            bmp.Dispose();
        }

        private void PbRightPanel_SizeChanged(object sender, EventArgs e)
        {
            pbRightPanel.Invalidate();
        }

        private void InvalidateCharactors()
        {
            if (!Directory.Exists(Helper.DefaultD2RFolder)) return;

            var files = Directory.GetFiles(Helper.DefaultD2RFolder, "*.d2s");
            List<D2S> charators = new List<D2S>();
            foreach (var file in files)
            {
                try
                {
                    charators.Add(Core.ReadD2S(file));
                }
                catch
                {

                }
            }


            lbCharactors.BeginUpdate();
            lbCharactors.Items.Clear();

            foreach (var cha in charators.OrderByDescending(s => s.LastPlayed).ToList())
            {
                lbCharactors.Items.Add(cha);
            }

            lbCharactors.EndUpdate();

            if (lbCharactors.Items.Count > 0)
            {
                lbCharactors.SelectedIndex = 0;
            }

            if (File.Exists(Helper.SharedD2IFileName)) Helper.SharedStashes = Core.ReadD2I2(Helper.SharedD2IFileName, Helper.Version);
        }

        private void BtnCreateNew_Click(object sender, EventArgs e)
        {
            FormCreateNewCharactor cncf = new FormCreateNewCharactor();
            if (DialogResult.OK == cncf.ShowDialog())
            {
                InvalidateCharactors();
            }
        }

        private void pbLeftPanel_Paint(object sender, PaintEventArgs e)
        {
            if (logoIndex > 26) logoIndex = 0;
            e.Graphics.DrawImage(anilist[logoIndex], new Rectangle(38, 56, 210, 210 * anilist[0].Height / anilist[0].Width));
            e.Graphics.DrawImage(anilist2[logoIndex], new Rectangle(90, 192, 100, 100 * anilist2[0].Height / anilist2[0].Width));

            using (Font f = new Font("SimSun", 12, FontStyle.Bold))
            {
                e.Graphics.DrawString("R E S U R R E C T E D", f, brush, new Point(50, 173));
            }
        }

        private void lbCharactors_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelCenter.BackgroundImage = Image.FromFile(
                Helper.GetResourceFile(
                    String.Format(
                        @"\heroes\cha{0}.jpg",
                        (lbCharactors.SelectedItem as D2S).ClassId
                    )
                )
            );
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;

            Helper.CurrentCharactor = lbCharactors.SelectedItem as D2S;
            Helper.CacheD2S(Helper.CurrentCharactor);
            (new MainForm()).ShowDialog();

            //this.WindowState = FormWindowState.Normal;            
            InvalidateCharactors();
        }

        private void FormSelectD2R_SizeChanged(object sender, EventArgs e)
        {
            //this.Text = String.Format("{0},{1}", this.Width, this.Height);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var d2s = (lbCharactors.SelectedItem as D2S);
            if (d2s == null) return;
            if (DialogResult.Yes == MessageBox.Show("确实要删除" + d2s.Name + "吗？", "确定", MessageBoxButtons.YesNo))
            {
                var fname = d2s.SaveFileName;
                File.Move(fname, fname.Replace(".d2s", ".d2s.delete"));

                InvalidateCharactors();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InvalidateCharactors();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            FormOptions fo = new FormOptions();
            fo.ShowDialog();

            Helper.RefreshSettings();
            this.InitData();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            FormAbout fa = new FormAbout();
            fa.ShowDialog();
        }
    }
}
