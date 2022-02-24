using System;
using System.IO;
using System.Linq;

namespace Netizen.Data
{
    public enum DataFormat
    {
        Unknown,
        Bmp,
        Gif,
        Jpeg,
        Png,
    }

    public static class FileFormat
    {
        public static byte[] JpegHead = new byte[]{ 0xff, 0xd8, };
        public static byte[] BmpHead = new byte[] { 0x42, 0x4d, };
        public static byte[] GifHead = new byte[] { 0x47, 0x49, 0x46, };
        public static byte[] PngHead = new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a };

        public static DataFormat Determinate(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                int count = (int)Math.Min(8, fs.Length);
                byte[] head = new byte[count];
                int length = fs.Read(head, 0, count);
                if (MatchBytes(JpegHead, head))
                {
                    return DataFormat.Jpeg;
                }
                if (MatchBytes(GifHead, head))
                {
                    return DataFormat.Gif;
                }
                if (MatchBytes(BmpHead, head))
                {
                    return DataFormat.Bmp;
                }
                if (MatchBytes(PngHead, head))
                {
                    return DataFormat.Png;
                }
            }
            return DataFormat.Unknown;
        }

        public static bool MatchBytes(byte[] one, byte[] two)
        {
            if (two.Length < one.Length) return false;
            for (int i = 0; i < one.Length; ++i)
            {
                if (one[i] != two[i]) return false;
            }
            return true;
        }
    }
}