using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace D2REditor
{


    public class LanguageMapping { public string Name { get; set; } public string Key { get; set; } public LanguageMapping(string name, string key) { this.Name = name; this.Key = key; } }


    public class Helper
    {
        public static string Host = "http://localhost";
        public const int Version = 0x61;
        public static float DisplayRatio = 1.0f;
        public static float DisplayRatio2 = 0.6f;
        public static string CurrentD2SFileName = "";
        public static string GeneralButtonImageFile = @"\frontend\hd\final\cinematics\cinematicsbtn";
        private static Dictionary<int, Bitmap> spmappings = new Dictionary<int, Bitmap>();
        private static Dictionary<int, Bitmap> spmappings2 = new Dictionary<int, Bitmap>();
        public static string DefaultD2RFolder = "";
        public static D2S CurrentCharactor;
        public static Brush TextBrush;
        public static List<D2I> SharedStashes;
        public static List<LanguageMapping> LanguageMappings = new List<LanguageMapping>();
        public static string CurrentLanguage
        {
            get { return Utils.CurrentLanguage; }
            set { Utils.CurrentLanguage = value; }
        }

        static Helper()
        {
            Helper.DefaultD2RFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Saved Games\Diablo II Resurrected\";
            Helper.SharedD2IFileName = Helper.DefaultD2RFolder + "SharedStashSoftCoreV2.d2i";

            Helper.IsHighDefinition = false;

            Helper.TextBrush = new SolidBrush(Color.FromArgb(199, 179, 119));

            LanguageMappings.Add(new LanguageMapping("English", "enUS"));
            LanguageMappings.Add(new LanguageMapping("Deutsch", "deDE"));
            LanguageMappings.Add(new LanguageMapping("Español (España)", "esES"));
            LanguageMappings.Add(new LanguageMapping("Français", "frFR"));
            LanguageMappings.Add(new LanguageMapping("Italiano", "itIT"));
            LanguageMappings.Add(new LanguageMapping("日本語", "jaJP"));
            LanguageMappings.Add(new LanguageMapping("Español (Latino)", "esMX"));
            LanguageMappings.Add(new LanguageMapping("한국어", "koKR"));
            LanguageMappings.Add(new LanguageMapping("Polonês", "ptBR"));
            LanguageMappings.Add(new LanguageMapping("Portugalski", "plPL"));
            LanguageMappings.Add(new LanguageMapping("Русский", "ruRU"));
            LanguageMappings.Add(new LanguageMapping("中文 (简体)", "zhCN"));
            LanguageMappings.Add(new LanguageMapping("中文（繁體）", "zhTW"));
        }

        public static string SharedD2IFileName
        {
            get; set;
        }
        public static string CacheFolder
        {
            get
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\D2REditor";
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                if (!Directory.Exists(folder + "\\backup")) Directory.CreateDirectory(folder + "\\backup");

                Utils.CacheFolder = folder;

                return folder;
            }
        }

        public static void WriteSharedStashes(List<D2I> list)
        {
            var bytes = D2I.Write2(list, 0x61);
            File.WriteAllBytes(Helper.SharedD2IFileName, bytes);
        }
        public static DefinitionInfo DefinitionInfo
        {
            get
            {
                if (IsHighDefinition) return highEndDefinitionInfo; else return lowEndDefinitionInfo;
            }
        }

        private static DefinitionInfo highEndDefinitionInfo = new DefinitionInfo()
        {
            StashTabStartX = 79,
            StashTabStartY = 160,
            StashTabTitleStartX = 170,
            StashTabTitleStartY = 180,
            StashTitleStartX = 490,
            StashTitleStartY = 66,
            InventoryTitleStartX = 1630,
            InventoryTitleStartY = 66,
            StashTabWidth = 249,
            StashTabHeight = 80,
            StashTabFontSize = 24,
            StashTitleFontSize = 48,

            TooltipFontSize = 36,

            StoreRangeX = new int[] { 1256, 1354, 1452, 1550, 1648, 1746, 1844, 1942, 2040, 2138 },
            StoreRangeY = new int[] { 901, 999, 1097, 1195 },
            StashRangeX = new int[] { 93, 191, 289, 387, 485, 583, 681, 779, 877, 975 },
            StashRangeY = new int[] { 240, 338, 436, 534, 632, 730, 828, 926, 1024, 1122 },
            BoxSize = 98,
            InnerBoxSize = 91,
            EquipedItem = new Rectangle[]{
                new Rectangle(1645, 183, 98 * 2, 98 * 2),//Head
                new Rectangle(1270, 232, 98 * 2, 98 * 4),//RightHand
                new Rectangle(2022, 232, 98 * 2, 98 * 4),//LeftHand
                new Rectangle(1881, 769, 98 * 1, 98 * 1),//LeftFinger
                new Rectangle(1506, 769, 98 * 1, 98 * 1),//RightFinger
                new Rectangle(1881, 353, 98 * 1, 98 * 1),//Neck
                new Rectangle(1645, 428, 98 * 2, 98 * 3),//Torso
                new Rectangle(2022, 671, 98 * 2, 98 * 2),//Feet
                new Rectangle(1269, 671, 98 * 2, 98 * 2),//Gloves
                new Rectangle(1645, 769, 98 * 2, 98 * 1),//Waist
                new Rectangle(1270, 232, 98 * 2, 98 * 4),//2nd RightHand
                new Rectangle(2022, 232, 98 * 2, 98 * 4),//2nd LeftHand
            },
        };

        public static void RefreshSettings()
        {
            try
            {
                var lines = File.ReadAllLines(Helper.CacheFolder + "\\settings.txt");
                foreach (var line in lines)
                {
                    if (line.StartsWith("language="))
                    {
                        Helper.CurrentLanguage = line.Substring(9);
                    }
                    else if (line.StartsWith("d2rfolder="))
                    {
                        Helper.DefaultD2RFolder = line.Substring(10);
                    }
                }
            }
            catch
            {
                Helper.CurrentLanguage = "zhTW";
            }
        }
        private static DefinitionInfo lowEndDefinitionInfo = new DefinitionInfo()
        {
            StashTabStartX = 40,
            StashTabStartY = 80,
            StashTabTitleStartX = 196,
            StashTabTitleStartY = 90,
            StashTitleStartX = 245,
            StashTitleStartY = 33,

            InventoryTitleStartX = 815,
            InventoryTitleStartY = 33,
            StashTabWidth = 125,
            StashTabHeight = 40,
            StashTabFontSize = 9,
            StashTitleFontSize = 12,

            TooltipFontSize = 12,

            //StoreRangeX = new int[] { 628, 677, 726, 775, 824, 873, 922, 971, 1020, 1069 },
            //StoreRangeY = new int[] { 450, 499, 548, 597 },
            //StashRangeX = new int[] { 47, 96, 145, 194, 243, 292, 341, 390, 439, 488 },
            //StashRangeY = new int[] { 120, 169, 218, 267, 316, 365, 414, 463, 512, 561 },
            StoreRangeX = new int[] { 629, 661, 694,727,759,792,825,857,890,923,955,988,1021,1053,1086},
            StoreRangeY = new int[] { 450, 483, 516,548,581,614,646,679},
            StashRangeX = new int[] { 47, 78,108,139,169,200,231,261,292,323,354,384,415,445,476,507},
            StashRangeY = new int[] { 121, 151,181,212,243,273,304,335,365,396,427,457,488,519,550,580},
            //BoxSize = 49,
            //InnerBoxSize = 45,
            BoxSize = 30,
            InnerBoxSize = 28,
            EquipedItem = new Rectangle[] {
                new Rectangle(822, 91, 49 * 2, 49 * 2),//Head, +581
                new Rectangle(635, 116, 49 * 2, 49 * 4),//RightHand
                new Rectangle(1011, 116, 49 * 2, 49 * 4),//LeftHand
                new Rectangle(940, 384, 49 * 1, 49 * 1),//LeftFinger
                new Rectangle(753, 384, 49 * 1, 49 * 1),//RightFinger
                new Rectangle(940, 176, 49 * 1, 49 * 1),//Neck
                new Rectangle(822, 214, 49 * 2, 49 * 3),//Torso
                new Rectangle(1011, 335, 49 * 2, 49 * 2),//Feet
                new Rectangle(634, 335, 49 * 2, 49 * 2),//Gloves
                new Rectangle(822, 384, 49 * 2, 49 * 1),//Waist
                new Rectangle(635, 116, 49 * 2, 49 * 4),//2nd RightHand
                new Rectangle(1011, 116, 49 * 2, 49 * 4),//2nd LeftHand
            },

            QuestTabStartX = 38,
            QuestTabStartY = 72,
            QuestTabWidth = 101,
            QuestTabHeight = 40,
            QuestTabTitleStartX = 80,
            QuestTabTitleStartY = 80,
            QuestTitleStartX = 260,
            QuestTitleStartY = 36,
            QuestIconStartX = 96,
            QuestIconStartY = 156,
            QuestIconWidth = 154,
            QuestIconHeight = 156,

            WaypointsTabStartX = 74,
            WaypointsTabStartY = 100,
            WaypointsTabWidth = 87,
            WaypointsTabHeight = 40,
            WaypointsTabTitleStartX = 104,
            WaypointsTabTitleStartY = 110,
            WaypointsTitleStartX = 260,
            WaypointsTitleStartY = 36,
            WaypointsIconStartX = 96,
            WaypointsIconStartY = 156,
            WaypointsIconWidth = 154,
            WaypointsIconHeight = 156,

        };

        public static bool IsHighDefinition { get; set; }

        public static Bitmap Sprite2Png(string fileName, bool skipSplit=true)
        {
            Bitmap bmp = null;

            var fname = fileName.Replace(".sprite", ".png");
            int hash = fileName.GetHashCode();

            if (false == spmappings.TryGetValue(hash, out bmp))
            {
                //if (!File.Exists(fname))
                //{
                //    var bytes = File.ReadAllBytes(fileName);
                //    int x, y;
                //    var version = BitConverter.ToUInt16(bytes, 4);
                //    var width = BitConverter.ToInt32(bytes, 8);
                //    var height = BitConverter.ToInt32(bytes, 0xC);
                //    bmp = new Bitmap(width, height);

                //    if (version == 31)
                //    {   // regular RGBA
                //        for (x = 0; x < height; x++)
                //        {
                //            for (y = 0; y < width; y++)
                //            {
                //                var baseVal = 0x28 + x * 4 * width + y * 4;
                //                bmp.SetPixel(y, x, Color.FromArgb(bytes[baseVal + 3], bytes[baseVal + 0], bytes[baseVal + 1], bytes[baseVal + 2]));
                //            }
                //        }
                //    }
                //    else if (version == 61)
                //    {   // DXT
                //        var tempBytes = new byte[width * height * 4];
                //        Dxt.DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                //        for (y = 0; y < height; y++)
                //        {
                //            for (x = 0; x < width; x++)
                //            {
                //                var baseVal = (y * width) + (x * 4);
                //                bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                //            }
                //        }
                //    }

                //    bmp.Save(fname);
                //}

                bmp = Image.FromFile(fname) as Bitmap;

                if (!skipSplit)
                {
                    spmappings[hash] = bmp;
                }
                else
                {
                    var zoombmp = new Bitmap((int)(bmp.Width * Helper.DisplayRatio), (int)(bmp.Height * Helper.DisplayRatio));
                    using (Graphics g = Graphics.FromImage(zoombmp))
                    {
                        g.DrawImage(bmp, new Rectangle(0, 0, zoombmp.Width, zoombmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                    }
                    spmappings[hash] = zoombmp;
                    bmp.Dispose();
                }                    
            }

            return spmappings[hash];
        }

        public static Bitmap Sprite2Png2(string fileName, bool skipSplit = true)
        {
            Bitmap bmp = null;

            var fname = fileName.Replace(".sprite", ".png");
            int hash = fileName.GetHashCode();

            if (false == spmappings2.TryGetValue(hash, out bmp))
            {
                //if (!File.Exists(fname))
                //{
                //    var bytes = File.ReadAllBytes(fileName);
                //    int x, y;
                //    var version = BitConverter.ToUInt16(bytes, 4);
                //    var width = BitConverter.ToInt32(bytes, 8);
                //    var height = BitConverter.ToInt32(bytes, 0xC);
                //    bmp = new Bitmap(width, height);

                //    if (version == 31)
                //    {   // regular RGBA
                //        for (x = 0; x < height; x++)
                //        {
                //            for (y = 0; y < width; y++)
                //            {
                //                var baseVal = 0x28 + x * 4 * width + y * 4;
                //                bmp.SetPixel(y, x, Color.FromArgb(bytes[baseVal + 3], bytes[baseVal + 0], bytes[baseVal + 1], bytes[baseVal + 2]));
                //            }
                //        }
                //    }
                //    else if (version == 61)
                //    {   // DXT
                //        var tempBytes = new byte[width * height * 4];
                //        Dxt.DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                //        for (y = 0; y < height; y++)
                //        {
                //            for (x = 0; x < width; x++)
                //            {
                //                var baseVal = (y * width) + (x * 4);
                //                bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                //            }
                //        }
                //    }

                //    bmp.Save(fname);
                //}

                bmp = Image.FromFile(fname) as Bitmap;

                if (!skipSplit)
                {
                    spmappings2[hash] = bmp;
                }
                else
                {
                    var zoombmp = new Bitmap((int)(bmp.Width * Helper.DisplayRatio2), (int)(bmp.Height * Helper.DisplayRatio2));
                    using (Graphics g = Graphics.FromImage(zoombmp))
                    {
                        g.DrawImage(bmp, new Rectangle(0, 0, zoombmp.Width, zoombmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                    }
                    spmappings2[hash] = zoombmp;
                    bmp.Dispose();
                }
            }

            return spmappings2[hash];
        }

        public static void CacheD2S(D2S d2s)
        {
            File.WriteAllBytes(String.Format("{0}\\backup\\{1}级的{2}-{3}于{4}备份.d2s", Helper.CacheFolder, d2s.Level, d2s.ClassName, d2s.DisplayName, DateTime.Now.ToString("yyyy-MM-dd HH点mm分")), Core.WriteD2S(d2s));
            if (File.Exists(Helper.SharedD2IFileName)) File.WriteAllBytes(String.Format("{0}\\backup\\SharedStashSoftCoreV2_{1}备份.d2i", Helper.CacheFolder, DateTime.Now.ToString("yyyy-MM-dd HH点mm分")), File.ReadAllBytes(Helper.SharedD2IFileName));
        }
        public static Bitmap GetImageByFrame(Image img, int frames, int frameno)
        {
            Bitmap ret = new Bitmap(img.Width / frames, img.Height);
            Graphics g = Graphics.FromImage(ret);
            g.DrawImage(img, new Rectangle(0, 0, ret.Width, ret.Height), new Rectangle(frameno * ret.Width, 0, ret.Width, ret.Height), GraphicsUnit.Pixel);

            return ret;
        }

        public static string GetResourceFile(string filename)
        {
            if (filename.StartsWith(@"\")) filename = filename.Substring(1);
            return Helper.CacheFolder + @"\" + filename;
        }
        public static string GetDefinitionFileName(string filename)
        {
            if (!IsHighDefinition)
            {
                filename += ".lowend";
            }

            filename += ".sprite";

            return Helper.GetResourceFile(filename);
        }

        public static bool IsPointInRange(Point p, Rectangle g)
        {
            return (p.X >= g.X) && (p.X < g.X + g.Width) && (p.Y >= g.Y) && (p.Y < g.Y + g.Height);
        }

        private static int formatCount;
        private static string FormatReplacement(Match m)
        {
            string num = m.Groups["Number"].Value;
            string type = m.Groups["Type"].Value;

            return String.Concat("{", formatCount++, "}");
        }

        public static string FormatCStyleStrings(string input)
        {
            formatCount = 0;
            input = input.Replace("%+d", "+%d").Replace("%-d", "-%d");

            string pattern = @"%(?<Number>\d+(\.\d+)?)?(?<Type>d|f|s)";
            string result = Regex.Replace(input, pattern, FormatReplacement);
            return result;
        }

        public static ItemStatCostFunc GetItemStatCostFunc(ItemStat stat)
        {
            ItemStatCostFunc ret = new ItemStatCostFunc();
            ret.ID = (int)stat.Id;

            var row = ItemStat.GetStatRow(stat);


            int opParam = 1;
            string opBase = row["op base"].Value;

            if (!String.IsNullOrEmpty(row["op param"].Value)) opParam = Convert.ToInt32(row["op param"].Value);

            if (opBase == "level") ret.IsPerLevel = true;

            if (row["op stat1"].Value == "armorclass") ret.IsArmorClass = true;
            else if (row["op stat1"].Value == "maxdamage") ret.IsMaxDamage = true;
            else if (row["op stat1"].Value == "mindamage") ret.IsMinDamage = true;

            if (row["*ID"].Value == "31") ret.IsArmorClass = true;
            if (row["*ID"].Value == "21" || row["*ID"].Value == "23") ret.IsMinDamage = true;
            if (row["*ID"].Value == "22" || row["*ID"].Value == "24") ret.IsMaxDamage = true;

            //计算真实的值
            if (opBase.Length > 0)
            {
                for (int opstatno = 27; opstatno < 30; opstatno++)
                {
                    var statvalue = row[opstatno].Value;
                    if (String.IsNullOrEmpty(statvalue)) continue;

                    switch (row["op"].Value)
                    {
                        case "":
                        case "0":
                        case "6":
                        case "7":
                        case "10":
                        case "12":
                            ret.NewValue = stat.Value;
                            break;
                        case "1"://percent operator
                            ret.NewValue = 1 + stat.Value * 1.0d / 100.0d;
                            break;
                        case "2"://by level operator
                            ret.NewValue = stat.Value * 1.0d / (1 << opParam);//乘以level就是最终的值
                            break;
                        case "3"://by level percent operator
                            ret.NewValue = stat.Value * 1.0d / (1 << opParam) / 100.0d;
                            break;
                        case "4"://by level source operator
                            ret.NewValue = stat.Value * 1.0d / (1 << opParam);
                            break;
                        case "5"://by level source percent operator
                            ret.NewValue = stat.Value * 1.0d / (1 << opParam) / 100.0d;
                            break;
                        case "8"://energy operator
                            int mana = Convert.ToInt32(ExcelTxt.CharStatsTxt["Necromancer"]["ManaPerMagic"].Value);
                            ret.NewValue = mana >> (8 - 2);//Calculated in fourths and is divided by 256
                            break;
                        case "9":
                            if (statvalue == "maxstamina")
                            {
                                int stamina = Convert.ToInt32(ExcelTxt.CharStatsTxt["Necromancer"]["StaminaPerVitality"].Value);
                                ret.NewValue = stamina >> (8 - 2);
                            }
                            else
                            {
                                int other = Convert.ToInt32(ExcelTxt.CharStatsTxt["Necromancer"]["LifePerVitality"].Value);
                                ret.NewValue = other >> (8 - 2);
                            }
                            break;
                        case "11"://player percent operator
                            ret.NewValue = 1.0d + stat.Value * 1.0d / 100.0d;//作用于角色
                            break;
                        case "13"://Item Percent Operator
                            ret.NewValue = stat.Value * 1.0d / 100.0d;//作用于装备
                            break;
                        default:
                            ret.NewValue = stat.Value;
                            break;
                    }

                    //System.Diagnostics.Debug.WriteLine(String.Format("{2} {0} : {1}", statvalue, ret.NewValue, this.Name));
                }
            }
            else
            {
                ret.NewValue = stat.Value;
            }

            ret.Format = GetFormatFromDescFunc(row, stat) + (ret.IsPerLevel ? "(" + Utils.AllJsons["increaseswithplaylevelX"] + ")" : "");
            ret.DgrpFormat = GetFormatFromDgrpFunc(row, stat);

            var priority = row["descpriority"].Value;
            if (priority.Length > 0) ret.Priority = Convert.ToInt32(priority);

            return ret;
        }

        private static string GetFormatFromDescFunc(TxtRow row, ItemStat stat)
        {
            string key = row["descstrpos"].Value.Trim();
            if (stat.Id == 83) key = ExcelTxt.CharStatsTxt.Rows[Convert.ToInt32(stat.Param)]["StrAllSkills"].Value;
            if (stat.Id == 188)
            {
                key = ExcelTxt.CharStatsTxt.Rows[Convert.ToInt32(stat.SkillLevel)]["StrSkillTab" + (Convert.ToInt32(stat.SkillTab) + 1).ToString()].Value;
            }

            if (key == "" && stat.Value < 0)
            {
                key = row["descstrneg"].Value;
            }
            if (String.IsNullOrEmpty(key))
            {
                return "";
            }

            var format = FormatCStyleStrings(Utils.AllJsons[key].Replace("%%", "%"));
            var desc = GetFormatFromFunc(row["descfunc"].Value, format, stat);

            return desc;
        }

        private static string GetFormatFromDgrpFunc(TxtRow row, ItemStat stat)
        {
            string key = row["dgrpstrpos"].Value.Trim();
            //if (stat.Id == 83) key = ExcelTxt.CharStatsTxt.Rows[Convert.ToInt32(stat.Param)]["StrAllSkills"].Value;

            if (key == "" && stat.Value < 0)
            {
                key = row["dgrpstrneg"].Value;
            }
            if (String.IsNullOrEmpty(key))
            {
                return "";
            }

            var format = FormatCStyleStrings(Utils.AllJsons[key].Replace("%%", "％"));
            var desc = GetFormatFromFunc(row["dgrpfunc"].Value, format, stat);

            return desc;
        }

        private static string GetFormatFromFunc(string func, string format, ItemStat stat)
        {

            string desc = "";
            //生成正确的format，只有level需要处理，以及那个/128的那个，所以外面替换掉。
            switch (Convert.ToInt32(func))
            {
                case 1:
                case 2:
                case 3:
                case 13:
                case 19:
                    desc = String.Format(format, "FQQ");
                    break;
                case 15://Proc Skill: Gets the skill name, skill level, and chance percent to insert into the “descstrpos” string
                    desc = String.Format(format, "FQQ", stat.SkillLevel, Utils.GetSkillName((int)stat.SkillId));
                    break;
                case 27:
                    var sid = stat.Param.Value;
                    if (ExcelTxt.SkillsTxt[sid].Data[2].Value == "")
                    {
                        desc = String.Format(format, "FQQ", Utils.GetSkillName(sid), "（限" + "(renhe)" + "使用）");
                    }
                    else
                    {
                        var _class = ExcelTxt.PlayerClassTxt[ExcelTxt.SkillsTxt[sid].Data[2].Value]["Player Class"].Value;
                        if (_class == "Necromancer") _class = "Necromaner";
                        var limit = "str" + _class + "Only";

                        desc = String.Format(format, "FQQ", Utils.GetSkillName(sid), Utils.AllJsons[limit]);
                    }
                    break;
                case 24:
                    desc = String.Format(format, "FQQ", Utils.GetSkillName(stat.SkillId.Value), stat.Value, stat.MaxCharges);
                    //ret.NewValue = Convert.ToDouble(stat.SkillLevel);
                    break;
                case 16:
                case 28:
                    desc = String.Format(format, "FQQ", Utils.GetSkillName(stat.Param.Value));
                    break;
                case 14:
                    desc = String.Format(format, "FQQ", stat.SkillTab, stat.Param);
                    break;
                case 4:
                    desc = "+" + String.Format(format, "FQQ");
                    break;
                case 5:
                case 10:
                    desc = "+" + String.Format(format, "FQQ2");// stat.Value * 100 / 128);
                    break;
                case 17:
                case 18:
                    desc = String.Format(format, "FQQ");
                    int h = DateTime.Now.Hour;
                    if (h > 5 && h < 18) desc += Utils.AllJsons["ModStre9e"];
                    else if (h == 5) desc += Utils.AllJsons["ModStre9f"];
                    else if (h == 18) desc += Utils.AllJsons["ModStre9g"];
                    else desc += Utils.AllJsons["ModStre9d"];

                    desc = desc.Replace("%s", "");

                    break;
                case 12:
                    if (stat.Value > 1) desc = "+";
                    desc += stat.Value.ToString() + String.Format(format, "FQQ");
                    break;
                default:
                    desc = "暂时还没处理:" + func;
                    break;
            }
            return desc;
        }

        public static string GetDescription(ItemStatCostFunc cost, int level)
        {
            double v = cost.NewValue;

            var format = cost.Format;

            if (cost.Format.IndexOf("FQQ2") > -1)
            {
                format = cost.Format.Replace("FQQ2", "{0}");
                v = v * 100.0d / 128.00;
            }
            else if (cost.Format.IndexOf("FQQ") > -1)
            {
                format = cost.Format.Replace("FQQ", "{0}");
            }

            if (cost.IsPerLevel) v = v * level;

            var desc = String.Format(format, Math.Floor(v));

            return desc;
        }

        public static Tuple<int, int, int, int> GetCharacterResistances()
        {
            int fire = 0, light = 0, cold = 0, poison = 0;


            List<ItemStat> statlist = new List<ItemStat>();
            var equipped = Helper.CurrentCharactor.PlayerItemList.Items.Where(i => i.Mode == ItemMode.Equipped);
            foreach (var item in equipped)
            {

                foreach (var group in item.StatLists)
                {
                    foreach (var stat in group.Stats)
                    {
                        statlist.Add(stat);
                    }
                }
                foreach (var sitem in item.SocketedItems)
                {
                    var allcost = Helper.GetGemStatCostFunc(sitem);
                    if (allcost.Count > 0)
                    {
                        if (item.IsWeapon)
                        {
                            foreach (var cost in allcost["weapon"])
                            {
                                statlist.Add(new ItemStat() { Id = (ushort)cost.ID, Value = (ushort)cost.NewValue });
                            }
                        }
                        else if (item.Location == ItemLocation.LeftHand || item.Location == ItemLocation.SwapLeft)
                        {
                            foreach (var cost in allcost["shield"])
                            {
                                statlist.Add(new ItemStat() { Id = (ushort)cost.ID, Value = (ushort)cost.NewValue });
                            }
                        }
                        else
                        {
                            foreach (var cost in allcost["helm"])
                            {
                                statlist.Add(new ItemStat() { Id = (ushort)cost.ID, Value = (ushort)cost.NewValue });
                            }
                        }
                    }
                }
            }

            fire = statlist.Where(s => s.Id == 39).Sum(s2 => s2.Value);
            light = statlist.Where(s => s.Id == 41).Sum(s2 => s2.Value);
            cold = statlist.Where(s => s.Id == 43).Sum(s2 => s2.Value);
            poison = statlist.Where(s => s.Id == 45).Sum(s2 => s2.Value);

            return Tuple.Create(fire, light, cold, poison);
        }
        public static string GetEnhancedDescription(int level, Item item)
        {
            List<string> list = new List<string>();

            int groupNo = 0;
            int groupCount = item.StatLists.Count;
            string ret = "";

            if (item.MaxDurability == 0)
            {
                ret += Utils.AllJsons["ModStre9s"] + "\r\n";
            }

            List<ItemStatCostFunc> costList = new List<ItemStatCostFunc>();

            if (item.TypeCode == "rune")
            {
                var gemlist = Helper.GetGemStatCostFunc(item);
                ret += Utils.AllJsons["StrGemX3"] + ":";
                foreach (var cost in gemlist["weapon"])
                {
                    ret += cost.Format.Replace("FQQ", Math.Floor(cost.NewValue).ToString());
                    ret += "\r\n";
                }

                ret += Utils.AllJsons["StrGemX4"] + ":";
                foreach (var cost in gemlist["helm"])
                {
                    ret += cost.Format.Replace("FQQ", Math.Floor(cost.NewValue).ToString());
                    ret += "\r\n";
                }

                ret += Utils.AllJsons["StrGemX1"] + ":";
                foreach (var cost in gemlist["helm"])
                {
                    ret += cost.Format.Replace("FQQ", Math.Floor(cost.NewValue).ToString());
                    ret += "\r\n";
                }

                ret += Utils.AllJsons["StrGemX2"] + ":";
                foreach (var cost in gemlist["shield"])
                {
                    ret += cost.Format.Replace("FQQ", Math.Floor(cost.NewValue).ToString());
                    ret += "\r\n";
                }

                return ret;
            }

            foreach (var sitem in item.SocketedItems)
            {
                var allcost = Helper.GetGemStatCostFunc(sitem);
                if (allcost.Count > 0)
                {
                    if (item.IsWeapon)
                    {
                        foreach (var cost in allcost["weapon"])
                        {
                            costList.Add(cost);
                        }
                    }
                    else if (item.Location == ItemLocation.LeftHand || item.Location == ItemLocation.SwapLeft)
                    {
                        foreach (var cost in allcost["shield"])
                        {
                            costList.Add(cost);
                        }
                    }
                    else
                    {
                        foreach (var cost in allcost["helm"])
                        {
                            costList.Add(cost);
                        }
                    }
                }
            }

            foreach (var group in item.StatLists)
            {
                if (group.Stats.Count == 0) continue;

                groupNo++;

                Dictionary<string[], List<int>> fuckedStat = new Dictionary<string[], List<int>>();
                fuckedStat[new string[] { "strModMinDamageRange", "strModEnhancedDamage" }] = new List<int> { 18, 17 };
                fuckedStat[new string[] { "strModFireDamageRange", "strModFireDamage" }] = new List<int> { 48, 49 };
                fuckedStat[new string[] { "strModLightningDamageRange", "strModLightningDamage" }] = new List<int> { 50, 51 };
                fuckedStat[new string[] { "strModMagicDamageRange", "strModMagicDamage" }] = new List<int> { 52, 53 };
                fuckedStat[new string[] { "strModColdDamageRange", "strModColdDamage" }] = new List<int> { 54, 55 };
                fuckedStat[new string[] { "strModPoisonDamageRange", "strModPoisonDamage" }] = new List<int> { 57, 58 };


                foreach (var stat in group.Stats)
                {
                    costList.Add(Helper.GetItemStatCostFunc(stat));
                }

                var idlist = costList.Select(c => c.ID).ToList();
                foreach (var key in fuckedStat.Keys)
                {
                    bool found = idlist.Contains(fuckedStat[key][0]) && idlist.Contains(fuckedStat[key][1]);
                    //remove the 2 stats
                    //insert 1 stat : same or not same
                    if (found)
                    {
                        var stat0 = costList.Where(c => c.ID == fuckedStat[key][0]).First();
                        var stat1 = costList.Where(c => c.ID == fuckedStat[key][1]).First();

                        costList.Remove(costList.Where(c => c.ID == fuckedStat[key][0]).First());
                        costList.Remove(costList.Where(c => c.ID == fuckedStat[key][1]).First());

                        ItemStatCostFunc iscf = stat0;

                        if (stat0.NewValue == stat1.NewValue)
                        {
                            var format = FormatCStyleStrings(Utils.AllJsons[key[1]].Replace("%%", "％"));
                            ///TODO 持续多少秒，这里不大好弄，先这么处理吧。
                            if (iscf.ID == 57 || iscf.ID == 54) format = format.Replace("{1}", "*");
                            iscf.Format = String.Format(format, stat0.NewValue);
                        }
                        else
                        {
                            var format = FormatCStyleStrings(Utils.AllJsons[key[0]].Replace("%%", "％"));
                            if (iscf.ID == 57 || iscf.ID == 54) format = format.Replace("{2}", "*");
                            iscf.Format = String.Format(format, stat0.NewValue, stat1.NewValue);
                        }

                        costList.Add(iscf);
                    }
                }

            }

            var mergedCost = MergeCost(costList);
            var newlist = mergedCost.OrderByDescending(cost => cost.Priority).ThenByDescending(cost2 => cost2.ID);
            foreach (var cost in newlist)
            {
                var desc = GetDescription(cost, level);
                if (String.IsNullOrEmpty(desc)) continue;


                list.Add(desc);
            }

            if (item.Quality == ItemQuality.Set)
            {

            }

            //var groupedList = list.GroupBy(desc => desc.Replace("上限","").Replace("底限",""));


            foreach (var desc in list)
            {
                ret += desc + "\r\n";
            }

            if (item.IsEthereal && item.TotalNumberOfSockets > 0)
            {
                ret += String.Format(Utils.AllJsons["strItemModEtherealSocketed"].Replace("%i", item.TotalNumberOfSockets.ToString()) + "\r\n");

            }
            else if (item.IsEthereal)
            {
                ret += String.Format(Utils.AllJsons["strethereal"] + "\r\n");
            }
            else if (item.TotalNumberOfSockets > 0)
            {
                ret += String.Format(Utils.AllJsons["Socketable"].Replace("%i", item.TotalNumberOfSockets.ToString()) + "\r\n");
            }

            return ret;
        }

        private static List<ItemStatCostFunc> MergeCost(List<ItemStatCostFunc> list)
        {
            List<ItemStatCostFunc> costList = new List<ItemStatCostFunc>();
            var ginfo = list.Where(cd=>!String.IsNullOrEmpty(cd.DgrpFormat)).GroupBy(c => c.ID);
            foreach (var group in ginfo)
            {
                double newvalue = group.Sum(g => g.NewValue);
                var f = group.ToList()[0];
                f.NewValue = newvalue;
                costList.Add(f);
            }

            var list2 = list.Where(cd => String.IsNullOrEmpty(cd.DgrpFormat));
            foreach(var stat in list2)
            {
                costList.Add(stat);
            }            

            var c39 = costList.Where(c => c.ID == 39).FirstOrDefault();
            var c41 = costList.Where(c => c.ID == 41).FirstOrDefault();
            var c43 = costList.Where(c => c.ID == 43).FirstOrDefault();
            var c45 = costList.Where(c => c.ID == 45).FirstOrDefault();

            if (c39 != null && c41 != null && c43 != null && c45 != null && c39.NewValue == c41.NewValue && c41.NewValue == c43.NewValue && c43.NewValue == c45.NewValue)
            {
                var newcost = c39;
                newcost.Format = newcost.DgrpFormat;
                costList.Add(newcost);

                costList.Remove(costList.Where(c => c.ID == 39).First());
                costList.Remove(costList.Where(c => c.ID == 41).First());
                costList.Remove(costList.Where(c => c.ID == 43).First());
                costList.Remove(costList.Where(c => c.ID == 45).First());
            }

            var c0 = costList.Where(c => c.ID == 0).FirstOrDefault();
            var c1 = costList.Where(c => c.ID == 1).FirstOrDefault();
            var c2 = costList.Where(c => c.ID == 2).FirstOrDefault();
            var c3 = costList.Where(c => c.ID == 3).FirstOrDefault();

            if (c0 != null && c1 != null && c2 != null && c3 != null && c0.NewValue == c1.NewValue && c1.NewValue == c2.NewValue && c2.NewValue == c3.NewValue)
            {
                var newcost = c0;
                newcost.Format = newcost.DgrpFormat;
                costList.Add(newcost);

                costList.Remove(costList.Where(c => c.ID == 0).First());
                costList.Remove(costList.Where(c => c.ID == 1).First());
                costList.Remove(costList.Where(c => c.ID == 2).First());
                costList.Remove(costList.Where(c => c.ID == 3).First());
            }

            return costList;
        }

        public static string GetSetDescription(int level, Item item)
        {
            if (item.Quality != ItemQuality.Set) return "";

            List<string> list = new List<string>();
            var row = ExcelTxt.SetItemsTxt[(int)item.FileIndex];
            int fullset = 0;

            for (int i = 1; i <= 5; i++)
            {
                var prop = row[String.Format("aprop{0}a", i)].Value;
                if (String.IsNullOrEmpty(prop)) continue;

                ++fullset;
            }
            for (int i = 1; i <= 5; i++)
            {
                var prop = row[String.Format("aprop{0}a", i)].Value;
                var par = row[String.Format("apar{0}a", i)].Value;
                var min = row[String.Format("amin{0}a", i)].ToInt32();
                var max = row[String.Format("amax{0}a", i)].ToInt32();

                if (String.IsNullOrEmpty(prop)) continue;

                var statlist = GetStatListFromProperty(prop, par, min, max);

                foreach (var stat in statlist)
                {
                    var cost = Helper.GetItemStatCostFunc(stat);
                    var desc = GetDescription(cost, level);
                    if (String.IsNullOrEmpty(desc)) continue;

                    //if (cost.IsPerLevel)
                    //{
                    //    desc += "(" + Utils.AllJsons["increaseswithplaylevelX"] + ")";
                    //}
                    desc += (i == fullset) ? "(完全装备)" : String.Format("({0} 装备)", i + 1);
                    list.Add(desc);
                }

                prop = row[String.Format("aprop{0}b", i)].Value;
                par = row[String.Format("apar{0}b", i)].Value;
                min = row[String.Format("amin{0}b", i)].ToInt32();
                max = row[String.Format("amax{0}b", i)].ToInt32();

                if (String.IsNullOrEmpty(prop)) continue;
                statlist = GetStatListFromProperty(prop, par, min, max);
                foreach (var stat in statlist)
                {
                    var cost = Helper.GetItemStatCostFunc(stat);
                    var desc = GetDescription(cost, level);
                    if (String.IsNullOrEmpty(desc)) continue;

                    //if (cost.IsPerLevel)
                    //{
                    //    desc += "(" + Utils.AllJsons["item_perlevel"] + ")";
                    //}
                    desc += (i == fullset) ? "(完全装备)" : String.Format("({0} 装备)", i + 1);
                    list.Add(desc);
                }
            }

            var ret = String.Empty;
            foreach (var desc in list)
            {
                ret += desc + "\r\n";
            }

            return ret;
        }
        private static Dictionary<string, List<ItemStatCostFunc>> GetGemStatCostFunc(Item item)
        {
            Dictionary<string, List<ItemStatCostFunc>> ret = new Dictionary<string, List<ItemStatCostFunc>>();

            var gem = ExcelTxt.GemsTxt[item.Code];
            if (gem == null) return ret;
            var prefixes = new string[] { "weapon", "helm", "shield" };

            for (int p = 1; p < 4; p++)
            {
                List<ItemStatCostFunc> costList = new List<ItemStatCostFunc>();

                for (int i = 1; i < 4; i++)
                {
                    var key = String.Format("{0}Mod{1}Code", prefixes[p - 1], i);
                    var prop = ExcelTxt.PropertiesTxt[gem[key].Value];

                    if (prop == null) continue;

                    var sparam = String.Format("{0}Mod{1}Param", prefixes[p - 1], i);
                    var smin = String.Format("{0}Mod{1}Min", prefixes[p - 1], i);
                    var smax = String.Format("{0}Mod{1}Max", prefixes[p - 1], i);

                    for (int j = 1; j < 8; j++)
                    {
                        var func = prop["func" + j.ToString()].Value;
                        var stat = prop["stat" + j.ToString()].Value;

                        if (String.IsNullOrEmpty(func) || String.IsNullOrEmpty(stat)) continue;

                        var itemRow = ExcelTxt.ItemStatCostTxt[stat];
                        ItemStat itemStat = new ItemStat();
                        itemStat.Id = Convert.ToUInt16(itemRow["*ID"].Value);


                        //System.Diagnostics.Debug.WriteLine("func = " + func);
                        var sec = 0;
                        if (stat.IndexOf("length") > 0) sec = Convert.ToInt32(gem[sparam].Value) / 25;//25frame每秒

                        //itemStat.Param = sec;
                        if (func == "15") itemStat.Value = Convert.ToInt32(gem[smin].Value);
                        if (func == "1" || func == "2" || func == "3" || func == "4" || func == "6" || func == "8" || func == "12" || func == "13" || func == "16" || func == "20") itemStat.Value = Convert.ToInt32(gem[smax].Value);
                        if (func == "17") itemStat.Value = Convert.ToInt32(gem[sparam].Value);

                        if (func == "7") itemStat.Value = Convert.ToInt32(gem[smax].Value) / 100;

                        var set = prop["set" + j.ToString()].Value;
                        bool overrideStatValue = (set == "1");   //true:覆盖；false:增加
                        var val = prop["val" + j.ToString()].Value;
                        int vval = String.IsNullOrEmpty(val) ? 0 : Convert.ToInt32(val);

                        var pcost = Helper.GetItemStatCostFunc(itemStat);

                        costList.Add(pcost);
                        //System.Diagnostics.Debug.WriteLine(pcost.Format + " = " + pcost.NewValue);
                    }
                }

                ret[prefixes[p - 1]] = costList;
            }

            return ret;
        }

        private static int GetLevelReq(Item item, string val)
        {
            int level = 0, level2 = 0, level3 = 0;

            if (!String.IsNullOrEmpty(val) && Convert.ToInt32(val) > 0) level = Convert.ToInt32(val);

            if (item.Quality == ItemQuality.Set)
            {
                level2 = ExcelTxt.SetItemsTxt[(int)item.FileIndex]["lvl req"].ToInt32();
            }
            if (item.Quality == ItemQuality.Unique)
            {
                level2 = ExcelTxt.UniqueItemsTxt[(int)item.FileIndex]["lvl req"].ToInt32();
            }

            if (item.IsRuneword)
            {
                int runeid = (int)item.RunewordId - 26;
                //if (item.RunewordId >= 106) runeid = (int)item.RunewordId - 26; else runeid = (int)item.RunewordId - 27;
                var row = ExcelTxt.RunesTxt["Runeword" + Convert.ToString(runeid)];
                if (row != null)
                {
                    for (int i = 15; i <= 20; i++)
                    {
                        var rune = row.Data[i].Value;
                        if (String.IsNullOrEmpty(rune)) continue;

                        level3 = Math.Max(level3, Convert.ToInt32(ExcelTxt.MiscTxt[rune].Data[5].Value));
                    }
                }
                else
                {
                    level3 = 999;
                }
            }

            int levelreq = Math.Max(Math.Max(level, level2), level3);

            return levelreq;
        }

        public static int ToInt32(object o)
        {
            int ret = 0;
            Int32.TryParse(Convert.ToString(o), out ret);

            return ret;
        }
        public static string GetBasicDescription(int level, Item item)
        {
            string ret = "";

            double maxPercent = 0, maxadd = 0, minPercent = 0, minadd = 0, armorPercent = 0, armorAdd = 0;

            foreach (var group in item.StatLists)
            {
                foreach (var stat in group.Stats)
                {
                    var cost = Helper.GetItemStatCostFunc(stat);
                    var row = ItemStat.GetStatRow(stat);
                    if (cost.IsArmorClass)
                    {
                        if (row["*ID"].Value == "16")
                        {
                            armorPercent += cost.NewValue / 100;//item_armor_percent
                        }
                        else
                        {
                            armorAdd += cost.NewValue;
                        }
                    }
                    if (cost.IsMaxDamage)
                    {
                        if (row["*ID"].Value == "17")
                        {
                            maxPercent += cost.NewValue / 100;//item_armor_percent
                        }
                        else
                        {
                            maxadd += cost.NewValue;
                        }
                    }

                    if (cost.IsMinDamage)
                    {
                        if (row["*ID"].Value == "18")
                        {
                            minPercent += cost.NewValue / 100;//item_armor_percent
                        }
                        else
                        {
                            minadd += cost.NewValue;
                        }
                    }
                }
            }

            if (item.IsWeapon)
            {
                var row = ExcelTxt.WeaponsTxt[item.Code];


                if (row["2handed"].Value == "1")//向下取整
                {
                    var dam = Utils.AllJsons["ItemStats1m"];
                    int last = dam.LastIndexOf("%d");
                    dam = dam.Substring(0, last) + "FQQ" + dam.Substring(last + 2);

                    ret = dam.Replace("%d", Convert.ToString(Math.Floor(Convert.ToInt32(row["2handmindam"].Value) * (1 + minPercent)) + minadd)).Replace("FQQ", Convert.ToString(Math.Floor(Convert.ToInt32(row["2handmaxdam"].Value) * (1 + maxPercent)) + maxadd));
                    ret += "\r\n";

                    //Convert.ToInt32(row["2handmindam"].Value), Convert.ToInt32(row["2handmaxdam"].Value)
                    //);
                }
                else
                {
                    var dam = Utils.AllJsons["ItemStats1l"];
                    ret = dam.Replace("%d", Convert.ToString(Math.Floor(Convert.ToInt32(row["mindam"].Value) * (1 + minPercent)) + minadd)).Replace("FQQ", Convert.ToString(Math.Floor(Convert.ToInt32(row["maxdam"].Value) * (1 + maxPercent)) + maxadd));
                    ret += "\r\n";
                    //ret = String.Format("单手伤害: {0:N0} - {1:N0}点 ({2} - {3})\r\n",
                    //    Math.Floor(Convert.ToInt32(row["mindam"].Value) * (1 + minPercent)) + minadd, Math.Floor(Convert.ToInt32(row["maxdam"].Value) * (1 + maxPercent)) + maxadd,
                    //    Convert.ToInt32(row["mindam"].Value), Convert.ToInt32(row["maxdam"].Value)
                    //    );
                }

                if (item.MaxDurability > 0) ret += String.Format("{2}: {0}/{1}\r\n", item.Durability, item.MaxDurability, Utils.AllJsons["StrHDHelpDurabilityStatus"]);
                if (!String.IsNullOrEmpty(row["reqdex"].Value) && Convert.ToInt32(row["reqdex"].Value) > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1f"]) + "\r\n", row["reqdex"].Value);
                if (!String.IsNullOrEmpty(row["reqstr"].Value) && Convert.ToInt32(row["reqstr"].Value) > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1e"]) + "\r\n", row["reqstr"].Value);

                int levelreq = GetLevelReq(item, row["levelreq"].Value);
                if (levelreq > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1p"]) + "\r\n", levelreq);
            }
            else if (item.IsArmor)
            {
                var row = ExcelTxt.ArmorTxt[item.Code];

                ret += String.Format("{1}: {0}\r\n", Math.Floor(item.Armor * (1 + armorPercent)) + armorAdd, Utils.AllJsons["strchrdef"]);
                if (item.MaxDurability > 0) ret += String.Format("{2}: {0}/{1}\r\n", item.Durability, item.MaxDurability, Utils.AllJsons["StrHDHelpDurabilityStatus"]);
                if (row[9].ToInt32() > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1f"]) + "\r\n", row[9].Value);
                if (row[8].ToInt32() > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1e"]) + "\r\n", row[8].Value);
                if (row[10].ToInt32() > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1r"]).Replace("%%", "") + "\r\n", row[10].Value);

                int levelreq = GetLevelReq(item, row[15].Value);
                if (levelreq > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1p"]) + "\r\n", levelreq);
            }
            else
            {
                var row = ExcelTxt.MiscTxt[item.Code];

                if (item.Armor > 0) ret += String.Format("{1}: {0}\r\n", Math.Floor(item.Armor * (1 + armorPercent)) + armorAdd, Utils.AllJsons["strchrdef"]);
                if (item.MaxDurability > 0) if (item.MaxDurability > 0) ret += String.Format("{2}: {0}/{1}\r\n", item.Durability, item.MaxDurability, Utils.AllJsons["StrHDHelpDurabilityStatus"]);
                if (row[7].ToInt32() > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1f"]) + "\r\n", row[7].Value);
                if (row[6].ToInt32() > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1e"]) + "\r\n", row[6].Value);

                int levelreq = GetLevelReq(item, row[5].Value);
                if (levelreq > 0) ret += String.Format(Helper.FormatCStyleStrings(Utils.AllJsons["ItemStats1p"]) + "\r\n", levelreq);
            }

            return ret;

        }



        private static int GetSkillId(string t1param)
        {
            int skillid = 0;

            var s = ExcelTxt.SkillsTxt.Rows.Where(s2 => s2["skill"].Value.ToLower() == t1param.ToLower()).FirstOrDefault();
            if (s != null)
            {
                skillid = Convert.ToInt32(s["*Id"].Value);
            }
            else
            {
                skillid = Convert.ToInt32(t1param);
            }

            return skillid;
        }
        private static Item CreateItem(string code)
        {
            Item item = new Item();
            item.Code = code;
            item.Page = 1;
            item.Mode = ItemMode.Stored;
            item.Flags = new System.Collections.BitArray(32);
            item.Row = 0;
            item.Column = 0;

            item.IsIdentified = true;
            item.IsMisc = false;
            item.IsRuneword = false;

            //item.Durability = 1;
            //item.MaxDurability = 0;

            item.IsArmor = (ExcelTxt.ArmorTxt[item.Code] != null);
            item.IsWeapon = (ExcelTxt.WeaponsTxt[item.Code] != null);
            item.IsMisc = (ExcelTxt.MiscTxt[item.Code] != null);

            item.ItemLevel = 99;

            item.StatLists.Add(new ItemStatList());
            item.StatLists.Add(new ItemStatList());
            item.Version = "101";

            Item.UpdateRowsAndColumnsInformation(item);

            return item;
        }

        public static List<Item> GetRunewords()
        {
            List<Item> runewords = new List<Item>();

            foreach (var row in ExcelTxt.RunesTxt.Rows)
            {
                var output = Utils.AllJsons[row["Name"].Value];
                //System.Diagnostics.Debug.WriteLine("Processing " + output);

                for (int i = 1; i <= 6; i++)
                {

                    var type = row["itype" + i.ToString()].Value;
                    if (String.IsNullOrEmpty(type)) continue;

                    List<string> runes = new List<string>();
                    for (int j = 1; j <= 6; j++)
                    {
                        var rune = row["Rune" + j.ToString()].Value;
                        if (String.IsNullOrEmpty(rune)) break;

                        runes.Add(rune);
                    }

                    TxtRow basicItemRow = null;

                    var itemtype = ExcelTxt.ItemTypesTxt[row["itype" + i.ToString()].Value];
                    var typecode = itemtype["Code"].Value;
                    var storepage = itemtype["StorePage"].Value;

                    if (typecode == "shld") { storepage = "armo"; typecode = "shie"; }
                    if (typecode == "pala") { storepage = "armo"; typecode = "ashd"; }

                    if (typecode == "weap") { storepage = "weap"; typecode = ""; }
                    if (typecode == "miss") { storepage = "weap"; typecode = "bow"; }
                    if (typecode == "mele") { storepage = "weap"; typecode = "swor"; }

                    switch (storepage)
                    {
                        case "weap":
                            if (typecode == "") basicItemRow = ExcelTxt.WeaponsTxt.Rows.Where(w => w["gemsockets"].ToInt32() >= runes.Count).FirstOrDefault();
                            else basicItemRow = ExcelTxt.WeaponsTxt.Rows.Where(w => w["type"].Value == typecode && w["gemsockets"].ToInt32() >= runes.Count).FirstOrDefault();
                            break;
                        case "armo":
                            basicItemRow = ExcelTxt.ArmorTxt.Rows.Where(w => w["type"].Value == typecode && w["gemsockets"].ToInt32() >= runes.Count).First();
                            break;
                        case "misc":
                            basicItemRow = ExcelTxt.MiscTxt.Rows.Where(w => w["type"].Value == typecode).First();
                            break;
                        case "":
                            System.Diagnostics.Debug.WriteLine(row["Name"].Value + " : " + type + ", " + typecode);
                            break;
                        default:
                            break;
                    }



                    if (basicItemRow == null)
                    {
                        System.Diagnostics.Debug.WriteLine(output);
                        continue;
                    }

                    string dep = runes.Count.ToString() + " " + Utils.AllJsons["sockets"];
                    dep += " ";
                    dep += Utils.AllJsons[itemtype["ItemType"].Value];
                    dep += "/";
                    output += "===> 底材: " + Utils.AllJsons[itemtype["ItemType"].Value] + ", " + basicItemRow["gemsockets"].Value + " 凹槽";//basicItemRow["code"].Value

                    foreach (var rune in runes)
                    {
                        output += Utils.AllJsons[rune];
                        output += " ";
                    }
                    //System.Diagnostics.Debug.WriteLine(output);

                    //优先精英底材
                    var code = basicItemRow["ultracode"].Value;
                    if (String.IsNullOrEmpty(code))
                    {
                        //次之扩展底材
                        code = basicItemRow["ubercode"].Value;
                        if (String.IsNullOrEmpty(code))
                        {
                            //最后无奈之下，普通底材
                            code = basicItemRow["normcode"].Value;
                        }
                    }
                    var item = CreateItem(code);
                    item.Id = (uint)(DateTime.Now.Ticks);
                    item.IsRuneword = true;
                    item.RunewordId = (uint)(Convert.ToInt32(row["Name"].Value.Replace("Runeword", "")) + 26);
                    if (item.RunewordId >= 106) item.RunewordId = item.RunewordId - 1;
                    item.Quality = ItemQuality.Superior;
                    item.TotalNumberOfSockets = (byte)runes.Count;
                    item.NumberOfSocketedItems = (byte)runes.Count;
                    item.IsSocketed = true;
                    Helper.SetDurability(item);

                    int column = 0;
                    foreach (var rcode in runes)
                    {
                        var rune = CreateItem(rcode);
                        rune.Id = 0;
                        rune.ItemLevel = 0;
                        rune.Quality = 0;
                        rune.IsCompact = true;
                        rune.Page = 0;
                        rune.StatLists.Clear();
                        rune.Row = 0;
                        rune.Column = (byte)(column++);
                        rune.Mode = ItemMode.Socket;

                        item.SocketedItems.Add(rune);
                    }

                    List<ItemStat> list = new List<ItemStat>();
                    for (int k = 1; k <= 7; k++)
                    {
                        //T1Code1	T1Param1	T1Min1	T1Max1
                        var t1code = row["T1Code" + k.ToString()].Value;
                        var t1param = row["T1Param" + k.ToString()].Value;
                        var t1min = row["T1Min" + k.ToString()].ToInt32();
                        var t1max = row["T1Max" + k.ToString()].ToInt32();

                        if (String.IsNullOrEmpty(t1code)) break;

                        var statlist = GetStatListFromProperty(t1code, t1param, t1min, t1max);
                        foreach (var stat in statlist)
                        {
                            list.Add(stat);
                        }
                    }

                    if (dep.EndsWith("/")) dep = dep.Substring(0, dep.Length - 1);
                    item.RunewordsDependency = dep;

                    SplitStatLists(item, list);
                    runewords.Add(item);
                }
                //System.Diagnostics.Debug.WriteLine("Done ");
            }

            return runewords;
        }

        public static void SetDurability(Item item)
        {
            if (item.IsWeapon)
            {
                if (ExcelTxt.WeaponsTxt[item.Code]["nodurability"].Value != "1")
                {
                    item.Durability = item.MaxDurability = ExcelTxt.WeaponsTxt[item.Code]["durability"].ToUInt16();
                }
            }
            if (item.IsArmor)
            {
                if (ExcelTxt.ArmorTxt[item.Code]["nodurability"].Value != "1")
                {
                    item.Durability = item.MaxDurability = ExcelTxt.ArmorTxt[item.Code]["durability"].ToUInt16();
                }
            }
            if (item.IsMisc)
            {
                if (ExcelTxt.MiscTxt[item.Code]["nodurability"].Value != "1")
                {
                    item.Durability = item.MaxDurability = ExcelTxt.MiscTxt[item.Code]["durability"].ToUInt16();
                }
            }
        }

        public static List<ItemStat> GetStatListFromProperty(string prop, string param, int min, int max)
        {
            int? skillid = null;
            int? skilllevel = null;
            int? maxcharges = null;
            int? parameter = null;
            int? skilltab = null;
            int value = max;

            List<ItemStat> ret = new List<ItemStat>();
            if (String.IsNullOrEmpty(prop)) return ret;

            for (int m = 1; m <= 7; m++)
            {
                var f = ExcelTxt.PropertiesTxt[prop]["func" + m.ToString()].Value;
                if (String.IsNullOrEmpty(f)) break;
                var func = Convert.ToInt32(f);

                var stat = ExcelTxt.PropertiesTxt[prop]["stat" + m.ToString()].Value;

                switch (func)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 8:
                    case 13:
                    case 16:
                        value = max;
                        break;
                    case 7:
                        value = max / 100;
                        break;
                    case 9:
                        skillid = GetSkillId(param);
                        skilllevel = max;
                        break;
                    case 18:
                    case 20:
                        break;
                    case 10:
                        //"[Class Skill Tab ID] = (Amazon = 0-2, Sorceress = 3-5, Necromancer = 6-8, Paladin = 9-11, Barbarian = 12-14, Druid = 15-17,  Assassin = 18-20)"
                        skilllevel = Convert.ToInt32(param) / 3;//class id
                        skilltab = Convert.ToInt32(param)%3;// details skill id
                        value = max;//added skill value
                        break;
                    case 11:
                        skillid = GetSkillId(param);
                        skilllevel = max;
                        value = min;
                        if (min == 0) value = 5;
                        break;
                    case 12:
                    case 14:
                    case 17:
                    case 23:
                    case 24:
                    case 36:
                        parameter = max;
                        break;
                    case 21:
                        value = max;
                        parameter = ExcelTxt.PropertiesTxt[prop]["val" + m.ToString()].ToInt32();
                        break;
                    case 19:
                        skillid = GetSkillId(param);

                        maxcharges = min;
                        skilllevel = max;
                        value = min;
                        break;
                    case 22:
                        value = max;
                        parameter = GetSkillId(param);
                        break;
                    default:
                        break;
                }


                if (prop == "dmg%")
                {
                    ret.Add(new ItemStat() { Id = 17, Param = parameter, SkillId = skillid,SkillTab=skilltab, SkillLevel = skilllevel, MaxCharges = maxcharges, Stat = "item_maxdamage_percent", Value = max });
                    ret.Add(new ItemStat() { Id = 18, Param = parameter, SkillId = skillid, SkillTab = skilltab, SkillLevel = skilllevel, MaxCharges = maxcharges, Stat = "item_mindamage_percent", Value = max });
                }
                else
                {
                    var cost = ExcelTxt.ItemStatCostTxt[stat];
                    if (cost != null)
                    {
                        ret.Add(new ItemStat() { Id = (UInt16)(cost["*ID"].ToInt32()), Param = parameter, SkillId = skillid, SkillTab = skilltab, SkillLevel = skilllevel, MaxCharges = maxcharges, Stat = cost["Stat"].Value, Value = value });
                    }
                }
            }

            return ret;
        }

        public static List<Item> GetWeapons()
        {
            List<Item> weapons = new List<Item>();
            foreach (var row in ExcelTxt.WeaponsTxt.Rows)
            {
                if (row["code"].Value == "") continue;

                var item = CreateItem(row["code"].Value);
                item.Id = (uint)(DateTime.Now.Ticks);
                item.Quality = ItemQuality.Superior;
                Helper.SetDurability(item);

                if (String.IsNullOrEmpty(item.Icon)) continue;

                weapons.Add(item);
            }

            return weapons;
        }

        public static List<Item> GetArmors()
        {
            List<Item> armors = new List<Item>();
            foreach (var row in ExcelTxt.ArmorTxt.Rows)
            {
                if (row["code"].Value == "") continue;

                var item = CreateItem(row["code"].Value);
                item.Id = (uint)(DateTime.Now.Ticks);
                item.Quality = ItemQuality.Normal;
                item.Armor = row["maxac"].ToUInt16();
                Helper.SetDurability(item);

                if (String.IsNullOrEmpty(item.Icon)) continue;

                armors.Add(item);
            }

            return armors;
        }

        public static List<Item> GetMisces()
        {
            List<Item> misces = new List<Item>();
            foreach (var row in ExcelTxt.MiscTxt.Rows)
            {
                if (row["code"].Value == "") continue;

                var item = CreateItem(row["code"].Value);
                item.Id = (uint)(DateTime.Now.Ticks);
                item.Quality = ItemQuality.Normal;
                Helper.SetDurability(item);

                if (String.IsNullOrEmpty(item.Icon)) continue;

                misces.Add(item);
            }

            return misces;
        }

        public static List<Item> GetUniques()
        {
            List<Item> uniques = new List<Item>();
            foreach (var row in ExcelTxt.UniqueItemsTxt.Rows)
            {
                if (row["code"].Value == "") continue;

                var item = CreateItem(row["code"].Value);
                item.Id = (uint)(DateTime.Now.Ticks);
                item.Quality = ItemQuality.Unique;
                item.FileIndex = (uint)(row["*ID"].ToInt32());
                Helper.SetDurability(item);


                if (String.IsNullOrEmpty(item.Icon))
                {
                    System.Diagnostics.Debug.WriteLine("没有图标：" + item.Name);
                    continue;
                }
                    

                List<ItemStat> list = new List<ItemStat>();
                for (int k = 1; k <= 12; k++)
                {
                    //T1Code1	T1Param1	T1Min1	T1Max1
                    var t1code = row["prop" + k.ToString()].Value;
                    var t1param = row["par" + k.ToString()].Value;
                    var t1min = row["min" + k.ToString()].ToInt32();
                    var t1max = row["max" + k.ToString()].ToInt32();

                    if (String.IsNullOrEmpty(t1code)) break;
                    if (t1code.StartsWith("*")) continue;

                    //System.Diagnostics.Debug.WriteLine(String.Format("{0},{1}", item.FileIndex, item.Name));
                    var statlist = GetStatListFromProperty(t1code, t1param, t1min, t1max);
                    foreach (var stat in statlist)
                    {
                        list.Add(stat);
                    }
                }

                SplitStatLists(item, list);

                uniques.Add(item);
            }

            return uniques;
        }

        private static void SplitStatLists(Item item, List<ItemStat> list)
        {
            item.StatLists.Clear();
            item.StatLists.Add(new ItemStatList());
            item.StatLists.Add(new ItemStatList());

            var list2 = list.OrderBy(s => s.Id).ThenBy(s2 => s2.Param).ToList();
            for (int i = 0; i < list2.Count; i++)
            {
                item.StatLists[0].Stats.Add(list2[i]);
                //如果遇到54和57，并且coldlength被拆开到了下一gruop，那么item的read或者write哪里就会出错
                //int group = i / 5;
                //if (item.StatLists.Count <= group) item.StatLists.Add(new ItemStatList());
                //item.StatLists[group].Stats.Add(list2[i]);
            }
        }
        public static List<Item> GetSets()
        {
            List<Item> sets = new List<Item>();

            foreach (var row in ExcelTxt.SetItemsTxt.Rows)
            {
                if (row["item"].Value == "") continue;

                var item = CreateItem(row["item"].Value);
                item.Id = (uint)(DateTime.Now.Ticks);
                item.Quality = ItemQuality.Set;
                item.FileIndex = (uint)(row["*ID"].ToInt32());
                Helper.SetDurability(item);


                if (String.IsNullOrEmpty(item.Icon))
                {
                    System.Diagnostics.Debug.WriteLine("没有图标：" + item.Name);
                    continue;
                }


                sets.Add(item);

                List<ItemStat> list = new List<ItemStat>();
                for (int i = 1; i <= 9; i++)
                {
                    var prop = row["prop" + i.ToString()].Value;
                    var par = row["par" + i.ToString()].Value;
                    var min = row["min" + i.ToString()].ToInt32();
                    var max = row["max" + i.ToString()].ToInt32();

                    if (String.IsNullOrEmpty(prop)) continue;
                    var statlist = GetStatListFromProperty(prop, par, min, max);
                    foreach (var stat in statlist)
                    {
                        list.Add(stat);
                    }
                }

                SplitStatLists(item, list);
                //System.Diagnostics.Debug.WriteLine(String.Format("{0}:{1}={2}", row["set"].Value, row["index"].Value, row["add func"].Value));
            }

            return sets;
        }

        public static void SetLevel99()
        {
            int curlevel = Convert.ToInt32(Helper.CurrentCharactor.Attributes.Stats["level"]);

            int base1 = 3520485;
            int mul = 1000;

            Helper.CurrentCharactor.Level = 99;
            Helper.CurrentCharactor.Attributes.Stats["experience"] = base1 * mul + 254;// ExcelTxt.ExperienceTxt[Helper.CurrentCharactor.Level.ToString()]["Amazon"].ToInt32();
            Helper.CurrentCharactor.Attributes.Stats["level"] = 99;

            //if (Helper.CurrentCharactor.Attributes.Stats.ContainsKey("statpts"))
            {
                Helper.CurrentCharactor.Attributes.Stats["strength"] = 1023;
                Helper.CurrentCharactor.Attributes.Stats["energy"] = 1023;
                Helper.CurrentCharactor.Attributes.Stats["dexterity"] = 1023;
                Helper.CurrentCharactor.Attributes.Stats["vitality"] = 1023;
                Helper.CurrentCharactor.Attributes.Stats["hitpoints"] = 1023;
                Helper.CurrentCharactor.Attributes.Stats["statpts"] = 0;
                //Helper.CurrentCharactor.Attributes.Stats["statpts"] = 1023;//可以多次更改，分配到不同的属性上。
            }
            //else
            //{
            //    Helper.CurrentCharactor.Attributes.Stats["statpts"] = (99 - curlevel) * 5;
            //}

            switch (Helper.CurrentCharactor.ClassId)
            {
                case 0: SetLevel99Class(2, 1.5, 1); break;
                case 1: SetLevel99Class(1, 2, 1); break;
                case 2: SetLevel99Class(1.5, 2, 1); break;
                case 3: SetLevel99Class(2, 1, 1.5); break;
                case 4: SetLevel99Class(2, 1, 2); break;
                case 5: SetLevel99Class(1.5, 2, 1); break;
                case 6: SetLevel99Class(2, 1, 1.5); break;
                default: break;
            }
        }

        private static void SetLevel99Class(double maxhp, double maxmana, double maxstamina)
        {
            //int curlevel = 0;// Convert.ToInt32(Helper.CurrentCharactor.Attributes.Stats["level"]);
            Helper.CurrentCharactor.Attributes.Stats["maxhp"] = 8191;// (int)(Math.Floor((99.0f-curlevel)*maxhp));
            Helper.CurrentCharactor.Attributes.Stats["maxmana"] = 8191; //(int)(Math.Floor((99.0f - curlevel) * maxmana));
            Helper.CurrentCharactor.Attributes.Stats["maxstamina"] = 8191;// (int)(Math.Floor((99.0f - curlevel) * maxstamina));
        }

        public static void SetSkill20()
        {
            Helper.CurrentCharactor.ClassSkills.Skills.ForEach(skill => skill.Points = 20);
        }

        public static void RestoreLevelAndSkill()
        {
            Helper.CurrentCharactor.Attributes.Stats["strength"] = ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class]["str"].ToInt32();
            Helper.CurrentCharactor.Attributes.Stats["energy"] = ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class]["int"].ToInt32();
            Helper.CurrentCharactor.Attributes.Stats["mana"] = ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class]["int"].ToInt32();
            Helper.CurrentCharactor.Attributes.Stats["dexterity"] = ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class]["dex"].ToInt32();
            Helper.CurrentCharactor.Attributes.Stats["vitality"] = ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class]["vit"].ToInt32();
            Helper.CurrentCharactor.Attributes.Stats["stamina"] = ExcelTxt.CharStatsTxt[Helper.CurrentCharactor.Class]["stamina"].ToInt32();
            Helper.CurrentCharactor.Attributes.Stats["level"] = 1;
            Helper.CurrentCharactor.Attributes.Stats["experience"] = 0;
            Helper.CurrentCharactor.Attributes.Stats["hitpoints"] = 0;
            Helper.CurrentCharactor.Attributes.Stats["statpts"] = 4092 - (Helper.CurrentCharactor.Attributes.Stats["strength"] + Helper.CurrentCharactor.Attributes.Stats["energy"] + Helper.CurrentCharactor.Attributes.Stats["dexterity"] + Helper.CurrentCharactor.Attributes.Stats["vitality"]);

            Helper.CurrentCharactor.Level = 1;
            Helper.CurrentCharactor.ClassSkills.Skills.ForEach(skill => skill.Points = 0);
        }
    }

    public class DefinitionInfo
    {
        public int StashTabStartX, StashTabStartY, StashTabTitleStartX, StashTabTitleStartY, StashTitleStartX, StashTitleStartY, InventoryTitleStartX, InventoryTitleStartY, StashTabWidth, StashTabHeight;
        public int QuestTabStartX, QuestTabStartY, QuestTabTitleStartX, QuestTabTitleStartY, QuestTabWidth, QuestTabHeight, QuestTitleStartX, QuestTitleStartY, QuestIconStartX, QuestIconStartY, QuestIconWidth, QuestIconHeight;
        public int WaypointsTabStartX, WaypointsTabStartY, WaypointsTabTitleStartX, WaypointsTabTitleStartY, WaypointsTabWidth, WaypointsTabHeight, WaypointsTitleStartX, WaypointsTitleStartY, WaypointsIconStartX, WaypointsIconStartY, WaypointsIconWidth, WaypointsIconHeight;
        public int StashTabFontSize, StashTitleFontSize;
        public int TooltipFontSize;
        //底图不是绝对的32个像素，特么的
        public int[] StoreRangeX;
        public int[] StoreRangeY;

        public int[] StashRangeX;
        public int[] StashRangeY;
        //public int BasicUnit = 98;
        public int BoxSize = 49, InnerBoxSize = 45;

        public Rectangle[] EquipedItem;

        public Rectangle BodyArea
        {
            get
            {
                return new Rectangle((int)(EquipedItem.Min(e => e.X)*Helper.DisplayRatio), (int)(EquipedItem.Min(e => e.Y) * Helper.DisplayRatio), (int)((EquipedItem.Max(e => e.X) + 49 * 2 - EquipedItem.Min(e => e.X)) * Helper.DisplayRatio), (int)((EquipedItem.Max(e => e.Y) + 49 - EquipedItem.Min(e => e.Y)) * Helper.DisplayRatio));
            }
        }

        public Rectangle StoreArea
        {
            get
            {
                return new Rectangle((int)(StoreRangeX[0] * Helper.DisplayRatio), (int)(StoreRangeY[0] * Helper.DisplayRatio), (int)((StoreRangeX[StoreRangeX.Length - 1] + Helper.DefinitionInfo.BoxSize) * Helper.DisplayRatio), (int)((StoreRangeY[StoreRangeY.Length - 1] + Helper.DefinitionInfo.BoxSize) * Helper.DisplayRatio));
            }
        }

        public Rectangle StashArea
        {
            get
            {
                return new Rectangle((int)(StashRangeX[0] * Helper.DisplayRatio), (int)(StashRangeY[0] * Helper.DisplayRatio), (int)((StashRangeX[StashRangeX.Length - 1] + Helper.DefinitionInfo.BoxSize) * Helper.DisplayRatio), (int)((StashRangeY[StashRangeY.Length - 1] + Helper.DefinitionInfo.BoxSize) * Helper.DisplayRatio));
            }
        }
    }

    public class ItemStatCostFunc
    {
        public int ID = 0;
        public string Format = "";
        public string DgrpFormat = "";
        public double NewValue = 0.0d;
        public bool IsPerLevel = false;
        public bool IsArmorClass = false;
        public bool IsMaxDamage = false;
        public bool IsMinDamage = false;
        public int Priority = 0;
    }
}