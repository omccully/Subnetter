using System;

namespace Subnetter
{
    class Address32
    {
        public byte[] IPBytes { get; set; }

        public Address32(string DottedDecimal)
        {
            SetAddress(DottedDecimal);
        }
        public Address32(byte a, byte b, byte c, byte d)
        {
            SetAddress(a, b, c, d);
        }
        public Address32() {  }

        public void SetAddress(string DottedDecimal)
        {
            if (DottedDecimal.Split('.').Length == 4)
            {
                IPBytes = DottedIPToBytes(DottedDecimal);
            }
            else
            {
                throw new FormatException();
            }
        }
        public void SetAddress(byte a, byte b, byte c, byte d)
        {
            IPBytes = new byte[4];
            IPBytes[0] = a;
            IPBytes[1] = b;
            IPBytes[2] = c;
            IPBytes[3] = d;
        }

        public string Binary
        {
            get
            {
                string s = null;
                foreach (byte b in IPBytes)
                {
                    s += ToBinary(b);
                }
                return s;
            }
        }

        public string DottedBinary
        {
            get
            {
                string s = null;
                foreach (byte b in IPBytes)
                {
                    s += "." + ToBinary(b);
                }
                return s.Substring(1);
            }
        }

        public string DottedBytes
        {
            get { return BytesToDottedIP(IPBytes); }
        }

        public override string ToString()
        {
            return DottedBytes;
        }

        #region Static Methods
        public static byte[] DottedIPToBytes(string s)
        {
            byte[] IP = new byte[4];
            int i = 0;
            foreach (string t in s.Split('.'))
            {
                IP[i] = ToByte(t);
                i++;
            }
            return IP;
        }

        public static string BytesToDottedIP(byte[] bs)
        {
            string s = null;
            int index = 0;
            foreach (byte b in bs)
            {
                s += b.ToString();
                if (index < 3)
                    s += ".";
                index++;
            }
            return s;
        }

        public static byte ToByte(string input)
        {
            if (input.Length == 8)
                return Convert.ToByte(input, 2);
            else if (input.Length <= 3 && input.Length >= 0)
                return Byte.Parse(input);
            else
                return 0;
        }

        public static string ToBinary(byte input)
        {
            string Last = "00000000" + Convert.ToString(input, 2);
            return Last.Substring(Last.Length - 8);
        }
        #endregion
    }
}
