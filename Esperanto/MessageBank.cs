using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Esperanto
{
    public class MessageBank
    {
        public int Number { get; private set; }
        public List<Message> Messages { get; private set; }

        public MessageBank(byte[] romData, int bankNumber, uint textOffset, uint idxOffset, GameLanguage language)
        {
            Number = bankNumber;
            Messages = new List<Message>();

            while (idxOffset + (Messages.Count * 2) < textOffset)
            {
                Messages.Add(new Message(romData, (textOffset + BitConverter.ToUInt16(romData, (int)(idxOffset + (Messages.Count * 2)))), bankNumber, language));
            }
        }
    }
}
