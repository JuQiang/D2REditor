using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class WaypointsControl : UserControl
    {


        private int currentTab, currentquest;
        private string[] chapters = new string[] { "I", "II", "III", "IV", "V" };
        private Dictionary<int, List<string>> waynamedict = new Dictionary<int, List<string>>();
        private Bitmap waypointsbackbmp, diffbmp;
        private Bitmap downimg, upimg, activebutton, normalbutton, disablebutton;
        private WaypointsDifficulty waydiff;
        private string title, difftext;
        private int level;
        public WaypointsControl()
        {
            InitializeComponent();

            currentTab = 0;

            if (Helper.IsHighDefinition)
            {
                this.Size = new Size(2324, 1507);
            }
            else
            {
                this.Size = new Size((int)(581 * Helper.DisplayRatio), (int)(753 * Helper.DisplayRatio));
            }

            var questback = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_base");
            waypointsbackbmp = Helper.Sprite2Png(questback);

            var diff = Helper.GetDefinitionFileName(@"\frontend\hd\final\frontend_difficultymodal");
            diffbmp = Helper.Sprite2Png(diff);

            var nb = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_button");
            normalbutton = Helper.GetImageByFrame(Helper.Sprite2Png(nb,false), 3, 0);

            var ab = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_button_active");
            activebutton = Helper.GetImageByFrame(Helper.Sprite2Png(ab, false), 3, 0);

            var db = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_button_disabled");
            disablebutton = Helper.GetImageByFrame(Helper.Sprite2Png(db, false), 2, 0);

            var imgname = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_tab");
            var img = Helper.Sprite2Png(imgname,false);
            downimg = Helper.GetImageByFrame(img, 2, 0);
            upimg = Helper.GetImageByFrame(img, 2, 1);

            imgname = Helper.GetDefinitionFileName(@"\questicons\questgem");
            img = Helper.Sprite2Png(imgname,false);

            SetDifficulty(1);
            List<object> allways = new List<object>() { waydiff.ActI, waydiff.ActII, waydiff.ActIII, waydiff.ActIV, waydiff.ActV };

            Dictionary<string, string> alljsons = new Dictionary<string, string>();
            foreach (var key in Utils.AllJsons.Keys)
            {
                alljsons[key.Replace(" ", "").Replace("'", "")] = Utils.AllJsons[key];
            }

            for (int i = 0; i < allways.Count; i++)
            {
                List<string> ways = new List<string>();
                foreach (var prop in allways[i].GetType().GetProperties())
                {
                    var way = alljsons[prop.Name];
                    ways.Add(way);
                }
                waynamedict[i] = ways;
            }

            this.level = 1;
            SetDifficulty(this.level);

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            title = Utils.AllJsons["teleport pad"];
            this.MouseUp += WaypointsControl_MouseUp;
            this.SizeChanged += WaypointsControl_SizeChanged;
        }

        int left = 0;
        private void WaypointsControl_SizeChanged(object sender, EventArgs e)
        {
            left = (this.Parent.Width - waypointsbackbmp.Width) / 2;
            this.Invalidate();
        }

        private void WaypointsControl_Paint(object sender, PaintEventArgs e)
        {
            RefreshWaypoints();

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(waypointsbackbmp, left  + 0, 0, waypointsbackbmp.Width, waypointsbackbmp.Height);

            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize * Helper.DisplayRatio, FontStyle.Bold))
            {
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    var text = title + " - " + difftext;
                    g.DrawString(text, f, Helper.TextBrush, new RectangleF(0, 36 * Helper.DisplayRatio, this.Width, 40 * Helper.DisplayRatio), sf);
                }
            }

            //g.DrawImage(diffbmp, diffleft, difftop, diffbmp.Width, diffbmp.Height);

            using (Font f = new Font("SimSun", 9, FontStyle.Bold))
            {
                //g.DrawString("选择要优化的难度", f, Brushes.White, diffleft + 60, difftop - 30);

                for (int i = 0; i < chapters.Length; i++)
                {
                    if (currentTab == i)
                    {
                        g.DrawImage(downimg, new RectangleF(left  + Helper.DefinitionInfo.WaypointsTabStartX*Helper.DisplayRatio + Helper.DefinitionInfo.WaypointsTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.WaypointsTabStartY * Helper.DisplayRatio, downimg.Width*Helper.DisplayRatio, downimg.Height * Helper.DisplayRatio),new RectangleF(0,0,downimg.Width,downimg.Height),GraphicsUnit.Pixel);
                        g.DrawString(chapters[i], f, Brushes.White, left + Helper.DefinitionInfo.WaypointsTabTitleStartX * Helper.DisplayRatio + Helper.DefinitionInfo.WaypointsTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.WaypointsTabTitleStartY * Helper.DisplayRatio - 5 * Helper.DisplayRatio);

                        for (int j = 0; j < waynamedict[currentTab].Count; j++)
                        {
                            if (waydict[currentTab][j])
                            {
                                g.DrawImage(normalbutton, new RectangleF(left + 75 * Helper.DisplayRatio, (62 * j + 128) * Helper.DisplayRatio,normalbutton.Width * Helper.DisplayRatio ,normalbutton.Height * Helper.DisplayRatio),new RectangleF(0,0,normalbutton.Width,normalbutton.Height),GraphicsUnit.Pixel);
                                g.DrawString(waynamedict[currentTab][j], f, Helper.TextBrush, left + 180 * Helper.DisplayRatio, (62 * j + 128 + 22) * Helper.DisplayRatio );
                            }
                            else
                            {
                                g.DrawImage(disablebutton, new RectangleF(left + 75 * Helper.DisplayRatio, (62 * j + 128) * Helper.DisplayRatio, disablebutton.Width * Helper.DisplayRatio, disablebutton.Height * Helper.DisplayRatio), new RectangleF(0, 0, disablebutton.Width, disablebutton.Height), GraphicsUnit.Pixel);
                                g.DrawString(waynamedict[currentTab][j], f, Brushes.Gray, left + 180*Helper.DisplayRatio, (62 * j + 128 + 22) * Helper.DisplayRatio);
                            }

                        }
                    }
                    else
                    {
                        g.DrawImage(upimg, new RectangleF(left + Helper.DefinitionInfo.WaypointsTabStartX * Helper.DisplayRatio + Helper.DefinitionInfo.WaypointsTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.WaypointsTabStartY * Helper.DisplayRatio, upimg.Width * Helper.DisplayRatio, upimg.Height * Helper.DisplayRatio), new RectangleF(0, 0, upimg.Width, upimg.Height), GraphicsUnit.Pixel);

                        g.DrawString(chapters[i], f, Brushes.Gray, left + Helper.DefinitionInfo.WaypointsTabTitleStartX * Helper.DisplayRatio + Helper.DefinitionInfo.WaypointsTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.WaypointsTabTitleStartY * Helper.DisplayRatio - 5 * Helper.DisplayRatio);
                    }
                }
            }

            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private Dictionary<int, List<bool>> waydict = new Dictionary<int, List<bool>>();

        public void SetDifficulty(int level)
        {
            this.level = level;

            if (level == 1) { difftext = Utils.AllJsons["strCreateGameNormalText"]; this.waydiff = Helper.CurrentCharactor.Waypoints.Normal; }
            if (level == 2) { difftext = Utils.AllJsons["strCreateGameNightmareText"]; this.waydiff = Helper.CurrentCharactor.Waypoints.Nightmare; }
            if (level == 3) { difftext = Utils.AllJsons["strCreateGameHellText"]; this.waydiff = Helper.CurrentCharactor.Waypoints.Hell; }


            currentTab = 0;
            currentquest = 0;

            RefreshWaypoints();
            getUncompletedAward();

            this.Invalidate();
        }

        private void RefreshWaypoints()
        {
            waydict.Clear();
            waydict[0] = new List<bool> { waydiff.ActI.RogueEncampment, waydiff.ActI.ColdPlains, waydiff.ActI.StonyField, waydiff.ActI.DarkWood, waydiff.ActI.BlackMarsh, waydiff.ActI.OuterCloister, waydiff.ActI.JailLevel1, waydiff.ActI.InnerCloister, waydiff.ActI.CatacombsLevel2 };
            waydict[1] = new List<bool> { waydiff.ActII.LutGholein, waydiff.ActII.SewersLevel2, waydiff.ActII.DryHills, waydiff.ActII.HallsoftheDeadLevel2, waydiff.ActII.FarOasis, waydiff.ActII.LostCity, waydiff.ActII.PalaceCellarLevel1, waydiff.ActII.ArcaneSanctuary, waydiff.ActII.CanyonoftheMagi };
            waydict[2] = new List<bool> { waydiff.ActIII.KurastDocktown, waydiff.ActIII.SpiderForest, waydiff.ActIII.GreatMarsh, waydiff.ActIII.FlayerJungle, waydiff.ActIII.LowerKurast, waydiff.ActIII.KurastBazaar, waydiff.ActIII.UpperKurast, waydiff.ActIII.Travincal, waydiff.ActIII.DuranceofHateLevel2 };
            waydict[3] = new List<bool> { waydiff.ActIV.ThePandemoniumFortress, waydiff.ActIV.CityoftheDamned, waydiff.ActIV.RiverofFlame };
            waydict[4] = new List<bool> { waydiff.ActV.Harrogath, waydiff.ActV.RigidHighlands, waydiff.ActV.ArreatPlateau, waydiff.ActV.CrystalizedCavernLevel1, waydiff.ActV.CrystalizedCavernLevel2, waydiff.ActV.HallsofDeathsCalling, waydiff.ActV.TundraWastelands, waydiff.ActV.GlacialCavesLevel1, waydiff.ActV.TheWorldstoneKeepLevel2 };

        }
        public void CompleteWaypoints()
        {
            List<WaypointsDifficulty> waypoints = new List<WaypointsDifficulty>() { Helper.CurrentCharactor.Waypoints.Normal, Helper.CurrentCharactor.Waypoints.Nightmare, Helper.CurrentCharactor.Waypoints.Hell };
            var way = waypoints[this.level - 1];

            foreach (var wayprop in way.GetType().GetProperties())
            {
                if (wayprop.Name.IndexOf("Header") > -1) continue;

                var act = wayprop.GetValue(way);
                foreach (var actprop in act.GetType().GetProperties())
                {
                    actprop.SetValue(act, true);
                }
            }

            RefreshWaypoints();
        }
        private void getUncompletedAward()
        {
            for (int j = 0; j < waydict[currentTab].Count; j++)
            {
                if (false == waydict[currentTab][j])
                {
                    currentquest = j;
                    break;
                }
            }

            //questtitle.Text = Utils.AllJsons[String.Format("qstsa{0}q{1}", currentTab + 1, currentquest + 1)];
            //questlist = new List<object>() { waydiff.ActI, waydiff.ActII, waydiff.ActIII, waydiff.ActIV, waydiff.ActV };
        }

        private void WaypointsControl_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.chapters.Length; i++)
            {
                Rectangle r = new Rectangle((int)(left + Helper.DefinitionInfo.WaypointsTabStartX*Helper.DisplayRatio + Helper.DefinitionInfo.WaypointsTabWidth * Helper.DisplayRatio * i), (int)(Helper.DefinitionInfo.WaypointsTabStartY * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.WaypointsTabWidth * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.WaypointsTabHeight * Helper.DisplayRatio));
                if (Helper.IsPointInRange(e.Location, r))
                {
                    currentTab = i;
                    currentquest = 0;

                    getUncompletedAward();

                    this.Invalidate();

                    break;
                }
            }

            //for (int j = 0; j < questIcons[currentTab].Count; j++)
            //{
            //    Rectangle r = new Rectangle(Helper.DefinitionInfo.WaypointsIconStartX + Helper.DefinitionInfo.WaypointsIconWidth * (j % 3), Helper.DefinitionInfo.WaypointsIconStartY + Helper.DefinitionInfo.WaypointsIconHeight * (j / 3), questiconwidth, questiconheight);
            //    if (Helper.IsPointInRange(e.Location, r))
            //    {
            //        currentquest = j;
            //        this.Invalidate();

            //        break;
            //    }
            //}

            if (currentTab > -1 && currentquest > -1)
            {
                //questtitle.Text = Utils.AllJsons[String.Format("qstsa{0}q{1}", currentTab + 1, currentquest + 1)];
                //questdesc.Text = waydict[currentTab][currentquest] ? "任务已完成" : "任务还没有完成，该拿到的奖励还没有拿，抓紧哦！";
            }
        }
    }
}
