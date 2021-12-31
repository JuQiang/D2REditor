using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace D2SLib
{
    public class Utils
    {
        private static Dictionary<string, string> itemNames = null;
        private static Dictionary<string, string> itemModifiers = null;
        private static Dictionary<string, string> itemAssets = null;
        private static Dictionary<int, string> setItems = null;
        private static Dictionary<int, string> uniqueItems = null;
        private static Dictionary<int, string> magicSuffixItems = null;
        private static Dictionary<int, string> magicPrefixItems = null;
        private static Dictionary<int, string> runeWords = null;
        private static Dictionary<int, string> rarePrefixItems = null;
        private static Dictionary<int, string> rareSuffixItems = null;
        private static Dictionary<string, string> allJsons = null;
        private static Dictionary<string, string> classes = new Dictionary<string, string>();
        private static List<string> classesList = null;
        private static List<MiniItem> miniItemList = null;
        private static List<MiniItemStatCost> miniItemStatCostList = null;

        private static List<SolidBrush> Brushes = new List<SolidBrush>() {
            new SolidBrush(Color.FromArgb(110, 110, 255)),
            new SolidBrush(Color.FromArgb(0,255,0)),
            new SolidBrush(Color.FromArgb(255,255,100)),
            new SolidBrush(Color.FromArgb(199,179,119)),
            new SolidBrush(Color.FromArgb(255,168,0)),
        };
        public static SolidBrush ColorMagic = Utils.Brushes[0];
        public static SolidBrush ColorSet = Utils.Brushes[1];
        public static SolidBrush ColorRare = Utils.Brushes[2];
        public static SolidBrush ColorUnique = Utils.Brushes[3];
        public static SolidBrush ColorCraft = Utils.Brushes[4];

        public static string CacheFolder = "";
        static Utils()
        {
            Utils.CacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\D2REditor";
            //var path = Assembly.GetEntryAssembly().Location;
            //int index = path.LastIndexOf("\\");
            //Utils.CacheFolder = path.Substring(0, index);
        }


        public static List<string> ClassesList
        {
            get
            {
                if (classesList == null)
                {
                    classesList = new List<string>();
                    classesList.Add(allJsons["Amazon"]);
                    classesList.Add(allJsons["Sorceress"]);
                    classesList.Add(allJsons["Necromancer"]);
                    classesList.Add(allJsons["Paladin"]);
                    classesList.Add(allJsons["Barbarian"]);
                    classesList.Add(allJsons["druidstr"]);
                    classesList.Add(allJsons["assassinstr"]);
                }

                return classesList;
            }
        }

        public static string CurrentLanguage = "zhTW";

        public static void ResetAll()
        {
            Utils.AllJsons = null;
            Utils.ItemAssets = null;
            Utils.ItemNames = null;
            Utils.ItemModifiers = null;
            Utils.SetItems = null;
            Utils.UniqueItems = null;
            Utils.MagicSuffixItems = null;
            Utils.MagicPrefixItems = null;
            Utils.RuneWords = null;
            Utils.RarePrefixItems = null;
            Utils.RareSuffixItems = null;
            Utils.MiniItemList = null;
            Utils.MiniItemStatCostList = null;
        }
        public static Dictionary<string, string> AllJsons
        {
            get
            {
                if (allJsons == null)
                {
                    allJsons = new Dictionary<string, string>();

                    var jsonFiles = Directory.GetFiles(Utils.CacheFolder + @"\strings");
                    foreach (var file in jsonFiles)
                    {
                        var json = File.ReadAllText(file);
                        var lines = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as JArray;

                        foreach (var line in lines)
                        {
                            allJsons[line["Key"].ToString()] = line[Utils.CurrentLanguage].ToString().Trim();
                            allJsons[line["enUS"].ToString()] = line[Utils.CurrentLanguage].ToString().Trim();
                        }
                    }
                }

                return allJsons;
            }
            set
            {
                allJsons = value;
            }
        }

        public static Dictionary<string, string> ItemAssets
        {
            get
            {
                if (itemAssets == null)
                {
                    itemAssets = new Dictionary<string, string>();

                    var jline = File.ReadAllText(Utils.CacheFolder + @"\assets\items.json");
                    var entries = jline.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var entry in entries)
                    {
                        var items = entry.Split(new char[] { ':' });
                        var code = items[0].Replace("{", "").Replace("\"", "").Trim();
                        if (code == "[" || code == "]") continue;

                        var assets = items[2].Replace("\"", "").Replace("}", "").Replace(",", "").Trim();

                        itemAssets[code.ToLower()] = assets.Replace("/", @"\");
                    }


                    foreach (var other in new string[] { "sets", "uniques" })
                    {
                        jline = File.ReadAllText(Utils.CacheFolder + @"\assets\" + other + ".json");
                        var jdata = JsonConvert.DeserializeObject<JArray>(jline);

                        foreach (var item in jdata)
                        {
                            var items = item.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                            var code = items[1].Replace("{", "").Replace("\"", "").Replace(":", "").Replace("-", "_").Trim();
                            if (code == "[" || code == "]") continue;

                            var assets = items[2].Split(new char[] { ':' })[1].Replace("{", "").Replace("\"", "").Replace(":", "").Replace(",", "").Replace("/", @"\").Trim();

                            itemAssets[code.ToLower()] = assets;
                        }
                    }
                }

                return itemAssets;
            }
            set
            {
                itemAssets = value;
            }
        }
        public static Dictionary<string, string> ItemNames
        {
            get
            {
                if (itemNames == null)
                {
                    itemNames = new Dictionary<string, string>();
                    JArray list = new JArray();
                    var line = File.ReadAllText(Utils.CacheFolder + @"\strings\item-names.json");
                    var items = Newtonsoft.Json.JsonConvert.DeserializeObject(line) as JArray;

                    line = File.ReadAllText(Utils.CacheFolder + @"\strings\item-runes.json");
                    var names = Newtonsoft.Json.JsonConvert.DeserializeObject(line) as JArray;

                    foreach (var name in names)
                    {
                        if (name["Key"].ToString().StartsWith("Runeword")) continue;

                        items.Add(name);
                    }

                    foreach (var item in items)
                    {
                        var key = item["Key"].ToString();
                        var name = item[Utils.CurrentLanguage].ToString();



                        itemNames[key] = name;

                    }
                }

                return itemNames;
            }
            set { itemNames = value; }
        }

        public static Dictionary<string, string> ItemModifiers
        {
            get
            {
                if (itemModifiers == null)
                {
                    itemModifiers = new Dictionary<string, string>();
                    JArray list = new JArray();
                    var line = File.ReadAllText(Utils.CacheFolder + @"\strings\item-modifiers.json");
                    var items = Newtonsoft.Json.JsonConvert.DeserializeObject(line) as JArray;

                    foreach (var item in items)
                    {
                        itemModifiers[item["Key"].ToString()] = item[Utils.CurrentLanguage].ToString().Replace("%%", "％");
                    }
                }

                return itemModifiers;
            }
            set { itemModifiers = value; }
        }
        public static Dictionary<int, string> SetItems
        {
            get
            {
                if (setItems == null)
                {
                    var allLines = File.ReadAllLines(Utils.CacheFolder + @"\excel\setitems.txt");

                    setItems = new Dictionary<int, string>();

                    for (int i = 1; i < allLines.Length; i++)
                    {
                        var tmp = allLines[i].Split(new char[] { '\t' });
                        if (tmp[1].Trim().Length == 0) continue;

                        int id = Convert.ToInt32(tmp[1].Trim());
                        string code = tmp[0].Trim();
                        setItems[id] = Utils.AllJsons[code];
                    }
                }

                return setItems;
            }
            set
            {
                setItems = value;
            }
        }

        public static Dictionary<int, string> UniqueItems
        {
            get
            {
                if (uniqueItems == null)
                {
                    var allLines = File.ReadAllLines(Utils.CacheFolder + @"\excel\uniqueitems.txt");

                    uniqueItems = new Dictionary<int, string>();

                    for (int i = 1; i < allLines.Length; i++)
                    {
                        var tmp = allLines[i].Split(new char[] { '\t' });
                        if (tmp[1].Trim().Length == 0) continue;

                        int id = Convert.ToInt32(tmp[1].Trim());
                        string code = tmp[0].Trim();

                        uniqueItems[id] = Utils.AllJsons[code];
                    }
                }

                return uniqueItems;
            }
            set
            {
                uniqueItems = value;
            }
        }

        public static Dictionary<int, string> MagicSuffixItems
        {
            get
            {
                if (magicSuffixItems == null)
                {
                    var allLines = File.ReadAllLines(Utils.CacheFolder + @"\excel\magicsuffix.txt");

                    magicSuffixItems = new Dictionary<int, string>();

                    for (int i = 1; i < allLines.Length; i++)
                    {
                        var tmp = allLines[i].Split(new char[] { '\t' });
                        string code = tmp[0].Trim();

                        if (code.Length == 0 || code == "Expansion")
                        {
                            continue;
                        }

                        magicSuffixItems[i - 1] = Utils.AllJsons[code];
                    }
                }

                return magicSuffixItems;
            }
            set
            {
                magicSuffixItems = value;
            }
        }

        public static Dictionary<int, string> MagicPrefixItems
        {
            get
            {
                if (magicPrefixItems == null)
                {
                    var allLines = File.ReadAllLines(Utils.CacheFolder + @"\excel\magicprefix.txt");

                    magicPrefixItems = new Dictionary<int, string>();

                    for (int i = 1; i < allLines.Length; i++)
                    {
                        var tmp = allLines[i].Split(new char[] { '\t' });
                        string code = tmp[0].Trim();

                        if (code.Length == 0 || code == "Expansion")
                        {
                            continue;
                        }

                        magicPrefixItems[i - 1] = Utils.AllJsons[code];
                    }
                }

                return magicPrefixItems;
            }
            set
            {
                magicPrefixItems = value;
            }
        }

        public static Dictionary<int, string> RuneWords
        {
            get
            {
                if (runeWords == null)
                {
                    var line = File.ReadAllText(Utils.CacheFolder + @"\strings\item-runes.json");
                    var names = Newtonsoft.Json.JsonConvert.DeserializeObject(line) as JArray;

                    runeWords = new Dictionary<int, string>();

                    var index = 1;
                    for (int i = 1; i <= names.Count; i++)
                    {
                        var item = names.Where(item2 => item2["Key"].Value<string>() == String.Format("Runeword{0}", i)).FirstOrDefault();
                        if (item == null)
                        {
                            index++;
                            continue;
                        }
                        var name = Utils.AllJsons[String.Format("Runeword{0}", i)];// item[Utils.CurrentLanguage].Value<string>();
                        //if (i >= 80) --index;

                        //if(i>=80)runeWords[index + 25] = name;else runeWords[index + 26] = name;
                        runeWords[index + 26] = name;
                        index++;
                    }
                }

                return runeWords;
            }
            set
            {
                runeWords = value;
            }
        }
        public static Dictionary<int, string> RarePrefixItems
        {
            get
            {
                if (rarePrefixItems == null)
                {
                    rarePrefixItems = new Dictionary<int, string>();
                    var lines = File.ReadAllLines(Utils.CacheFolder + @"\excel\rareprefix.txt");

                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (lines[i].Trim().Length == 0) continue;

                        var code = lines[i].Split(new char[] { '\t' })[0];
                        var key = 156 + (i - 1);//参照xmission关于rareitem first name那部分，看ID的开始位置，第一个就是0x01

                        string val = "";
                        if (Utils.AllJsons.TryGetValue(code, out val))
                        {
                            rarePrefixItems[key] = Utils.AllJsons[code];
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(String.Format("Rare prefix code={0}", code));
                        }
                    }
                }

                return rarePrefixItems;
            }
            set
            {
                rarePrefixItems = value;
            }
        }
        public static Dictionary<int, string> RareSuffixItems
        {
            get
            {
                if (rareSuffixItems == null)
                {
                    rareSuffixItems = new Dictionary<int, string>();
                    var lines = File.ReadAllLines(Utils.CacheFolder + @"\excel\raresuffix.txt");

                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (lines[i].Trim().Length == 0) continue;

                        var code = lines[i].Split(new char[] { '\t' })[0];
                        var key = 1 + (i - 1);//参照xmission关于rareitem first name那部分，看ID的开始位置，第一个就是0x01

                        string val = "";
                        if (Utils.AllJsons.TryGetValue(code, out val))
                        {
                            rareSuffixItems[key] = Utils.AllJsons[code];
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(String.Format("Rare suffix code={0}", code));
                        }
                    }
                }

                return rareSuffixItems;
            }
            set
            {
                rareSuffixItems = value;
            }
        }

        public static string GetSkillName(int skillId)
        {
            string ret = "";

            //对比skills.txt和skills.json，里面的"Key": "Skillname222",这个就知道了，从221开始，错位了，特么的！！！
            //if (skillId >= 221) ++skillId;
            //var key = "skillname" + Convert.ToString(skillId);
            //应该通过skilldesc链接过去，否则282以上的就找不到了
            var key = ExcelTxt.SkillDescTxt[ExcelTxt.SkillsTxt[skillId]["skilldesc"].Value]["str name"].Value;
            ret = Utils.AllJsons[key];
            //if (!Utils.AllJsons.TryGetValue(key,out ret))
            //{
            //    key = "Skillname" + Convert.ToString(skillId);
            //    if (!Utils.AllJsons.TryGetValue(key, out ret))
            //    {
            //        ret = "UNKNOWN skill name";
            //    }
            //}

            return ret;
        }

        public static List<MiniItem> MiniItemList
        {
            get
            {
                if (miniItemList == null)
                {
                    miniItemList = new List<MiniItem>();

                    //weap,武器
                    //armo,防具
                    //misc,其他

                    Dictionary<string, string> typeMappings = new Dictionary<string, string>();
                    #region 类型
                    typeMappings.Add("shie", Utils.AllJsons["shield"]);
                    typeMappings.Add("tors", Utils.AllJsons["Armor"]);
                    typeMappings.Add("boot", Utils.AllJsons["lbt"]);
                    typeMappings.Add("glov", Utils.AllJsons["lgl"]);
                    typeMappings.Add("belt", Utils.AllJsons["mbl"]);
                    typeMappings.Add("helm", Utils.AllJsons["hlm"]);
                    typeMappings.Add("phlm", Utils.AllJsons["barbarian_helms"]);
                    typeMappings.Add("ashd", Utils.AllJsons["paladin_shields"]);
                    typeMappings.Add("head", Utils.AllJsons["necromancer_shrunken_heads"]);
                    typeMappings.Add("pelt", Utils.AllJsons["druid_pelts"]);
                    //typeMappings.Add("cloa", "斗篷");
                    typeMappings.Add("circ", Utils.AllJsons["circlet"]);

                    typeMappings.Add("bowq", "弓");
                    typeMappings.Add("xboq", "弩");
                    typeMappings.Add("play", "玩家身体");
                    typeMappings.Add("herb", "赫拉迪姆大箱子");
                    typeMappings.Add("poti", "药水");
                    typeMappings.Add("ring", Utils.allJsons["rin"]);
                    typeMappings.Add("elix", Utils.allJsons["elx"]);
                    typeMappings.Add("amul", Utils.allJsons["amu"]);
                    typeMappings.Add("char", "护身符");
                    typeMappings.Add("book", "书");
                    typeMappings.Add("gem", "宝石");
                    typeMappings.Add("torc", "火炬");
                    typeMappings.Add("scro", "卷轴");

                    typeMappings.Add("body", "身体");
                    typeMappings.Add("key", "钥匙");


                    typeMappings.Add("jewl", Utils.allJsons["jew"]);
                    typeMappings.Add("rune", Utils.allJsons["Rune"]);
                    typeMappings.Add("hpot", Utils.allJsons["hp3"]);
                    typeMappings.Add("mpot", Utils.allJsons["mp3"]);
                    typeMappings.Add("rpot", Utils.allJsons["rvs"]);
                    typeMappings.Add("spot", Utils.allJsons["vps"]);
                    typeMappings.Add("apot", Utils.allJsons["yps"]);
                    typeMappings.Add("wpot", Utils.allJsons["wms"]);
                    typeMappings.Add("scha", Utils.allJsons["small_charm"]);
                    typeMappings.Add("mcha", Utils.allJsons["medium_charm"]);
                    typeMappings.Add("lcha", Utils.allJsons["large_charm"]);

                    typeMappings.Add("mboq", "魔法弓");
                    typeMappings.Add("mxbq", "魔法弩");
                    typeMappings.Add("gem0", "碎裂的宝石");
                    typeMappings.Add("gem1", "裂开的宝石");
                    typeMappings.Add("gem2", "普通的宝石");
                    typeMappings.Add("gem3", "无瑕疵的宝石");
                    typeMappings.Add("gem4", "完美的宝石");
                    typeMappings.Add("gema", Utils.AllJsons["gsv"]);
                    typeMappings.Add("gemd", Utils.AllJsons["Diamond"]);
                    typeMappings.Add("geme", Utils.AllJsons["Emerald"]);
                    typeMappings.Add("gemr", Utils.AllJsons["Ruby"]);
                    typeMappings.Add("gems", Utils.AllJsons["Sapphire"]);
                    typeMappings.Add("gemt", Utils.AllJsons["gsy"]);
                    typeMappings.Add("gemz", Utils.AllJsons["sku"]);
                    typeMappings.Add("ques", Utils.AllJsons["ques"]);

                    typeMappings.Add("scep", Utils.AllJsons["scp"]);
                    typeMappings.Add("wand", Utils.AllJsons["wnd"]);
                    typeMappings.Add("staf", Utils.AllJsons["Staff"]);
                    typeMappings.Add("tkni", Utils.AllJsons["tkf"]);
                    typeMappings.Add("taxe", Utils.AllJsons["tax"]);
                    typeMappings.Add("jave", Utils.AllJsons["jav"]);
                    typeMappings.Add("bow", Utils.AllJsons["lbw"]);
                    typeMappings.Add("axe", Utils.AllJsons["axe"]);
                    typeMappings.Add("club", Utils.AllJsons["clb"]);
                    typeMappings.Add("swor", Utils.AllJsons["lsd"]);
                    typeMappings.Add("hamm", Utils.AllJsons["mau"]);
                    typeMappings.Add("knif", Utils.AllJsons["dgr"]);
                    typeMappings.Add("spea", Utils.AllJsons["spr"]);
                    typeMappings.Add("pole", Utils.AllJsons["Polearm"]);
                    typeMappings.Add("xbow", Utils.AllJsons["mxb"]);
                    typeMappings.Add("mace", Utils.AllJsons["mac"]);
                    typeMappings.Add("h2h", Utils.AllJsons["ktr"]);
                    typeMappings.Add("orb", Utils.AllJsons["ob2"]);
                    typeMappings.Add("abow", Utils.AllJsons["amazon_bow"]);
                    typeMappings.Add("aspe", Utils.AllJsons["amazon_xbow"]);
                    typeMappings.Add("ajav", Utils.AllJsons["amazon_javelin"]);
                    typeMappings.Add("h2h2", Utils.AllJsons["ktr"]);
                    typeMappings.Add("tpot", Utils.AllJsons["tpot"]);
                    #endregion 类型

                    var w = ExcelTxt.WeaponsTxt.Rows.GroupBy(r => r["type"].Value);
                    foreach (var subtype in w)
                    {
                        foreach (var item in subtype)
                        {
                            if (subtype.Key == "") continue;
                            miniItemList.Add(new MiniItem() { TypeCode = "weap", TypeName = Utils.AllJsons["strBSWeapons"], SubTypeCode = subtype.Key, SubTypeName = typeMappings[subtype.Key], ItemCode = item["code"].Value, ItemName = Utils.AllJsons[item["code"].Value] });///* Utils.ItemNames[item["code"].Value]*/
                        }

                    }
                    var a = ExcelTxt.ArmorTxt.Rows.GroupBy(r => r["type"].Value);
                    foreach (var subtype in a)
                    {
                        foreach (var item in subtype)
                        {
                            if (subtype.Key == "") continue;
                            miniItemList.Add(new MiniItem() { TypeCode = "armo", TypeName = Utils.AllJsons["strBSArmor"], SubTypeCode = subtype.Key, SubTypeName = typeMappings[subtype.Key], ItemCode = item["code"].Value, ItemName = Utils.AllJsons[item["code"].Value] });
                        }

                    }

                    //var ignoreKeys = new List<string>() {"scha","mcha","lcha", "gem0", "gem1", "gem2", "gem3", "gem4", "hpot", "mpot", "rpot", "spot", "apot", "wpot", "jewl", };
                    var m = ExcelTxt.MiscTxt.Rows.GroupBy(r => r["type"].Value);
                    foreach (var subtype in m)
                    {
                        foreach (var item in subtype)
                        {
                            //if (ignoreKeys.Contains(subtype.Key)) continue;
                            if (subtype.Key == "") continue;
                            if (Utils.ItemNames.ContainsKey(item["code"].Value))
                            {
                                miniItemList.Add(new MiniItem() { TypeCode = "misc", TypeName = Utils.AllJsons["strBSMisc"], SubTypeCode = subtype.Key, SubTypeName = typeMappings[subtype.Key], ItemCode = item["code"].Value, ItemName = Utils.AllJsons[item["code"].Value] });
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(item["code"].Value);
                            }
                        }
                    }
                }

                return miniItemList;
            }
            set
            {
                miniItemList = value;
            }
        }

        public static List<MiniItemStatCost> MiniItemStatCostList
        {
            get
            {
                if (miniItemStatCostList == null)
                {
                    miniItemStatCostList = new List<MiniItemStatCost>();

                    foreach (var row in ExcelTxt.ItemStatCostTxt.Rows)
                    {
                        var sbits = row["Save Bits"].Value;
                        var sadd = row["Save Add"].Value;

                        int bits = 0;
                        int add = 0;

                        Int32.TryParse(sbits, out bits);
                        Int32.TryParse(sadd, out add);

                        int max = (1 << bits) - add - 1;

                        string descfunc = row["descfunc"].Value;
                        if (String.IsNullOrEmpty(descfunc))
                        {
                            miniItemStatCostList.Add(new MiniItemStatCost() { ID = row["*ID"].Value, DescFunc = row["Stat"].Value, MaxValue = max, Stat1 = row["op stat1"].Value, Stat2 = row["op stat2"].Value, Stat3 = row["op stat3"].Value, Visible = false });
                            continue;
                        }

                        string desckey = "";
                        string descformat = "";



                        int opParam = 1;
                        string opBase = row["op base"].Value;

                        if (!String.IsNullOrEmpty(row["op param"].Value)) opParam = Convert.ToInt32(row["op param"].Value);

                        bool isPerLevel = (opBase == "level");

                        desckey = row["descstrpos"].Value.Trim();
                        var dgrpfunc = "";
                        var dgrpkey = "";
                        var dgrpformat = "";

                        bool dgrp = Convert.ToString(row["dgrp"].Value).Length > 0;
                        if (dgrp)
                        {
                            dgrpfunc = row["dgrpfunc"].Value;
                            dgrpkey = row["dgrpstrpos"].Value;
                            dgrpformat = Utils.AllJsons[dgrpkey].Replace("%%", "%");// FormatCStyleStrings(Utils.AllJsons[dgrpkey].Replace("%%", "％"));
                        }

                        descformat = Utils.AllJsons[desckey].Replace("%%", "%");// FormatCStyleStrings(Utils.AllJsons[desckey].Replace("%%", "％"));


                        MiniItemStatCost misc = new MiniItemStatCost()
                        {
                            ID = row["*ID"].Value,
                            IsPerLevel = isPerLevel,
                            Param = 0,
                            Value = 0,
                            MaxValue = max,
                            Stat1 = row["op stat1"].Value,
                            Stat2 = row["op stat2"].Value,
                            Stat3 = row["op stat3"].Value,
                            DescFunc = descformat + (isPerLevel ? "(" + Utils.AllJsons["increaseswithplaylevelX"] + ")" : ""),
                            DgrpFunc = dgrpformat,
                            Visible = true
                        };

                        string info = "";
                        if (!String.IsNullOrEmpty(misc.Stat1))
                        {
                            info = misc.Stat1;
                            Utils.AllJsons.TryGetValue(ExcelTxt.ItemStatCostTxt[misc.Stat1]["descstrpos"].Value, out info);
                            misc.Stat1 = info;
                        }

                        if (!String.IsNullOrEmpty(misc.Stat2))
                        {
                            info = misc.Stat2;
                            Utils.AllJsons.TryGetValue(ExcelTxt.ItemStatCostTxt[misc.Stat2]["descstrpos"].Value, out info);
                            misc.Stat2 = info;
                        }

                        if (!String.IsNullOrEmpty(misc.Stat3))
                        {
                            info = misc.Stat3;
                            Utils.AllJsons.TryGetValue(ExcelTxt.ItemStatCostTxt[misc.Stat3]["descstrpos"].Value, out info);
                            misc.Stat3 = info;
                        }

                        miniItemStatCostList.Add(misc);
                    }
                }

                return miniItemStatCostList;
            }
            set
            {
                miniItemStatCostList = value;
            }
        }


        public static List<string> QualityList = new List<string>() { "白色劣质", "白色普通", "白色超强", "蓝色魔法", "绿色套装", "黄色亮金", "暗金", "橙色手工品", "Tempered品质" };


    }

    public class MiniItem
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string SubTypeCode { get; set; }
        public string SubTypeName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }

    public class MiniItemStatCost
    {
        public string ID { get; set; }
        public bool IsPerLevel { get; set; }
        public string CostFunc { get; set; }
        public string DescFunc { get; set; }
        public int Value { get; set; }
        public int Param { get; set; }
        public int MaxValue { get; set; }
        public string Stat1 { get; set; }
        public string Stat2 { get; set; }
        public string Stat3 { get; set; }
        public string DgrpFunc { get; set; }

        public bool Visible { get; set; }

        public MiniItemStatCost Clone()
        {
            return new MiniItemStatCost() { ID = this.ID, IsPerLevel = this.IsPerLevel, CostFunc = this.CostFunc, DescFunc = this.DescFunc, Value = this.Value, Param = this.Param, MaxValue = this.MaxValue, Stat1 = this.Stat1, Stat2 = this.Stat2, Stat3 = this.Stat3, DgrpFunc = this.DgrpFunc, Visible = this.Visible };
        }
    }


}
