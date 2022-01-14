using D2REditor.Controls;
using D2REditor.Forms;
using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace D2REditor
{
    public partial class MainForm : Form
    {
        private List<D2I> sharedStashes;

        private CharactorControl charactorControl;
        private WaypointsControl waypointsControl;
        private QuestsControl questsControl;
        private SkillsControl skillsControl;
        private ItemsControl itemsControl;
        private OptimizeControl optimizeControl;

        private Bitmap[] healthlist = new Bitmap[46];
        private Bitmap[] manalist = new Bitmap[46];
        private Timer timer;
        private int healthindex = 0;
        private Bitmap bottombmp, sprintbmp, staminabmp;
        private double ratio = 1.0d;

        private Pen[] lightPens;
        private Bitmap closebmp;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private const int VM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 2;

        public MainForm()
        {
            InitializeComponent();
        }

        private void PBottom_Paint(object sender, PaintEventArgs e)
        {
            int top = 0;
            if (healthindex > 45) healthindex = 0;

            Bitmap bmp = new Bitmap(pBottom.Width, pBottom.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(bottombmp, new Rectangle(0, top + 0, (int)(ratio * bottombmp.Width), (int)(ratio * bottombmp.Height)), new Rectangle(0, 0, bottombmp.Width, bottombmp.Height), GraphicsUnit.Pixel);
            g.DrawImage(healthlist[healthindex], new RectangleF(132*Helper.DisplayRatio, top + 27 * Helper.DisplayRatio, (int)(healthlist[0].Width * ratio) * Helper.DisplayRatio, (int)(healthlist[0].Height * ratio) * Helper.DisplayRatio), new RectangleF(0, 0, healthlist[0].Width, healthlist[0].Height), GraphicsUnit.Pixel);
            g.DrawImage(manalist[healthindex], new RectangleF(933 * Helper.DisplayRatio, top + 27 * Helper.DisplayRatio, (int)(manalist[0].Width * ratio) * Helper.DisplayRatio, (int)(manalist[0].Height * ratio) * Helper.DisplayRatio), new RectangleF(0, 0, manalist[0].Width, manalist[0].Height), GraphicsUnit.Pixel);
            g.DrawImage(sprintbmp, new RectangleF(280 * Helper.DisplayRatio, top + 113 * Helper.DisplayRatio, 16 * Helper.DisplayRatio, 16 * Helper.DisplayRatio), new Rectangle(0, 0, sprintbmp.Width, sprintbmp.Height), GraphicsUnit.Pixel);
            g.DrawImage(staminabmp, new RectangleF(302 * Helper.DisplayRatio, top + 113 * Helper.DisplayRatio, 180 * Helper.DisplayRatio, 16 * Helper.DisplayRatio), new Rectangle(0, 0, staminabmp.Width, staminabmp.Height), GraphicsUnit.Pixel);

            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize, FontStyle.Bold))
            {
                var text = Helper.CurrentCharactor.DisplayName;
                var sf = e.Graphics.MeasureString(text, f);
                g.DrawString(text, f, Helper.TextBrush, (this.Width - sf.Width) / 2, top + 27);
            }

            if (itemClicked) g.DrawRectangle(lightPens[healthindex / 12], 696 * Helper.DisplayRatio, top + 122 * Helper.DisplayRatio, 34 * Helper.DisplayRatio, 32 * Helper.DisplayRatio);

            e.Graphics.DrawImage(bmp, 0, 0);

            g.Dispose();
            bmp.Dispose();
        }

        private void PBottom_SizeChanged(object sender, EventArgs e)
        {
            pBottom.Invalidate();
        }

        private void PTop_SizeChanged(object sender, EventArgs e)
        {
            pTop.Invalidate();
        }

        private void PTop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X >= this.Width - closebmp.Width && e.X < this.Width)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            if (e.X >= this.Width - closebmp.Width*2 && e.X < this.Width - closebmp.Width)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void PTop_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(closebmp, pTop.Width - closebmp.Width, 0);
            g.DrawString("_", pTop.Font, Brushes.White, pTop.Width - closebmp.Width*2, 0);

            e.Graphics.DrawImage(bmp, 0, 0);
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                g.DrawString(Utils.AllJsons["title"], pTop.Font, Brushes.White, new Rectangle(0, 4, pTop.Width, pTop.Height - 4), sf);

            }

            e.Graphics.DrawImage(bmp, 0, 0);
            g.Dispose();
            bmp.Dispose();
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void itemsControl_OnItemSelected(object sender, ItemSelectedEventArgs e)
        {
            FormEditItem fei = new FormEditItem(e.Item);
            fei.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                FormCreateItemFromTemplate fcift = new FormCreateItemFromTemplate(dlg.FileName);
                fcift.ShowDialog();
            }
        }

        private void ResetStatesAndEnableState(Button btn)
        {
            //pContainer.Controls.Clear();
            //(btn.Tag as UserControl).Dock = DockStyle.Fill;
            //pContainer.Controls.Add(btn.Tag as UserControl);

            btnExtra1.Visible = false; btnExtra1.Text = "";
            btnExtra2.Visible = false; btnExtra2.Text = "";
            btnExtra3.Visible = false; btnExtra3.Text = "";
            btnExtra4.Visible = false; btnExtra4.Text = "";

            itemClicked = false;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //this.Text = String.Format("{0},{1}", this.Width, this.Height);
            //this.Width = 1177;
            //this.Height = 820;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            charactorControl = new CharactorControl();
            waypointsControl = new WaypointsControl();
            questsControl = new QuestsControl();
            skillsControl = new SkillsControl();
            itemsControl = new ItemsControl();
            optimizeControl = new OptimizeControl();
            //followerControl = new FollowerControl();

            btnCharactorSkill.Text = "";
            btnCharactorSkill.Tag = charactorControl;
            btnOptimizeAll.Text = "";
            btnOptimizeAll.Tag = waypointsControl;
            btnItems.Text = "";
            btnItems.Tag = questsControl;
            btnQuestsWaypoints.Text = "";
            btnQuestsWaypoints.Tag = skillsControl;
            btnItems2.Tag = itemsControl;
            btnOptimizeAll2.Tag = optimizeControl;

            lightPens = new Pen[4] { new Pen(Color.Red, 3), new Pen(Color.Green, 3), new Pen(Color.Red, 3), new Pen(Color.Green, 3) };

            var close = Helper.GetDefinitionFileName(@"\lobby\friendslist\friendslist_rejectinvite_button");
            closebmp = Helper.GetImageByFrame(Helper.Sprite2Png(close), 3, 0);

            pTop.SizeChanged += PTop_SizeChanged;
            pTop.Paint += PTop_Paint;
            pTop.MouseUp += PTop_MouseUp;

            pBottom.SizeChanged += PBottom_SizeChanged;
            pBottom.Paint += PBottom_Paint;

            //this.sharedStashes = Core.ReadD2I2(Helper.SharedD2IFileName, Helper.Version);

            itemsControl.InitData();
            itemsControl.OnItemSelected += itemsControl_OnItemSelected;
            charactorControl.InitData();

            ResetStatesAndEnableState(btnCharactorSkill);

            var back = Helper.GetDefinitionFileName(@"\panel\hud_02\healthmanaanimation\healthidle\4k\globe_health_man_idle");
            var logo = Helper.Sprite2Png(back,false);
            var mana = Image.FromFile(Helper.CacheFolder + @"\panel\hud_02\healthmanaanimation\healthidle\4k\globe_mana_man_idle.png") as Bitmap;

            var bottom = Helper.GetDefinitionFileName(@"\panel\hud_02\front_panel");
            bottombmp = Helper.Sprite2Png(bottom);
            ratio = this.Width * 1.0d / bottombmp.Width;
            for (int i = 0; i < healthlist.Length; i++)
            {
                healthlist[i] = Helper.GetImageByFrame(logo, 46, i);
                manalist[i] = Helper.GetImageByFrame(mana, 46, i);
                //var mbmp = Helper.GetImageByFrame(mana, 46, i);
                //var zoombmp = new Bitmap((int)(mbmp.Width * Helper.DisplayRatio), (int)(mbmp.Height * Helper.DisplayRatio));
                //using (Graphics g = Graphics.FromImage(zoombmp))
                //{
                //    g.DrawImage(mbmp, new Rectangle(0, 0, zoombmp.Width, zoombmp.Height), new Rectangle(0, 0, mbmp.Width, mbmp.Height), GraphicsUnit.Pixel);
                //}
                //mbmp.Dispose();
                //manalist[i] = zoombmp;
            }

            var sprint = Helper.GetDefinitionFileName(@"\panel\hud_02\sprint");
            sprintbmp = Helper.GetImageByFrame(Helper.Sprite2Png(sprint), 6, 3);
            var stamina = Helper.GetDefinitionFileName(@"\panel\hud_02\stamina_bar");
            staminabmp = Helper.GetImageByFrame(Helper.Sprite2Png(stamina), 3, 0);

            timer = new Timer();

            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;

            //this.Width = 1177;
            //this.Height = 968;

            this.Paint += MainForm_Paint;

            btnCharactorSkill.ImageFrames = 6;
            btnCharactorSkill.ImageFile = @"\panel\hud_02\character";
            tooltip.SetToolTip(btnCharactorSkill, Utils.AllJsons["character&skill"]);

            btnQuestsWaypoints.ImageFrames = 6;
            btnQuestsWaypoints.ImageFile = @"\panel\hud_02\questlog";
            tooltip.SetToolTip(btnQuestsWaypoints, Utils.AllJsons["quests&waypoints"]);

            btnItems.ImageFrames = 3;
            btnItems.ImageFile = @"\panel\hud_02\inventory";
            tooltip.SetToolTip(btnItems, Utils.AllJsons["minipanelinv"]);


            //btnItems2.ImageFrames = 3;           
            //btnItems2.ImageFile = @"\panel\hud_02\inventory";
            //tooltip.SetToolTip(btnItems2, Utils.AllJsons["minipanelinv"]);

            //btnWaypoints.ImageFrames = 3;
            //btnWaypoints.ImageFile = @"\panel\hud_02\pausemenu";
            //tooltip.SetToolTip(btnWaypoints, Utils.AllJsons["waypointsheader"]);

            btnOptimizeAll.ImageFrames = 4;
            btnOptimizeAll.ImageFile = @"\panel\hud_02\quest_button"; //@"\panel\hud_02\messages";
            tooltip.SetToolTip(btnOptimizeAll, Utils.AllJsons["optimize_all"]);// Utils.AllJsons["minipanelparty"]);

            btnReportBug.ImageFrames = 3;
            btnReportBug.ImageFile = @"\panel\hud_02\bugbtn";
            tooltip.SetToolTip(btnReportBug, Utils.AllJsons["report_bug"]);

            btnSave.ImageFrames = 1;
            btnSave.ImageFile = @"\items\quest\scroll_of_horadric_quest_info";
            tooltip.SetToolTip(btnSave, Utils.AllJsons["KeyBindingCustomDefaultsSave"]);

            btnBooks.ImageFrames = 1;
            btnBooks.ImageFile = @"\items\quest\book_of_skill";
            tooltip.SetToolTip(btnBooks, Utils.AllJsons["everything_you_should_know"]);

            btnCharactor_Click(btnCharactorSkill, null);
            this.Text = "";

            pTop.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            pTop.UpdateStyles();
            pTop.MouseDown += PTop_MouseDown;

            pBottom.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            pBottom.UpdateStyles();
        }

        private void PTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < pTop.Width - 48)
            {
                ReleaseCapture();
                SendMessage((IntPtr)this.Handle, VM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (healthindex++ > 45) healthindex = 0;
            pBottom.Invalidate();
        }

        private void ClearEvents(Button b)
        {

            FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);

            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);

        }
        private void btnCharactor_Click(object sender, EventArgs e)
        {
            pContainer.Controls.Clear();
            charactorControl.Left = 0;
            pContainer.Controls.Add(charactorControl);
            skillsControl.Left = charactorControl.Width;
            pContainer.Controls.Add(skillsControl);

            ResetStatesAndEnableState(sender as Button);
            ClearEvents(btnExtra1); ClearEvents(btnExtra2); ClearEvents(btnExtra3); //ClearEvents(btnExtra4);

            btnExtra1.Visible = true; btnExtra1.Text = "99"; tooltip.SetToolTip(btnExtra1, Utils.AllJsons["level99"]);
            btnExtra1.Click += (asender, ae) => { Helper.SetLevel99(); this.charactorControl.SetAdjustmentVisible(); this.charactorControl.Invalidate(); };
            btnExtra2.Visible = true; btnExtra2.Text = "20"; tooltip.SetToolTip(btnExtra2, Utils.AllJsons["skill20"]);// Utils.AllJsons["respec"]);
            btnExtra2.Click += (asender, ae) => { Helper.SetSkill20(); this.skillsControl.Invalidate(); };
            btnExtra3.Visible = true; btnExtra3.Text = "1"; tooltip.SetToolTip(btnExtra3, Utils.AllJsons["levelskill1"]);
            btnExtra3.Click += (asender, ae) => { Helper.RestoreLevelAndSkill(); this.charactorControl.SetAdjustmentVisible(); this.charactorControl.Invalidate(); this.skillsControl.Invalidate(); };
            //btnExtra4.Visible = true; btnExtra4.Text = "0"; tooltip.SetToolTip(btnExtra4, Utils.AllJsons["levelskill0"]);
            //btnExtra4.Click += (asender, ae) => { Helper.RestoreLevelAndSkill(); Helper.CurrentCharactor.Attributes.Stats["statpts"] = 1023 * 4; this.charactorControl.SetAdjustmentVisible(); this.charactorControl.Invalidate(); this.skillsControl.Invalidate(); };

            itemClicked = false;
        }

        bool itemClicked = false;
        private void btnItems_Click(object sender, EventArgs e)
        {
            pContainer.Controls.Clear();
            itemsControl.Left = 0;
            pContainer.Controls.Add(itemsControl);

            ResetStatesAndEnableState(sender as Button);
            ClearEvents(btnExtra1); ClearEvents(btnExtra2);

            btnExtra1.Visible = true; btnExtra1.Text = "+"; tooltip.SetToolTip(btnExtra1, Utils.AllJsons["click_import"]);
            btnExtra1.Click += (asender, ae) =>
            {
                FormCreateItemFromTemplate fcift = new FormCreateItemFromTemplate();
                if (DialogResult.OK == fcift.ShowDialog())
                {
                    itemsControl.SetCurrentDraggingItem(fcift.NewItem);
                }
            };

            btnExtra2.Visible = true; btnExtra2.Text = "+"; tooltip.SetToolTip(btnExtra2, Utils.AllJsons["click_import_local"]);
            btnExtra2.Click += (asender, ae) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    var buf = File.ReadAllBytes(ofd.FileName);
                    var buf2 = new byte[buf.Length - 2];
                    Array.Copy(buf, 2, buf2, 0, buf2.Length);

                    var item = Core.ReadItem(buf, 0x60);
                    itemsControl.SetCurrentDraggingItem(item);
                }
            };

            itemClicked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Helper.CurrentCharactor.Save2File();
            Helper.WriteSharedStashes(Helper.SharedStashes);
            MessageBox.Show("OK");
        }

        private void btnOptimizeAll_Click(object sender, EventArgs e)
        {
            pContainer.Controls.Clear();
            optimizeControl.Left = 0;
            pContainer.Controls.Add(optimizeControl);

            ResetStatesAndEnableState(sender as Button);

            itemClicked = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            //this.Text = String.Format("{0},{1}", e.X, e.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var back = Helper.GetDefinitionFileName(@"\panel\hud_02\healthmanaanimation\healthidle\4k\globe_health_man_idle");
            var logo = Helper.Sprite2Png(back);
            var mana = new Bitmap(logo.Width, logo.Height);

            for (int m = 0; m < logo.Width; m++)
            {
                for (int n = 0; n < logo.Height; n++)
                {
                    Color c = logo.GetPixel(m, n);
                    mana.SetPixel(m, n, Color.FromArgb(c.A, c.B, c.G, c.R));
                }
            }

            mana.Save("globe_mana_man_idle.png");
        }

        private void UpgradeVersion()
        {
            List<string> allfiles = new List<string>();
            GetAllFiles(@"C:\Users\Administrator\source\repos\D2REditor\D2REditor\bin\Debug\Documents\ItemPacks", ref allfiles);
            foreach (var file in allfiles)
            {
                try
                {
                    var item = Core.ReadItem(file, 0x60);
                    File.WriteAllBytes(file, Core.WriteItem(item, 0x61));//老的，都转换为最新的版本
                    System.Diagnostics.Debug.WriteLine(String.Format("{0} - {1}", (new FileInfo(file)).Name, item.Name));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

        }

        private void btnQuestsWaypoints_Click(object sender, EventArgs e)
        {
            pContainer.Controls.Clear();
            questsControl.Left = 0;
            pContainer.Controls.Add(questsControl);
            waypointsControl.Left = questsControl.Width;
            pContainer.Controls.Add(waypointsControl);
            //ResetStatesAndEnableState(sender as Button);
            ClearEvents(btnExtra1); ClearEvents(btnExtra2); ClearEvents(btnExtra3); ClearEvents(btnExtra4);

            btnExtra1.Visible = true; btnExtra1.Text = "1"; tooltip.SetToolTip(btnExtra1, Utils.AllJsons["game_normal"]);
            btnExtra2.Visible = true; btnExtra2.Text = "2"; tooltip.SetToolTip(btnExtra2, Utils.AllJsons["game_nightmare"]);
            btnExtra3.Visible = true; btnExtra3.Text = "3"; tooltip.SetToolTip(btnExtra3, Utils.AllJsons["game_hell"]);
            btnExtra4.Visible = true; btnExtra4.Text = "!"; tooltip.SetToolTip(btnExtra4, Utils.AllJsons["complete_quest_waypoint"]);// Utils.AllJsons["strCreateGameHellText"]);
            btnExtra1.Click += (asender, ae) => { this.questsControl.SetDifficulty(1); this.waypointsControl.SetDifficulty(1); };
            btnExtra2.Click += (asender, ae) => { this.questsControl.SetDifficulty(2); this.waypointsControl.SetDifficulty(2); };
            btnExtra3.Click += (asender, ae) => { this.questsControl.SetDifficulty(3); this.waypointsControl.SetDifficulty(3); };
            btnExtra4.Click += (asender, ae) => { this.questsControl.CompleteQuests(); this.questsControl.Invalidate(); this.waypointsControl.CompleteWaypoints(); this.waypointsControl.Invalidate(); };

            itemClicked = false;
        }

        private void btnItems_Click_1(object sender, EventArgs e)
        {

        }

        private void btnReportBug_Click(object sender, EventArgs e)
        {
            FormReportBug frb = new FormReportBug();
            frb.ShowDialog();
        }

        private void GetAllFiles(string path, ref List<string> allfiles)
        {
            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs) GetAllFiles(dir, ref allfiles);

            var files = Directory.GetFiles(path, "*.d2i");
            foreach (var file in files) allfiles.Add(file);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            FormKnowledges fk = new FormKnowledges();
            fk.Show();
        }
    }
}
