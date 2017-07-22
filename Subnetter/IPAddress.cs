namespace Subnetter
{
    class IPAddress : Address32
    {
        public IPAddress(string DottedDecimal)
            : base(DottedDecimal)
        { }

        public IPAddress(byte a, byte b, byte c, byte d)
            : base(a, b, c, d)
        { }

        public IPAddress() : base() { }

        public string Class
        {
            get
            {
                string _Binary = Binary;

                return _Binary.StartsWith("0") ? "A" :
                    _Binary.StartsWith("10") ? "B" :
                    _Binary.StartsWith("110") ? "C" :
                    _Binary.StartsWith("1110") ? "D" :
                    _Binary.StartsWith("1111") ? "E" : null;
            }
        }

        public bool IsPrivateClassA
        {
            get
            {
                // Class A private is 10.0.0.0 to 10.255.255.255
                return IPBytes[0] == 10;
            }
        }

        public bool IsPrivateClassB
        {
            get
            {
                // Class B private is 172.16.0.0 to 172.31.255.255
                return IPBytes[0] == 172 && (16 <= IPBytes[1] && IPBytes[1] <= 31);
            }
        }

        public bool IsPrivateClassC
        {
            get
            {
                // Class C private is 192.168.0.0 to 192.168.255.255
                return IPBytes[0] == 192 && IPBytes[1] == 168;
            }
        }

        public bool IsPrivate
        {
            get
            {
                return IsPrivateClassA || IsPrivateClassB || IsPrivateClassC;
            }
        }

        public SubnetMask DefaultSubnet
        {
            get
            {
                string _Class = Class;
                return _Class == "A" ? new SubnetMask(255, 0, 0, 0) :
                    _Class == "B" ? new SubnetMask(255, 255, 0, 0) :
                    _Class == "C" ? new SubnetMask(255, 255, 255, 0) :
                    new SubnetMask(0, 0, 0, 0);
            }
        }

        public string Info
        {
            get
            {
                string s = "Info for " + DottedBytes + ":\n" +
                   DottedBinary + "\n" +
                   "Class: " + Class + "\n" +
                   "Usability: ";

                if (IsPrivate)
                {
                    s += "Private";
                }
                else
                {
                    s += "Public";
                }
                s += "\nDefault subnet mask: " + DefaultSubnet.DottedBytes;
                return s;
            }
        }
    }
}
