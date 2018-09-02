using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subnetter
{
    class Subnetwork
    {
        // network address and broadcast address is invalid
        public static readonly int InvalidHostAddressesPerNetwork = 2;

        public int NetworkSuffix
        {
            get
            {
                return SubnetMask.SubnetSpread * SubnetIndex;
            }
        }

        public string NetworkBits
        {
            get
            {
                string subaddress_binary = Address32.ToBinary((byte)NetworkSuffix);
                return subaddress_binary.Substring(0,
                    SubnetMask.SubnetPrefixLength - SubnetMask.NetworkPrefixLength);
            }
        }

        public int FirstHostSubAddress
        {
            get
            {
                return NetworkSuffix + 1;
            }
        }

        public int LastHostSubAddress
        {
            get
            {
                return (NetworkSuffix + SubnetMask.SubnetSpread) - 
                    InvalidHostAddressesPerNetwork;
            }
        }


        public readonly SubnetMask SubnetMask;
        public readonly int SubnetIndex;
        public Subnetwork(SubnetMask mask, int subnet_index)
        {
            this.SubnetMask = mask;
            this.SubnetIndex = subnet_index;

            if (subnet_index >= mask.SubnetCount) throw new ArgumentException("subnet_index too large");
        }

        public override string ToString()
        {
            return SubnetMask.NetworkPrefix +
                NetworkSuffix +
                SubnetMask.HostSuffix;
        }
    }
}
