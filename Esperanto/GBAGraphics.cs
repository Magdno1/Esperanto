using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Esperanto
{
    public static class GBAGraphics
    {
        static byte[] defaultPalette = new byte[]
        {
            0x00, 0x00, 0xFF, 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        private static byte[] ConvertPalette(byte[] gbaPalette, int offset)
        {
            byte[] outputPalette = new byte[16 * 4];

            for (int i = 0, j = 0; (i < gbaPalette.Length && j < outputPalette.Length); i += 2, j += 4)
            {
                ushort color = BitConverter.ToUInt16(gbaPalette, offset + i);
                byte r = (byte)(((color >> 0) & 0x001F) << 3);
                byte g = (byte)(((color >> 5) & 0x001F) << 3);
                byte b = (byte)(((color >> 10) & 0x001F) << 3);

                outputPalette[j] = b;
                outputPalette[j + 1] = g;
                outputPalette[j + 2] = r;
                outputPalette[j + 3] = 0xFF;
            }

            return outputPalette;
        }

        private static BitmapData BeginPixelAccess(Bitmap bitmap, out byte[] pixelData)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            pixelData = new byte[bmpData.Stride * bmpData.Height];
            Marshal.Copy(bmpData.Scan0, pixelData, 0, pixelData.Length);
            return bmpData;
        }

        private static void FinishPixelAccess(Bitmap bitmap, BitmapData bmpData, byte[] pixelData)
        {
            Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length);
            bitmap.UnlockBits(bmpData);
        }

        public static Bitmap GetBitmap(byte[] tileData, int tileOffset, int widthInTiles, int heightInTiles)
        {
            return GetBitmap(tileData, tileOffset, defaultPalette, 0, widthInTiles, heightInTiles);
        }

        public static Bitmap GetBitmap(byte[] tileData, int tileOffset, byte[] paletteData, int paletteOffset, int widthInTiles, int heightInTiles)
        {
            byte[] convPalette = ConvertPalette(paletteData, paletteOffset);
            byte[] pixelData;

            Bitmap bitmap = new Bitmap(widthInTiles * 8, heightInTiles * 8, PixelFormat.Format32bppArgb);
            BitmapData bmpData = BeginPixelAccess(bitmap, out pixelData);

            for (int globalY = 0; globalY < bitmap.Height; globalY += 8)
            {
                for (int globalX = 0; globalX < bitmap.Width; globalX += 8)
                {
                    for (int tileY = 0; tileY < 8; tileY++)
                    {
                        for (int tileX = 0; tileX < 8; tileX += 2)
                        {
                            if (tileOffset >= tileData.Length) continue;

                            for (int i = 0; i < 2; i++)
                            {
                                byte pixel = (byte)((tileData[tileOffset] >> (i * 4)) & 0x0F);

                                int pixelDestOffset = ((((globalY + tileY) * bmpData.Width) + (globalX + tileX + i)) * 4);
                                Buffer.BlockCopy(convPalette, (pixel * 4), pixelData, pixelDestOffset, 4);
                            }

                            tileOffset++;
                        }
                    }
                }
            }

            FinishPixelAccess(bitmap, bmpData, pixelData);

            return bitmap;
        }

        public static Bitmap GetMMZFont(byte[] tileData, int tileOffset, byte[] paletteData, int paletteOffset, bool highlighted)
        {
            byte[] convPalette = ConvertPalette(paletteData, paletteOffset);
            byte[] pixelData;

            Bitmap bitmap = new Bitmap(128, 512, PixelFormat.Format32bppArgb);
            BitmapData bmpData = BeginPixelAccess(bitmap, out pixelData);

            for (int globalY = 0; globalY < bitmap.Height; globalY += 8)
            {
                for (int globalX = 0; globalX < bitmap.Width; globalX += 8)
                {
                    for (int tileY = 0; tileY < 8; tileY++)
                    {
                        for (int tileX = 0; tileX < 8; tileX += 2)
                        {
                            if (tileOffset >= tileData.Length) continue;

                            for (int i = 0; i < 2; i++)
                            {
                                byte pixel = (byte)((tileData[tileOffset] >> (i * 4)) & 0x0F);

                                if (highlighted) { pixel *= 4; pixel &= 0x0F; }

                                int pixelDestOffset = ((((globalY + tileY) * bmpData.Width) + (globalX + tileX + i)) * 4);
                                Buffer.BlockCopy(convPalette, (pixel * 4), pixelData, pixelDestOffset, 4);
                            }

                            tileOffset++;
                        }
                    }
                }
            }

            FinishPixelAccess(bitmap, bmpData, pixelData);

            return bitmap;
        }
    }
}
