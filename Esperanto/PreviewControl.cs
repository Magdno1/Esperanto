using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esperanto
{
    public partial class PreviewControl : UserControl
    {
        RomHandler rom;
        Message currentMessage;

        public PreviewControl()
        {
            InitializeComponent();

            rom = null;
            currentMessage = null;

            System.Drawing.Text.InstalledFontCollection fontCollection = new System.Drawing.Text.InstalledFontCollection();
            FontFamily jpnFontFamily = fontCollection.Families.FirstOrDefault(x => x.Name == "Meiryo");
            if (jpnFontFamily != null) txtMessage.Font = new Font(jpnFontFamily, 9.0f);

            pbPreview.Paint += ((s, ev) =>
            {
                if (currentMessage != null)
                {
                    ev.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    ev.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    using (Bitmap img = rom.GetMessageImage(currentMessage, vsbPages.Value))
                    {
                        ev.Graphics.DrawImage(img, new Rectangle(0, 0, img.Width * 2, img.Height * 2));
                    }
                }
            });

            vsbPages.ValueChanged += ((s, ev) =>
            {
                pbPreview.Refresh();
            });
        }

        public void Initialize(RomHandler rom)
        {
            this.rom = rom;
        }

        public void UpdateMessage(int bankIndex, int messageIndex)
        {
            if (rom == null) return;

            currentMessage = rom.MessageBanks[bankIndex].Messages[messageIndex];

            txtMessage.DataBindings.Clear();
            txtMessage.DataBindings.Add("Text", currentMessage, "Text", false, DataSourceUpdateMode.OnPropertyChanged);

            int numPages = currentMessage.Data.Count(x => x == 0xFD);
            vsbPages.Value = vsbPages.Minimum = 0;
            vsbPages.Maximum = numPages;

            gbContainer.Text = string.Format("Offset: 0x{0:X6}", currentMessage.Offset);
            pbPreview.Refresh();
        }
    }
}
