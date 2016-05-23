using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Esperanto
{
    public class RomHandler
    {
        public string FileName { get; private set; }
        public GameLanguage Language { get; private set; }
        public MessageBank[] MessageBanks { get; private set; }
        public MessageStyle[] MessageStyles { get; private set; }

        public Bitmap FontImage { get; private set; }
        public Bitmap FontImageHighlighted { get; private set; }

        uint fontOffset, baseMessageTextOffset, baseMessageIdxOffset, bankXTextOffset, bankXIdxOffset;

        public RomHandler(string filename)
        {
            FileName = filename;

            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] romData = new byte[fileStream.Length];
                if (romData.Length != fileStream.Read(romData, 0, romData.Length))
                    throw new Exception("Data read mismatch");

                string gameId = Encoding.ASCII.GetString(romData, 0xAC, 4);

                switch (gameId)
                {
                    case "ARZJ":
                        fontOffset = FollowPointer(romData, 0xBC11C);
                        baseMessageTextOffset = FollowPointer(romData, 0xBC99C);
                        baseMessageIdxOffset = FollowPointer(romData, 0xBC9A0);
                        bankXTextOffset = FollowPointer(romData, 0xF440);
                        bankXIdxOffset = FollowPointer(romData, 0xF438);

                        Language = GameLanguage.Japanese;
                        break;

                    case "AZCE":
                        fontOffset = FollowPointer(romData, 0xBBFF8);
                        baseMessageTextOffset = FollowPointer(romData, 0xBC878);
                        baseMessageIdxOffset = FollowPointer(romData, 0xBC87C);
                        bankXTextOffset = FollowPointer(romData, 0xF44C);
                        bankXIdxOffset = FollowPointer(romData, 0xF444);

                        Language = GameLanguage.English;
                        break;

                    default:
                        throw new Exception(string.Format("Unknown ROM or game version '{0}'", gameId));
                }

                MessageBanks = new MessageBank[((baseMessageTextOffset - baseMessageIdxOffset) / 4) + 1];

                MessageBanks[0] = new MessageBank(romData, -1, bankXTextOffset, bankXIdxOffset, Language);
                for (int i = 1; i < MessageBanks.Length; i++)
                    MessageBanks[i] = new MessageBank(
                        romData,
                        i,
                        FollowPointer(romData, (uint)(baseMessageTextOffset + ((i - 1) * 4))),
                        FollowPointer(romData, (uint)(baseMessageIdxOffset + ((i - 1) * 4))),
                        Language);

                MessageStyles = new MessageStyle[BitConverter.ToUInt32(romData, MessageStyle.BaseOffset) / MessageStyle.SizeOf];
                for (int i = 0; i < MessageStyles.Length; i++) MessageStyles[i] = new MessageStyle(romData, (MessageStyle.BaseOffset + (i * MessageStyle.SizeOf)));

                FontImage = GBAGraphics.GetMMZFont(romData, (int)fontOffset, romData, (int)(fontOffset - 0x20), false);
                FontImageHighlighted = GBAGraphics.GetMMZFont(romData, (int)fontOffset, romData, (int)(fontOffset - 0x20), true);
            }
        }

        private uint FollowPointer(byte[] data, uint pointerOffset)
        {
            uint pointer = BitConverter.ToUInt32(data, (int)pointerOffset);
            return (pointer & 0xFFFFFF);
        }

        private void GetPageInformation(byte[] array, int page, out int start, out byte style)
        {
            style = 0;
            start = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0xF3)
                    style = array[i + 1];

                if (array[i] == 0xFD)
                {
                    page--;
                    if (page <= 0)
                    {
                        start = (page == -1) ? -1 : i;
                        if (array[start + 1] == 0xF3)
                            style = array[start + 2];
                        return;
                    }
                }
            }
        }

        private Bitmap CreateTextboxImage(byte style, ref int leftBound, ref int rightBound)
        {
            Bitmap image;

            if (style == 0 || style == 1)
                image = new Bitmap(28 * 8, 16);
            else
                image = new Bitmap(28 * 8, (4 * 2) * 8);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.Black);

                if (style >= 0x04)
                {
                    if (style % 2 == 0)
                    {
                        leftBound = 8 * 8;
                        rightBound = 28 * 8;

                        g.DrawImageUnscaled(MessageStyles[style / 2].StyleImage, 0, 0);
                    }
                    else
                    {
                        leftBound = 0;
                        rightBound = 20 * 8;

                        g.DrawImageUnscaled(MessageStyles[(style - 1) / 2].StyleImageFlipped, rightBound, 0);
                    }
                }
            }

            return image;
        }

        public Bitmap GetMessageImage(Message msg)
        {
            List<Bitmap> allImages = new List<Bitmap>();
            int numPages = msg.Data.Count(x => x == 0xFD);

            for (int i = 0; i <= numPages; i++)
                allImages.Add(GetMessageImage(msg, i));

            if (allImages.Count == 0)
                return new Bitmap(32, 32);

            Bitmap image = new Bitmap(allImages.Max(x => x.Width), allImages.Sum(x => x.Height));
            using (Graphics g = Graphics.FromImage(image))
            {
                for (int i = 0, y = 0; i < allImages.Count; i++)
                {
                    g.DrawImageUnscaled(allImages[i], 0, y);
                    y += allImages[i].Height;
                }
            }
            return image;
        }

        public Bitmap GetMessageImage(Message msg, int pg)
        {
            int leftBound = 0, rightBound = 28 * 8;

            int start = -1;
            byte style = 0;
            bool hilight = false;

            GetPageInformation(msg.Data, pg, out start, out style);

            if (msg.BankNumber == -1)
                style = 3;

            Bitmap image = CreateTextboxImage(style, ref leftBound, ref rightBound);

            using (Graphics g = Graphics.FromImage(image))
            {
                int px = leftBound, py = 0;
                for (int i = start + 1; i < msg.Data.Length; i++)
                {
                    if (msg.Data[i] < 0xF0 && px < rightBound)
                    {
                        int sx = (msg.Data[i] & 0xF) * 8;
                        int sy = (msg.Data[i] >> 4) * 16;
                        if (!hilight)
                            g.DrawImage(FontImage, new Rectangle(px, py, 8, 16), sx, sy, 8, 16, GraphicsUnit.Pixel);
                        else
                            g.DrawImage(FontImageHighlighted, new Rectangle(px, py, 8, 16), sx, sy, 8, 16, GraphicsUnit.Pixel);
                        px += 8;
                    }
                    else if (msg.Data[i] == 0xF0 && px < rightBound)
                    {
                        int sx = (msg.Data[i + 1] & 0xF) * 8;
                        int sy = 240 + ((msg.Data[i + 1] >> 4) * 16);
                        if (!hilight)
                            g.DrawImage(FontImage, new Rectangle(px, py, 8, 16), sx, sy, 8, 16, GraphicsUnit.Pixel);
                        else
                            g.DrawImage(FontImageHighlighted, new Rectangle(px, py, 8, 16), sx, sy, 8, 16, GraphicsUnit.Pixel);
                        px += 8;
                        i++;
                    }
                    else if (msg.Data[i] == 0xF1)
                    {
                        hilight = false;
                    }
                    else if (msg.Data[i] == 0xF2)
                    {
                        hilight = true;
                    }
                    else if (msg.Data[i] == 0xF3 || msg.Data[i] == 0xF4 || msg.Data[i] == 0xF6 || msg.Data[i] == 0xF8)
                    {
                        i++;
                    }
                    else if (msg.Data[i] == 0xFC)
                    {
                        px = leftBound;
                        py += 16;
                    }
                    else if (msg.Data[i] == 0xFD || msg.Data[i] == 0xFE)
                    {
                        break;
                    }
                }
            }

            return image;
        }
    }
}
