using D2SLib.IO;
using System;
using System.Collections.Generic;

namespace D2SLib.Model.Save
{
    public class D2I
    {

        public ItemList ItemList { get; set; }

        public static D2I Read(byte[] bytes, UInt32 version)
        {
            D2I d2i = new D2I();
            using (BitReader reader = new BitReader(bytes))
            {
                d2i.ItemList = ItemList.Read(reader, version);
                return d2i;
            }
        }

        public int Gold { get; set; }
        public static List<D2I> Read2(byte[] buf, UInt32 version)
        {
            List<D2I> list = new List<D2I>();

            //前0x40个字节固定: 55AA55AA0000，然后是版本，0x0C开头的四个字节，是大箱子的钱，上限是250万。0x10是该buf的总长度（含这个头本身）
            //下面就是4A4D开始的itemlist，与d2s格式一致。
            //第二个页，重复上面的格式

            int pos = 0;
            while (true)
            {
                int len = buf[0x10 + pos + 1] * 256 + buf[0x10 + pos + 0] - 0x40;
                var stash = new byte[len];
                Array.Copy(buf, pos + 0x40, stash, 0, len);

                var d2i = D2I.Read(stash, version);
                d2i.Gold = (buf[0x0f + pos] << 24) + (buf[0x0e + pos] << 16) + (buf[0x0d + pos] << 8) + (buf[0x0c + pos]);
                list.Add(d2i);

                pos += len + 0x40;
                if (pos >= buf.Length) break;
            }

            return list;
        }

        public static byte[] Write2(List<D2I> list, UInt32 version)
        {
            List<byte> bytes = new List<byte>();

            foreach (D2I d2i in list)
            {
                byte[] header = new byte[0x40];
                header[0] = 0x55; header[1] = 0xAA; header[2] = 0x55; header[3] = 0xAA;
                header[8] = (byte)version;
                header[0x0c] = 0xA0; header[0x0d] = 0x25; header[0x0e] = 0x26;


                var newbytes = D2I.Write(d2i, version);

                int len = 0x40 + newbytes.Length;
                header[0x10] = (byte)(len % 256); header[0x11] = (byte)(len / 256);

                bytes.AddRange(header);
                bytes.AddRange(newbytes);

            }

            return bytes.ToArray();
        }

        public static byte[] Write(D2I d2i, UInt32 version)
        {
            using (BitWriter writer = new BitWriter())
            {
                writer.WriteBytes(ItemList.Write(d2i.ItemList, version));
                return writer.ToArray();
            }
        }

    }
}
