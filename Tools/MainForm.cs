using D2REditor;
using D2SLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void MergeStashCharactor()
        {
            Bitmap bmp = new Bitmap(640, 432);
            Graphics g = Graphics.FromImage(bmp);

            var stash = Image.FromFile("stash.bmp");
            var chara = Image.FromFile("char.bmp");

            g.DrawImage(stash, 0, 0, 320, 432);
            g.DrawImage(chara, 320, 0);

            bmp.Save("Charactor.bmp");
        }
        private void MergeStash()
        {
            var path = @"C:\Users\Administrator\Documents\Resources\expandedstash_bmp\00\";
            Bitmap bmp = new Bitmap(512, 512);
            Graphics g = Graphics.FromImage(bmp);

            var img3 = Image.FromFile(path + "0003.bmp");
            var img2 = Image.FromFile(path + "0002.bmp");
            var img1 = Image.FromFile(path + "0001.bmp");
            var img0 = Image.FromFile(path + "0000.bmp");

            int magic = 176;
            g.DrawImage(img3, 256, magic);
            g.DrawImage(img2, 0, magic);
            g.DrawImage(img1, 256, 0);
            g.DrawImage(img0, 0, 0);


            bmp.Save("Stash.bmp");


        }
        private void MergeBottom()
        {
            var path = @"C:\Users\Administrator\Documents\Resources\800ctrlpnl7_bmp\00\";
            List<Image> images = new List<Image>();
            int width = 0, height = 0;

            for (int i = 0; i < 6; i++)
            {
                var img = Image.FromFile(path + String.Format("{0:d4}.bmp", i));
                width += img.Width;

                if (height < img.Height) height = img.Height;
                images.Add(img);
            }

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            int left = 0;
            foreach (var image in images)
            {
                g.DrawImage(image, left, height - image.Height);
                left += image.Width;
            }

            //这里要动态处理，不同的角色的球的颜色。先写死我的死灵的颜色。
            path = @"C:\Users\Administrator\Documents\Resources\hlthmana_bmp\00\";
            Bitmap red = Image.FromFile(path + "0000.bmp") as Bitmap;
            red.MakeTransparent();
            g.DrawImage(red, 28, 16);

            Bitmap blue = Image.FromFile(path + "0001.bmp") as Bitmap;
            blue.MakeTransparent();
            g.DrawImage(blue, 592, 16);

            bmp.Save("Bottom2.bmp");
        }
        private void MergeCharactors()
        {
            Bitmap bmp = new Bitmap(640, 256 + 176);
            Graphics g = Graphics.FromImage(bmp);

            var img2 = Image.FromFile("char2.bmp");
            var img1 = Image.FromFile("char.bmp");

            g.DrawImage(img2, 0, 0);
            g.DrawImage(img1, 320, 0);

            bmp.Save("Charactor.bmp");
        }
        private void MergeCharactor2()
        {
            int magicHeight = 176;//黑色的眼睛让我发现了黑魔法
            int magicWidth = 320;

            Bitmap bmp = new Bitmap(magicWidth, 256 + magicHeight);
            Graphics g = Graphics.FromImage(bmp);
            var path = @"C:\Users\Administrator\Documents\Resources\invchar6_bmp\00\";

            var img3 = Image.FromFile(path + "0003.bmp");
            var img2 = Image.FromFile(path + "0002.bmp");
            var img1 = Image.FromFile(path + "0001.bmp");
            var img0 = Image.FromFile(path + "0000.bmp");


            g.DrawImage(img3, 256, magicHeight);
            g.DrawImage(img2, 0, magicHeight);
            g.DrawImage(img1, 256, 0);
            g.DrawImage(img0, 0, 0);
            //for(int i = 7; i >=4; i--)
            //{
            //    var file = String.Format("{0:d4}.bmp", i);
            //    var img = Image.FromFile(path + file);
            //    g.DrawImage(img, 256 * (i % 2), 256 * ((i - 4) / 2));
            //}

            bmp.Save("char2.bmp");
        }

        private void MergeCharactor()
        {
            int magicHeight = 176;//黑色的眼睛让我发现了黑魔法
            int magicWidth = 320;

            Bitmap bmp = new Bitmap(magicWidth, 256 + magicHeight);
            Graphics g = Graphics.FromImage(bmp);
            var path = @"C:\Users\Administrator\Documents\Resources\invchar6_bmp\00\";

            var img7 = Image.FromFile(path + "0007.bmp");
            var img6 = Image.FromFile(path + "0006.bmp");
            var img5 = Image.FromFile(path + "0005.bmp");
            var img4 = Image.FromFile(path + "0004.bmp");


            g.DrawImage(img7, 256, magicHeight);
            g.DrawImage(img6, 0, magicHeight);
            g.DrawImage(img5, 256, 0);
            g.DrawImage(img4, 0, 0);
            //for(int i = 7; i >=4; i--)
            //{
            //    var file = String.Format("{0:d4}.bmp", i);
            //    var img = Image.FromFile(path + file);
            //    g.DrawImage(img, 256 * (i % 2), 256 * ((i - 4) / 2));
            //}

            bmp.Save("char.bmp");
        }


        private void GetManyFiles(string path, ref List<string> manyFiles)
        {
            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {
                GetManyFiles(dir, ref manyFiles);
            }

            var files = Directory.GetFiles(path, "*.dc6");
            foreach (var file in files)
            {
                manyFiles.Add(file);
            }

            return;
        }

        private void ProcessUI()
        {
            List<string> manyFiles = new List<string>();
            GetManyFiles(@"C:\temp", ref manyFiles);
            foreach (var file in manyFiles)
            {
                D2Palette dp = new D2Palette(file);
                var bmp = dp.Transform();
                bmp.Save(file.Replace(".dc6", ".png"));
            }
            MessageBox.Show("Done");
        }
        private void ProcessPictures()
        {
            var path = @"C:\Users\Administrator\source\repos\D2REditor\D2REditor\Documents\Pictures\";
            var files = Directory.GetFiles(path, "*.bmp");
            foreach (var file in files)
            {
                int num = 0;
                int index = file.LastIndexOf("\\");
                var dir = file.Substring(0, index);
                var fname = file.Substring(index + 1);

                if (int.TryParse(fname[0].ToString(), out num))
                {
                    var items = fname.Split(new char[] { ' ' });
                    var newName = items[1];

                    for (int i = 2; i < items.Length; i++)
                    {
                        newName += " " + items[i];
                    }
                    File.Move(file, dir + "\\" + newName);
                }

            }
        }
        private void ProcessSkills()
        {
            var character = Core.ReadD2S(Helper.CurrentD2SFileName);
            character.ClassSkills.Skills.ForEach(skill => skill.Points = 20);
            var buf = Core.WriteD2S(character);
            File.WriteAllBytes(Helper.CurrentD2SFileName, buf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var spriteFiles = Directory.GetFiles(textBox1.Text, "*.sprite");
            int index = 1;
            int count = spriteFiles.Length;

            foreach (var fileName in spriteFiles)
            {
                this.Text = String.Format("{0}/{1} {2}", index++, count, new FileInfo(fileName).Name);
                var bmp = Sprite2Png(fileName);
                bmp.Save(fileName.Replace(".sprite", ".png"));

                Application.DoEvents();
            }
        }

        private Bitmap Sprite2Png(string fileName)
        {
            var bytes = File.ReadAllBytes(fileName);
            int x, y;
            var version = BitConverter.ToUInt16(bytes, 4);
            var width = BitConverter.ToInt32(bytes, 8);
            var height = BitConverter.ToInt32(bytes, 0xC);
            var bmp = new Bitmap(width, height);

            if (version == 31)
            {   // regular RGBA
                for (x = 0; x < height; x++)
                {
                    for (y = 0; y < width; y++)
                    {
                        var baseVal = 0x28 + x * 4 * width + y * 4;
                        bmp.SetPixel(y, x, Color.FromArgb(bytes[baseVal + 3], bytes[baseVal + 0], bytes[baseVal + 1], bytes[baseVal + 2]));
                    }
                }
            }
            else if (version == 61)
            {   // DXT
                var tempBytes = new byte[width * height * 4];
                Dxt.DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        var baseVal = (y * width) + (x * 4);
                        bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                    }
                }
            }

            return bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bmp = Image.FromFile("c:\\temp\\Heavy_Gloves.png");
            textBox1.Text = bmp.Width.ToString();

            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcessUI();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> allfiles = new List<string>();
            GetAllFiles(@"C:\data\DiabloII_Item_Warehouse_1.0.11.46-master", ref allfiles);

            foreach (var file in allfiles)
            {
                var item = Core.ReadItem(file, 0x61);
                //System.Diagnostics.Debug.WriteLine(String.Format("{0} - {1}", (new FileInfo(file)).Name, item.Name));
            }
        }

        private void GetAllFiles(string path, ref List<string> allfiles)
        {
            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs) GetAllFiles(dir, ref allfiles);

            var files = Directory.GetFiles(path, "*.d2i");
            foreach (var file in files) allfiles.Add(file);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var path = textBox1.Text;// @"C:\Users\Administrator\Documents\D2R\data\data\hd\global\ui";

            tv.Nodes.Clear();
            var root = tv.Nodes.Add("UI");
            root.Tag = path;

            FetchNodes(root);

        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            FetchNodes(e.Node);
        }

        private void FetchNodes(TreeNode node)
        {
            var path = node.Tag as string;
            var dirs = Directory.GetDirectories(path);

            foreach (var dir in dirs)
            {
                int index = dir.LastIndexOf(@"\");
                node.Nodes.Add(dir.Substring(index + 1)).Tag = dir;
            }

            var files = Directory.GetFiles(path);
            lb.Items.Clear();
            foreach (var file in files)
            {
                if (file.IndexOf("lowend") < 0) continue;
                lb.Items.Add(file);
            }
        }

        private void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var png = ConvertSprite2Bmp(lb.SelectedItem as string);
            preview.Image = png;
        }

        private Bitmap ConvertSprite2Bmp(string sprite)
        {
            var bytes = File.ReadAllBytes(sprite);
            int x, y;
            var version = BitConverter.ToUInt16(bytes, 4);
            var width = BitConverter.ToInt32(bytes, 8);
            var height = BitConverter.ToInt32(bytes, 0xC);
            var bmp = new Bitmap(width, height);

            if (version == 31)
            {   // regular RGBA
                for (x = 0; x < height; x++)
                {
                    for (y = 0; y < width; y++)
                    {
                        var baseVal = 0x28 + x * 4 * width + y * 4;
                        bmp.SetPixel(y, x, Color.FromArgb(bytes[baseVal + 3], bytes[baseVal + 0], bytes[baseVal + 1], bytes[baseVal + 2]));
                    }
                }
            }
            else if (version == 61)
            {   // DXT
                var tempBytes = new byte[width * height * 4];
                Dxt.DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        var baseVal = (y * width) + (x * 4);
                        bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                    }
                }
            }

            return bmp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(@"c:\users\administrator\desktop\bbs", "p*.jpg");
            foreach (var file in files)
            {
                Bitmap ori = Bitmap.FromFile(file) as Bitmap;
                Bitmap bmp = new Bitmap(ori.Width / 2, ori.Height / 2);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(ori, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(0, 0, ori.Width, ori.Height), GraphicsUnit.Pixel);
                g.Dispose();
                bmp.Save((new FileInfo(file)).FullName.Replace(@"\p", @"\s"));
                bmp.Dispose();
                ori.Dispose();
            }
        }
    }
}
