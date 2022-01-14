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
            btnAdd1.Location = new Point(left + (int)(218*Helper.DisplayRatio), (int)(194 * Helper.DisplayRatio)); btnAdd1.Size = new Size((int)(36 * Helper.DisplayRatio), (int)(36 * Helper.DisplayRatio));
            btnAdd1.KeyDown += BtnAdd_KeyDown; btnAdd1.KeyUp += BtnAdd_KeyUp;
            btnAdd1.MouseUp += BtnAdd_MouseUp;
            this.Controls.Add(btnAdd1);
            btnAdd2 = new ButtonEx();
            btnAdd2.Name = "btnAdd2";
            btnAdd2.ImageFrames = 4;
            btnAdd2.ImageFile = @"\panel\character_sheet\levelupbutton";
            btnAdd2.Location = new Point(left + (int)(218 * Helper.DisplayRatio), (int)(324 * Helper.DisplayRatio)); btnAdd2.Size = new Size((int)(36 * Helper.DisplayRatio), (int)(36 * Helper.DisplayRatio));
            btnAdd2.KeyDown += BtnAdd_KeyDown; btnAdd2.KeyUp += BtnAdd_KeyUp;
            btnAdd2.MouseUp += BtnAdd_MouseUp;
            this.Controls.Add(btnAdd2);
            btnAdd3 = new ButtonEx();
            btnAdd3.Name = "btnAdd3";
            btnAdd3.ImageFrames = 4;
            btnAdd3.ImageFile = @"\panel\character_sheet\levelupbutton";
            btnAdd3.Location = new Point(left + (int)(218 * Helper.DisplayRatio), (int)(450 * Helper.DisplayRatio)); btnAdd3.Size = new Size((int)(36 * Helper.DisplayRatio), (int)(36 * Helper.DisplayRatio));
            btnAdd3.KeyDown += BtnAdd_KeyDown; btnAdd3.KeyUp += BtnAdd_KeyUp;
            btnAdd3.MouseUp += BtnAdd_MouseUp;
            this.Controls.Add(btnAdd3);
            btnAdd4 = new ButtonEx();
            btnAdd4.Name = "btnAdd4";
            btnAdd4.ImageFrames = 4;
            btnAdd4.ImageFile = @"\panel\character_sheet\levelupbutton";
            btnAdd4.Location = new Point(left + (int)(218 * Helper.DisplayRatio), (int)(530 * Helper.DisplayRatio)); btnAdd4.Size = new Size((int)(36 * Helper.DisplayRatio), (int)(36 * Helper.DisplayRatio));
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

            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize * Helper.DisplayRatio, FontStyle.Bold))
            {
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString(Utils.AllJsons["minipanelchar"], f, Helper.TextBrush, new RectangleF(0, 36*Helper.DisplayRatio, this.Width, 40 * Helper.DisplayRatio), sf);
                }
            }

            using (Font f = new Font("SimSun", 9, FontStyle.Bold))
            {
                //g.DrawString("角色", f, Helper.TextBrush, 260, 36);
                g.DrawString(Helper.CurrentCharactor.DisplayName, f, Brushes.White, left + 60 * Helper.DisplayRatio, 80 * Helper.DisplayRatio);
                if (stats.ContainsKey("statpts") && Convert.ToInt32(stats["statpts"]) > 0)
                {

                    DrawSimpleTextCenter(g, f, String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["CharacterSheetStatPointsRemainingSingular"]), stats["statpts"]), new Rectangle(left + 60, 690, 460, 40), Brushes.Red);
                }

                //DrawSimpleTextCenter(g, f, Utils.AllJsons["AdvancedStats"], new Rectangle(left+chabackbmp.Width+75, 35, 300, 30),Helper.TextBrush);
            }


            using (Font f = new Font("SimSun", 9, FontStyle.Bold))
            {
                g.DrawString(String.Format("{2} {0} {1}", Helper.CurrentCharactor.Level, Helper.CurrentCharactor.ClassName, Utils.AllJsons["Level"]), f, Brushes.White, left + 60 * Helper.DisplayRatio, 126 * Helper.DisplayRatio);

                DrawExperience(g, f);

                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrstr"], new RectangleF(left + 45 * Helper.DisplayRatio, 200 * Helper.DisplayRatio, 124 * Helper.DisplayRatio, 25 * Helper.DisplayRatio)); try { DrawSimpleTextCenter(g, f, stats["strength"].ToString(), new RectangleF(left + 170 * Helper.DisplayRatio, 200 * Helper.DisplayRatio, 48 * Helper.DisplayRatio, 22 * Helper.DisplayRatio)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrdex"], new RectangleF(left + 45 * Helper.DisplayRatio, 330 * Helper.DisplayRatio, 124 * Helper.DisplayRatio, 25 * Helper.DisplayRatio)); try { DrawSimpleTextCenter(g, f, stats["dexterity"].ToString(), new RectangleF(left + 170 * Helper.DisplayRatio, 330 * Helper.DisplayRatio, 48 * Helper.DisplayRatio, 22 * Helper.DisplayRatio)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrvit"], new RectangleF(left + 45 * Helper.DisplayRatio, 456 * Helper.DisplayRatio, 124 * Helper.DisplayRatio, 25 * Helper.DisplayRatio)); try { DrawSimpleTextCenter(g, f, stats["vitality"].ToString(), new RectangleF(left + 170 * Helper.DisplayRatio, 456 * Helper.DisplayRatio, 48 * Helper.DisplayRatio, 22 * Helper.DisplayRatio)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchreng"], new RectangleF(left + 45 * Helper.DisplayRatio, 536 * Helper.DisplayRatio, 124 * Helper.DisplayRatio, 25 * Helper.DisplayRatio)); try { DrawSimpleTextCenter(g, f, stats["energy"].ToString(), new RectangleF(left + 170 * Helper.DisplayRatio, 536 * Helper.DisplayRatio, 48 * Helper.DisplayRatio, 22 * Helper.DisplayRatio)); } catch { }//stamina,maxstamina;hitpoints,maxhp,;mana,maxmana;


                //DrawSimpleTextCenter(g, f, Utils.AllJsons["Damage"], new Rectangle(left + 270, 165, 140, 44));
                //DrawSimpleTextCenter(g, f, Utils.AllJsons["strchratr"], new Rectangle(left + 270, 270, 140, 44));
                //DrawSimpleTextCenter(g, f, Utils.AllJsons["AC"], new Rectangle(left + 270, 373, 140, 36));

                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrstm"], new RectangleF(left + 270 * Helper.DisplayRatio, 432 * Helper.DisplayRatio, 140 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); try { DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["stamina"], stats["maxstamina"]), new RectangleF(left + 425 * Helper.DisplayRatio, 432 * Helper.DisplayRatio, 105 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrlif"], new RectangleF(left + 270 * Helper.DisplayRatio, 484 * Helper.DisplayRatio, 140 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); try { DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["hitpoints"], stats["maxhp"]), new RectangleF(left + 425 * Helper.DisplayRatio, 484 * Helper.DisplayRatio, 105 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); } catch { }
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrman"], new RectangleF(left + 270 * Helper.DisplayRatio, 537 * Helper.DisplayRatio, 140 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); try { DrawSimpleTextCenter(g, f, String.Format("{0}/{1}", stats["mana"], stats["maxmana"]), new RectangleF(left + 425 * Helper.DisplayRatio, 537 * Helper.DisplayRatio, 105 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); } catch { }

                var reslist = Helper.GetCharacterResistances();
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrfir"], new RectangleF(left + 48 * Helper.DisplayRatio, 595 * Helper.DisplayRatio, 175 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); DrawSimpleTextCenter(g, f, reslist.Item1.ToString() + "%", new RectangleF(left + 230 * Helper.DisplayRatio, 595 * Helper.DisplayRatio, 50 * Helper.DisplayRatio, 25 * Helper.DisplayRatio), Helper.TextBrush);
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrlit"], new RectangleF(left + 300 * Helper.DisplayRatio, 595 * Helper.DisplayRatio, 175 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); DrawSimpleTextCenter(g, f, reslist.Item2.ToString() + "%", new RectangleF(left + 484 * Helper.DisplayRatio, 595 * Helper.DisplayRatio, 50 * Helper.DisplayRatio, 25 * Helper.DisplayRatio), Helper.TextBrush);
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrcol"], new RectangleF(left + 48 * Helper.DisplayRatio, 630 * Helper.DisplayRatio, 175 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); DrawSimpleTextCenter(g, f, reslist.Item3.ToString() + "%", new RectangleF(left + 230 * Helper.DisplayRatio, 630 * Helper.DisplayRatio, 50 * Helper.DisplayRatio, 25 * Helper.DisplayRatio), Helper.TextBrush);
                DrawSimpleTextCenter(g, f, Utils.AllJsons["strchrpos"], new RectangleF(left + 300 * Helper.DisplayRatio, 630 * Helper.DisplayRatio, 175 * Helper.DisplayRatio, 30 * Helper.DisplayRatio)); DrawSimpleTextCenter(g, f, reslist.Item4.ToString() + "%", new RectangleF(left + 484 * Helper.DisplayRatio, 630 * Helper.DisplayRatio, 50 * Helper.DisplayRatio, 25 * Helper.DisplayRatio), Helper.TextBrush);
            }

            g.DrawImage(expbmp, new RectangleF(left + 66 * Helper.DisplayRatio, 114 * Helper.DisplayRatio, expbmp.Width * Helper.DisplayRatio * Helper.CurrentCharactor.Level / 99, expbmp.Height * Helper.DisplayRatio));
            g.DrawImage(advbtnbmp, left + 557 * Helper.DisplayRatio, 355 * Helper.DisplayRatio);

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
            var r = new RectangleF(left + 220 * Helper.DisplayRatio, 126 * Helper.DisplayRatio, 300 * Helper.DisplayRatio, 20 * Helper.DisplayRatio);
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

        private void DrawSimpleTextCenter(Graphics g, Font f, string text, RectangleF r, Brush b)
        {
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(text, f, b, r, sf);
            }
        }

        private void DrawSimpleTextCenter(Graphics g, Font f, string text, RectangleF r)
        {
            DrawSimpleTextCenter(g, f, text, r, Brushes.White);
        }

    }
}
