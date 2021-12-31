using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormKnowledges : Form
    {
        internal class Knowledge
        {
            public string FullFileName { get; set; }
            public string FileName { get; set; }

            public override string ToString()
            {
                return FileName;
            }
        }

        private List<Knowledge> knowledges = new List<Knowledge>();
        public FormKnowledges()
        {
            InitializeComponent();
        }

        private void FormKnowledges_Load(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(Helper.CacheFolder + @"\excel", "*.txt");

            foreach (var file in files)
            {
                knowledges.Add(new Knowledge() { FullFileName = file, FileName = (new FileInfo(file)).Name.Replace(".txt", "") });
            }

            //为啥没有datasource、displaymember和valuemember？
            foreach (var knowledge in knowledges)
            {
                tscbTxtList.Items.Add(knowledge);
            }
        }

        private void tscbTxtList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fullname = (tscbTxtList.SelectedItem as Knowledge).FullFileName;
            var alllines = File.ReadAllLines(fullname);

            dgvTxt.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvTxt.RowHeadersVisible = false;

            dgvTxt.Columns.Clear();
            var columns = alllines[0].Split(new char[] { '\t' });
            foreach (var column in columns)
            {
                DataGridViewComboBoxColumn dgvc = new DataGridViewComboBoxColumn();
                dgvc.Name = column;
                dgvc.HeaderText = column;

                dgvTxt.Columns.Add(column, column);
            }

            for (int i = 1; i < alllines.Length; i++)
            {
                dgvTxt.Rows.Add(alllines[i].Split(new char[] { '\t' }));
            }

            dgvTxt.RowHeadersVisible = true;
            dgvTxt.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

        }

        private void dgvTxt_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            for (int i = 0; i < this.dgvTxt.Rows.Count; i++)
            {
                dgvTxt.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
            }

            //dgvTxt.Refresh();
        }
    }
}
