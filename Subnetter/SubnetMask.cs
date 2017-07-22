using System;

namespace Subnetter
{
    class SubnetMask : Address32
    {
        public SubnetMask(string DottedDecimal)
            : base(DottedDecimal)
        { }

        public SubnetMask(byte a, byte b, byte c, byte d)
            : base(a, b, c, d)
        { }

        public SubnetMask() : base() { }

        public int SubnetPrefixLength
        {
            get
            {
                int temp = Binary.IndexOf('0');
                if (temp != Binary.Split('1').Length - 1)
                    throw new FormatException();
                return temp;
            }
        }
        public int NetworkPrefixLength
        {
            get
            {
                int SPL = SubnetPrefixLength;
                return SPL >= 24 ? 24 :
                    SPL >= 16 ? 16 :
                    SPL >= 8 ? 8 : 0;
            }
        }

        public string Class
        {
            get
            {
                switch (SubnetPrefixLength)
                {
                    case 8:
                        return "A";
                    case 16:
                        return "B";
                    case 24:
                        return "C";
                    default:
                        return "N/A";
                }
            }
        }

        public string Info
        {
            get
            {
                int SubnetCount = (int)Math.Pow(2, SubnetPrefixLength - NetworkPrefixLength);
                if (SubnetCount == 1)
                    SubnetCount = 0;

                string s = "Info for " + DottedBytes + ":\n" +
                    DottedBinary + "\n" +
                    "Class: " + Class + "\n" +
                     (Math.Pow(2, 32 - SubnetPrefixLength) - 2).ToString();


                if (SubnetCount != 0)
                {
                    s += " hosts per subnet\n" +
                        SubnetCount.ToString() + " subnetworks\n" +
                    "Possible subnet addresses:";

                    string NetworkPrefix = null;
                    string HostSuffix = null;

                    if (NetworkPrefixLength == 0)
                    {
                        NetworkPrefix = "";
                        HostSuffix = ".HHH.HHH.HHH";
                    }
                    else if (NetworkPrefixLength == 8)
                    {
                        NetworkPrefix = "NNN.";
                        HostSuffix = ".HHH.HHH";
                    }
                    else if (NetworkPrefixLength == 16)
                    {
                        NetworkPrefix = "NNN.NNN.";
                        HostSuffix = ".HHH";
                    }
                    else if (NetworkPrefixLength == 24)
                    {
                        NetworkPrefix = "NNN.NNN.NNN.";
                        HostSuffix = "";
                    }

                    int SubnetSpread = (int)Math.Pow(2, Math.Abs(SubnetPrefixLength - NetworkPrefixLength - 8));
                    for (int i = 0; i < 255; i += SubnetSpread)
                    {
                        s += "\n\t" + ToBinary((byte)i).Substring(0, SubnetPrefixLength - NetworkPrefixLength) +
                            "\t" + NetworkPrefix + i.ToString() + HostSuffix +
                            "\t" + (i + 1).ToString() + "-" + (i + SubnetSpread - 2).ToString();
                    }
                }
                else
                {
                    s += " hosts per network";
                }
                return s;
            }
        }
    }
}
