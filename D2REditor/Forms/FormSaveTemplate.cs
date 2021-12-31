using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormSaveTemplate : Form
    {
        private D2S character;
        private List<D2I> d2iList;
        public FormSaveTemplate()
        {
            InitializeComponent();

            this.character = Core.ReadD2S(Helper.CurrentD2SFileName);
            this.d2iList = Core.ReadD2I2(Helper.SharedD2IFileName, Helper.Version);


            this.InvaliadteData();
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            if (tbItemDescription.Tag == null)
            {
                MessageBox.Show("没有需要保存的内容。");
                return;
            }

            Item item = tbItemDescription.Tag as Item;
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = String.Format("{0}-{1}-{2}-{3}.binary",
                item.Id,
                item.Code,
                ExcelTxt.ItemGetByCode(item.Code).Data[0].Value,
                ExcelTxt.ItemGetByCode(item.Code).Data[48].Value
                );
            sfd.OverwritePrompt = true;

            if (DialogResult.OK == sfd.ShowDialog())
            {
                byte[] buf = Core.WriteItem(item, Helper.Version);
                File.WriteAllBytes(sfd.FileName, buf);
            }
        }

        private void InvaliadteData()
        {
            var equiped = tvLocation.Nodes.Add("身体");
            var items = character.PlayerItemList.Items.Where(item => (item.Mode.ToString() == "Equipped" && item.Page == 0)).ToList();
            foreach (var item in items)
            {
                equiped.Nodes.Add(ExcelTxt.ItemGetByCode(item.Code).Data[0].Value + "-" + ExcelTxt.ItemGetByCode(item.Code).Data[48].Value).Tag = item;
            }

            var belt = tvLocation.Nodes.Add("腰带");
            items = character.PlayerItemList.Items.Where(item => (item.Mode.ToString() == "Belt" && item.Page == 0)).ToList();
            foreach (var item in items)
            {
                belt.Nodes.Add(ExcelTxt.ItemGetByCode(item.Code).Data[0].Value + "-" + ExcelTxt.ItemGetByCode(item.Code).Data[48].Value).Tag = item;
            }

            var store = tvLocation.Nodes.Add("物品栏");
            items = character.PlayerItemList.Items.Where(item => (item.Mode.ToString() == "Stored" && item.Page == 1)).ToList();
            foreach (var item in items)
            {
                store.Nodes.Add(ExcelTxt.ItemGetByCode(item.Code).Data[0].Value + "-" + ExcelTxt.ItemGetByCode(item.Code).Data[48].Value).Tag = item;
            }


            var myBox = tvLocation.Nodes.Add("本人大箱子");
            items = character.PlayerItemList.Items.Where(item => (item.Mode.ToString() == "Stored" && item.Page == 5)).ToList();
            foreach (var item in items)
            {
                myBox.Nodes.Add(ExcelTxt.ItemGetByCode(item.Code).Data[0].Value + "-" + ExcelTxt.ItemGetByCode(item.Code).Data[48].Value).Tag = item;
            }

            var sharedBox = tvLocation.Nodes.Add("共享大箱子");
            foreach (var d2i in this.d2iList)
            {
                var shared = sharedBox.Nodes.Add("共享箱子");
                foreach (var item in d2i.ItemList.Items)
                {
                    shared.Nodes.Add(ExcelTxt.ItemGetByCode(item.Code).Data[0].Value + "-" + ExcelTxt.ItemGetByCode(item.Code).Data[48].Value).Tag = item;
                }
            }
        }

        private void tvLocation_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //.txt里面的invfile列，就是该装备对应的静态图片
            TreeNode node = tvLocation.SelectedNode;
            if (node == null)
            {
                tbItemDescription.Tag = null;
                return;
            }

            Item item = node.Tag as Item;
            if (item == null) return;

            D2Palette dp = new D2Palette(item.Icon);
            var bmp = dp.Transform();

            this.pbPreview.Image = bmp;

            var info = String.Format("宽度: {0}，高度: {1}", bmp.Width, bmp.Height);
            tbItemDescription.Text = item.Name + "\r\n\r\n" + info + "\r\n\r\n" + item.ToString();
            tbItemDescription.Tag = item;

        }
    }
}
