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

        public string NetworkPrefix
        {
            get
            {
                switch(NetworkPrefixLength)
                {
                    case 0:
                        return "";
                    case 8:
                        return "NNN.";
                    case 16:
                        return "NNN.NNN.";
                    case 24:
                        return "NNN.NNN.NNN.";
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public string HostSuffix
        {
            get
            {
                switch (NetworkPrefixLength)
                {
                    case 0:
                        return ".HHH.HHH.HHH";
                    case 8:
                        return ".HHH.HHH";
                    case 16:
                        return ".HHH";
                    case 24:
                        return "";
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public int HostsPerSubnet
        {
            get
            {
                // network address and broadcast address is invalid
                const int InvalidHostAddressesPerNetwork = 2;

                return (int)Math.Pow(2, 32 - SubnetPrefixLength) - InvalidHostAddressesPerNetwork;
            }
        }

        public int SubnetCount
        {
            get
            {
                return (int)Math.Pow(2, SubnetPrefixLength - NetworkPrefixLength);
                // don't count it as a subnet if it's a classful mask
                //return sc == 1 ? 0 : sc; 
            }
        }

        Subnetwork[] _subnetworks;
        public Subnetwork[] Subnetworks
        {
            get
            {
                if (_subnetworks != null) return _subnetworks;

                _subnetworks = new Subnetwork[SubnetCount];

                for(int i = 0; i < SubnetCount; i++)
                {
                    _subnetworks[i] = new Subnetwork(this, i);
                }

                return _subnetworks;
            }
        }

        public int SubnetSpread
        {
            get
            {
                return (int)Math.Pow(2, Math.Abs(SubnetPrefixLength - NetworkPrefixLength - 8));
            }
        }

        public string Info
        {
            get
            {
                string s = "Info for " + DottedBytes + ":\n" +
                    DottedBinary + "\n" +
                    "Class: " + Class + "\n" +
                     HostsPerSubnet.ToString();

                if (SubnetCount > 1)
                {
                    s += " hosts per subnet\n" +
                        SubnetCount.ToString() + " subnetworks\n" +
                    "Possible subnet addresses:";

                    foreach(Subnetwork subnet in Subnetworks)
                    {
                        s += "\n\t" + subnet.NetworkBits +
                            "\t" + subnet.ToString() +
                            "\t" + subnet.FirstHostSubAddress + "-" + subnet.LastHostSubAddress;
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
