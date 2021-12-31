﻿using D2SLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class SkillsControl : UserControl
    {
        string[] classes = new string[] { "amazon", "sorceress", "necromancer", "paladin", "barbarian", "druid", "assassin" };
        Bitmap[] backbmp = new Bitmap[3];
        Bitmap[] skillbmp = new Bitmap[60];
        Bitmap bgbmp, waypointsbackbmp;
        int curtab = 0;
        List<TxtRow> skillList;
        string clsname;
        public SkillsControl()
        {
            InitializeComponent();

            clsname = classes[Helper.CurrentCharactor.ClassId];

            var questback = Helper.GetDefinitionFileName(@"\panel\waypoints\waypoints_base");
            waypointsbackbmp = Helper.Sprite2Png(questback);

            var bg = Helper.GetDefinitionFileName(@"\spells\skill_trees\panel_skilltreebg");
            bgbmp = Helper.Sprite2Png(bg);

            var back = Helper.GetDefinitionFileName(@"\spells\skill_trees\" + clsname.Substring(0, 2) + "skilltree");
            var bmp = Helper.Sprite2Png(back);
            for (int i = 0; i < 3; i++) backbmp[i] = Helper.GetImageByFrame(bmp, 3, i);

            var skills = Helper.GetDefinitionFileName(@"\spells\" + clsname + @"\" + clsname.Substring(0, 2) + "skillicon");
            var bmp2 = Helper.Sprite2Png(skills);
            for (int i = 0; i < 60; i++) skillbmp[i] = Helper.GetImageByFrame(bmp2, 60, i);

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            skillList = ExcelTxt.SkillsTxt.Rows.Where(s => s["charclass"].Value == classes[Helper.CurrentCharactor.ClassId].Substring(0, 3)).Join(ExcelTxt.SkillDescTxt.Rows, t1 => t1["skill"].Value.ToLower(), t2 => t2["skilldesc"].Value.ToLower(), (t1, t2) => t2).ToList();
            curtab = 0;

            //this.SizeChanged += SkillsControl_SizeChanged;
            this.Paint += SkillsControl_Paint;
            this.MouseUp += SkillsControl_MouseUp;
            this.MouseMove += SkillsControl_MouseMove;

            this.Width = waypointsbackbmp.Width;
            this.Height = waypointsbackbmp.Height;
            BuildMappings();
        }

        private Rectangle currentRectangle = new Rectangle(-1, -1, 0, 0);
        private void SkillsControl_MouseMove(object sender, MouseEventArgs e)
        {
            currentRectangle = new Rectangle(-1, -1, 0, 0);
            foreach (var r in mappings.Keys)
            {
                if (Helper.IsPointInRange(e.Location, r))
                {
                    currentRectangle = r;
                    break;
                }
            }

            this.Invalidate();
        }

        private void SkillsControl_SizeChanged(object sender, EventArgs e)
        {
            //this.left = (this.Width - waypointsbackbmp.Width) / 2;

        }

        private Dictionary<Rectangle, TxtRow> mappings = new Dictionary<Rectangle, TxtRow>();
        private void SkillsControl_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (e.X >= left + 150 * i + tableft && e.X < left + 150 * i + 150 + tableft && e.Y >= 0 + top && e.Y < 42 + top)
                {
                    curtab = i;
                    BuildMappings();
                    this.Invalidate();

                    break;
                }
            }
        }

        private void BuildMappings()
        {
            mappings.Clear();

            var skills = skillList.Where(s => s["SkillPage"].ToInt32() == (3 - curtab));
            foreach (var skill in skills)
            {
                int row = skill["SkillRow"].ToInt32();
                int col = skill["SkillColumn"].ToInt32();

                mappings[new Rectangle(left + tableft + 60 + (col - 1) * 131, top + 69 + (row - 1) * 89, 67, 65)] = skill;
            }
        }

        private int tableft = 67, top = 80;
        private int left = 0;
        private SolidBrush tooltipBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0));

        private void SkillsControl_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            var skills = skillList.Where(s => s["SkillPage"].ToInt32() == (3 - curtab));

            g.DrawImage(waypointsbackbmp, 0, 0);
            g.DrawImage(bgbmp, 36, 70);
            g.DrawImage(backbmp[curtab], tableft, top);
            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize, FontStyle.Bold))
            {
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString(Utils.AllJsons["minipaneltree"], f, Helper.TextBrush, new Rectangle(0, 36, this.Width, 40), sf);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                var s = clsname.Substring(0, 2);

                var key = "SkillCategory" + Convert.ToString(s[0]).ToUpper() + Convert.ToString(s[1]) + Convert.ToString(i + 1);
                var name = Utils.AllJsons[key];
                var sf = g.MeasureString(name, this.Font);//4,4,140,30
                if (curtab == i)
                {
                    using (Font f = new Font("SimSun", 12, FontStyle.Bold))
                    {
                        g.DrawString(name, f, Brushes.White, 50 + 150 * i + 4 + (150 - sf.Width) / 2, top + 4 + (40 - sf.Height) / 2);
                    }
                }
                else
                {
                    using (Font f = new Font("SimSun", 12))
                    {
                        g.DrawString(name, f, Brushes.DarkGray, 50 + 150 * i + 4 + (150 - sf.Width) / 2, top + 4 + (40 - sf.Height) / 2);
                    }
                }
            }

            foreach (var skill in skills)
            {
                int row = skill["SkillRow"].ToInt32();
                int col = skill["SkillColumn"].ToInt32();

                //e.Graphics.DrawRectangle(Pens.Red, new Rectangle(60 + (col - 1) * 131, 69 + (row - 1) * 89, 65, 65));
                g.DrawImage(skillbmp[skill["IconCel"].ToInt32()], tableft + 60 + (col - 1) * 131, top + 69 + (row - 1) * 89);
                int point = Helper.CurrentCharactor.ClassSkills.Skills.Where(s => s.Id == Convert.ToInt32(skill["str name"].Value.ToLower().Replace("skillsname", "").Replace("skillname", ""))).First().Points;
                using (Font f = new Font("SimSun", 16, FontStyle.Bold))
                {
                    g.DrawString(point.ToString(), f, Brushes.White, tableft + 130 + (col - 1) * 131, top + 112 + (row - 1) * 89);
                }

                //Utils.AllJsons[skill["str name"].Value]
            }

            if (currentRectangle.Left > -1 && currentRectangle.Top > -1)
            {
                var skill = mappings[currentRectangle];
                var title = Utils.AllJsons[skill["str name"].Value];
                var tooltip = Utils.AllJsons[skill["str long"].Value];
                int width = 0, height = 0;

                using (Font f = new Font("SimSun", Helper.DefinitionInfo.TooltipFontSize, FontStyle.Bold))
                {
                    var sf = g.MeasureString(title, f, this.Width - 20);
                    var sf2 = g.MeasureString(tooltip, f, this.Width - 20);

                    width = (int)(Math.Max(sf.Width, sf2.Width) + 10);
                    height = (int)(sf.Height + 10 + sf2.Height + 10);


                    var trec = new Rectangle(currentRectangle.Left + 67 - left, currentRectangle.Top + 65, width, height);
                    if (trec.X + left + trec.Width > this.Width)
                    {
                        trec.X = this.Width - trec.Width - left - 10;
                    }
                    if (trec.Y + top + trec.Height > this.Height)
                    {
                        trec.Y = this.Height - trec.Height - top - 10;
                    }


                    g.FillRectangle(this.tooltipBrush, trec);
                    g.DrawRectangle(Pens.Wheat, trec);

                    using (StringFormat format = new StringFormat())
                    {
                        format.Alignment = StringAlignment.Center;

                        g.DrawString(title, f, Brushes.White, new Rectangle(trec.X, trec.Y + 10, trec.Width, (int)sf.Height + 2), format);
                        g.DrawString(tooltip, f, Brushes.White, new Rectangle(trec.X, trec.Y + (int)sf.Height + 10, trec.Width, (int)sf2.Height), format);
                    }
                }
            }

            e.Graphics.DrawImage(bmp, left + 0, 0);
        }
    }
}