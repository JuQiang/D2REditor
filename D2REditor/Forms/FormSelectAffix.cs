using D2SLib;
using System;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormSelectAffix : Form
    {
        public FormSelectAffix()
        {
            InitializeComponent();
        }

        private ushort affix;
        private ExcelTxt txt;
        public ushort Affix { get; set; }
        public FormSelectAffix(ExcelTxt txt, ushort affix) : this()
        {
            this.txt = txt;
            this.affix = affix;
        }

        private ListViewItem lastAffix = null;
        private void FormSelectAffix_Load(object sender, EventArgs e)
        {
            var list = this.txt.Rows.GroupBy(p => p["group"].Value).ToList();
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

            for (int i = 0; i < lvAffixList.Items.Count; i++)
            {
                if (lvAffixList.Items[i].Text == this.affix.ToString())
                {
                    lvAffixList.Items[i].Selected = true;
                    lvAffixList.Items[i].Checked = true;
                    lastAffix = lvAffixList.Items[i];
                    lvAffixList.EnsureVisible(i);
                    break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int checknum = 0;

            for (int i = 0; i < lvAffixList.Items.Count; i++)
            {
                if (lvAffixList.Items[i].Checked)
                {
                    ++checknum;
                    this.Affix = Convert.ToUInt16(lvAffixList.Items[i].Text);
                }
            }


            if (checknum > 1)
            {
                MessageBox.Show("不能选择多于一个词缀！", "错误");
                return;
            }

            if (checknum == 0) this.Affix = 0;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvAffixList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvAffixList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lastAffix != null)
            {
                lastAffix.Checked = false;
                lastAffix.Selected = false;
            }

            e.Item.Selected = true;
            lastAffix = e.Item;
        }
    }
}
