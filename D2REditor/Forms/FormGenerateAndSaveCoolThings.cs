using System;
using System.Windows.Forms;

namespace D2REditor.Forms
{
    public partial class FormGenerateCoolThings : Form
    {
        public FormGenerateCoolThings()
        {
            InitializeComponent();
        }

        private void btnCreateGems_Click(object sender, EventArgs e)
        {
            //List<string> gems = new List<string>()
            //{
            //    "gcr","gfr","gsr","glr","gpr",
            //    "gcb","gfb","gsb","glb","gpb",
            //    "gcy","gfy","gsy","gly","gpy",
            //    "gcg","gfg","gsg","glg","gpg",
            //    "gcw","gfw","gsw","glw","gpw",
            //    "gcv","gfv","gsv","gzv","gpv",
            //    "skc","skf","sku","skl","skz"
            //};

            //List<string> prefix = new List<string>() { "碎裂的", "裂开的", "", "无瑕疵的", "完美的" };
            //List<string> suffix = new List<string>() { "红", "蓝", "黄", "绿", "钻", "紫", "骷髅" };

            //var buf = File.ReadAllBytes(String.Format("{0}Gems\\gem.gem", Helper.TemplatePath));


            //for (int i = 0; i < gems.Count; i++)
            //{
            //    var gem = Core.ReadItem(buf, Helper.Version);

            //    gem.Code = gems[i];
            //    var tmpbuf = Core.WriteItem(gem, Helper.Version);
            //    File.WriteAllBytes(String.Format("{0}Gems\\{1}{2}宝石.gem", Helper.TemplatePath, prefix[i % 5], suffix[i / 5]), tmpbuf);
            //}

            //MessageBox.Show("都生成好了！");
        }

        private void btnCreateRuns_Click(object sender, EventArgs e)
        {
            //var buf = File.ReadAllBytes(String.Format("{0}Runes\\rune.rune", Helper.TemplatePath));
            //string[] names = new string[] {
            //    "艾尔", "艾德", "特尔", "那夫", "爱斯",
            //    "伊司", "塔尔", "拉尔", "欧特", "舒尔",
            //    "安姆", "索尔", "沙尔", "多尔", "海尔",
            //    "埃欧", "卢姆", "科", "法尔", "蓝姆",
            //    "普尔", "乌姆", "马尔", "伊司特", "古尔",
            //    "伐克斯", "欧姆", "罗", "瑟", "贝",
            //    "乔", "查姆", "萨德"
            //};

            //for (int i = 1; i <= 33; i++)
            //{
            //    var rune = Core.ReadItem(buf, Helper.Version);

            //    rune.Code = String.Format("r{0:d2} ",i);
            //    var tmpbuf = Core.WriteItem(rune, Helper.Version);
            //    File.WriteAllBytes(String.Format("{0}Runes\\{1}号{2}.rune", Helper.TemplatePath,i,names[i-1]), tmpbuf);
            //}

            //MessageBox.Show("都生成好了！");
        }

        private void btnCreateKeys_Click(object sender, EventArgs e)
        {
            //var buf = File.ReadAllBytes(String.Format("{0}Keys\\key.key", Helper.TemplatePath));
            //string[] names = new string[] {"恐惧之钥", "憎恨之钥", "毁灭之钥"};

            //for (int i = 0; i < 3; i++)
            //{
            //    var key = Core.ReadItem(buf, Helper.Version);

            //    key.Code = String.Format("pk{0} ", i+1);
            //    var tmpbuf = Core.WriteItem(key, Helper.Version);
            //    File.WriteAllBytes(String.Format("{0}Keys\\{1}.key", Helper.TemplatePath, names[i]), tmpbuf);
            //}

            //MessageBox.Show("都生成好了！");
        }
    }
}
