using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.IO;

namespace D2SLib
{
    public class Core
    {
        public static D2S ReadD2S(string path)
        {
            FileInfo fi = new FileInfo(path);
            var d2s = D2S.Read(File.ReadAllBytes(path));
            d2s.FileName = fi.Name.Replace(fi.Extension, "");
            d2s.SaveFileName = path;

            return d2s;
        }

        //public static D2S ReadD2S(byte[] bytes)
        //{
        //    return D2S.Read(bytes);
        //}

        public static Item ReadItem(string path, UInt32 version)
        {
            return ReadItem(File.ReadAllBytes(path), version);
        }

        public static Item ReadItem(byte[] bytes, UInt32 version)
        {
            return Item.Read(bytes, version);
        }

        public static D2I ReadD2I(string path, UInt32 version)
        {
            return ReadD2I(File.ReadAllBytes(path), version);
        }

        public static D2I ReadD2I(byte[] bytes, UInt32 version)
        {
            return D2I.Read(bytes, version);
        }

        public static List<D2I> ReadD2I2(string path, UInt32 version)
        {
            return ReadD2I2(File.ReadAllBytes(path), version);
        }

        public static List<D2I> ReadD2I2(byte[] bytes, UInt32 version)
        {
            return D2I.Read2(bytes, version);
        }

        public static byte[] WriteD2S(D2S d2s)
        {
            return D2S.Write(d2s);
        }

        public static byte[] WriteItem(Item item, UInt32 version)
        {
            return Item.Write(item, version);
        }

        public static byte[] WriteD2I(D2I d2i, UInt32 version)
        {
            return D2I.Write(d2i, version);
        }

    }
}
