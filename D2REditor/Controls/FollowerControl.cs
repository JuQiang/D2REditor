using D2SLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class FollowerControl : UserControl
    {
        private Bitmap chabackbmp, advchabackmp, expbmp, advbtnbmp, advitembmp, advbackbmp, levelbmp;
        private ListBox lbAdvancedProperties;
        private Brush brush, brush2;
        private bool reorg;
        public FollowerControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);


            var chaback = Helper.GetDefinitionFileName(@"\panel\hireling\hirelingpanel");
            chabackbmp = Helper.Sprite2Png(chaback);

            var advchaback = Helper.GetDefinitionFileName(@"\panel\advancedcharactersheet\advancedcharactersheet_bg");
            advchabackmp = Helper.Sprite2Png(advchaback);

            var exp = Helper.GetDefinitionFileName(@"\panel\character_sheet\expbarfill");
            expbmp = Helper.Sprite2Png(exp);

            var advbtn = Helper.GetDefinitionFileName(@"\panel\character_sheet\advancedstatsbutton");
            advbtnbmp = Helper.GetImageByFrame(Helper.Sprite2Png(advbtn), 4, 0);

            var advitem = Helper.GetDefinitionFileName(@"\panel\advancedcharactersheet\advancedcharactersheet_valuebox");
            advitembmp = Helper.GetImageByFrame(Helper.Sprite2Png(advitem), 1, 0);

            var levelitem = Helper.GetDefinitionFileName(@"\panel\character_sheet\levelupbutton");
            levelbmp = Helper.GetImageByFrame(Helper.Sprite2Png(levelitem), 4, 0);


            brush = new SolidBrush(Color.FromArgb(255, 9, 9, 10));
            brush2 = new SolidBrush(Color.FromArgb(70, 199, 179, 119));

            lbAdvancedProperties = new ListBox();

            lbAdvancedProperties.Size = new Size(300, 570);
            lbAdvancedProperties.Location = new Point(chabackbmp.Width + 80, 125);

            advbackbmp = new Bitmap(lbAdvancedProperties.Size.Width, lbAdvancedProperties.Size.Height);
            //把原图的对应那部分背景图片扣下来
            Graphics.FromImage(advbackbmp).DrawImage(advchabackmp, 0, 0, new Rectangle(lbAdvancedProperties.Location.X - chabackbmp.Width, lbAdvancedProperties.Location.Y, advbackbmp.Width, advbackbmp.Height), GraphicsUnit.Pixel);

            lbAdvancedProperties.BorderStyle = BorderStyle.None;
            lbAdvancedProperties.BackColor = Color.Black;

            //lbAdvancedProperties.
            //lbAdvancedProperties.MeasureItem += LbAdvancedProperties_MeasureItem;
            //lbAdvancedProperties.DrawItem += LbAdvancedProperties_DrawItem;
            lbAdvancedProperties.DrawMode = DrawMode.OwnerDrawVariable;

            //this.Controls.Add(lbAdvancedProperties);

            this.Paint += FollowerControl_Paint;

            reorg = false;
        }

        private void FollowerControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(chabackbmp, 0, 0, chabackbmp.Width, chabackbmp.Height);
            g.DrawImage(advchabackmp, chabackbmp.Width, 0, advchabackmp.Width, advchabackmp.Height);
            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize, FontStyle.Bold))
            {
                var text = Utils.AllJsons["MiniPanelHireinv"];
                var sf = g.MeasureString(text, f);
                g.DrawString(text, f, Helper.TextBrush, (chabackbmp.Width - sf.Width) / 2, 34);
            }

            int nameid = Helper.CurrentCharactor.Mercenary.NameId;
            var mname = Utils.AllJsons[String.Format("merc{0:d2}", nameid + 1)];
            var row = ExcelTxt.HirelingTxt.Rows.Where(r => r["Difficulty"].ToInt32() == 3 && r["Version"].ToInt32() == 100 && r["Id"].ToInt32() == Helper.CurrentCharactor.Mercenary.TypeId).FirstOrDefault();
            if (row == null) return;

            var stats = Helper.CurrentCharactor.Attributes.Stats;

            using (Font f = new Font("SimSun", 14, FontStyle.Bold))
            {
                g.DrawString(String.Format("等级 {0} {1}", Helper.CurrentCharactor.Level, Helper.CurrentCharactor.ClassName), f, Brushes.White, 60, 436);

                DrawExperience(g, f);

                if (stats != null)
                {
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrstr"], new Rectangle(45, 200, 124, 25)); if (stats.ContainsKey("strength")) DrawSimpleTextCenter(g, f, stats["strength"].ToString(), new Rectangle(176, 200, 36, 22));
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrdex"], new Rectangle(45, 330, 124, 25)); if (stats.ContainsKey("dexterity")) DrawSimpleTextCenter(g, f, stats["dexterity"].ToString(), new Rectangle(176, 330, 36, 22));
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrvit"], new Rectangle(45, 456, 124, 25)); if (stats.ContainsKey("vitality")) DrawSimpleTextCenter(g, f, stats["vitality"].ToString(), new Rectangle(176, 456, 36, 22));
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchreng"], new Rectangle(45, 536, 124, 25)); if (stats.ContainsKey("energy")) DrawSimpleTextCenter(g, f, stats["energy"].ToString(), new Rectangle(176, 536, 36, 22));

                    DrawSimpleTextCenter(g, f, Utils.AllJsons["Damage"], new Rectangle(270, 165, 140, 44));
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchratr"], new Rectangle(270, 270, 140, 44));
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["AC"], new Rectangle(270, 373, 140, 36));

                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrstm"], new Rectangle(270, 432, 140, 30)); if (stats.ContainsKey("stamina")) DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["stamina"], stats["maxstamina"]), new Rectangle(425, 432, 105, 30));
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrlif"], new Rectangle(270, 484, 140, 30)); if (stats.ContainsKey("hitpoints")) DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["hitpoints"], stats["maxhp"]), new Rectangle(425, 484, 105, 30));
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrman"], new Rectangle(270, 537, 140, 30)); if (stats.ContainsKey("mana")) DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["mana"], stats["maxmana"]), new Rectangle(425, 537, 105, 30));

                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrfir"], new Rectangle(48, 595, 175, 30)); DrawSimpleTextCenter(g, f, "75%", new Rectangle(230, 595, 50, 25), Helper.TextBrush);
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrlit"], new Rectangle(300, 595, 175, 30)); DrawSimpleTextCenter(g, f, "75%", new Rectangle(484, 595, 50, 25), Helper.TextBrush);
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrcol"], new Rectangle(48, 630, 175, 30)); DrawSimpleTextCenter(g, f, "75%", new Rectangle(230, 630, 50, 25), Helper.TextBrush);
                    DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrpos"], new Rectangle(300, 630, 175, 30)); DrawSimpleTextCenter(g, f, "75%", new Rectangle(484, 630, 50, 25), Helper.TextBrush);
                }
            }

            g.DrawImage(expbmp, new Rectangle(66, 114, expbmp.Width * Helper.CurrentCharactor.Level / 99, expbmp.Height));
            g.DrawImage(advbtnbmp, 557, 355);

            if (this.reorg)
            {
                g.DrawImage(levelbmp, 215, 190);
                g.DrawImage(levelbmp, 215, 320);
                g.DrawImage(levelbmp, 215, 447);
                g.DrawImage(levelbmp, 215, 527);
            }
        }

        private void DrawExperience(Graphics g, Font f)
        {
            var r = new Rectangle(120, 126, 200, 20);
            var expdesc = "";
            uint curexp = Helper.CurrentCharactor.Mercenary.Experience;
            int matchedexp = 0;// ExcelTxt.ExperienceTxt[this.charactor.Level.ToString()]["Amazon"].ToInt32();

            //if (Helper.CurrentCharactor.Mercenary.this.charactor.Level == 99)
            //{
            //    expdesc = "最高等级，经验值已满";
            //}
            //else
            {
                expdesc = String.Format("{0}之{1}", curexp, matchedexp);
            }

            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Far;
                g.DrawString(expdesc, f, Brushes.White, r, sf);
            }
        }

        private void DrawSimpleTextCenter(Graphics g, Font f, string text, Rectangle r, Brush b)
        {
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(text, f, b, r, sf);
            }
        }

        private void DrawSimpleTextCenter(Graphics g, Font f, string text, Rectangle r)
        {
            DrawSimpleTextCenter(g, f, text, r, Brushes.White);
        }
    }
}
