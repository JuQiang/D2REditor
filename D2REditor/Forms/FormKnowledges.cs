using D2SLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            BuildGridviewContent(alllines);
        }

        private void dgvTxt_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            for (int i = 0; i < this.dgvTxt.Rows.Count; i++)
            {
                dgvTxt.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
            }

            //dgvTxt.Refresh();
        }

        private void tsmeMaxSockets_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("类型\t子类型\t名称\t1~25\t26~40\t41~99\t行*列");

            var w = ExcelTxt.WeaponsTxt.Rows.GroupBy(r => r["type"].Value);
            foreach (var subtype in w)
            {
                foreach (var item in subtype)
                {
                    if (subtype.Key == "") continue;
                    int socketsBasedOnItemSize = item["invwidth"].ToInt32() * item["invheight"].ToInt32();
                    int sockets1 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets1"].ToInt32();
                    int sockets2 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets2"].ToInt32();
                    int sockets3 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets3"].ToInt32();

                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", Utils.AllJsons["strBSWeapons"], Utils.TypeMappings[subtype.Key], Utils.AllJsons[item["code"].Value],
                        String.Format("{0}({1})",Math.Min(sockets1, socketsBasedOnItemSize),sockets1), String.Format("{0}({1})", Math.Min(sockets2, socketsBasedOnItemSize), sockets2), String.Format("{0}({1})", Math.Min(sockets3, socketsBasedOnItemSize), sockets3),
                        String.Format("{0} * {1}",item["invwidth"].ToInt32(),item["invheight"].ToInt32())
                        ));
                }

            }

            var a = ExcelTxt.ArmorTxt.Rows.GroupBy(r => r["type"].Value);
            foreach (var subtype in a)
            {
                foreach (var item in subtype)
                {
                    if (subtype.Key == "") continue;
                    int socketsBasedOnItemSize = item["invwidth"].ToInt32() * item["invheight"].ToInt32();
                    int sockets1 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets1"].ToInt32();
                    int sockets2 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets2"].ToInt32();
                    int sockets3 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets3"].ToInt32();

                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", Utils.AllJsons["strBSArmor"], Utils.TypeMappings[subtype.Key], Utils.AllJsons[item["code"].Value],
                        String.Format("{0}({1})", Math.Min(sockets1, socketsBasedOnItemSize), sockets1), String.Format("{0}({1})", Math.Min(sockets2, socketsBasedOnItemSize), sockets2), String.Format("{0}({1})", Math.Min(sockets3, socketsBasedOnItemSize), sockets3),
                        String.Format("{0} * {1}", item["invwidth"].ToInt32(), item["invheight"].ToInt32())
                        ));
                }

            }

            var m = ExcelTxt.MiscTxt.Rows.GroupBy(r => r["type"].Value);
            foreach (var subtype in m)
            {
                foreach (var item in subtype)
                {
                    if (subtype.Key == "") continue;
                    if (!Utils.ItemNames.ContainsKey(item["code"].Value))continue;

                    int socketsBasedOnItemSize = item["invwidth"].ToInt32() * item["invheight"].ToInt32();
                    int sockets1 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets1"].ToInt32();
                    int sockets2 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets2"].ToInt32();
                    int sockets3 = ExcelTxt.ItemTypesTxt[subtype.Key]["MaxSockets3"].ToInt32();

                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", Utils.AllJsons["strBSMisc"], Utils.TypeMappings[subtype.Key], Utils.AllJsons[item["code"].Value],
                        String.Format("{0}({1})", Math.Min(sockets1, socketsBasedOnItemSize), sockets1), String.Format("{0}({1})", Math.Min(sockets2, socketsBasedOnItemSize), sockets2), String.Format("{0}({1})", Math.Min(sockets3, socketsBasedOnItemSize), sockets3),
                        String.Format("{0} * {1}", item["invwidth"].ToInt32(), item["invheight"].ToInt32())
                        ));
                }

            }

            BuildGridviewContent(sb.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

        }

        private void tsmeLevels_Click(object sender, EventArgs e)
        {
            //var rows=ExcelTxt.LevelsTxt.Rows.Where(l => l["Waypoint"].ToInt32() < 255);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("第几幕\t场景\t普通\t噩梦\t地狱");

            foreach(var row in ExcelTxt.LevelsTxt.Rows)
            {
                if (row["Name"].Value.StartsWith("Act") && row["MonLvlEx"].ToInt32()>0)
                {
                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}",row["Act"].ToInt32()+1, Utils.AllJsons[row["LevelName"].Value], row["MonLvlEx"].ToInt32(), row["MonLvlEx(N)"].ToInt32(), row["MonLvlEx(H)"].ToInt32()));
                }
            }

            BuildGridviewContent(sb.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void BuildGridviewContent(string[] alllines)
        {
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

        private void tsmiCubemain_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("系统隐藏\t输出\t输入\t输出等级\t需要人物等级\t需要装备等级\t属性1\t属性2\t属性3\t属性4\t属性5");

            foreach (var row in ExcelTxt.CubemainTxt.Rows)
            {
                StringBuilder sb2 = new StringBuilder();

                for(int i = 1; i <= 7; i++)
                {
                    var data = row["input " + i.ToString()].Value;
                    data = GetCubemainInput(data);
                    if (String.IsNullOrEmpty(data)) break;

                    sb2.Append(data);
                    sb2.Append("+");
                }

                sb2.Remove(sb2.Length - 1, 1);
                

                bool usetype = false, useitem = false, mod = false;
                var output = GetCubemainOutput(row["output"].Value, ref usetype,ref useitem,ref mod);

                var code = row["input 1"].Value.Replace("\"", "").Split(new char[] { ',' })[0];
                if (usetype) output += Utils.AllJsons[code];
                if (useitem) output += Utils.AllJsons[code];
                if (mod) output = "同词缀的" + output;

                var rtxt = ExcelTxt.WeaponsTxt.Rows.Where(r => r["code"].Value == code).FirstOrDefault();
                if (rtxt == null)
                {
                    rtxt = ExcelTxt.ArmorTxt.Rows.Where(r => r["code"].Value == code).FirstOrDefault();
                    if (rtxt == null)
                    {
                        rtxt = ExcelTxt.MiscTxt.Rows.Where(r => r["code"].Value == code).FirstOrDefault();
                    }
                }

                int ilevel = 0;
                if (rtxt != null) ilevel = rtxt["level"].ToInt32();

                string[] propdesc = new string[5];
                for(int i = 1; i <= 5; i++)
                {
                    var statlist = Helper.GetStatListFromProperty(row["mod " + i.ToString()].Value, row["mod " + i.ToString() + " param"].Value, row["mod " + i.ToString() + " min"].ToInt32(), row["mod " + i.ToString() + " max"].ToInt32());
                    if (statlist.Count == 0) break;
                    
                    {
                        foreach(var stat in statlist)
                        {
                            if (stat.Id == 194)
                            {
                                propdesc[i - 1] = Utils.AllJsons["sockets"]+" "+row["mod "+i.ToString()+" min"].Value+"~"+ row["mod " + i.ToString() + " max"].Value;
                            }
                            else
                            {
                                var cost = Helper.GetItemStatCostFunc(stat);
                                var desc = Helper.GetDescription(cost, Helper.CurrentCharactor.Level);
                                propdesc[i - 1] = desc;
                            }
                        }
                    }
                }
                sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", row["enabled"].ToInt32() == 1 ? "" : "是", output, sb2.ToString(),
                    row["lvl"].ToInt32(), row["plvl"].ToInt32()*Helper.CurrentCharactor.Level/100, row["ilvl"].ToInt32()*ilevel/100,
                    propdesc[0], propdesc[1], propdesc[2], propdesc[3], propdesc[4])
                    );
            }

            BuildGridviewContent(sb.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private string GetCubemainInput(string data)
        {
            string ret = String.Empty;
            var input = data.Replace("\"", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (input.Length == 0) return ret;

            var code = input[0];
            var item = String.Empty;
            if (code.Length >= 3 && code.ToLower().StartsWith("r") && Char.IsNumber(code[1]) && Char.IsNumber(code[2]))
            {
                item = ((Utils.AllJsons[code] + "(#" + Convert.ToInt32(code.Substring(1, 2)).ToString() + ")").Replace("符文：", ""));
            }
            else
            {
                item = (Utils.AllJsons[code]);
            }

            int qty = 0, sock = 0;
            for (int j = 1; j < input.Length; j++)
            {
                if (input[j].StartsWith("qty="))
                {
                    qty = Convert.ToInt32(input[j].ToLower().Replace("qty=", ""));
                }
                else if (input[j].StartsWith("sock="))
                {
                    sock = Convert.ToInt32(input[j].ToLower().Replace("sock=", ""));
                }
                else
                {
                    var desc = Utils.AllJsons[input[j]];
                    ret += desc;
                    //System.Diagnostics.Debug.WriteLine(desc);
                }
            }

            if (qty > 0) item += ("*" + qty.ToString());
            if (sock > 0) ret += (sock.ToString() + Utils.AllJsons["sockets"]);

            ret = ret + item;

            return ret;
        }

        private string GetCubemainOutput(string data,ref bool usetype,ref bool useitem,ref bool mod)
        {
            string ret = String.Empty;
            var input = data.Replace("\"", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (input.Length == 0) return ret;

            var code = input[0];
            var item = "";
            if (code.Length >= 3 && code.ToLower().StartsWith("r") && Char.IsNumber(code[1]) && Char.IsNumber(code[2]))
            {
                item = ((Utils.AllJsons[code] + "(#" + Convert.ToInt32(code.Substring(1, 2)).ToString() + ")").Replace("符文：", ""));
            }
            else
            {
                if (code == "usetype") usetype = true;
                else if (code == "useitem") useitem = true;
                else item = (Utils.AllJsons[code]);
            }

            int qty = 0, sock = 0;
            string prefix=String.Empty, suffix = String.Empty;
            for (int j = 1; j < input.Length; j++)
            {
                if (input[j].StartsWith("qty="))
                {
                    qty = Convert.ToInt32(input[j].ToLower().Replace("qty=", ""));
                }
                else if (input[j].StartsWith("sock="))
                {
                    sock = Convert.ToInt32(input[j].ToLower().Replace("sock=", ""));
                }
                else if (input[j].StartsWith("pre="))
                {
                    prefix = Utils.AllJsons[ExcelTxt.MagicPrefixTxt[Convert.ToInt32(input[j].ToLower().Replace("pre=", ""))]["Name"].Value];
                }
                else if (input[j].StartsWith("suf="))
                {
                    suffix = Utils.AllJsons[ExcelTxt.MagicSuffixTxt[Convert.ToInt32(input[j].ToLower().Replace("suf=", ""))]["Name"].Value];
                }
                else if (input[j].StartsWith("mod"))
                {
                    mod = true;
                }
                else
                {
                    var desc = Utils.AllJsons[input[j]];
                    ret += desc;
                    //System.Diagnostics.Debug.WriteLine(desc);
                }
            }

            if (qty > 0) ret += ("*" + qty.ToString());
            if (sock > 0) ret += (sock.ToString() + Utils.AllJsons["sockets"]);

            ret = prefix + ret + suffix + item;

            return ret;
        }

        private void dgvTxt_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 && double.TryParse(Convert.ToString(dgvTxt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), out double val)) {
                if (val == 0.0d)
                {
                    e.Value = "";
                }
            }
        }
    }
}
