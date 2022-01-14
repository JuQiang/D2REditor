using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class OptimizeControl : UserControl
    {
        public OptimizeControl()
        {
            InitializeComponent();
        }

        private void OptimizeControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize*Helper.DisplayRatio, FontStyle.Bold))
            {
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString(Utils.AllJsons["Unblock"], f, Helper.TextBrush, new RectangleF(0, 36 * Helper.DisplayRatio, this.Width, 40 * Helper.DisplayRatio), sf);
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (cbLevel99.Checked) Helper.SetLevel99();

            if (cbSkill20.Checked) Helper.SetSkill20();

            if (cbAllWays.Checked)
            {
                List<WaypointsDifficulty> waypoints = new List<WaypointsDifficulty>() { Helper.CurrentCharactor.Waypoints.Normal, Helper.CurrentCharactor.Waypoints.Nightmare, Helper.CurrentCharactor.Waypoints.Hell };
                foreach (var way in waypoints)
                {
                    foreach (var wayprop in way.GetType().GetProperties())
                    {
                        if (wayprop.Name.IndexOf("Header") > -1) continue;

                        var act = wayprop.GetValue(way);
                        foreach (var actprop in act.GetType().GetProperties())
                        {
                            actprop.SetValue(act, true);
                        }
                    }
                }
            }

            if (CbAllQuests.Checked)
            {
                List<QuestsDifficulty> quests = new List<QuestsDifficulty>() { Helper.CurrentCharactor.Quests.Normal, Helper.CurrentCharactor.Quests.Nightmare, Helper.CurrentCharactor.Quests.Hell };
                foreach (var quest in quests)
                {
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
                }
                Helper.CurrentCharactor.Progression = 15;
            }

            if (cbAllMoney.Checked)
            {
                Helper.CurrentCharactor.Attributes.Stats["gold"] = 990000;
                Helper.CurrentCharactor.Attributes.Stats["goldbank"] = 2500000;
            }
        }

        Bitmap backbmp;

        private void OptimizeControl_Load(object sender, EventArgs e)
        {
            this.Size = new Size((int)(1162 * Helper.DisplayRatio), (int)(753 * Helper.DisplayRatio));

            var back = Helper.GetDefinitionFileName(@"\panel\hireling\hireablepanel\hireables_bg");
            backbmp = Helper.Sprite2Png(back);
            this.BackgroundImage = backbmp;

            btnModify.ImageFile = Helper.GeneralButtonImageFile;
        }
    }
}
