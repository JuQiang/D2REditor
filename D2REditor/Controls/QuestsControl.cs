using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class QuestsControl : UserControl
    {
        private int currentTab, currentquest;
        private string[] chapters = new string[] { "I", "II", "III", "IV", "V" };
        private Dictionary<int, List<string>> questIcons = new Dictionary<int, List<string>>();
        private Bitmap questbackbmp, diffbmp;
        private Label questdesc;
        private Bitmap downimg, upimg, gem;
        private int questiconwidth, questiconheight;
        private QuestsDifficulty questdiff;
        private List<object> questlist;
        private Dictionary<int, List<bool>> awarddict = new Dictionary<int, List<bool>>();
        private string title, difftext;
        private int level;

        public QuestsControl()
        {
            InitializeComponent();

            currentTab = 0;
            this.questIcons[0] = new List<string>() { "a1q1", "a1q2", "a1q3", "a1q4", "a1q5", "a1q6", };
            this.questIcons[1] = new List<string>() { "a2q1", "a2q2", "a2q3", "a2q4", "a2q5", "a2q6", };
            this.questIcons[2] = new List<string>() { "a3q1", "a3q2", "a3q3", "a3q4", "a3q5", "a3q6", };
            this.questIcons[3] = new List<string>() { "a4q1", "a4q2", "a4q3", };
            this.questIcons[4] = new List<string>() { "a5q1", "a5q2", "a5q3", "a5q4", "a5q5", "a5q6", };

            

            var questback = Helper.GetDefinitionFileName(@"\panel\quest_log\questlog_bg");
            questbackbmp = Helper.Sprite2Png(questback);

            var diff = Helper.GetDefinitionFileName(@"\frontend\hd\final\frontend_difficultymodal");
            diffbmp = Helper.Sprite2Png(diff);

            questdesc = new Label();
            questdesc.Text = "任务完成情况任务完成情况任务完成情况任务完成情况任务完成情况任务完成情况任务完成情况任务完成情况";
            questdesc.ForeColor = Color.White;
            questdesc.BackColor = Color.Transparent;
            questdesc.AutoSize = true;
            questdesc.Font = new Font(Helper.CurrentFontFamily, 9, FontStyle.Bold);
            questdesc.AutoSize = false;
            questdesc.BorderStyle = BorderStyle.FixedSingle;
            questdesc.Width = questbackbmp.Width - (int)(100*Helper.DisplayRatio);
            questdesc.Height = (int)(100 * Helper.DisplayRatio);
            questdesc.TextAlign = ContentAlignment.MiddleLeft;
            questdesc.Location = new Point((int)(50 * Helper.DisplayRatio), (int)(500 * Helper.DisplayRatio));
            this.Controls.Add(questdesc);

            title = Utils.AllJsons["minipanelquest"];
            SetDifficulty(1);

            var imgname = Helper.GetDefinitionFileName(@"\panel\quest_log\questlogtab");
            var img = Helper.Sprite2Png(imgname);
            downimg = Helper.GetImageByFrame(img, 2, 0);
            upimg = Helper.GetImageByFrame(img, 2, 1);

            imgname = Helper.GetDefinitionFileName(@"\questicons\questgem");
            img = Helper.Sprite2Png(imgname,false);
            gem = Helper.GetImageByFrame(img, 28, 27) as Bitmap;

            level = 1;

            SetDifficulty(level);

            this.Width = questbackbmp.Width;
            this.Height = questbackbmp.Height;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            this.Paint += QuestsControl_Paint;
            this.MouseUp += QuestsControl_MouseUp;
            this.SizeChanged += QuestsControl_SizeChanged;
        }

        int left = 0;
        private void QuestsControl_SizeChanged(object sender, EventArgs e)
        {
            left = (this.Parent.Width - questbackbmp.Width) / 2;
            questdesc.Left = left + (int)(50 * Helper.DisplayRatio);
            this.Invalidate();
        }

        private void BeOptimizeAll_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BeOptimizeChapter_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshAwards()
        {
            awarddict.Clear();
            awarddict[0] = new List<bool> { questdiff.ActI.DenOfEvil.RewardGranted, questdiff.ActI.SistersBurialGrounds.RewardGranted, questdiff.ActI.TheSearchForCain.RewardGranted, questdiff.ActI.TheForgottenTower.RewardGranted, questdiff.ActI.ToolsOfTheTrade.RewardGranted, questdiff.ActI.SistersToTheSlaughter.RewardGranted };
            awarddict[1] = new List<bool> { questdiff.ActII.RadamentsLair.RewardGranted, questdiff.ActII.TheHoradricStaff.RewardGranted, questdiff.ActII.TaintedSun.RewardGranted, questdiff.ActII.ArcaneSanctuary.RewardGranted, questdiff.ActII.TheSummoner.RewardGranted, questdiff.ActII.TheSevenTombs.RewardGranted };
            awarddict[2] = new List<bool> { questdiff.ActIII.TheGoldenBird.RewardGranted, questdiff.ActIII.KhalimsWill.RewardGranted, questdiff.ActIII.BladeOfTheOldReligion.RewardGranted, questdiff.ActIII.LamEsensTome.RewardGranted, questdiff.ActIII.TheBlackenedTemple.RewardGranted, questdiff.ActIII.TheGuardian.RewardGranted };
            awarddict[3] = new List<bool> { questdiff.ActIV.TheFallenAngel.RewardGranted, questdiff.ActIV.TerrorsEnd.RewardGranted, questdiff.ActIV.Hellforge.RewardGranted };
            awarddict[4] = new List<bool> { questdiff.ActV.SiegeOnHarrogath.RewardGranted, questdiff.ActV.RescueOnMountArreat.RewardGranted, questdiff.ActV.PrisonOfIce.RewardGranted, questdiff.ActV.BetrayalOfHarrogath.RewardGranted, questdiff.ActV.RiteOfPassage.RewardGranted, questdiff.ActV.EveOfDestruction.RewardGranted };
        }
        public void SetDifficulty(int level)
        {
            this.level = level;
            if (level == 1) { difftext = Utils.AllJsons["strCreateGameNormalText"]; this.questdiff = Helper.CurrentCharactor.Quests.Normal; }
            if (level == 2) { difftext = Utils.AllJsons["strCreateGameNightmareText"]; this.questdiff = Helper.CurrentCharactor.Quests.Nightmare; }
            if (level == 3) { difftext = Utils.AllJsons["strCreateGameHellText"]; this.questdiff = Helper.CurrentCharactor.Quests.Hell; }


            currentTab = 0;
            currentquest = 0;

            RefreshAwards();

            getUncompletedAward();

            this.Invalidate();
        }

        public void CompleteQuests()
        {
            List<QuestsDifficulty> quests = new List<QuestsDifficulty>() { Helper.CurrentCharactor.Quests.Normal, Helper.CurrentCharactor.Quests.Nightmare, Helper.CurrentCharactor.Quests.Hell };
            var quest = quests[this.level - 1];
            var questlist = new List<object>() { quest.ActI, quest.ActII, quest.ActIII, quest.ActIV, quest.ActV };

            foreach (var questprop in quest.GetType().GetProperties())
            {
                var q = questprop.GetValue(quest);
                foreach (var actprop in q.GetType().GetProperties())
                {
                    //System.Diagnostics.Debug.WriteLine(actprop.Name);
                    var q2 = actprop.GetValue(q);
                    foreach (var actprop2 in q2.GetType().GetProperties())
                    {
                        //if (actprop2.Name.IndexOf("RewardGranted") <0)
                        {
                            actprop2.SetValue(q2, true);
                        }
                        //else
                        //{
                        //    actprop2.SetValue(q2, false);
                        //}
                    }
                }
            }

            RefreshAwards();

            Helper.CurrentCharactor.Progression = 15;
        }
        private void getUncompletedAward()
        {
            for (int j = 0; j < awarddict[currentTab].Count; j++)
            {
                if (false == awarddict[currentTab][j])
                {
                    currentquest = j;
                    break;
                }
            }

            questdesc.Text = awarddict[currentTab][currentquest] ? Utils.AllJsons["qstsprevious"] : "";
            questlist = new List<object>() { questdiff.ActI, questdiff.ActII, questdiff.ActIII, questdiff.ActIV, questdiff.ActV };
        }

        private void QuestsControl_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.chapters.Length; i++)
            {
                Rectangle r = new Rectangle(left+(int)(Helper.DefinitionInfo.QuestTabStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio * i), (int)(Helper.DefinitionInfo.QuestTabStartY * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.QuestTabHeight * Helper.DisplayRatio));
                if (Helper.IsPointInRange(e.Location, r))
                {
                    currentTab = i;
                    currentquest = 0;

                    getUncompletedAward();

                    this.Invalidate();

                    break;
                }
            }

            for (int j = 0; j < questIcons[currentTab].Count; j++)
            {
                Rectangle r = new Rectangle(left+(int)(Helper.DefinitionInfo.QuestIconStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconWidth * Helper.DisplayRatio * (j % 3)), (int)(Helper.DefinitionInfo.QuestIconStartY * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconHeight * Helper.DisplayRatio * (j / 3)), questiconwidth , questiconheight);
                if (Helper.IsPointInRange(e.Location, r))
                {
                    currentquest = j;
                    questdesc.Text = awarddict[currentTab][currentquest] ? Utils.AllJsons["qstsprevious"] : "";
                    this.Invalidate();

                    break;
                }
            }

            if (currentTab > -1 && currentquest > -1)
            {
                this.Invalidate();

            }
        }


        private void QuestsControl_Paint(object sender, PaintEventArgs e)
        {
            RefreshAwards();

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(questbackbmp, left * Helper.DisplayRatio + 0, 0, questbackbmp.Width, questbackbmp.Height);
            //g.DrawImage(diffbmp, diffleft, difftop, diffbmp.Width,diffbmp.Height);

            using (Font f = new Font(Helper.CurrentFontFamily, Helper.DefinitionInfo.StashTitleFontSize * Helper.DisplayRatio, FontStyle.Bold))
            {
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    var text = title + " - " + difftext;
                    g.DrawString(text, f, Helper.TextBrush, new RectangleF(0, 36 * Helper.DisplayRatio, this.Width, 40 * Helper.DisplayRatio), sf);
                }
            }

            using (Font f = new Font(Helper.CurrentFontFamily, 9, FontStyle.Bold))
            {
                var text = Utils.AllJsons[String.Format("qstsa{0}q{1}", currentTab + 1, currentquest + 1)]; ;
                var sf = g.MeasureString(text, f);
                g.DrawString(text, f, Helper.TextBrush, left * Helper.DisplayRatio + (questbackbmp.Width - sf.Width) / 2, 447 * Helper.DisplayRatio);
                //questdesc.Text = awarddict[currentTab][currentquest] ? "任务已完成" : "任务还没有完成，该拿到的奖励还没有拿，抓紧哦！";
                //"qstsprevious"
            }

            using (Font f = new Font(Helper.CurrentFontFamily, Helper.DefinitionInfo.StashTabFontSize * Helper.DisplayRatio, FontStyle.Bold))
            {
                //g.DrawString("选择要优化的难度", f, Brushes.White, diffleft + 60, difftop - 30);

                for (int i = 0; i < chapters.Length; i++)
                {
                    if (currentTab == i)
                    {
                        //var downimg = Image.FromFile(Helper.GetDefinitionFileName(Helper.ExePath + Helper.DefinitionPath + @"customized\tabdown"));
                        g.DrawImage(downimg, left * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.QuestTabStartY * Helper.DisplayRatio, Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio, Helper.DefinitionInfo.QuestTabHeight * Helper.DisplayRatio);
                        g.DrawString(chapters[i], f, Brushes.White, left * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabTitleStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.QuestTabTitleStartY * Helper.DisplayRatio);

                        for (int j = 0; j < questIcons[currentTab].Count; j++)
                        {
                            var sprite = Helper.GetDefinitionFileName(@"\questicons\" + this.questIcons[currentTab][j]);

                            var img = Helper.Sprite2Png(sprite,false);
                            questiconwidth = (int)(img.Width / 27 * Helper.DisplayRatio);
                            questiconheight = (int)(img.Height * Helper.DisplayRatio);

                            int frame = 0;
                            if (awarddict[currentTab][j]) frame = 18;
                            var icon = Helper.GetImageByFrame(img, 27, frame);
                            g.DrawImage(icon,new RectangleF(left * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconWidth * Helper.DisplayRatio * (j % 3), Helper.DefinitionInfo.QuestIconStartY * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconHeight * Helper.DisplayRatio * (j / 3),icon.Width*Helper.DisplayRatio,icon.Height*Helper.DisplayRatio),new Rectangle(0,0,icon.Width,icon.Height),GraphicsUnit.Pixel);

                            if (currentquest == j)
                            {
                                g.DrawImage(gem,
                                    new RectangleF(0+left * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconWidth * Helper.DisplayRatio * (j % 3) + 25*Helper.DisplayRatio,Helper.DefinitionInfo.QuestIconStartY * Helper.DisplayRatio + Helper.DefinitionInfo.QuestIconHeight  *Helper.DisplayRatio*(j / 3) - 40*Helper.DisplayRatio+4,icon.Width*Helper.DisplayRatio,icon.Height*Helper.DisplayRatio),
                                    new RectangleF(0,0,icon.Width,icon.Height),
                                    GraphicsUnit.Pixel);
                            }
                        }
                    }
                    else
                    {
                        //var upimg = Image.FromFile(Helper.GetDefinitionFileName(Helper.ExePath + Helper.DefinitionPath + @"customized\tabup"));
                        g.DrawImage(upimg, left * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.QuestTabStartY * Helper.DisplayRatio , Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio, Helper.DefinitionInfo.QuestTabHeight * Helper.DisplayRatio);

                        g.DrawString(chapters[i], f, Brushes.Gray, left * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabTitleStartX * Helper.DisplayRatio + Helper.DefinitionInfo.QuestTabWidth * Helper.DisplayRatio * i, Helper.DefinitionInfo.QuestTabTitleStartY * Helper.DisplayRatio);
                    }
                }
            }

            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
