using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
//using ColorShift.Properties;

namespace D2REditor
{
    /*
 *   @Author: d1ck @ d2mods.com, phrozenkeep
 *	@Filename: D2Palette.cs
 *   @Last Edit: 5 July 2012
 *	@Description: Port of Paul Siramy's dc6color.c
 *	@Notes: main method uses dc6, palette, and color dat files, you can load them as embedded resources (byte[])
 *
 */

    public struct DC6_Header_S
    {
        public int version;        // 0x00000006
        public int sub_version;    // 0x00000001
        public int zeros;          // 0x00000000
        public int termination;    // 0xEEEEEEEE or 0xCDCDCDCD //BYTE ARRAY!
        public int directions;     // 0x000000xx
        public int frames_per_dir; // 0x000000xx
    }

    public struct DC6_FRAME_HEADER_S
    {
        public int flip;
        public int width;
        public int height;
        public int offset_x;
        public int offset_y; // from bottom border, NOT upper border
        public int zeros;
        public int next_block;
        public int length;
    }

    public class D2Palette
    {
        private Bitmap current { get; set; }
        private int shift_color { get; set; }
        private string file { get; set; }

        private int dc6_frame_ptr;
        private DC6_Header_S dc6_header;
        private DC6_FRAME_HEADER_S dc6_frame_header;

        private Color[] palette = new Color[256];
        private Color[] palette_shift = new Color[256];
        private byte[] colormap = new byte[256];
        private byte[,] dc6_indexed;

        private byte[] dc6_file;
        private byte[] act_file;
        private byte[] color_file;

        /// <summary>
        /// Initializes D2Palette Class
        /// Send byte arrays from embedded resource.
        /// Shift refers to a color index goes from black -> purple with reds/blues/greens inbetween
        /// </summary>
        /// <param name="byte[] dc6"></param>
        /// <param name="byte[] act"></param>
        /// <param name="byte[] color"></param>
        /// <param name="int shift"></param>		
        public D2Palette(byte[] dc6, byte[] act, byte[] color, int shift)
        {
            dc6_file = dc6;
            act_file = act;
            color_file = color;
            shift_color = shift;
        }

        public D2Palette(string dc6File)
        {
            dc6_file = File.ReadAllBytes(dc6File);
            act_file = File.ReadAllBytes(@"c:\temp\pal.dat");
            color_file = File.ReadAllBytes(@"c:\temp\invgrey.dat");
            shift_color = 0;
        }

        /// <summary>
        /// Applies the transformation and returns an Image
        /// </summary>			
        public Image Transform()
        {
            LoadHeader();
            IndexDC6();

            Bitmap bmp = new Bitmap(this.dc6_frame_header.width, this.dc6_frame_header.height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int index = this.dc6_indexed[i, j];

                    bmp.SetPixel(i, j, Color.FromArgb(255, act_file[index * 3 + 2], act_file[index * 3 + 1], act_file[index * 3]));
                }
            }

            LoadPalette();
            PaletteShift();

            bmp.MakeTransparent();
            //透明貌似不对，缺了不少东西

            //bmp.Save(Guid.NewGuid().ToString() + ".png");
            return bmp;
        }

        void LoadHeader()
        {
            long nb, s;

            int size = Marshal.SizeOf(typeof(DC6_Header_S));
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(dc6_file, 0, buffer, size);
                dc6_header = (DC6_Header_S)Marshal.PtrToStructure(buffer, typeof(DC6_Header_S));
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            nb = dc6_header.directions * dc6_header.frames_per_dir;
            s = sizeof(int) * nb;

            size = Marshal.SizeOf(s);
            buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(dc6_file, Marshal.SizeOf(typeof(DC6_Header_S)), buffer, size);
                dc6_frame_ptr = (int)Marshal.PtrToStructure(buffer, typeof(int));
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            size = Marshal.SizeOf(typeof(DC6_FRAME_HEADER_S));
            buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(dc6_file, dc6_frame_ptr, buffer, size);
                dc6_frame_header = (DC6_FRAME_HEADER_S)Marshal.PtrToStructure(buffer, typeof(DC6_FRAME_HEADER_S));
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        void IndexDC6()
        {
            DC6_FRAME_HEADER_S fh;
            long i, i2, pos;
            byte c2;
            int c, x, y;

            fh = dc6_frame_header;

            dc6_indexed = new byte[fh.width, fh.height];

            if ((fh.width <= 0) || (fh.height <= 0))
            {
                return;
            }

            dc6_indexed = new byte[(int)fh.width, (int)fh.height];

            pos = dc6_frame_ptr + 32;

            x = 0;
            y = (int)fh.height - 1;

            for (i = 0; i < fh.length; i++)
            {
                c = dc6_file[pos + i];
                //i++; // adding this line should solve the problem


                if (c == 0x80)
                {
                    x = 0;
                    y--;
                }
                else if ((c & 0x80) > 0)
                {
                    x += c & 0x7F;
                }
                else
                {
                    for (i2 = 0; i2 < c; i2++)
                    {
                        c2 = dc6_file[pos + i];
                        i++;
                        dc6_indexed[x, y] = c2;
                        x++;
                    }
                }
            }
        }

        void LoadPalette()
        {
            for (int i = 0; i < 256; i++)
            {
                Color from_index = Color.FromArgb(255, act_file[i * 3 + 2], act_file[i * 3 + 1], act_file[i * 3]);
                palette[i] = from_index;
            }

            for (int i = 0; i < 256; i++)
            {
                colormap[i] = color_file[shift_color * 256 + i];
                palette_shift[i] = palette[colormap[i]];
            }
        }

        void PaletteShift()
        {
            current = new Bitmap(dc6_frame_header.width, dc6_frame_header.height);

            for (int y = 0; y < dc6_frame_header.height; y++)
            {
                for (int x = 0; x < dc6_frame_header.width; x++)
                {
                    current.SetPixel(x, y, palette_shift[dc6_indexed[x, y]]);
                }
            }
        }
    }
}
