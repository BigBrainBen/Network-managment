using System;
using System.Collections.Generic;
using System.Text;

namespace DHCP
{
    class Host
    {
        private string network , broadcast , subnetMask , hostIP;
        private int forthByte;  
        private buildDHCP DHCPserver;

        public Host(buildDHCP DHCPserver, Network hostArray)
        {
            this.DHCPserver = DHCPserver;
            this.network = DHCPserver.getNetworkIP();
            this.setForthByte(DHCPserver.getIP());// sets his forth byte
            this.setHostIP();
            this.subnetMask = DHCPserver.getSubnetMask();
            this.broadcast = DHCPserver.getBroadcast();// changes the last byte of the network to 255-last byte of subnet mask
            hostArray.insertHostToList(this);// inserts the host into the hosts array
        }

        private void setForthByte(int Byte)
        {
            this.forthByte = Byte;
        }
        private void setHostIP()
        {
            this.hostIP = this.network.Remove(this.network.Length - 1, 1)+forthByte.ToString();
        }
        public int getForthByte()
        {
            return this.forthByte;
        }

        public void printHostInfo()
        {
            Console.WriteLine("IPv4 Address. . . . . . . . . . . : " + this.hostIP);
            Console.WriteLine("Network Address . . . . . . . . . : " + this.network);
            Console.WriteLine("Subnet Mask . . . . . . . . . . . : " + this.subnetMask);
            Console.WriteLine("Broadcast . . . . . . . . . . . . : " + this.broadcast);
            Console.WriteLine("");

        }



    }
}
