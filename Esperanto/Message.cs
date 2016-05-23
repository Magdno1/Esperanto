using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esperanto
{
    public class Message
    {
        public uint Offset { get; private set; }
        public int BankNumber { get; private set; }
        public GameLanguage Language { get; private set; }

        public byte[] Data { get; private set; }
        public string Text { get { return StringHandler.GetString(Data, Language); } }

        public Message(byte[] romData, uint offset, int bankNumber, GameLanguage language)
        {
            Offset = offset;
            BankNumber = bankNumber;
            Language = language;

            int until = Array.IndexOf(romData, (byte)0xFF, (int)offset) + 1;
            Data = new byte[until - offset];
            Buffer.BlockCopy(romData, (int)offset, Data, 0, Data.Length);
        }
    }
}
