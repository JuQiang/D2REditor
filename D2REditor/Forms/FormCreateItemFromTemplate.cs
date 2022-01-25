using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormCreateItemFromTemplate : Form
    {
        private D2S character;
        private string templateFileName;
        public Item NewItem { get; set; }
        private Bitmap closebmp, backbmp, blockbmp, blockbmp2;

        string[] types = new string[] { Utils.AllJsons["strBSWeapons"], Utils.AllJsons["strBSArmor"], Utils.AllJsons["strBSMisc"], Utils.AllJsons["uniques"], Utils.AllJsons["sets"], Utils.AllJsons["runewords"] };
        public Dictionary<string, List<Item>> curSubTypes;

        public FormCreateItemFromTemplate()
        {
            InitializeComponent();


        }

        public FormCreateItemFromTemplate(string templateFileName) : this()
        {
            this.character = Core.ReadD2S(Helper.CurrentD2SFileName);
            this.templateFileName = templateFileName;
        }

        private void QuitNow()
        {
            if (lbItems.SelectedIndex < 0) return;

            NewItem = lbItems.Items[lbItems.SelectedIndex] as Item;
            NewItem.Page = 1;
            NewItem.Mode = ItemMode.Stored;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }


        private void FormCreateItemFromTemplate_Load(object sender, EventArgs e)
        {
            var back = Helper.GetDefinitionFileName(@"\panel\options\frontendoptionsbg");
            backbmp = Helper.Sprite2Png(back);
            //this.BackgroundImage = backbmp;
            var close = Helper.GetDefinitionFileName(@"\lobby\friendslist\friendslist_rejectinvite_button");
            closebmp = Helper.GetImageByFrame(Helper.Sprite2Png(close), 3, 0);

            var block = Helper.GetDefinitionFileName(@"\frontend\hd\final\frontend_difficultymodal");
            blockbmp = Helper.Sprite2Png(block);

            blockbmp2 = new Bitmap(blockbmp.Width / 2, blockbmp.Height / 2);
            Graphics g = Graphics.FromImage(blockbmp2);
            g.DrawImage(blockbmp, new Rectangle(0, 0, blockbmp2.Width, blockbmp2.Height), new Rectangle(0, 0, blockbmp.Width, blockbmp.Height), GraphicsUnit.Pixel);

            this.MouseMove += FormCreateItemFromTemplate_MouseMove;
            this.MouseUp += FormOptions_MouseUp;
            this.Paint += FormOptions_Paint;

            //btnImport.ImageFile = Helper.GeneralButtonImageFile;
            //btnImport.Text = Utils.AllJsons["import"];

            btnNext.ImageFile = Helper.GeneralButtonImageFile;
            btnNext.Text = Utils.AllJsons["nav_next"];
            btnPrevious.ImageFile = Helper.GeneralButtonImageFile;
            btnPrevious.Text = Utils.AllJsons["nav_prev"];
            SetNavigation();

            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Enabled = curtab == 2;

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            //btnCancel.ImageFile = Helper.GeneralButtonImageFile;
            //btnOk.ImageFile = Helper.GeneralButtonImageFile;
            //btnCancel.Text = Utils.AllJsons["cancel"];
            //btnOk.Text = Utils.AllJsons["ok"];
            //ProcessTemplates(root);

            //lbItems.BackColor = Color.FromArgb(0, 0, 0);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (elapsed++ > 100) elapsed = 0;
            if (curtab == 2) this.Invalidate();
        }

        Timer timer = new Timer();
        int curtab = 0, hoveringIndex = -1, curPage = 0, elapsed = 0;
        private void FormCreateItemFromTemplate_MouseMove(object sender, MouseEventArgs e)
        {
            if (curtab == 0)
            {
                hoveringIndex = -1;
                for (int i = 0; i < 6; i++)
                {
                    if (Helper.IsPointInRange(new Point(e.X, e.Y), new Rectangle((int)((50 + (i % 3) * 300)*Helper.DisplayRatio), (int)((160 + (i / 3) * 300)*Helper.DisplayRatio), blockbmp.Width, blockbmp.Height)))
                    {
                        hoveringIndex = i;
                        this.Invalidate();
                        break;
                    }
                }
            }
            else if (curtab == 1)
            {
                hoveringIndex = -1;

                var pages = (curSubTypes.Keys.Count - 1) / 30 + 1;
                int total = 30;
                if (curPage == pages) total = curSubTypes.Keys.Count - 30 * pages;

                for (int i = 0; i < total; i++)
                {
                    if (Helper.IsPointInRange(new Point(e.X, e.Y), new Rectangle((int)((50 + 150 * (i % 6))*Helper.DisplayRatio), (int)((80 + ((i % 30) / 6) * 130)*Helper.DisplayRatio), blockbmp2.Width, blockbmp2.Height)))
                    {
                        hoveringIndex = i;
                        this.Invalidate();
                        break;
                    }
                }
            }
        }

        private string curTitle = Utils.AllJsons["import"];
        private void FormOptions_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X >= this.Width - 24 && e.X < this.Width)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (curtab == 0 && hoveringIndex > -1)
            {
                if (hoveringIndex == 0) { ProcessWeapons(); curPage = 0; }
                if (hoveringIndex == 1) { ProcessArmors(); curPage = 0; }
                if (hoveringIndex == 2) { ProcessMisces(); curPage = 0; }
                if (hoveringIndex == 3) { ProcessUniques(); curPage = 0; }
                if (hoveringIndex == 4) { ProcessSets(); curPage = 0; }
                if (hoveringIndex == 5) { ProcessRunewords(); curPage = 0; }

                curTitle = types[hoveringIndex];
                curtab = 1;
                timer.Enabled = false;
                this.Invalidate();

                SetNavigation();

                return;
            }

            if (curtab > 0 && e.X >= 24 && e.X <= 60 && e.Y >= 24 && e.Y <= 60)
            {
                --curtab;
                if (curtab == 0) curTitle = Utils.AllJsons["import"];

                this.Invalidate();
                timer.Enabled = false;

                SetNavigation();

                return;
            }

            if (curtab == 1 && hoveringIndex > -1)
            {
                curtab = 2;
                lbItems.BeginUpdate();
                lbItems.Items.Clear();

                var key = curSubTypes.Keys.ElementAt(30 * curPage + hoveringIndex);

                foreach (var d2i in curSubTypes[key])
                {
                    lbItems.Items.Add(d2i);//否则那个isarmor和isweapon不更新
                }

                lbItems.EndUpdate();

                timer.Enabled = true;

                this.Invalidate();

                SetNavigation();

            }


        }

        private void SetNavigation()
        {
            btnNext.Visible = (curtab == 1 && curSubTypes != null && curPage < (curSubTypes.Keys.Count - 1) / 30);
            btnPrevious.Visible = (curtab == 1 && curSubTypes != null && curPage > 0);
            lbItems.Visible = (curtab == 2);
        }

        private void FormOptions_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(backbmp, 0, 0);
            g.DrawImage(closebmp, this.Width - closebmp.Width, 0);

            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                if (curtab == 0) g.DrawString(curTitle, this.Font, Brushes.White, new RectangleF(0, 40 * Helper.DisplayRatio, this.Width, this.Height - 4), sf);
                if (curtab == 1) g.DrawString(curTitle + "(" + curSubTypes.Keys.Count.ToString() + ")", this.Font, Brushes.White, new RectangleF(0, 40*Helper.DisplayRatio, this.Width, this.Height - 4), sf);
            }

            if (curtab == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    g.DrawImage(blockbmp, (50 + (i % 3) * 300)*Helper.DisplayRatio, (160 + (i / 3) * 300)*Helper.DisplayRatio);
                    using (Font f = new Font("SimSun", 12, FontStyle.Bold))
                    {
                        using (var sf = new StringFormat())
                        {
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;
                            g.DrawString(types[i], f, Helper.TextBrush, new RectangleF((50 + (i % 3) * 300)*Helper.DisplayRatio, (160 + (i / 3) * 300)*Helper.DisplayRatio, blockbmp.Width, blockbmp.Height), sf);
                        }
                    }
                }

                if (hoveringIndex > -1)
                {
                    using (Pen p = new Pen(Helper.TextBrush, 3))
                    {
                        g.DrawRectangle(p, new Rectangle((int)((50 + (hoveringIndex % 3) * 300)*Helper.DisplayRatio), (int)((160 + (hoveringIndex / 3) * 300)*Helper.DisplayRatio), blockbmp.Width, blockbmp.Height));
                    }
                }
            }
            else if (curtab > 0)
            {
                using (Font f = new Font("SimSun", 24, FontStyle.Bold))
                {
                    g.DrawString("<", f, Brushes.White, 28, 24);
                }
            }

            if (curtab == 1)
            {
                if (curSubTypes != null && curSubTypes.Count > 0)
                {
                    int index = 0;
                    foreach (var key in curSubTypes.Keys)
                    {
                        if (index < curPage * 30)
                        {
                            ++index;
                            continue;
                        }
                        if (index > (curPage + 1) * 30)
                        {
                            break;
                        }
                        g.DrawImage(blockbmp2, (50 + 150 * (index % 6))*Helper.DisplayRatio, (80 + ((index % 30) / 6) * 130)*Helper.DisplayRatio);


                        using (Font f = new Font("SimSun", 12))
                        {
                            using (var sf = new StringFormat())
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;
                                g.DrawString(key, f, Brushes.White, new RectangleF((50 + 150 * (index % 6))*Helper.DisplayRatio, (80 + ((index % 30) / 6) * 130)*Helper.DisplayRatio, blockbmp2.Width, blockbmp2.Height), sf);
                            }
                        }

                        ++index;
                    }
                }

                if (hoveringIndex > -1 && 30 * curPage + hoveringIndex < curSubTypes.Keys.Count)
                {
                    using (Pen p = new Pen(Helper.TextBrush, 2))
                    {
                        g.DrawRectangle(p, new Rectangle((int)((50 + 150 * (hoveringIndex % 6))*Helper.DisplayRatio), (int)((80 + ((hoveringIndex % 30) / 6) * 130)*Helper.DisplayRatio), blockbmp2.Width, blockbmp2.Height));
                    }
                }
            }

            if (curtab == 2)
            {
                var import = Utils.AllJsons["double_click_to_import"];
                var sf = g.MeasureString(import, this.Font);
                g.DrawString(import, this.Font, (elapsed % 2 == 0) ? Brushes.Red : Brushes.Yellow, (this.Width - sf.Width) / 2, 45*Helper.DisplayRatio);
            }

            e.Graphics.DrawImage(bmp, 0, 0);

            g.Dispose();
            bmp.Dispose();
        }

        private void ProcessRunewords()
        {
            var list = Helper.GetRunewords();
            curSubTypes = new Dictionary<string, List<Item>>();

            var grouped = list.GroupBy(r => r.RunewordId);
            foreach (var group in grouped)
            {
                var glist = group.ToList();
                curSubTypes[glist[0].Name] = glist;
            }
        }

        private void ProcessWeapons()
        {
            var list = Helper.GetWeapons();
            curSubTypes = new Dictionary<string, List<Item>>();

            var grouped = list.GroupBy(w => w.TypeCode);
            foreach (var group in grouped)
            {
                var glist = group.ToList();
                curSubTypes[glist[0].TypeName] = glist;
            }
        }

        private void ProcessArmors()
        {
            var list = Helper.GetArmors();
            curSubTypes = new Dictionary<string, List<Item>>();

            var grouped = list.GroupBy(w => w.TypeCode);
            foreach (var group in grouped)
            {
                var glist = group.ToList();
                curSubTypes[glist[0].TypeName] = glist;
            }
        }

        private void ProcessMisces()
        {
            var list = Helper.GetMisces();
            curSubTypes = new Dictionary<string, List<Item>>();

            var grouped = list.GroupBy(w => w.TypeCode);
            var ignoreKeys = new List<string>() { "scha", "mcha", "lcha", "gem0", "gem1", "gem2", "gem3", "gem4", "hpot", "mpot", "rpot", "spot", "apot", "wpot", "jewl",
            "bowq","xboq","play","herb","poti","ring","elix","amul","char","book","gem","torc","scro","key","jewl","body","gold"
            };

            foreach (var group in grouped)
            {
                //if (ignoreKeys.Contains(group.Key)) continue;
                var glist = group.ToList();
                curSubTypes[glist[0].TypeName] = glist;
            }
        }

        private void ProcessUniques()
        {
            var list = Helper.GetUniques();
            curSubTypes = new Dictionary<string, List<Item>>();

            var grouped = list.GroupBy(w => w.TypeCode);
            foreach (var group in grouped)
            {
                var glist = group.ToList();
                curSubTypes[glist[0].TypeName] = glist;
            }
        }

        private void ProcessSets()
        {
            var list = Helper.GetSets();
            curSubTypes = new Dictionary<string, List<Item>>();

            foreach (var row in ExcelTxt.SetsTxt.Rows)
            {
                var matched = ExcelTxt.SetItemsTxt.Rows.Where(s => s[2].Value == row["index"].Value);
                List<Item> sublist = new List<Item>();
                foreach (var item in matched)
                {
                    var set = list.Where(s => s.FileIndex == item[1].ToInt32()).First();
                    sublist.Add(set);
                }

                curSubTypes[Utils.AllJsons[row["index"].Value]] = sublist;
            }
        }

        private void lbItems_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = lbItems.Items[e.Index] as Item;

            e.ItemHeight = Math.Max(item.Rows * 49, MeasureSizeInfo(item).Item1.Height) + 20;
        }

        private string GetCorrespondingDescription(Item item)
        {
            var title = item.Name;
            if (item.QualityVersion.Length > 0) title += "(" + item.QualityVersion + ")";
            title += "\r\n";

            if (item.IsEthereal && item.TotalNumberOfSockets > 0)
            {
                title += Utils.AllJsons["strItemModEtherealSocketed"].Replace("%i", item.TotalNumberOfSockets.ToString());
            }
            else if (item.IsEthereal)
            {
                title += Utils.AllJsons["strethereal"];
            }
            else if (item.TotalNumberOfSockets > 0)
            {
                title += String.Format(Utils.AllJsons["Socketable"].Replace("%i", item.TotalNumberOfSockets.ToString()));
            }

            title += "\r\n";
            title += Helper.GetBasicDescription(Helper.CurrentCharactor.Level, item);
            title += Helper.GetEnhancedDescription(Helper.CurrentCharactor.Level, item);

            return title;
        }
        private void lbItems_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = lbItems.Items[e.Index] as Item;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds);
            }
            e.Graphics.DrawRectangle(Pens.White, e.Bounds);


            if (!String.IsNullOrEmpty(item.Icon))
            {
                var imgname = Helper.GetDefinitionFileName(@"\items\" + item.Icon);

                var bmp = Helper.Sprite2Png(imgname);

                e.Graphics.DrawImage(bmp,
                    new Rectangle(e.Bounds.X + (196 - bmp.Width) / 2, e.Bounds.Y + (e.Bounds.Height - bmp.Height) / 2 + 10, bmp.Width, bmp.Height),
                    new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            }

            var tuple = MeasureSizeInfo(item);
            using (Font f = new Font("SimSun", Helper.DefinitionInfo.TooltipFontSize*Helper.DisplayRatio, FontStyle.Bold))
            {
                for (int i = 0; i < tuple.Item2.Length; i++)
                {
                    e.Graphics.DrawString(tuple.Item2[i], f, tuple.Item3[i], 200 + tuple.Item4[i].X, e.Bounds.Y + tuple.Item4[i].Y + 10);
                }
            }
            //e.Graphics.DrawString(GetCorrespondingDescription(item), lbItems.Font, Brushes.Black, new Point(200, e.Bounds.Y+10));

            //this.Text = "Index="+e.Index.ToString()+","+e.State.ToString();

        }

        private int margin;
        private int left, top, maxw, maxh;

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (--curPage < 0) curPage = 0;
            SetNavigation();
            this.Invalidate();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (++curPage > (curSubTypes.Keys.Count - 1) / 30) curPage = (curSubTypes.Keys.Count - 1) / 30;
            SetNavigation();
            this.Invalidate();
        }

        private Rectangle[] rectangles = new Rectangle[4];

        private void lbItems_DoubleClick(object sender, EventArgs e)
        {
            QuitNow();
        }

        private string[] tooltips = new string[4];
        private Brush[] brushes = new Brush[4];

        public Tuple<Size, string[], Brush[], Rectangle[]> MeasureSizeInfo(Item item)
        {
            Size ret = new Size();

            margin = 0;
            left = 0; top = 0; maxw = 0; maxh = top;
            rectangles = new Rectangle[5];
            tooltips = new string[] { item.Name + (item.QualityVersion != "" ? "(" + item.QualityVersion + ")" : ""), item.RunewordsDependency, item.SocketItemsName, Helper.GetBasicDescription(Helper.CurrentCharactor.Level, item), Helper.GetEnhancedDescription(Helper.CurrentCharactor.Level, item) };
            brushes = new Brush[] { item.NameColor, item.NameColor, item.NameColor, Brushes.White, item.EnhancedColor };

            using (Graphics g = this.CreateGraphics())
            {
                using (Font f = new Font("SimSun", Helper.DefinitionInfo.TooltipFontSize, FontStyle.Bold))
                {
                    for (int i = 0; i < tooltips.Length; i++)
                    {
                        var sf = g.MeasureString(tooltips[i], f);
                        maxw = Math.Max((int)sf.Width, maxw);
                        rectangles[i] = new Rectangle(left, maxh, 0, (int)sf.Height);
                        maxh += (int)sf.Height + margin;
                    }
                }
            }

            ret.Width = maxw + 2 * left + 20;
            ret.Height = maxh + 2 * top;

            for (int i = 0; i < 5; i++)
            {
                rectangles[i].Width = ret.Width;
            }

            return new Tuple<Size, string[], Brush[], Rectangle[]>(ret, tooltips, brushes, rectangles);
        }
    }
}
