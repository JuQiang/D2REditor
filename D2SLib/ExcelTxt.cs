using D2SLib.Model.Huffman;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D2SLib
{
    public class ExcelTxt
    {
        private string intKey, strKey;
        public ExcelTxt(string intKey, string strKey)
        {
            this.intKey = intKey;
            this.strKey = strKey;
        }

        private static HuffmanTree itemCodeTree = null;
        public static HuffmanTree ItemCodeTree
        {
            get
            {
                if (itemCodeTree == null)
                {
                    itemCodeTree = InitializeHuffmanTree();
                }
                return itemCodeTree;
            }
            set
            {
                itemCodeTree = value;
            }
        }

        private static HuffmanTree InitializeHuffmanTree()
        {
            var itemCodeTree = new HuffmanTree();
            itemCodeTree.Build(new List<string>());
            return itemCodeTree;
        }

        public static ExcelTxt GetInstance(string inKey, string strKey)
        {
            ExcelTxt txtFile = new ExcelTxt(inKey, strKey);
            return txtFile;
        }

        public TxtRow this[int i] => this.GetByColumnAndValue(this.intKey, i.ToString());
        public TxtRow this[string i] => this.GetByColumnAndValue(this.strKey.Trim(),/*"Stat",*/ i);

        public Dictionary<string, int> Columns { get; set; }
        public List<TxtRow> Rows { get; set; }

        public ExcelTxt Read(string fname)
        {
            using (Stream data = File.OpenRead(fname))
            {
                Columns = new Dictionary<string, int>();
                Rows = new List<TxtRow>();
                using (StreamReader reader = new StreamReader(data))
                {
                    //skip header
                    int idx = 0;
                    var columns = reader.ReadLine().Split('\t');
                    foreach (var col in columns)
                    {
                        if (Columns.ContainsKey(col)) continue;
                        Columns.Add(col, idx++);
                    }
                    while (reader.Peek() >= 0)
                    {
                        Rows.Add(new TxtRow(Columns, reader.ReadLine().Split('\t')));
                    }
                }
            }

            return this;
        }

        public TxtRow GetByColumnAndValue(string name, string value)
        {
            foreach (var row in Rows)
            {
                if (row[name].Value.Trim() == value.Trim())
                    return row;
            }
            return null;
        }

        private static ExcelTxt itemStatCostTxt, armorTxt, weaponsTxt, miscTxt, skillsTxt, itemTypeTxt, setItemsTxt, setsTxt, uniqueItemsTxt, superUniqueItemsTxt, runesTxt, charStatsTxt, gemsTxt,
            propertiesTxt, experienceTxt, inventoryTxt, skillDescTxt, hirelingTxt,
            autoMagicTxt, magicPrefixTxt, magicSuffixTxt, playerClassTxt, levelsTxt, cubemainTxt;

        public static ExcelTxt AutoMagicTxt
        {
            get
            {
                if (autoMagicTxt == null)
                {
                    autoMagicTxt = ExcelTxt.GetInstance("Id", "Name").Read(Utils.CacheFolder + @"\excel\automagic.txt");
                }

                return autoMagicTxt;
            }
        }

        public static ExcelTxt LevelsTxt
        {
            get
            {
                if (levelsTxt == null)
                {
                    levelsTxt = ExcelTxt.GetInstance("*ID", "Stat").Read(Utils.CacheFolder + @"\excel\levels.txt");
                }

                return levelsTxt;
            }
        }

        public static ExcelTxt CubemainTxt
        {
            get
            {
                if (cubemainTxt == null)
                {
                    cubemainTxt = ExcelTxt.GetInstance("", "").Read(Utils.CacheFolder + @"\excel\cubemain.txt");
                }

                return cubemainTxt;
            }
        }

        public static ExcelTxt MagicPrefixTxt
        {
            get
            {
                if (magicPrefixTxt == null)
                {
                    magicPrefixTxt = new ExcelTxt("Id", "Name");
                    magicPrefixTxt.Columns = new Dictionary<string, int>();

                    var allLines = File.ReadAllLines(Utils.CacheFolder + @"\excel\magicprefix.txt");

                    int idx = 0;
                    var columns = allLines[0].Split('\t');
                    foreach (var col in columns)
                    {
                        magicPrefixTxt.Columns.Add(col, idx++);
                    }
                    magicPrefixTxt.Columns.Add("Id", idx++);
                    magicPrefixTxt.Rows = new List<TxtRow>();

                    for (int i = 1; i < allLines.Length; i++)
                    {
                        var tmp = allLines[i].Split(new char[] { '\t' });
                        string code = tmp[0].Trim();

                        if (code.Length == 0 || code == "Expansion")
                        {
                            continue;
                        }

                        tmp = tmp.Append(Convert.ToString(i - 1)).ToArray();
                        magicPrefixTxt.Rows.Add(new TxtRow(magicPrefixTxt.Columns, tmp));
                    }
                }

                return magicPrefixTxt;
            }
        }

        public static ExcelTxt MagicSuffixTxt
        {
            get
            {
                if (magicSuffixTxt == null)
                {
                    magicSuffixTxt = new ExcelTxt("Id", "Name");
                    magicSuffixTxt.Columns = new Dictionary<string, int>();

                    var allLines = File.ReadAllLines(Utils.CacheFolder + @"\excel\magicsuffix.txt");

                    int idx = 0;
                    var columns = allLines[0].Split('\t');
                    foreach (var col in columns)
                    {
                        magicSuffixTxt.Columns.Add(col, idx++);
                    }
                    magicSuffixTxt.Columns.Add("Id", idx++);
                    magicSuffixTxt.Rows = new List<TxtRow>();

                    for (int i = 1; i < allLines.Length; i++)
                    {
                        var tmp = allLines[i].Split(new char[] { '\t' });
                        string code = tmp[0].Trim();

                        if (code.Length == 0 || code == "Expansion")
                        {
                            continue;
                        }

                        tmp = tmp.Append(Convert.ToString(i - 1)).ToArray();
                        magicSuffixTxt.Rows.Add(new TxtRow(magicSuffixTxt.Columns, tmp));
                    }
                }

                return magicSuffixTxt;
            }
        }

        public static ExcelTxt ItemStatCostTxt
        {
            get
            {
                if (itemStatCostTxt == null)
                {
                    itemStatCostTxt = ExcelTxt.GetInstance("*ID", "Stat").Read(Utils.CacheFolder + @"\excel\ItemStatCost.txt");
                }

                return itemStatCostTxt;
            }
        }
        public static ExcelTxt ArmorTxt
        {
            get
            {
                if (armorTxt == null)
                {
                    armorTxt = ExcelTxt.GetInstance("", "code").Read(Utils.CacheFolder + @"\excel\Armor.txt");
                }

                return armorTxt;
            }
        }
        public static ExcelTxt WeaponsTxt
        {
            get
            {
                if (weaponsTxt == null)
                {
                    weaponsTxt = ExcelTxt.GetInstance("", "code").Read(Utils.CacheFolder + @"\excel\Weapons.txt");
                }

                return weaponsTxt;
            }
        }
        public static ExcelTxt MiscTxt
        {
            get
            {
                if (miscTxt == null)
                {
                    miscTxt = ExcelTxt.GetInstance("", "code").Read(Utils.CacheFolder + @"\excel\Misc.txt");
                }

                return miscTxt;
            }
        }

        public static ExcelTxt SkillsTxt
        {
            get
            {
                if (skillsTxt == null)
                {
                    skillsTxt = ExcelTxt.GetInstance("*Id", "code").Read(Utils.CacheFolder + @"\excel\Skills.txt");
                }

                return skillsTxt;
            }
        }

        public static ExcelTxt ItemTypesTxt
        {
            get
            {
                if (itemTypeTxt == null)
                {
                    itemTypeTxt = ExcelTxt.GetInstance("", "Code").Read(Utils.CacheFolder + @"\excel\ItemTypes.txt");
                }

                return itemTypeTxt;
            }
        }

        public static ExcelTxt SetItemsTxt
        {
            get
            {
                if (setItemsTxt == null)
                {
                    setItemsTxt = ExcelTxt.GetInstance("*ID", "index").Read(Utils.CacheFolder + @"\excel\setitems.txt");
                }

                return setItemsTxt;
            }
        }

        public static ExcelTxt SetsTxt
        {
            get
            {
                if (setsTxt == null)
                {
                    setsTxt = ExcelTxt.GetInstance("*ID", "index").Read(Utils.CacheFolder + @"\excel\sets.txt");
                }

                return setsTxt;
            }
        }

        public static ExcelTxt UniqueItemsTxt
        {
            get
            {
                if (uniqueItemsTxt == null)
                {
                    uniqueItemsTxt = ExcelTxt.GetInstance("*ID", "index").Read(Utils.CacheFolder + @"\excel\uniqueitems.txt");
                }

                return uniqueItemsTxt;
            }
        }
        public static ExcelTxt SuperUniqueItemsTxt
        {
            get
            {
                if (superUniqueItemsTxt == null)
                {
                    superUniqueItemsTxt = ExcelTxt.GetInstance("*ID", "index").Read(Utils.CacheFolder + @"\excel\superuniques.txt");
                }

                return superUniqueItemsTxt;
            }
        }

        public static ExcelTxt RunesTxt
        {
            get
            {
                if (runesTxt == null)
                {
                    runesTxt = ExcelTxt.GetInstance("", "Name").Read(Utils.CacheFolder + @"\excel\runes.txt");
                }

                return runesTxt;
            }
        }

        public static ExcelTxt CharStatsTxt
        {
            get
            {
                if (charStatsTxt == null)
                {
                    charStatsTxt = ExcelTxt.GetInstance("", "class").Read(Utils.CacheFolder + @"\excel\charstats.txt");
                }

                return charStatsTxt;
            }
        }

        public static ExcelTxt GemsTxt
        {
            get
            {
                if (gemsTxt == null)
                {
                    gemsTxt = ExcelTxt.GetInstance("", "code").Read(Utils.CacheFolder + @"\excel\gems.txt");
                }

                return gemsTxt;
            }
        }

        public static ExcelTxt PropertiesTxt
        {
            get
            {
                if (propertiesTxt == null)
                {
                    propertiesTxt = ExcelTxt.GetInstance("", "code").Read(Utils.CacheFolder + @"\excel\properties.txt");
                }

                return propertiesTxt;
            }
        }

        public static ExcelTxt ExperienceTxt
        {
            get
            {
                if (experienceTxt == null)
                {
                    experienceTxt = ExcelTxt.GetInstance("", "Level").Read(Utils.CacheFolder + @"\excel\experience.txt");
                }

                return experienceTxt;
            }
        }

        public static ExcelTxt InventoryTxt
        {
            get
            {
                if (inventoryTxt == null)
                {
                    inventoryTxt = ExcelTxt.GetInstance("", "class").Read(Utils.CacheFolder + @"\excel\inventory.txt");
                }

                return inventoryTxt;
            }
        }

        public static ExcelTxt SkillDescTxt
        {
            get
            {
                if (skillDescTxt == null)
                {
                    skillDescTxt = ExcelTxt.GetInstance("", "skilldesc").Read(Utils.CacheFolder + @"\excel\skilldesc.txt");
                }

                return skillDescTxt;
            }
        }

        //hirelingTxt
        public static ExcelTxt HirelingTxt
        {
            get
            {
                if (hirelingTxt == null)
                {
                    hirelingTxt = ExcelTxt.GetInstance("Id", "Hireling").Read(Utils.CacheFolder + @"\excel\hireling.txt");
                }

                return hirelingTxt;
            }
        }

        public static ExcelTxt PlayerClassTxt
        {
            get
            {
                if (playerClassTxt == null)
                {
                    playerClassTxt = ExcelTxt.GetInstance("", "Code").Read(Utils.CacheFolder + @"\excel\playerclass.txt");
                }

                return playerClassTxt;
            }
        }

        public static TxtRow ItemGetByCode(string code)
        {
            return ExcelTxt.ArmorTxt[code] ?? ExcelTxt.WeaponsTxt[code] ?? ExcelTxt.MiscTxt[code];// Core.TXT.ItemsTXT.GetByCode(item.Code);
        }
    }

    public class TxtRow
    {
        public Dictionary<string, int> Columns { get; set; }
        public TxtCell[] Data { get; set; }

        public TxtCell this[int i] => this.GetByIndex(i);
        public TxtCell this[string i] => this.GetByColumn(i);

        public TxtRow()
        {

        }
        public TxtRow(Dictionary<string, int> columns, string[] data)
        {
            Columns = columns;
            Data = data.Select(e => new TxtCell(e)).ToArray();
        }

        public TxtCell GetByIndex(int idx)
        {
            return Data[idx];
        }

        public TxtCell GetByColumn(string col)
        {
            return GetByIndex(Columns[col]);
        }

        public void SetByColumn(string col, string value)
        {
            this.Data[Columns[col]].Value = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var keylist = this.Columns.Keys.ToList();

            for (int i = 0; i < this.Columns.Keys.Count; i++)
            {
                sb.AppendFormat("{0}={1}\r\n", keylist[i], this.GetByIndex(i).Value);
            }

            return sb.ToString();
        }
    }

    public class TxtCell
    {
        public string Value { get; set; }

        public Int32 ToInt32()
        {
            Int32 ret = 0;
            Int32.TryParse(Value, out ret);
            return ret;
        }

        public UInt32 ToUInt32()
        {
            UInt32 ret = 0;
            UInt32.TryParse(Value, out ret);
            return ret;
        }

        public UInt16 ToUInt16()
        {
            UInt16 ret = 0;
            UInt16.TryParse(Value, out ret);
            return ret;
        }

        public Int16 ToInt16()
        {
            Int16 ret = 0;
            Int16.TryParse(Value, out ret);
            return ret;
        }

        public bool ToBool()
        {
            return ToInt32() == 1;
        }
        public TxtCell(string value)
        {
            Value = value;
        }
    }
}
