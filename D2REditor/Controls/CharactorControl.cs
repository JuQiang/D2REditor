using D2SLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class CharactorControl : UserControl
    {
        private Bitmap chabackbmp, advchabackmp, expbmp, advbtnbmp, advitembmp, levelbmp;
        private Dictionary<string, int> stats;
        private ButtonEx btnAdd1, btnAdd2, btnAdd3, btnAdd4;
        public CharactorControl()
        {
            InitializeComponent();

            var chaback = Helper.GetDefinitionFileName(@"\panel\character_sheet\character_background");
            chabackbmp = Helper.Sprite2Png(chaback);

            var advchaback = Helper.GetDefinitionFileName(@"\panel\advancedcharactersheet\advancedcharactersheet_bg");
            advchabackmp = Helper.Sprite2Png(advchaback);

            var exp = Helper.GetDefinitionFileName(@"\panel\character_sheet/expbarfill");
            expbmp = Helper.Sprite2Png(exp);

            var advbtn = Helper.GetDefinitionFileName(@"\panel\character_sheet\advancedstatsbutton");
            advbtnbmp = Helper.GetImageByFrame(Helper.Sprite2Png(advbtn), 4, 0);

            var advitem = Helper.GetDefinitionFileName(@"\panel\advancedcharactersheet\advancedcharactersheet_valuebox");
            advitembmp = Helper.GetImageByFrame(Helper.Sprite2Png(advitem), 1, 0);

            var levelitem = Helper.GetDefinitionFileName(@"\panel\character_sheet\levelupbutton");
            levelbmp = Helper.GetImageByFrame(Helper.Sprite2Png(levelitem), 4, 0);


            btnAdd1 = new ButtonEx();
            btnAdd1.Name = "btnAdd1";
            btnAdd1.ImageFrames = 4;
            btnAdd1.ImageFile = @"\panel\character_sheet\levelupbutton";
            btnAdd1.Location = new Point(left + 218, 194); btnAdd1.Size = new Size(36, 36);
            btnAdd1.KeyDown += BtnAdd_KeyDown; btnAdd1.KeyUp += BtnAdd_KeyUp;
            btnAdd1.MouseUp += BtnAdd_MouseUp;
            this.Controls.Add(btnAdd1);
            btnAdd2 = new ButtonEx();
            btnAdd2.Name = "btnAdd2";
            btnAdd2.ImageFrames = 4;
            btnAdd2.ImageFile = @"\panel\character_sheet\levelupbutton";
            btnAdd2.Location = new Point(left + 218, 324); btnAdd2.Size = new Size(36, 36);
            btnAdd2.KeyDown += BtnAdd_KeyDown; btnAdd2.KeyUp += BtnAdd_KeyUp;
            btnAdd2.MouseUp += BtnAdd_MouseUp;
            this.Controls.Add(btnAdd2);
            btnAdd3 = new ButtonEx();
            btnAdd3.Name = "btnAdd3";
            btnAdd3.ImageFrames = 4;
            btnAdd3.ImageFile = @"\panel\character_sheet\levelupbutton";
            btnAdd3.Location = new Point(left + 218, 450); btnAdd3.Size = new Size(36, 36);
            btnAdd3.KeyDown += BtnAdd_KeyDown; btnAdd3.KeyUp += BtnAdd_KeyUp;
            btnAdd3.MouseUp += BtnAdd_MouseUp;
            this.Controls.Add(btnAdd3);
            btnAdd4 = new ButtonEx();
            btnAdd4.Name = "btnAdd4";
            btnAdd4.ImageFrames = 4;
            btnAdd4.ImageFile = @"\panel\character_sheet\levelupbutton";
            btnAdd4.Location = new Point(left + 218, 530); btnAdd4.Size = new Size(36, 36);
            btnAdd4.KeyDown += BtnAdd_KeyDown; btnAdd4.KeyUp += BtnAdd_KeyUp;
            btnAdd4.MouseUp += BtnAdd_MouseUp;
            this.Controls.Add(btnAdd4);

            this.Width = chabackbmp.Width;
            this.Height = chabackbmp.Height;

            this.KeyDown += BtnAdd_KeyDown;
            this.KeyUp += BtnAdd_KeyUp;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void BtnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            isAltPressed = e.Alt;
            isCtrlPressed = e.Control;
            isShiftPressed = e.Shift;
        }

        private void BtnAdd_KeyUp(object sender, KeyEventArgs e)
        {
            isAltPressed = false;
            isCtrlPressed = false;
            isShiftPressed = false;
        }

        private bool isAltPressed, isCtrlPressed, isShiftPressed;

        private void BtnAdd_MouseUp(object sender, MouseEventArgs e)
        {
            string[] keys = new string[] { "nouse", "strength", "dexterity", "vitality", "energy" };
            string[] orikeys = new string[] { "nouse", "str", "dex", "vit", "int" };

            var key = Convert.ToInt32((sender as Button).Name.Replace("btnAdd", ""));

            int ori = this.stats[keys[key]];

            if (((!isAltPressed) && (!isCtrlPressed) && (!isShiftPressed)) || (isAltPressed)) { this.stats[keys[key]] = this.stats[keys[key]] + 1; }
            if (isCtrlPressed) { this.stats[keys[key]] = this.stats[keys[key]] + 5; }
            if (isShiftPressed) { this.stats[keys[key]] = 1023; }// + ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class][orikeys[key]].ToInt32();  }

            if (this.stats[keys[key]] > 1023)/*+ ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class][orikeys[key]].ToInt32())*/ this.stats[keys[key]] = 1023;// + ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class][orikeys[key]].ToInt32();

            this.stats["statpts"] = this.stats["statpts"] - (this.stats[keys[key]] - ori);
            this.Invalidate();
        }

        public void InitData()
        {
            this.stats = Helper.CurrentCharactor.Attributes.Stats;
            SetAdjustmentVisible();


            //for (int i = 0; i < 20; i++)
            //{
            //    lbAdvancedProperties.Items.Add("法力恢复速度提高 10%");
            //    lbAdvancedProperties.Items.Add("回复耐力加 10%");
            //    lbAdvancedProperties.Items.Add("699%\r\n更加的机会取得魔法装备");
            //}

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        public void SetAdjustmentVisible()
        {
            bool vis = stats.ContainsKey("statpts");
            if (vis) vis = Convert.ToInt32(stats["statpts"]) > 0;

            btnAdd1.Visible = vis;
            btnAdd2.Visible = vis;
            btnAdd3.Visible = vis;
            btnAdd4.Visible = vis;
        }

        int left = 0;
        private void CharactorControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(chabackbmp, left, 0, chabackbmp.Width, chabackbmp.Height);
            //g.DrawImage(advchabackmp, left + chabackbmp.Width, 0, advchabackmp.Width, advchabackmp.Height);            

            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize, FontStyle.Bold))
            {
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString(Utils.AllJsons["minipanelchar"], f, Helper.TextBrush, new Rectangle(0, 36, this.Width, 40), sf);
                }
            }

            using (Font f = new Font("SimSun", 18, FontStyle.Bold))
            {
                //g.DrawString("角色", f, Helper.TextBrush, 260, 36);
                g.DrawString(Helper.CurrentCharactor.DisplayName, f, Brushes.White, left + 60, 80);
                if (stats.ContainsKey("statpts") && Convert.ToInt32(stats["statpts"]) > 0)
                {

                    DrawSimpleTextCenter(g, f, String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["CharacterSheetStatPointsRemainingSingular"]), stats["statpts"]), new Rectangle(left + 60, 690, 460, 40), Brushes.Red);
                }

                //DrawSimpleTextCenter(g, f, Utils.AllJsons["AdvancedStats"], new Rectangle(left+chabackbmp.Width+75, 35, 300, 30),Helper.TextBrush);
            }


            using (Font f = new Font("SimSun", 12, FontStyle.Bold))
            {
                g.DrawString(String.Format("{2} {0} {1}", Helper.CurrentCharactor.Level, Helper.CurrentCharactor.ClassName, Utils.AllJsons["Level"]), f, Brushes.White, left + 60, 126);

                DrawExperience(g, f);

                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrstr"], new Rectangle(left + 45, 200, 124, 25)); try { DrawSimpleTextCenter(g, f, stats["strength"].ToString(), new Rectangle(left + 170, 200, 48, 22)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrdex"], new Rectangle(left + 45, 330, 124, 25)); try { DrawSimpleTextCenter(g, f, stats["dexterity"].ToString(), new Rectangle(left + 170, 330, 48, 22)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrvit"], new Rectangle(left + 45, 456, 124, 25)); try { DrawSimpleTextCenter(g, f, stats["vitality"].ToString(), new Rectangle(left + 170, 456, 48, 22)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchreng"], new Rectangle(left + 45, 536, 124, 25)); try { DrawSimpleTextCenter(g, f, stats["energy"].ToString(), new Rectangle(left + 170, 536, 48, 22)); } catch { }//stamina,maxstamina;hitpoints,maxhp,;mana,maxmana;


                //DrawSimpleTextCenter(g, f, Utils.AllJsons["Damage"], new Rectangle(left + 270, 165, 140, 44));
                //DrawSimpleTextCenter(g, f, Utils.AllJsons["strchratr"], new Rectangle(left + 270, 270, 140, 44));
                //DrawSimpleTextCenter(g, f, Utils.AllJsons["AC"], new Rectangle(left + 270, 373, 140, 36));

                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrstm"], new Rectangle(left + 270, 432, 140, 30)); try { DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["stamina"], stats["maxstamina"]), new Rectangle(left + 425, 432, 105, 30)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrlif"], new Rectangle(left + 270, 484, 140, 30)); try { DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["hitpoints"], stats["maxhp"]), new Rectangle(left + 425, 484, 105, 30)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrman"], new Rectangle(left + 270, 537, 140, 30)); try { DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["mana"], stats["maxmana"]), new Rectangle(left + 425, 537, 105, 30)); } catch { }

                var reslist = Helper.GetCharacterResistances();
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrfir"], new Rectangle(left + 48, 595, 175, 30)); DrawSimpleTextCenter(g, f, reslist.Item1.ToString() + "%", new Rectangle(left + 230, 595, 50, 25), Helper.TextBrush);
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrlit"], new Rectangle(left + 300, 595, 175, 30)); DrawSimpleTextCenter(g, f, reslist.Item2.ToString() + "%", new Rectangle(left + 484, 595, 50, 25), Helper.TextBrush);
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrcol"], new Rectangle(left + 48, 630, 175, 30)); DrawSimpleTextCenter(g, f, reslist.Item3.ToString() + "%", new Rectangle(left + 230, 630, 50, 25), Helper.TextBrush);
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrpos"], new Rectangle(left + 300, 630, 175, 30)); DrawSimpleTextCenter(g, f, reslist.Item4.ToString() + "%", new Rectangle(left + 484, 630, 50, 25), Helper.TextBrush);
            }

            g.DrawImage(expbmp, new Rectangle(left + 66, 114, expbmp.Width * Helper.CurrentCharactor.Level / 99, expbmp.Height));
            g.DrawImage(advbtnbmp, left + 557, 355);

            //if (this.reorg)
            //{
            //    g.DrawImage(levelbmp, left + 215, 190);
            //    g.DrawImage(levelbmp, left + 215, 320);
            //    g.DrawImage(levelbmp, left + 215, 447);
            //    g.DrawImage(levelbmp, left + 215, 527);
            //}
        }

        private void DrawExperience(Graphics g, Font f)
        {
            var r = new Rectangle(left + 220, 126, 300, 20);
            var expdesc = "";
            uint curexp = 0;
            if (stats.ContainsKey("experience")) curexp = (uint)(stats["experience"]);
            uint matchedexp = ExcelTxt.ExperienceTxt[Helper.CurrentCharactor.Level.ToString()]["Amazon"].ToUInt32();

            var exp = Utils.AllJsons["panelexp"];
            int index = exp.LastIndexOf("%u");
            exp = exp.Substring(0, index) + "FQQ";
            expdesc = exp.Replace("%u", curexp.ToString()).Replace("FQQ", matchedexp.ToString());
            //if (Helper.CurrentCharactor.Level == 99)
            //{
            //    expdesc = "最高等级，经验值已满";
            //}
            //else
            //{
            //    expdesc = String.Format("{0}之{1}", curexp, matchedexp);
            //}

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
