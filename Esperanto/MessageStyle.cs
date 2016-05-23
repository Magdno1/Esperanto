using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Esperanto
{
    public class MessageStyle
    {
        public const int BaseOffset = 0x7E0000;
        public const int SizeOf = 0x14;

        public uint TileDataOffset { get; private set; }
        public uint TileDataSize { get; private set; }
        public ushort Unknown0x08 { get; private set; }
        public ushort Unknown0x0A { get; private set; }
        public uint PaletteDataOffset { get; private set; }
        public uint PaletteDataSize { get; private set; }

        public byte[] TileData { get; private set; }
        public byte[] PaletteData { get; private set; }

        public Bitmap StyleImage { get; private set; }
        public Bitmap StyleImageFlipped { get; private set; }

        public MessageStyle(byte[] romData, int offset)
        {
            TileDataOffset = (uint)(offset + BitConverter.ToUInt32(romData, offset));
            TileDataSize = BitConverter.ToUInt32(romData, offset + 0x04);
            Unknown0x08 = BitConverter.ToUInt16(romData, offset + 0x08);
            Unknown0x0A = BitConverter.ToUInt16(romData, offset + 0x0A);
            PaletteDataOffset = (uint)(offset + 0x0C + BitConverter.ToUInt32(romData, offset + 0x0C));
            PaletteDataSize = BitConverter.ToUInt32(romData, offset + 0x10);

            TileData = new byte[TileDataSize];
            Buffer.BlockCopy(romData, (int)TileDataOffset, TileData, 0, TileData.Length);

            PaletteData = new byte[PaletteDataSize];
            Buffer.BlockCopy(romData, (int)PaletteDataOffset, PaletteData, 0, PaletteData.Length);

            if (Unknown0x08 == 0x0040 && Unknown0x0A == 0x0020)
            {
                StyleImage = GBAGraphics.GetBitmap(TileData, 0, PaletteData, 0, 8, 8);
                StyleImageFlipped = (StyleImage.Clone() as Bitmap);
                StyleImageFlipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }
    }
}
