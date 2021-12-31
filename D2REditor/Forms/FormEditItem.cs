using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormEditItem : Form
    {
        public FormEditItem()
        {
            InitializeComponent();
        }

        private Item item, originalItem;
        bool isLoading = false;

        public FormEditItem(Item item) : this()
        {
            this.item = item;
            originalItem = Core.ReadItem(Core.WriteItem(item, 0x61), 0x61);

            this.Text = item.Name;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

        }

        private void FormEditItem_Load(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            isLoading = true;
            cbTypes.SelectedIndexChanged -= cbTypes_SelectedIndexChanged;
            cbSubTypes.SelectedIndexChanged -= cbSubTypes_SelectedIndexChanged;
            cbItems.SelectedIndexChanged -= cbItems_SelectedIndexChanged;

            labelSockets.Text = Utils.AllJsons["Addsocketsui"];
            Dictionary<string, string> typeMappings = new Dictionary<string, string>();

            //cbIsCompact.Checked = this.item.IsCompact;
            cbIsEthereal.Checked = this.item.IsEthereal;

            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            //品质
            foreach (var q in Utils.QualityList) cbQuality.Items.Add(q);
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            //类型
            var typenames = Utils.MiniItemList.GroupBy(mi => mi.TypeName);

            foreach (var typename in typenames)
            {
                cbTypes.Items.Add(typename.Key);
            }
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
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
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            BuildGridView();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            //图片
            var imgname = Helper.GetDefinitionFileName(@"\items\" + item.Icon);
            var bmp = Helper.Sprite2Png(imgname);
            pbItemPicture.Image = bmp;
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            //基本描述
            var lines = Helper.GetBasicDescription(Helper.CurrentCharactor.Level, item).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines) lbBasicDescription.Items.Add(line);
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            //基本属性
            cbSockets.SelectedIndex = item.TotalNumberOfSockets;
            //cbIsRuneWord.Checked = item.IsRuneword;
            cbSockets.Enabled = !item.IsRuneword;

            //cbItems.EndUpdate();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            if (this.item.IsWeapon) cbTypes.SelectedIndex = 0;
            else if (this.item.IsArmor) cbTypes.SelectedIndex = 1;
            else cbTypes.SelectedIndex = 2;
            FillSubTypes();

            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            //cbSubTypes.Text = Utils.MiniItemList.Where(mi => mi.ItemName == this.item.TypeName).First().SubTypeName;
            cbSubTypes.Text = this.item.TypeName;
            FillItems();

            cbItems.Text = Utils.AllJsons[this.item.Code.Trim()];

            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            if (this.item.IsArmor)
            {
                cbWeight.Items.Add(Utils.AllJsons["arm_light"]);
                cbWeight.Items.Add(Utils.AllJsons["arm_medium"]);
                cbWeight.Items.Add(Utils.AllJsons["arm_heavy"]);

                cbWeight.SelectedIndex = this.item.Speed / 5;
            }
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            SetMaxSockets(item.Code);
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);



            cbTypes.EndUpdate();
            cbSubTypes.EndUpdate();
            cbItems.EndUpdate();

            cbTypes.SelectedIndexChanged += cbTypes_SelectedIndexChanged;
            cbSubTypes.SelectedIndexChanged += cbSubTypes_SelectedIndexChanged;
            cbItems.SelectedIndexChanged += cbItems_SelectedIndexChanged;

            if (dgvItemStatCost.Rows.Count > 0)
            {
                dgvItemStatCost.CurrentCell = dgvItemStatCost.Rows[0].Cells[2];
            }

            labelRunewordsCannotEdit.Text = Utils.AllJsons["runewords_cannot_edit"];
            dgvItemStatCost.Enabled = (!item.IsRuneword);
            btnAddStat.Enabled = (!item.IsRuneword);
            btnDeleteStat.Enabled = (!item.IsRuneword);
            btnMaxValues.Enabled = (!item.IsRuneword);
            cbItems.Enabled = (!item.IsRuneword);
            labelRunewordsCannotEdit.Visible = (item.IsRuneword);

            isLoading = false;
        }

        private void SetMaxSockets(string code)
        {
            int max = 0;
            if (this.item.IsArmor) max = ExcelTxt.ArmorTxt[code]["gemsockets"].ToInt32();
            if (this.item.IsWeapon) max = ExcelTxt.WeaponsTxt[code]["gemsockets"].ToInt32();
            if (this.item.IsMisc) max = ExcelTxt.MiscTxt[code]["gemsockets"].ToInt32();

            cbSockets.Items.Clear();

            for (int i = 0; i <= max; i++) cbSockets.Items.Add(i);
            cbSockets.SelectedIndex = max;
        }

        internal class CostFunc
        {
            public string ID { get; set; }
            public string Func { get; set; }

            public CostFunc(string id, string func) { this.ID = id; this.Func = func; }
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

            var funcList = Utils.MiniItemStatCostList.Where(m => m.Visible == true).Select(cost => new CostFunc(cost.ID, "[" + cost.ID + "] " + cost.DescFunc)).OrderBy(cf => Convert.ToInt32(cf.ID)).ToList();

            colDescFunc.DataSource = funcList;
            colDescFunc.DisplayMember = "Func";
            colDescFunc.ValueMember = "ID";

            for (int i = 0; i < miscList.Count; i++)
            {
                dgvItemStatCost.Rows.Add();
                //dgvItemStatCost.Rows[i].Cells["colID"].Value = miscList[i].ID;
                dgvItemStatCost.Rows[i].Cells["colDescFunc"].Value = miscList[i].ID;
                dgvItemStatCost.Rows[i].Cells["colDescFuncValue"].Value = Helper.GetItemStatCostFunc(statList[i]).Format.Replace("FQQ", miscList[i].Value.ToString());
                dgvItemStatCost.Rows[i].Cells["colValue"].Value = miscList[i].Value;
                //dgvItemStatCost.Rows[i].Cells["colParam"].Value = miscList[i].Param;
                dgvItemStatCost.Rows[i].Cells["colMaxValue"].Value = miscList[i].MaxValue;
                //dgvItemStatCost.Rows[i].Cells["colStat1"].Value = miscList[i].Stat1;
                //dgvItemStatCost.Rows[i].Cells["colStat2"].Value = miscList[i].Stat2;
                //dgvItemStatCost.Rows[i].Cells["colStat3"].Value = miscList[i].Stat3;
                //dgvItemStatCost.Rows[i].Cells["colDgrpFunc"].Value = miscList[i].DgrpFunc;

                dgvItemStatCost.Rows[i].Tag = statList[i];
            }
        }
        private void cbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubTypes();
        }

        private void FillSubTypes()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            cbSubTypes.BeginUpdate();
            cbSubTypes.Items.Clear();
            var typenames = Utils.MiniItemList.Where(mi => mi.TypeName == cbTypes.Text).GroupBy(mi => mi.SubTypeName).ToList();
            System.Diagnostics.Debug.WriteLine("Fillsubtypes group" + sw.ElapsedMilliseconds.ToString());
            sw.Restart();

            foreach (var typename in typenames)
            {
                cbSubTypes.Items.Add(typename.Key);
                System.Diagnostics.Debug.WriteLine("Fill" + " " + typename.Key + " " + sw.ElapsedMilliseconds.ToString());
                sw.Restart();
            }
            cbSubTypes.EndUpdate();
            sw.Stop();
            System.Diagnostics.Debug.WriteLine("Fillsubtypes" + sw.ElapsedMilliseconds.ToString());
        }

        private void cbSubTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Fill items");
            FillItems();
        }

        private void FillItems()
        {
            cbItems.BeginUpdate();
            cbItems.Items.Clear();
            var itemnames = Utils.MiniItemList.Where(mi => mi.TypeName == cbTypes.Text && mi.SubTypeName == cbSubTypes.Text);
            foreach (var itemname in itemnames)
            {
                cbItems.Items.Add(itemname);
            }
            cbItems.EndUpdate();
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
            if (dgvItemStatCost.CurrentCell.ColumnIndex == 0 && cb.Value != null)
            {
                var stat = Utils.MiniItemStatCostList.Where(cost => cost.ID == cb.Value.ToString()).First();
                //dgvItemStatCost.Rows[e.RowIndex].Cells["colID"].Value = stat.ID;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colDescFuncValue"].Value = 0;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colValue"].Value = 0;
                //dgvItemStatCost.Rows[e.RowIndex].Cells["colParam"].Value = 0;
                dgvItemStatCost.Rows[e.RowIndex].Cells["colMaxValue"].Value = stat.MaxValue;
                //dgvItemStatCost.Rows[e.RowIndex].Cells["colStat1"].Value = stat.Stat1;
                //dgvItemStatCost.Rows[e.RowIndex].Cells["colStat2"].Value = stat.Stat2;
                //dgvItemStatCost.Rows[e.RowIndex].Cells["colStat3"].Value = stat.Stat3;
                //dgvItemStatCost.Rows[e.RowIndex].Cells["colDgrpFunc"].Value = stat.DgrpFunc;
                dgvItemStatCost.Rows[e.RowIndex].Tag = new ItemStat() { Id = Convert.ToUInt16(stat.ID) };


                dgvItemStatCost.Invalidate();
            }
            else if (dgvItemStatCost.CurrentCell.ColumnIndex == 2)
            {
                dgvItemStatCost.Rows[e.RowIndex].Cells["colDescFuncValue"].Value = Helper.GetItemStatCostFunc(dgvItemStatCost.Rows[e.RowIndex].Tag as ItemStat).Format.Replace("FQQ", dgvItemStatCost.CurrentCell.Value.ToString());
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

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;

            SaveItem();
            this.Close();
        }

        private bool ValidateData()
        {
            var ret = true;


            return ret;
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

        private void btnMaxValues_Click(object sender, EventArgs e)
        {
            foreach (var row in dgvItemStatCost.Rows)
            {
                var stat = (row as DataGridViewRow).Tag as ItemStat;
                if (stat == null) continue;
                (row as DataGridViewRow).Cells[2].Value = (row as DataGridViewRow).Cells[3].Value;
            }
        }

        private void cbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            SetMaxSockets((cbItems.SelectedItem as MiniItem).ItemCode);
        }

        private void dgvItemStatCost_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnRestoreValues_Click(object sender, EventArgs e)
        {

        }
    }
}
