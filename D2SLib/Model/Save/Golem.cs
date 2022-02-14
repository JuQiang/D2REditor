using D2SLib.IO;
using System;

namespace D2SLib.Model.Save
{
    public class Golem
    {
        public UInt16? Header { get; set; }
        public bool Exists { get; set; }
        public Item Item { get; set; }

        public static Golem Read(BitReader reader, UInt32 version)
        {
            Golem golem = new Golem();
            try
            {
                golem.Header = reader.ReadUInt16();
                golem.Exists = reader.ReadByte() == 1;
                if (golem.Exists)
                {
                    golem.Item = Item.Read(reader, version);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return golem;
            
        }

        public static byte[] Write(Golem golem, UInt32 version)
        {
            BitWriter writer = new BitWriter();
            byte[] data = null;
            try
            {

                writer.WriteUInt16(golem.Header ?? 0x666B);
                writer.WriteByte((byte)(golem.Exists ? 1 : 0));
                if (golem.Exists)
                {
                    writer.WriteBytes(Item.Write(golem.Item, version));
                }
                data= writer.ToArray();
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                writer.Dispose();
            }

            return data;
        }
    }
}
