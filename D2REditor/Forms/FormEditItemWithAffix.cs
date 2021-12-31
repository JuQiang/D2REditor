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
    public partial class FormEditItemWithAffix : Form
    {
        public FormEditItemWithAffix()
        {
            InitializeComponent();
        }

        private D2S charactor;
        private Item item;
        string gemname = null;
        Bitmap gembmp = null, gem = null;
        int level;
        bool isLoading = false;

        public FormEditItemWithAffix(D2S charactor, Item item) : this()
        {
            this.charactor = charactor;
            this.level = charactor.Level;
            this.item = item;

            //this.Text = item.Name;
            //pbItemPicture.Width = Helper.DefinitionInfo.BoxSize * 2;
            //pbItemPicture.Height = Helper.DefinitionInfo.BoxSize * 4;

            labelItemName.Text = item.Name;
            labelItemName.ForeColor = item.NameColor.Color;

            gemname = Helper.GetDefinitionFileName(@"\panel\gemsocket");
            gembmp = Helper.Sprite2Png(gemname);

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

        }

        private void FormEditItem_Load(object sender, EventArgs e)
        {
            isLoading = true;
            cbTypes.SelectedIndexChanged -= cbTypes_SelectedIndexChanged;
            cbSubTypes.SelectedIndexChanged -= cbSubTypes_SelectedIndexChanged;

            Dictionary<string, string> typeMappings = new Dictionary<string, string>();

            cbIsCompact.Checked = this.item.IsCompact;
            cbIsEthereal.Checked = this.item.IsEthereal;

            //品质
            foreach (var q in Utils.QualityList) cbQuality.Items.Add(q);

            //类型
            var typenames = Utils.MiniItemList.GroupBy(mi => mi.TypeName);

            foreach (var typename in typenames)
            {
                cbTypes.Items.Add(typename.Key);
            }

            //子类型
            cbItems.DisplayMember = "ItemName";
            cbItems.ValueMember = "ItemCode";
            //对应的装备
            //cbItems.BeginUpdate();
            //foreach (var item in Utils.MiniItemList)
            //{
            //    cbItems.Items.Add(item.ItemName);
            //}


            //显示该装备对应的属性
            cbQuality.Text = item.QualityName;
            cbTypes.Text = Utils.MiniItemList.Where(mi => mi.ItemCode.Trim() == item.Code.Trim()).FirstOrDefault().TypeName;
            cbSubTypes.Text = Utils.MiniItemList.Where(mi => mi.ItemCode.Trim() == item.Code.Trim()).FirstOrDefault().SubTypeName;
            cbItems.Text = Utils.MiniItemList.Where(mi => mi.ItemCode.Trim() == item.Code.Trim()).FirstOrDefault().ItemName;

            BuildGridView();

            //图片
            var imgname = Helper.GetDefinitionFileName(@"\items\" + item.Icon);
            var bmp = Helper.Sprite2Png(imgname);
            pbItemPicture.Image = bmp;

            //基本描述
            var lines = Helper.GetBasicDescription(level, item).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines) lbBasicDescription.Items.Add(line);


            //基本属性
            cbSockets.SelectedIndex = item.TotalNumberOfSockets;
            cbIsRuneWord.Checked = item.IsRuneword;
            cbIsIdentified.Checked = item.IsIdentified;


            //事件
            gbSocket.Paint += GbSocket_Paint;
            gbSocket.MouseMove += GbSocket_MouseMove;
            gbSocket.Invalidate();

            cbTypes.EndUpdate();
            cbSubTypes.EndUpdate();
            //cbItems.EndUpdate();

            cbTypes.SelectedIndexChanged += cbTypes_SelectedIndexChanged;
            cbSubTypes.SelectedIndexChanged += cbSubTypes_SelectedIndexChanged;

            if (this.item.IsWeapon) cbTypes.SelectedIndex = 0;
            else if (this.item.IsArmor) cbTypes.SelectedIndex = 1;
            else cbTypes.SelectedIndex = 2;
            FillSubTypes();

            //cbSubTypes.Text = Utils.MiniItemList.Where(mi => mi.ItemName == this.item.TypeName).First().SubTypeName;
            cbSubTypes.Text = this.item.TypeName;
            FillItems();

            cbItems.Text = Utils.AllJsons[this.item.Code.Trim()];

            if (this.item.MagicPrefixIds[0] > 0)
            {
                btnPrefix0.Text = Utils.MagicPrefixItems[this.item.MagicPrefixIds[0]];
            }
            btnPrefix0.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicPrefixTxt, this.item.MagicPrefixIds[0]);

            if (this.item.MagicPrefixIds[1] > 0)
            {
                btnPrefix1.Text = Utils.MagicPrefixItems[this.item.MagicPrefixIds[1]];
            }
            btnPrefix1.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicPrefixTxt, this.item.MagicPrefixIds[1]);

            if (this.item.MagicPrefixIds[2] > 0)
            {
                btnPrefix2.Text = Utils.MagicPrefixItems[this.item.MagicPrefixIds[2]];
            }
            btnPrefix2.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicPrefixTxt, this.item.MagicPrefixIds[2]);

            if (this.item.MagicSuffixIds[0] > 0)
            {
                btnSuffix0.Text = Utils.MagicSuffixItems[this.item.MagicSuffixIds[0]];
            }
            btnSuffix0.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicSuffixTxt, this.item.MagicSuffixIds[0]);

            if (this.item.MagicSuffixIds[1] > 0)
            {
                btnSuffix1.Text = Utils.MagicSuffixItems[this.item.MagicSuffixIds[1]];
            }
            btnSuffix1.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicSuffixTxt, this.item.MagicSuffixIds[1]);

            if (this.item.MagicSuffixIds[2] > 0)
            {
                btnSuffix2.Text = Utils.MagicSuffixItems[this.item.MagicSuffixIds[2]];
            }
            btnSuffix2.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicSuffixTxt, this.item.MagicSuffixIds[2]);
            //tabControl1.SelectedIndex = 2;
            //ProcessAffix(lvPfefixList,ExcelTxt.MagicPrefixTxt,this.item.MagicPrefixIds);
            //ProcessAffix(lvSuffixList, ExcelTxt.MagicSuffixTxt, this.item.MagicSuffixIds);

            //cbPrefix0.Items.Add("(无)"); cbPrefix1.Items.Add("(无)"); cbPrefix2.Items.Add("(无)");

            //foreach (var key in Utils.MagicPrefixItems.Keys)
            //{
            //    cbPrefix0.Items.Add(Utils.MagicPrefixItems[key]);
            //    cbPrefix1.Items.Add(Utils.MagicPrefixItems[key]);
            //    cbPrefix2.Items.Add(Utils.MagicPrefixItems[key]);
            //}

            //cbPrefix0.SelectedIndex = GetPrefixById(this.item.MagicPrefixIds[0]);
            //cbPrefix1.SelectedIndex = GetPrefixById(this.item.MagicPrefixIds[1]);
            //cbPrefix2.SelectedIndex = GetPrefixById(this.item.MagicPrefixIds[2]);

            //cbSuffix0.Items.Add("(无)"); cbSuffix1.Items.Add("(无)"); cbSuffix2.Items.Add("(无)");

            //foreach (var key in Utils.MagicSuffixItems.Keys)
            //{
            //    cbSuffix0.Items.Add(Utils.MagicSuffixItems[key]);
            //    cbSuffix1.Items.Add(Utils.MagicSuffixItems[key]);
            //    cbSuffix2.Items.Add(Utils.MagicSuffixItems[key]);
            //}

            //cbSuffix0.SelectedIndex = GetSuffixById(this.item.MagicSuffixIds[0]);
            //cbSuffix1.SelectedIndex = GetSuffixById(this.item.MagicSuffixIds[1]);
            //cbSuffix2.SelectedIndex = GetSuffixById(this.item.MagicSuffixIds[2]);

            if (this.item.Quality == ItemQuality.Magic)
            {
                btnPrefix1.Enabled = false; btnPrefix2.Enabled = false;
                btnSuffix1.Enabled = false; btnSuffix2.Enabled = false;
            }
            isLoading = false;

            imgname = Helper.GetDefinitionFileName(@"\questicons\questgem");
            var img = Helper.Sprite2Png(imgname);
            gem = Helper.GetImageByFrame(img, 28, 27) as Bitmap;
        }


        private void ProcessAffix(ListView lvAffixList, ExcelTxt txt, ushort[] affixIds)
        {
            var list = txt.Rows.GroupBy(p => p["group"].Value).ToList();
            foreach (var g in list)
            {
                if (g.Key == "") continue;

                ListViewGroup group = new ListViewGroup();
                group.Name = g.Key;
                group.Header = g.Key;
                lvAffixList.Groups.Add(group);

                foreach (var row in g)
                {
                    string can = "";
                    for (int i = 1; i <= 7; i++)
                    {
                        if (row[String.Format("itype{0}", i)].Value == "") break;
                        can += row[String.Format("itype{0}", i)].Value;
                        can += ",";
                    }

                    string cannot = "";
                    for (int i = 1; i <= 5; i++)
                    {
                        if (row[String.Format("etype{0}", i)].Value == "") break;
                        cannot += row[String.Format("etype{0}", i)].Value;
                        cannot += ",";
                    }

                    var funcdesc = "";
                    for (int i = 1; i <= 3; i++)
                    {
                        if (row[String.Format("mod{0}code", i)].Value == "") break;
                        var statlist = Helper.GetStatListFromProperty(row[String.Format("mod{0}code", i)].Value, row[String.Format("mod{0}param", i)].Value, row[String.Format("mod{0}min", i)].ToInt32(), row[String.Format("mod{0}max", i)].ToInt32());
                        foreach (var stat in statlist)
                        {
                            var func = Helper.GetItemStatCostFunc(stat);
                            var desc = Helper.GetDescription(func, 99);
                            funcdesc += desc;
                            funcdesc += ",";
                        }
                    }

                    ListViewItem lvi = new ListViewItem(row["Id"].Value);
                    lvi.SubItems.Add(Utils.AllJsons[row["Name"].Value]);
                    lvi.SubItems.Add(funcdesc);
                    lvi.SubItems.Add(can);
                    lvi.SubItems.Add(cannot);
                    lvi.SubItems.Add(String.Format("{0} - {1}", row["level"].Value, row["maxlevel"].Value));
                    lvi.SubItems.Add(row["levelreq"].Value);

                    lvi.Group = group;
                    lvi.Tag = row;

                    lvAffixList.Items.Add(lvi);
                }
            }

            for (int j = 0; j < affixIds.Length; j++)
            {
                bool found = false;
                if (affixIds[j] == 0) continue;
                for (int i = 0; i < lvAffixList.Items.Count; i++)
                {
                    if (lvAffixList.Items[i].Text == affixIds[j].ToString())
                    {
                        lvAffixList.Items[i].Checked = true;
                        lvAffixList.Items[i].Selected = true;
                        lvAffixList.EnsureVisible(i);

                        found = true;
                        break;
                    }

                    if (found) break;
                }
            }
        }
        private int GetPrefixById(int id)
        {
            if (id == 0) return 0;

            int index = 1;
            foreach (var key in Utils.MagicPrefixItems.Keys)
            {
                if (key == id) break;
                ++index;
            }

            return index;
        }

        private int GetSuffixById(int id)
        {
            if (id == 0) return 0;

            int index = 1;
            foreach (var key in Utils.MagicSuffixItems.Keys)
            {
                if (key == id) break;
                ++index;
            }

            return index;
        }

        private ushort GetPrefixByText(string text)
        {
            var prefix = Utils.MagicPrefixItems.Where(p => p.Value == text).FirstOrDefault();

            return (ushort)prefix.Key;
        }

        private ushort GetSuffixByText(string text)
        {
            var suffix = Utils.MagicSuffixItems.Where(s => s.Value == text).FirstOrDefault();

            return (ushort)suffix.Key;
        }

        private void BuildGridView()
        {
            List<ItemStat> statList = new List<ItemStat>();
            foreach (var group in item.StatLists)
            {
                foreach (var stat in group.Stats)
                {
                    statList.Add(stat);
                }
            }

            List<MiniItemStatCost> miscList = new List<MiniItemStatCost>();
            foreach (var stat in statList)
            {
                //这里需要clone一个新的，否则如下面注释说的两个一样的cost，name实际上指向的同一个object，更改赋值会冲掉前一个。
                miscList.Add(Utils.MiniItemStatCostList.Where(misc => misc.ID == stat.Id.ToString()).First().Clone());
            }
            //var miscList = Utils.MiniItemStatCostList.Where(misc => statList.SelectMany(stat => stat.Id.ToString()).Contains(misc.ID)).ToList();
            //对于符文之语: "理性之聲"而言，有两个ID198的cost func，上面代码会过滤掉一个。
            //同样，下面代码，first部分，会把第一个18%复制到第二个15%上，导致错误。
            var funcList = Utils.MiniItemStatCostList.Select(cost => cost.DescFunc).ToArray<string>();

            for (int i = 0; i < statList.Count; i++)
            {
                miscList[i].Param = (statList[i].Param == null) ? 0 : (int)statList[i].Param;
                miscList[i].Value = (int)statList[i].Value;
            }
            //foreach (var misc in miscList)
            //{
            //    var stat = statList.Where(s => s.Id.ToString() == misc.ID).First();
            //    misc.Param = (stat.Param == null) ? 0 : (int)stat.Param;
            //    misc.Value = (int)stat.Value;
            //}

            dgvItemStatCost.Rows.Clear();
            colDescFunc.Items.AddRange(funcList);

            for (int i = 0; i < miscList.Count; i++)
            {
                dgvItemStatCost.Rows.Add();
                dgvItemStatCost.Rows[i].Cells["colID"].Value = miscList[i].ID;
                dgvItemStatCost.Rows[i].Cells["colDescFunc"].Value = miscList[i].DescFunc;
                dgvItemStatCost.Rows[i].Cells["colValue"].Value = miscList[i].Value;
                dgvItemStatCost.Rows[i].Cells["colParam"].Value = miscList[i].Param;
                dgvItemStatCost.Rows[i].Cells["colMaxValue"].Value = miscList[i].MaxValue;
                dgvItemStatCost.Rows[i].Cells["colStat1"].Value = miscList[i].Stat1;
                dgvItemStatCost.Rows[i].Cells["colStat2"].Value = miscList[i].Stat2;
                dgvItemStatCost.Rows[i].Cells["colStat3"].Value = miscList[i].Stat3;
                dgvItemStatCost.Rows[i].Cells["colDgrpFunc"].Value = miscList[i].DgrpFunc;

                dgvItemStatCost.Rows[i].Tag = statList[i];
            }
        }
        private void BuildGridView2()
        {
            List<ItemStat> statList = new List<ItemStat>();
            foreach (var group in item.StatLists)
            {
                foreach (var stat in group.Stats)
                {
                    statList.Add(stat);
                }
            }

            var miscList = Utils.MiniItemStatCostList.Where(misc => statList.Select(stat => stat.Id.ToString()).Contains(misc.ID)).ToList();

            foreach (var misc in miscList)
            {
                var stat = statList.Where(s => s.Id.ToString() == misc.ID).First();
                misc.Param = (stat.Param == null) ? 0 : (int)stat.Param;
                misc.Value = (int)stat.Value;
            }

            dgvItemStatCost.Columns.Clear();
            dgvItemStatCost.AutoGenerateColumns = false;
            dgvItemStatCost.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItemStatCost.Columns.Add("ID", "编号");

            DataGridViewComboBoxColumn descColumn = new DataGridViewComboBoxColumn();
            descColumn.Name = "DescFunc";
            descColumn.DataPropertyName = "ID";//不加这个不能显示对应的值，擦，太古老的技能了，都忘记了。
            descColumn.HeaderText = "属性";
            descColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            descColumn.DisplayMember = "DescFunc";
            descColumn.ValueMember = "ID";
            descColumn.DataSource = Utils.MiniItemStatCostList;
            dgvItemStatCost.Columns.Add(descColumn);

            dgvItemStatCost.Columns.Add("Value", "值");
            dgvItemStatCost.Columns.Add("Param", "参数");
            dgvItemStatCost.Columns.Add("MaxValue", "最大值");
            dgvItemStatCost.Columns.Add("Stat1", "影响1");
            dgvItemStatCost.Columns.Add("Stat2", "影响2");
            dgvItemStatCost.Columns.Add("Stat3", "影响3");

            dgvItemStatCost.Columns["ID"].DataPropertyName = "ID"; dgvItemStatCost.Columns["ID"].ReadOnly = true; dgvItemStatCost.Columns["ID"].DefaultCellStyle.BackColor = Color.LightGray;

            dgvItemStatCost.Columns["Value"].DataPropertyName = "Value";
            dgvItemStatCost.Columns["Param"].DataPropertyName = "Param"; dgvItemStatCost.Columns["Param"].ReadOnly = true; dgvItemStatCost.Columns["Param"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvItemStatCost.Columns["MaxValue"].DataPropertyName = "MaxValue"; dgvItemStatCost.Columns["MaxValue"].ReadOnly = true; dgvItemStatCost.Columns["MaxValue"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvItemStatCost.Columns["Stat1"].DataPropertyName = "Stat1"; dgvItemStatCost.Columns["Stat1"].ReadOnly = true; dgvItemStatCost.Columns["Stat1"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvItemStatCost.Columns["Stat2"].DataPropertyName = "Stat2"; dgvItemStatCost.Columns["Stat2"].ReadOnly = true; dgvItemStatCost.Columns["Stat2"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvItemStatCost.Columns["Stat3"].DataPropertyName = "Stat3"; dgvItemStatCost.Columns["Stat3"].ReadOnly = true; dgvItemStatCost.Columns["Stat3"].DefaultCellStyle.BackColor = Color.LightGray;

            dgvItemStatCost.DataSource = miscList;
        }

        private void GbSocket_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < item.SocketedItems.Count; i++)
            {
                if (Helper.IsPointInRange(e.Location, new Rectangle(18 + 60 * i, 34, gembmp.Width, gembmp.Height)))
                {
                    System.Diagnostics.Debug.WriteLine("Move to ." + i.ToString());
                    if ((currentGem == null) || (item.SocketedItems[i] != currentGem))
                    {
                        currentGem = item.SocketedItems[i];
                        System.Diagnostics.Debug.WriteLine("Repaint on ." + i.ToString());
                        gbSocket.Invalidate();
                        return;
                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine("No repaint.");
            //currentGem = null;
        }

        private Item currentGem = null;

        private void GbSocket_Paint(object sender, PaintEventArgs e)
        {
            var gemname = Helper.GetDefinitionFileName(@"\panel\gemsocket");
            var gembmp = Helper.Sprite2Png(gemname);

            Graphics g = e.Graphics;

            for (int i = 0; i < item.TotalNumberOfSockets; i++)
            {
                g.DrawImage(gembmp, 18 + 60 * i, 34);
            }

            for (int i = 0; i < item.SocketedItems.Count; i++)
            {
                var iconname = Helper.GetDefinitionFileName(@"\items\" + item.SocketedItems[i].Icon);
                g.DrawImage(Helper.Sprite2Png(iconname), 18 + 60 * i, 34);
            }

            if (currentGem != null)
            {
                g.DrawString(currentGem.Name, this.Font, Brushes.Black, 18, 90);
            }
        }

        private void cbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubTypes();
        }

        private void FillSubTypes()
        {
            cbSubTypes.Items.Clear();
            var typenames = Utils.MiniItemList.Where(mi => mi.TypeName == cbTypes.Text).GroupBy(mi => mi.SubTypeName);
            foreach (var typename in typenames)
            {
                cbSubTypes.Items.Add(typename.Key);
            }
        }

        private void cbSubTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillItems();
        }

        private void FillItems()
        {
            cbItems.Items.Clear();
            var itemnames = Utils.MiniItemList.Where(mi => mi.TypeName == cbTypes.Text && mi.SubTypeName == cbSubTypes.Text);
            foreach (var itemname in itemnames)
            {
                cbItems.Items.Add(itemname);
            }
        }

        private void btnDeleteStat_Click(object sender, EventArgs e)
        {
            var count = dgvItemStatCost.SelectedRows.Count;

            if (count == 0) return;
            if (MessageBox.Show(String.Format("确实要删除这{0}条高级属性吗？\r\n不过也没关系，只要不保存，就没事，可以反悔。", count), "请确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var row in dgvItemStatCost.SelectedRows)
                {
                    dgvItemStatCost.Rows.Remove(row as DataGridViewRow);
                }
            }
        }

        private void dgvItemStatCost_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dgvItemStatCost.IsCurrentCellDirty)
            {
                dgvItemStatCost.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvItemStatCost_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (isLoading) return;

            DataGridViewComboBoxCell cb = dgvItemStatCost.Rows[e.RowIndex].Cells["colDescFunc"] as DataGridViewComboBoxCell;
            if (cb.Value != null && dgvItemStatCost.CurrentCell.ColumnIndex == 1)
            {
                var stat = Utils.MiniItemStatCostList.Where(cost => cost.DescFunc == cb.Value.ToString()).First();
                dgvItemStatCost.Rows[e.RowIndex].Cells["colID"].Value = stat.ID;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colValue"].Value = 0;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colParam"].Value = 0;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colMaxValue"].Value = stat.MaxValue;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colStat1"].Value = stat.Stat1;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colStat2"].Value = stat.Stat2;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colStat3"].Value = stat.Stat3;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colDgrpFunc"].Value = stat.DgrpFunc;
                dgvItemStatCost.Rows[e.RowIndex].Tag = stat.ID;


                dgvItemStatCost.Invalidate();
            }
        }

        private void btnAddStat_Click(object sender, EventArgs e)
        {
            dgvItemStatCost.Rows.Add();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveItem()
        {
            foreach (var group in this.item.StatLists)
            {
                group.Stats.Clear();
            }

            foreach (var row in dgvItemStatCost.Rows)
            {
                var stat = (row as DataGridViewRow).Tag as ItemStat;
                if (stat == null) continue;
                stat.Value = Convert.ToInt32((row as DataGridViewRow).Cells[2].Value);
                this.item.StatLists[0].Stats.Add(stat);
            }

            this.item.IsWeapon = (cbTypes.SelectedIndex == 0);
            this.item.IsArmor = (cbTypes.SelectedIndex == 1);
            this.item.IsMisc = (cbTypes.SelectedIndex == 2);

            this.item.Code = (cbItems.SelectedItem as MiniItem).ItemCode;
            this.item.IsSocketed = cbSockets.SelectedIndex > 0;
            this.item.TotalNumberOfSockets = (byte)cbSockets.SelectedIndex;
            this.item.IsEthereal = cbIsEthereal.Checked;

            this.item.MagicPrefixIds[0] = (btnPrefix0.Tag as Tuple<ExcelTxt, ushort>).Item2;
            this.item.MagicPrefixIds[1] = (btnPrefix1.Tag as Tuple<ExcelTxt, ushort>).Item2;
            this.item.MagicPrefixIds[2] = (btnPrefix2.Tag as Tuple<ExcelTxt, ushort>).Item2;

            this.item.MagicSuffixIds[0] = (btnSuffix0.Tag as Tuple<ExcelTxt, ushort>).Item2;
            this.item.MagicSuffixIds[1] = (btnSuffix1.Tag as Tuple<ExcelTxt, ushort>).Item2;
            this.item.MagicSuffixIds[2] = (btnSuffix2.Tag as Tuple<ExcelTxt, ushort>).Item2;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveItem();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new FormSelectAffix(ExcelTxt.MagicPrefixTxt, this.item.MagicPrefixIds[0])).ShowDialog();
        }

        private void OnPrefixClicked(object sender, EventArgs e)
        {
            var btn = (sender as Button);
            var t = btn.Tag as Tuple<ExcelTxt, ushort>;
            var fsa = new FormSelectAffix(t.Item1, t.Item2);
            if (DialogResult.OK == fsa.ShowDialog())
            {
                if (fsa.Affix > 0)
                {
                    btn.Text = Utils.MagicPrefixItems[fsa.Affix];
                    var row = ExcelTxt.MagicPrefixTxt[fsa.Affix];

                    var funcdesc = "";
                    for (int i = 1; i <= 3; i++)
                    {
                        if (row[String.Format("mod{0}code", i)].Value == "") break;
                        var statlist = Helper.GetStatListFromProperty(row[String.Format("mod{0}code", i)].Value, row[String.Format("mod{0}param", i)].Value, row[String.Format("mod{0}min", i)].ToInt32(), row[String.Format("mod{0}max", i)].ToInt32());
                        foreach (var stat in statlist)
                        {
                            var func = Helper.GetItemStatCostFunc(stat);
                            var desc = Helper.GetDescription(func, 99);
                            funcdesc += desc;
                            funcdesc += ",";
                        }
                    }
                }
                else
                {
                    btn.Text = "前缀";
                }

                btn.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicPrefixTxt, fsa.Affix);
            }
        }

        private void OnSuffixClicked(object sender, EventArgs e)
        {
            var btn = (sender as Button);
            var t = btn.Tag as Tuple<ExcelTxt, ushort>;
            var fsa = new FormSelectAffix(t.Item1, t.Item2);
            if (DialogResult.OK == fsa.ShowDialog())
            {
                if (fsa.Affix > 0)
                {
                    btn.Text = Utils.MagicSuffixItems[fsa.Affix];
                }
                else
                {
                    btn.Text = "后缀";
                }

                btn.Tag = new Tuple<ExcelTxt, ushort>(ExcelTxt.MagicSuffixTxt, fsa.Affix);
            }
        }

        private void btnSaveAsTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "暗黑2模板文件(*.d2i)|*.d2i";
            if (DialogResult.OK == sfd.ShowDialog())
            {
                SaveItem();
                File.WriteAllBytes(sfd.FileName, Item.Write(this.item, 0x61));
                this.Close();
            }
        }


    }
}
