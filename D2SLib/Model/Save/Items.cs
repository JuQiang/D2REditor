using D2SLib.IO;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace D2SLib.Model.Save
{
    public enum ItemMode
    {
        Stored = 0x0,
        Equipped = 0x1,
        Belt = 0x2,
        Buffer = 0x4,
        Socket = 0x6,
    }

    public enum ItemLocation
    {
        None,
        Head,
        Neck,
        Torso,
        RightHand,
        LeftHand,
        RightFinger,
        LeftFinger,
        Waist,
        Feet,
        Gloves,
        SwapRight,
        SwapLeft
    }

    public enum ItemQuality
    {
        Inferior = 0x1,
        Normal,
        Superior,
        Magic,
        Set,
        Rare,
        Unique,
        Craft,
        Tempered
    }

    public enum ItemIconState
    {
        Normal,
        Hovering,
    }

    public class ItemList
    {
        public UInt16? Header { get; set; }
        public UInt16 Count { get { return (UInt16)(this.Items.Count); } }
        //public UInt16 Count { get; set;}
        public List<Item> Items { get; set; } = new List<Item>();

        public static ItemList Read(BitReader reader, UInt32 version)
        {
            //这是D2SLib的一个bug，用clone item之后，添加到了items里面，它没有更新Count这个property，导致文件被写坏了，我丢了好几次赫拉迪姆方块，每次重新再打，郁闷！
            //character.PlayerItemList.Count = (ushort)(character.PlayerItemList.Items.Count);

            ItemList itemList = new ItemList();
            itemList.Header = reader.ReadUInt16();
            //itemList.Count = reader.ReadUInt16();
            UInt16 count = reader.ReadUInt16();
            for (int i = 0; i < count; i++)
            {
                itemList.Items.Add(Item.Read(reader, version));
            }
            return itemList;
        }

        public static byte[] Write(ItemList itemList, UInt32 version)
        {
            using (BitWriter writer = new BitWriter())
            {
                writer.WriteUInt16(itemList.Header ?? (UInt16)0x4D4A);
                writer.WriteUInt16(itemList.Count);
                for (int i = 0; i < itemList.Count; i++)
                {
                    writer.WriteBytes(Item.Write(itemList.Items[i], version));
                }
                return writer.ToArray();
            }
        }
    }

    public class Item
    {
        public UInt16? Header { get; set; }
        [JsonIgnore]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public BitArray? Flags { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string Version { get; set; }
        public ItemMode Mode { get; set; }
        public ItemLocation Location { get; set; }
        public bool IsWeapon { get; set; }
        public bool IsArmor { get; set; }
        public bool IsMisc { get; set; }
        public byte Column { get; set; }
        public byte Row { get; set; }
        public byte Page { get; set; }
        public byte EarLevel { get; set; }
        public string PlayerName { get; set; } //used for personalized or ears
        public string Code { get; set; }
        public byte NumberOfSocketedItems { get; set; }
        public byte TotalNumberOfSockets { get; set; }
        public List<Item> SocketedItems { get; set; } = new List<Item>();
        public UInt32 Id { get; set; }
        public byte ItemLevel { get; set; }
        public ItemQuality Quality { get; set; }
        public bool HasMultipleGraphics { get; set; }
        public byte GraphicId { get; set; }
        public bool IsAutoAffix { get; set; }
        public UInt16 AutoAffixId { get; set; } //?
        public UInt32 FileIndex { get; set; }
        public UInt16[] MagicPrefixIds { get; set; } = new UInt16[3];
        public UInt16[] MagicSuffixIds { get; set; } = new UInt16[3];
        public UInt16 RarePrefixId { get; set; }
        public UInt16 RareSuffixId { get; set; }
        public UInt32 RunewordId { get; set; }
        [JsonIgnore]
        public bool HasRealmData { get; set; }
        [JsonIgnore]
        public UInt32[] RealmData { get; set; } = new UInt32[3];
        public UInt16 Armor { get; set; }
        public UInt16 MaxDurability { get; set; }
        public UInt16 Durability { get; set; }
        public UInt16 Quantity { get; set; }
        public byte SetItemMask { get; set; }
        public List<ItemStatList> StatLists { get; set; } = new List<ItemStatList>();
        public bool IsIdentified { get { return Flags[4]; } set { Flags[4] = value; } }
        public bool IsSocketed { get { return Flags[11]; } set { Flags[11] = value; } }
        public bool IsNew { get { return Flags[13]; } set { Flags[13] = value; } }
        public bool IsEar { get { return Flags[16]; } set { Flags[16] = value; } }
        public bool IsStarterItem { get { return Flags[17]; } set { Flags[17] = value; } }
        public bool IsCompact { get { return Flags[21]; } set { Flags[21] = value; } }
        public bool IsEthereal { get { return Flags[22]; } set { Flags[22] = value; } }
        public bool IsPersonalized { get { return Flags[24]; } set { Flags[24] = value; } }
        public bool IsRuneword { get { return Flags[26]; } set { Flags[26] = value; } }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public List<Rectangle> Rectangles = new List<Rectangle>();
        public Rectangle Rectangle { get; set; }
        public ItemIconState IconState { get; set; }

        public static Item Read(byte[] bytes, UInt32 version)
        {
            using (BitReader reader = new BitReader(bytes))
            {
                return Read(reader, version);
            }
        }

        public static Item Read(BitReader reader, UInt32 version)
        {
            Item item = new Item();
            if (version <= 0x60)
            {
                item.Header = reader.ReadUInt16();
            }
            ReadCompact(reader, item, version);
            if (!item.IsCompact)
            {
                ReadComplete(reader, item, version);
            }
            reader.Align();
            for (int i = 0; i < item.NumberOfSocketedItems; i++)
            {
                item.SocketedItems.Add(Read(reader, version));
            }

            item.IsMisc = (ExcelTxt.MiscTxt[item.Code] != null);// Core.TXT.ItemsTXT.IsMisc(item.Code);

            UpdateRowsAndColumnsInformation(item);

            return item;
        }

        public static void UpdateRowsAndColumnsInformation(Item item)
        {
            if (item.IsWeapon)
            {
                var data = ExcelTxt.WeaponsTxt.Rows.Where(stat => stat.Data[3].Value.Trim() == item.Code.ToString().Trim()).FirstOrDefault();
                item.Columns = Convert.ToInt32(data[44].Value);
                item.Rows = Convert.ToInt32(data[45].Value);
            }
            else if (item.IsArmor)
            {
                var data = ExcelTxt.ArmorTxt.Rows.Where(stat => stat.Data[18].Value.Trim() == item.Code.ToString().Trim()).FirstOrDefault();
                item.Columns = Convert.ToInt32(data[27].Value);
                item.Rows = Convert.ToInt32(data[28].Value);
            }
            else if (item.IsMisc)
            {
                var data = ExcelTxt.MiscTxt.Rows.Where(stat => stat.Data[14].Value.Trim() == item.Code.ToString().Trim()).FirstOrDefault();
                item.Columns = Convert.ToInt32(data[18].Value);
                item.Rows = Convert.ToInt32(data[19].Value);
            }
            else
            {
                item.Columns = -1;
                item.Rows = -1;
            }
        }
        public static byte[] Write(Item item, UInt32 version)
        {
            using (BitWriter writer = new BitWriter())
            {
                if (version <= 0x60)
                {
                    writer.WriteUInt16(item.Header ?? (UInt16)0x4D4A);
                }
                WriteCompact(writer, item, version);
                if (!item.IsCompact)
                {
                    WriteComplete(writer, item, version);
                }
                writer.Align();
                for (int i = 0; i < item.NumberOfSocketedItems; i++)
                {
                    writer.WriteBytes(Item.Write(item.SocketedItems[i], version));
                }
                return writer.ToArray();
            }
        }

        protected static string ReadPlayerName(BitReader reader)
        {
            char[] name = new char[15];
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = (char)reader.ReadByte(7);
                if (name[i] == '\0')
                {
                    break;
                }
            }
            return new string(name).Replace("\0", "");
        }

        protected static void WritePlayerName(BitWriter writer, string name)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(name.Replace("\0", ""));
            for (int i = 0; i < bytes.Length; i++)
            {
                writer.WriteByte(bytes[i], 7);
            }
            writer.WriteByte((byte)'\0', 7);
        }

        protected static void ReadCompact(BitReader reader, Item item, UInt32 version)
        {
            item.Flags = new BitArray(reader.ReadBytes(4));
            if (version <= 0x60)
            {
                item.Version = Convert.ToString(reader.ReadUInt16(10), 10);
            }
            else if (version >= 0x61)
            {
                item.Version = Convert.ToString(reader.ReadUInt16(3), 2);
            }
            item.Mode = (ItemMode)reader.ReadByte(3);
            item.Location = (ItemLocation)reader.ReadByte(4);
            item.Column = reader.ReadByte(4);
            item.Row = reader.ReadByte(4);
            item.Page = reader.ReadByte(3);
            if (item.IsEar)
            {
                item.FileIndex = reader.ReadByte(3);
                item.EarLevel = reader.ReadByte(7);
                item.PlayerName = ReadPlayerName(reader);
            }
            else
            {
                item.Code = "";
                if (version <= 0x60)
                {
                    item.Code = reader.ReadString(4);
                }
                else if (version >= 0x61)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        item.Code += ExcelTxt.ItemCodeTree.DecodeChar(reader);
                    }
                }
                item.NumberOfSocketedItems = reader.ReadByte(item.IsCompact ? 1 : 3);
            }
        }

        protected static void WriteCompact(BitWriter writer, Item item, UInt32 version)
        {
            BitArray flags = item.Flags;
            if (flags == null)
            {
                flags = new BitArray(32);
                flags[4] = item.IsIdentified;
                flags[11] = item.IsSocketed;
                flags[13] = item.IsNew;
                flags[16] = item.IsEar;
                flags[17] = item.IsStarterItem;
                flags[21] = item.IsCompact;
                flags[22] = item.IsEthereal;
                flags[24] = item.IsPersonalized;
                flags[26] = item.IsRuneword;
            }
            foreach (var flag in flags.Cast<bool>())
            {
                writer.WriteBit(flag);
            }
            if (version <= 0x60)
            {
                //todo. how do we handle 1.15 version to 1.14. maybe this should be a string
                writer.WriteUInt16(Convert.ToUInt16(item.Version, 10), 10);
            }
            else if (version >= 0x61)
            {
                writer.WriteUInt16(Convert.ToUInt16(item.Version, 2), 3);
            }
            writer.WriteByte((byte)item.Mode, 3);
            writer.WriteByte((byte)item.Location, 4);
            writer.WriteByte(item.Column, 4);
            writer.WriteByte(item.Row, 4);
            writer.WriteByte(item.Page, 3);
            if (item.IsEar)
            {
                writer.WriteUInt32(item.FileIndex, 3);
                writer.WriteByte(item.EarLevel, 7);
                WritePlayerName(writer, item.PlayerName);
            }
            else
            {
                var code = Encoding.ASCII.GetBytes(item.Code.PadRight(4, ' '));
                if (version <= 0x60)
                {
                    writer.WriteBytes(code);
                }
                else if (version >= 0x61)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        BitArray bits = ExcelTxt.ItemCodeTree.EncodeChar((char)code[i]);
                        foreach (var bit in bits.Cast<bool>())
                        {
                            writer.WriteBit(bit);
                        }
                    }
                }
                writer.WriteByte(item.NumberOfSocketedItems, item.IsCompact ? 1 : 3);
            }

        }

        protected static void ReadComplete(BitReader reader, Item item, UInt32 version)
        {
            item.Id = reader.ReadUInt32();
            item.ItemLevel = reader.ReadByte(7);
            item.Quality = (ItemQuality)reader.ReadByte(4);
            item.HasMultipleGraphics = reader.ReadBit();
            if (item.HasMultipleGraphics)
            {
                item.GraphicId = reader.ReadByte(3);
            }
            item.IsAutoAffix = reader.ReadBit();
            if (item.IsAutoAffix)
            {
                item.AutoAffixId = reader.ReadUInt16(11);
            }
            switch (item.Quality)
            {
                case ItemQuality.Normal:
                    break;
                case ItemQuality.Inferior:
                case ItemQuality.Superior:
                    item.FileIndex = reader.ReadUInt16(3);
                    break;
                case ItemQuality.Magic:
                    item.MagicPrefixIds[0] = reader.ReadUInt16(11);
                    item.MagicSuffixIds[0] = reader.ReadUInt16(11);
                    break;
                case ItemQuality.Rare:
                case ItemQuality.Craft:
                    item.RarePrefixId = reader.ReadUInt16(8);
                    item.RareSuffixId = reader.ReadUInt16(8);
                    for (int i = 0; i < 3; i++)
                    {
                        if (reader.ReadBit())
                        {
                            item.MagicPrefixIds[i] = reader.ReadUInt16(11);
                        }
                        if (reader.ReadBit())
                        {
                            item.MagicSuffixIds[i] = reader.ReadUInt16(11);
                        }
                    }
                    break;
                case ItemQuality.Set:
                case ItemQuality.Unique:
                    item.FileIndex = reader.ReadUInt16(12);
                    break;
            }
            UInt16 propertyLists = 0;
            if (item.IsRuneword)
            {
                item.RunewordId = reader.ReadUInt32(12);
                propertyLists |= (UInt16)(1 << (reader.ReadUInt16(4) + 1));
            }
            if (item.IsPersonalized)
            {
                item.PlayerName = ReadPlayerName(reader);
            }
            if (item.Code.Trim() == "tbk" || item.Code.Trim() == "ibk")
            {
                item.MagicSuffixIds[0] = reader.ReadByte(5);
            }
            item.HasRealmData = reader.ReadBit();
            if (item.HasRealmData)
            {
                reader.ReadBits(96);
            }

            TxtRow row = ExcelTxt.ItemGetByCode(item.Code);// Core.TXT.ItemsTXT.GetByCode(item.Code);
            item.IsArmor = (ExcelTxt.ArmorTxt[item.Code] != null);// Core.TXT.ItemsTXT.IsArmor(item.Code);
            item.IsWeapon = (ExcelTxt.WeaponsTxt[item.Code] != null); //Core.TXT.ItemsTXT.IsWeapon(item.Code);            

            bool isStackable = row["stackable"].ToBool();
            if (item.IsArmor)
            {
                //why do i need this cast?
                //item.Armor = (UInt16)(reader.ReadUInt16(11) + ExcelTxt.ItemStatCostTxt["armorclass"]["Save Add"].ToUInt16());
                item.Armor = (UInt16)(reader.ReadUInt16(11) - ExcelTxt.ItemStatCostTxt["armorclass"]["Save Add"].ToUInt16());
            }
            if (item.IsArmor || item.IsWeapon)
            {
                var maxDurabilityStat = ExcelTxt.ItemStatCostTxt["maxdurability"];
                var durabilityStat = ExcelTxt.ItemStatCostTxt["maxdurability"];
                item.MaxDurability = (UInt16)(reader.ReadUInt16(maxDurabilityStat["Save Bits"].ToInt32()) + maxDurabilityStat["Save Add"].ToUInt16());
                if (item.MaxDurability > 0)
                {
                    item.Durability = (UInt16)(reader.ReadUInt16(durabilityStat["Save Bits"].ToInt32()) + durabilityStat["Save Add"].ToUInt16());
                    //what is this?
                    reader.ReadBit();
                }
            }
            if (isStackable)
            {
                item.Quantity = reader.ReadUInt16(9);
            }
            if (item.IsSocketed)
            {
                item.TotalNumberOfSockets = reader.ReadByte(4);
            }
            item.SetItemMask = 0;
            if (item.Quality == ItemQuality.Set)
            {
                item.SetItemMask = reader.ReadByte(5);
                propertyLists |= item.SetItemMask;
            }
            item.StatLists.Add(ItemStatList.Read(reader));
            for (int i = 1; i <= 64; i <<= 1)
            {
                if ((propertyLists & i) != 0)
                {
                    item.StatLists.Add(ItemStatList.Read(reader));
                }
            }
        }

        protected static void WriteComplete(BitWriter writer, Item item, UInt32 version)
        {
            writer.WriteUInt32(item.Id);
            writer.WriteByte(item.ItemLevel, 7);
            writer.WriteByte((byte)item.Quality, 4);
            writer.WriteBit(item.HasMultipleGraphics);
            if (item.HasMultipleGraphics)
            {
                writer.WriteByte(item.GraphicId, 3);
            }
            writer.WriteBit(item.IsAutoAffix);
            if (item.IsAutoAffix)
            {
                writer.WriteUInt16(item.AutoAffixId, 11);
            }
            switch (item.Quality)
            {
                case ItemQuality.Normal:
                    break;
                case ItemQuality.Inferior:
                case ItemQuality.Superior:
                    writer.WriteUInt32(item.FileIndex, 3);
                    break;
                case ItemQuality.Magic:
                    writer.WriteUInt16(item.MagicPrefixIds[0], 11);
                    writer.WriteUInt16(item.MagicSuffixIds[0], 11);
                    break;
                case ItemQuality.Rare:
                case ItemQuality.Craft:
                    writer.WriteUInt16(item.RarePrefixId, 8);
                    writer.WriteUInt16(item.RareSuffixId, 8);
                    for (int i = 0; i < 3; i++)
                    {
                        var hasPrefix = item.MagicPrefixIds[i] > 0;
                        var hasSuffix = item.MagicSuffixIds[i] > 0;
                        writer.WriteBit(hasPrefix);
                        if (hasPrefix)
                        {
                            writer.WriteUInt16(item.MagicPrefixIds[i], 11);
                        }
                        writer.WriteBit(hasSuffix);
                        if (hasSuffix)
                        {
                            writer.WriteUInt16(item.MagicSuffixIds[i], 11);
                        }
                    }
                    break;
                case ItemQuality.Set:
                case ItemQuality.Unique:
                    writer.WriteUInt32(item.FileIndex, 12);
                    break;
            }
            UInt16 propertyLists = 0;
            if (item.IsRuneword)
            {
                writer.WriteUInt32(item.RunewordId, 12);
                propertyLists |= 1 << 6;
                writer.WriteUInt16((UInt16)5, 4);
            }
            if (item.IsPersonalized)
            {
                WritePlayerName(writer, item.PlayerName);
            }
            if (item.Code.Trim() == "tbk" || item.Code.Trim() == "ibk")
            {
                writer.WriteUInt16(item.MagicSuffixIds[0], 5);
            }
            writer.WriteBit(item.HasRealmData);
            if (item.HasRealmData)
            {
                //todo 96 bits
            }

            TxtRow row = ExcelTxt.ItemGetByCode(item.Code);// Core.TXT.ItemsTXT.GetByCode(item.Code);
            bool isArmor = (ExcelTxt.ArmorTxt[item.Code] != null);// Core.TXT.ItemsTXT.IsArmor(item.Code);
            bool isWeapon = (ExcelTxt.WeaponsTxt[item.Code] != null); //Core.TXT.ItemsTXT.IsWeapon(item.Code);
            bool isStackable = row["stackable"].ToBool();
            if (isArmor)
            {
                writer.WriteUInt16((UInt16)(item.Armor - ExcelTxt.ItemStatCostTxt["armorclass"]["Save Add"].ToUInt16()), 11);
            }
            if (isArmor || isWeapon)
            {
                var maxDurabilityStat = ExcelTxt.ItemStatCostTxt["maxdurability"];
                var durabilityStat = ExcelTxt.ItemStatCostTxt["maxdurability"];
                writer.WriteUInt16((UInt16)(item.MaxDurability - maxDurabilityStat["Save Add"].ToUInt16()), maxDurabilityStat["Save Bits"].ToInt32());
                if (item.MaxDurability > 0)
                {
                    writer.WriteUInt16((UInt16)(item.Durability - durabilityStat["Save Add"].ToUInt16()), durabilityStat["Save Bits"].ToInt32());
                    ////what is this?
                    writer.WriteBit(false);
                }
            }
            if (isStackable)
            {
                writer.WriteUInt16(item.Quantity, 9);
            }
            if (item.IsSocketed)
            {
                writer.WriteByte(item.TotalNumberOfSockets, 4);
            }
            if (item.Quality == ItemQuality.Set)
            {
                writer.WriteByte(item.SetItemMask, 5);
                propertyLists |= item.SetItemMask;
            }
            ItemStatList.Write(writer, item.StatLists[0]);
            var idx = 1;
            for (int i = 1; i <= 64; i <<= 1)
            {
                if ((propertyLists & i) != 0)
                {
                    ItemStatList.Write(writer, item.StatLists[idx++]);
                }
            }
        }

        public override string ToString()
        {
            var proerties = this.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();

            foreach (var prop in proerties)
            {
                var val = prop.GetValue(this);
                sb.AppendFormat("{0}={1}\r\n", prop.Name, Convert.ToString(val));
            }

            sb.AppendLine();
            sb.AppendLine();

            foreach (var stalist in this.StatLists)
            {
                foreach (var sta in stalist.Stats)
                {
                    var match = ExcelTxt.ItemStatCostTxt.Rows.Where(stat => stat.Data[1].Value == sta.Id.ToString()).FirstOrDefault();
                    var max = (1 << (Convert.ToInt32(match.Data[20].Value)));
                    if (false == String.IsNullOrEmpty(match.Data[21].Value))
                    {
                        max -= Convert.ToInt32(match.Data[21].Value);
                    }
                    max--;

                    sb.AppendFormat("Name:{0}, Max:{1}, Current:{2}\r\n", match.Data[0].Value, max, sta.Value);
                }
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public string Name
        {
            get
            {
                string prefix = "";
                var suffix = "";
                string name = "";

                if (this.Quality == ItemQuality.Inferior)
                {
                    suffix = Utils.AllJsons["Low Quality"];
                    name = Utils.AllJsons[this.Code.Trim()];
                }
                else if (this.Quality == ItemQuality.Superior)
                {
                    suffix = Utils.AllJsons["Hiquality"];
                    name = Utils.AllJsons[this.Code.Trim()];
                    //name = Utils.SetItems[(int)this.FileIndex];
                }
                else if (this.Quality == ItemQuality.Rare || this.Quality == ItemQuality.Craft)
                {
                    suffix = Utils.RarePrefixItems[this.RarePrefixId];
                    name = "";
                    prefix = Utils.RareSuffixItems[this.RareSuffixId];
                }
                else if (this.Quality == ItemQuality.Set)
                {
                    name = Utils.SetItems[(int)this.FileIndex];
                }
                else if (this.Quality == ItemQuality.Unique)
                {
                    name = Utils.UniqueItems[(int)this.FileIndex];
                }
                else if (this.Quality == ItemQuality.Magic)
                {
                    name = Utils.AllJsons[this.Code.Trim()];

                    foreach (var ix in this.MagicPrefixIds)
                    {
                        if (ix == 0) continue;
                        if (Utils.MagicPrefixItems.ContainsKey(ix))
                        {
                            prefix += Utils.MagicPrefixItems[ix];
                        }
                        else
                        {
                            prefix += ix.ToString();
                        }
                    }

                    foreach (var ix in this.MagicSuffixIds)
                    {
                        if (ix == 0) continue;
                        if (Utils.MagicSuffixItems.ContainsKey(ix))
                        {
                            suffix += Utils.MagicSuffixItems[ix];
                        }
                        else
                        {
                            suffix += ix.ToString();
                        }
                    }
                }
                else
                {
                    if (Utils.AllJsons.ContainsKey(this.Code.Trim())) name = Utils.AllJsons[this.Code.Trim()];
                    else name = this.Code.Trim();
                }

                if (this.IsRuneword)
                {
                    prefix = suffix = "";
                    var rid = (int)this.RunewordId;
                    if (rid >= 106) ++rid;//=107;//只有106怨恨不正常，其他都OK！！！
                    if (Utils.RuneWords.ContainsKey(rid)) name = Utils.RuneWords[rid];
                    else name = "未知的" + this.Code.Trim();
                }


                if (this.Quality == ItemQuality.Magic)
                {
                    name = String.Format("{0}{1}{2}", suffix, prefix, name);
                }
                else
                {
                    name = String.Format("{0}{1}{2}", suffix, name, prefix);
                }
                if (this.IsPersonalized)
                {
                    name = String.Format("（{0}）{1}", this.PlayerName, name);
                }

                if (this.Code.Length >= 3 && this.Code.ToLower().StartsWith("r") && Char.IsNumber(this.Code[1]) && Char.IsNumber(this.Code[2]))
                {
                    name += "(#" + Convert.ToInt32(this.Code.Substring(1, 2)).ToString() + ")";
                    name = name.Replace("符文：", "");
                }

                return name;
            }
        }

        public string Icon
        {
            get
            {
                var code = this.Code.Trim();

                if (this.Quality == ItemQuality.Set)
                {
                    code = ExcelTxt.SetItemsTxt[(int)this.FileIndex]["index"].Value;
                }
                else if (this.Quality == ItemQuality.Unique)
                {
                    code = ExcelTxt.UniqueItemsTxt[(int)this.FileIndex]["index"].Value;
                }

                var newcode = code.Replace("'", "").Replace("-", "_");

                if (newcode.ToLower() == "cutthroat1") newcode = "cutthroat_1";
                var names = Regex.Split(newcode, @"(?<!^)(?=[A-Z])");

                if (names.Length > 1)
                {
                    newcode = "";
                    foreach (var name in names)
                    {
                        newcode += name.ToLower().Trim().Replace(" ", "_");
                        newcode += "_";
                    }
                    newcode = newcode.Remove(newcode.Length - 1, 1);
                }
                newcode = newcode.Replace("__", "_").Replace(" ", "_");
                //if (code == "McAuley's Paragon") code = "mc_auleys_paragon";
                //if (code == "McAuley's Taboo") code = "mc_auleys_taboo";
                //if (code == "McAuley's Superstition") code = "mc_auleys_superstition";
                //if (code == "McAuley's Riprap") code = "mc_auleys_riprap";


                if (D2SLib.Utils.ItemAssets.ContainsKey(newcode.ToLower())) return D2SLib.Utils.ItemAssets[newcode.ToLower()];
                else return "";

                //return D2SLib.Utils.ItemAssets[newcode.ToLower()];


            }
        }


        Dictionary<string, string> groupInfo = new Dictionary<string, string>();



        public string TypeName
        {
            get
            {
                //if (Utils.AllJsons.ContainsKey(this.Code.Trim())) return Utils.AllJsons[this.Code.Trim()];
                //else return this.Code.Trim();
                if (String.IsNullOrEmpty(this.typeCode)) return "Empty";
                //if (Utils.AllJsons.ContainsKey(this.typeCode)) return Utils.AllJsons[this.typeCode];

                var sub = Utils.MiniItemList.Where(m => m.SubTypeCode == this.typeCode).FirstOrDefault();
                if (sub != null) return sub.SubTypeName;
                else return this.typeCode;
            }
        }

        public string SocketItemsName
        {
            get
            {
                string ret = "";
                foreach (var item in this.SocketedItems)
                {
                    ret += item.Name.Replace("符文", "");
                    ret += " ";
                }

                return ret;
            }
        }

        private string typeCode;
        public string TypeCode
        {
            get
            {
                if (this.IsWeapon) typeCode = ExcelTxt.WeaponsTxt[this.Code]["type"].Value;
                else if (this.IsArmor) typeCode = ExcelTxt.ArmorTxt[this.Code]["type"].Value;
                else typeCode = ExcelTxt.MiscTxt[this.Code]["type"].Value;

                //if (!Utils.AllJsons.ContainsKey(typeCode.Trim()))
                //{
                //    this.typeCode = this.Code;
                //}
                return this.typeCode;
            }
        }

        public SolidBrush NameColor
        {
            get
            {
                if (this.IsRuneword) return Utils.ColorUnique;
                else if (this.Quality == ItemQuality.Magic) return Utils.ColorMagic;
                else if (this.Quality == ItemQuality.Set) return Utils.ColorSet;
                else if (this.Quality == ItemQuality.Rare) return Utils.ColorRare;
                else if (this.Quality == ItemQuality.Unique) return Utils.ColorUnique;
                else if (this.Quality == ItemQuality.Craft) return Utils.ColorCraft;
                else return Brushes.White as SolidBrush;
            }
        }

        public SolidBrush EnhancedColor
        {
            get
            {
                return Utils.ColorMagic;
            }
        }
        //public string EnhancedDescription
        //{
        //    get
        //    {
        //        groupInfo.Clear();
        //        List<TxtRow> rowlist = new List<TxtRow>();

        //        var ret = "";

        //        foreach(var group in this.StatLists)
        //        {
        //            foreach(var stat in group.Stats)
        //            {
        //                ret += GetDesc(stat);
        //                ret += "\r\n";

        //            }
        //        }

        //        if (this.TotalNumberOfSockets > 0)
        //        {
        //            ret += String.Format("凹槽({0})\r\n",this.TotalNumberOfSockets);
        //        }

        //        return ret;
        //    }
        //}



        private int formatCount;
        private string FormatReplacement(Match m)
        {
            string num = m.Groups["Number"].Value;
            string type = m.Groups["Type"].Value;

            return String.Concat("{", formatCount++, "}");
        }

        private string FormatCStyleStrings(string input)
        {
            formatCount = 0;
            input = input.Replace("%+d", "+%d").Replace("%-d", "-%d");

            string pattern = @"%(?<Number>\d+(\.\d+)?)?(?<Type>d|f|s)";
            string result = Regex.Replace(input, pattern, FormatReplacement);
            return result;
        }

        public string QualityName
        {
            get
            {
                if (this.Quality == 0) return "";
                return Utils.QualityList[(int)this.Quality - 1];
            }
        }

        public string QualityVersion
        {
            get
            {
                if (this.IsMisc) return "";

                TxtRow row = null;

                if (this.IsArmor) row = ExcelTxt.ArmorTxt[this.Code];
                if (this.IsWeapon) row = ExcelTxt.WeaponsTxt[this.Code];
                //misc.txt里面，没有normcode/ubercode/ultracode这三项了


                if (row["code"].Value == row["normcode"].Value) return Utils.AllJsons["item_normal"];
                if (row["code"].Value == row["ubercode"].Value) return Utils.AllJsons["item_exceptional"];
                if (row["code"].Value == row["ultracode"].Value) return Utils.AllJsons["item_elite"];

                return "";
            }
        }

        public string WeightDesc
        {
            get
            {
                if (!this.IsArmor) return "";

                int speed = ExcelTxt.ArmorTxt[this.Code]["speed"].ToInt32();
                if (speed == 0) return Utils.AllJsons["arm_light"];
                else if (speed == 5) return Utils.AllJsons["arm_medium"];
                else if (speed == 10) return Utils.AllJsons["arm_heavy"];
                else return speed.ToString();
            }
        }

        public int Speed
        {
            get
            {
                if (this.IsArmor) return ExcelTxt.ArmorTxt[this.Code]["speed"].ToInt32();
                else if (this.IsWeapon) return ExcelTxt.WeaponsTxt[this.Code]["speed"].ToInt32();
                else return ExcelTxt.MiscTxt[this.Code]["speed"].ToInt32();
            }
        }

        public string RunewordsDependency { get; set; }
        public Item Clone()
        {
            return Core.ReadItem(Core.WriteItem(this, 0x61), 0x61);
        }
    }

    public class ItemStatList
    {
        public List<ItemStat> Stats { get; set; } = new List<ItemStat>();

        public static ItemStatList Read(BitReader reader)
        {
            ItemStatList itemStatList = new ItemStatList();
            UInt16 id = reader.ReadUInt16(9);
            while (id != 0x1ff)
            {
                itemStatList.Stats.Add(ItemStat.Read(reader, id));
                //https://github.com/ThePhrozenKeep/D2MOO/blob/master/source/D2Common/src/Items/Items.cpp#L7332
                if (id == 52        //magicmindam
                    || id == 17     //item_maxdamage_percent
                    || id == 48     //firemindam
                    || id == 50)    //lightmindam
                {
                    itemStatList.Stats.Add(ItemStat.Read(reader, (UInt16)(id + 1)));
                }
                else if (id == 54  //coldmindam
                  || id == 57     //poisonmindam
                  )
                {
                    itemStatList.Stats.Add(ItemStat.Read(reader, (UInt16)(id + 1)));
                    itemStatList.Stats.Add(ItemStat.Read(reader, (UInt16)(id + 2)));
                }
                id = reader.ReadUInt16(9);
            }
            return itemStatList;
        }

        public static void Write(BitWriter writer, ItemStatList itemStatList)
        {
            int len = itemStatList.Stats.Count;
            for (int i = 0; i < itemStatList.Stats.Count; i++)
            {
                var stat = itemStatList.Stats[i];
                TxtRow property = ItemStat.GetStatRow(stat);
                UInt16 id = property["*ID"].ToUInt16();
                writer.WriteUInt16(id, 9);
                ItemStat.Write(writer, stat);

                //排好序后，不做假设，有啥就写啥。
                //assume these stats are in order...
                //https://github.com/ThePhrozenKeep/D2MOO/blob/master/source/D2Common/src/Items/Items.cpp#L7332
                if (id == 52        //magicmindam
                    || id == 17     //item_maxdamage_percent
                    || id == 48     //firemindam
                    || id == 50)    //lightmindam
                {
                    ItemStat.Write(writer, itemStatList.Stats[++i]);
                }
                else if (id == 54  //coldmindam,coldmaxdam,collength 54~56
                  || id == 57     //poisonmindam,poisonmaxdam,poisonlength,57~59
                  )
                {
                    ItemStat.Write(writer, itemStatList.Stats[++i]);
                    ItemStat.Write(writer, itemStatList.Stats[++i]);
                }
            }
            writer.WriteUInt16(0x1ff, 9);
        }

    }

    public class ItemStat
    {
        public UInt16? Id { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? Stat { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Int32? SkillTab { get; set; }
        public Int32? SkillId { get; set; }
        public Int32? SkillLevel { get; set; }
        public Int32? MaxCharges { get; set; }
        public Int32? Param { get; set; }
        public Int32 Value { get; set; }

        public static ItemStat Read(BitReader reader, UInt16 id)
        {
            ItemStat itemStat = new ItemStat();
            TxtRow property = ExcelTxt.ItemStatCostTxt[id];
            if (property == null)
            {
                throw new Exception($"No ItemStatCost record found for id: {id} at bit {reader.Position - 9}");
            }
            itemStat.Id = id;
            itemStat.Stat = property["Stat"].Value;
            Int32 saveParamBitCount = property["Save Param Bits"].ToInt32();
            Int32 encode = property["Encode"].ToInt32();
            if (saveParamBitCount != 0)
            {
                Int32 saveParam = reader.ReadInt32(saveParamBitCount);
                //todo is there a better way to identify skill tab stats.
                switch (property["descfunc"].ToInt32())
                {
                    case 14: //+[value] to [skilltab] Skill Levels ([class] Only) : stat id 188
                        itemStat.SkillTab = saveParam & 0x7;
                        itemStat.SkillLevel = (saveParam >> 3) & 0x1fff;
                        break;
                    default:
                        break;
                }
                switch (encode)
                {
                    case 2: //chance to cast skill
                    case 3: //skill charges
                        itemStat.SkillLevel = saveParam & 0x3f;
                        itemStat.SkillId = (saveParam >> 6) & 0x3ff;
                        break;
                    case 1:
                    case 4: //by times
                    default:
                        itemStat.Param = saveParam;
                        break;
                }
            }
            Int32 saveBits = reader.ReadInt32(property["Save Bits"].ToInt32());
            saveBits -= property["Save Add"].ToInt32();
            switch (encode)
            {
                case 3: //skill charges
                    itemStat.MaxCharges = (saveBits >> 8) & 0xff;
                    itemStat.Value = saveBits & 0xff;
                    break;
                default:
                    itemStat.Value = saveBits;
                    break;
            }
            return itemStat;
        }

        public static void Write(BitWriter writer, ItemStat stat)
        {
            TxtRow property = GetStatRow(stat);
            if (property == null)
            {
                throw new Exception($"No ItemStatCost record found for id: {stat.Id}");
            }
            Int32 saveParamBitCount = property["Save Param Bits"].ToInt32();
            Int32 encode = property["Encode"].ToInt32();
            if (saveParamBitCount != 0)
            {
                if (stat.Param != null)
                {
                    writer.WriteInt32((Int32)stat.Param, saveParamBitCount);
                }
                else
                {
                    Int32 saveParamBits = 0;
                    switch (property["descfunc"].ToInt32())
                    {
                        case 14: //+[value] to [skilltab] Skill Levels ([class] Only) : stat id 188
                            saveParamBits |= (stat.SkillTab ?? 0 & 0x7);
                            saveParamBits |= ((stat.SkillLevel ?? 0 & 0x1fff) << 3);
                            break;
                        default:
                            break;
                    }
                    switch (encode)
                    {
                        case 2: //chance to cast skill
                        case 3: //skill charges
                            saveParamBits |= (stat.SkillLevel ?? 0 & 0x3f);
                            saveParamBits |= ((stat.SkillId ?? 0 & 0x3ff) << 6);
                            break;
                        case 4: //by times
                        case 1:
                        default:
                            break;
                    }
                    //always use param if it is there.
                    if (stat.Param != null)
                    {
                        saveParamBits = (Int32)stat.Param;
                    }
                    writer.WriteInt32(saveParamBits, saveParamBitCount);
                }
            }
            Int32 saveBits = stat.Value;
            saveBits += property["Save Add"].ToInt32();
            switch (encode)
            {
                case 3: //skill charges
                    saveBits &= 0xff;
                    saveBits |= ((stat.MaxCharges ?? 0 & 0xff) << 8);
                    break;
                default:
                    break;
            }
            writer.WriteInt32(saveBits, property["Save Bits"].ToInt32());
        }
        public static TxtRow GetStatRow(ItemStat stat)
        {
            if (stat.Id != null)
            {
                return ExcelTxt.ItemStatCostTxt[(UInt16)stat.Id];
            }
            else
            {
                return ExcelTxt.ItemStatCostTxt[(string)stat.Stat];
            }
        }

        public static TxtRow GetStatRow(UInt16 id)
        {
            return ExcelTxt.ItemStatCostTxt[id];
        }
    }
}
