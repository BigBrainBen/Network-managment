using System;
using System.Collections.Generic;
using System.Text;

namespace DHCP
{
    class Network
    {
        Host[] hosts;
        buildDHCP myDHCP;
        string network, subnetMask, broadcast;

        public Network(int num,string network, string subnetmask, string broadcast,buildDHCP myDHCP)
        {
            this.myDHCP = myDHCP;
            hosts = new Host[num];
            this.network = network;
            this.subnetMask = subnetmask;
            this.broadcast = broadcast;
        }
        public void insertHostToList(Host host)
        {
            for (int i = 0; i < hosts.Length; i++)
            {
                if (hosts[i] == null)
                {
                    hosts[i] = host;
                    return;
                }
            }
            Console.WriteLine("pool is empty cant make more hosts");
        }

        public Host[] getHostList()
        {
            return this.hosts;
        }

        public string getNetworkIP()
        {
            return this.network;
        }
        public buildDHCP getDHCP()
        {
            return this.myDHCP;
        }

        public void deleteHostFromArray(int index)
        {

            this.hosts[index] = null;
        }

        public int hostIndexFromIP(int lastByte)//get the host from his IP
        {
            for (int i = 0; i < this.hosts.Length; i++) //passing the array
            {
                if (this.hosts[i] != null) //if there is an host in the array block
                {
                    if (lastByte == this.hosts[i].getForthByte()) //if host's last byte is the last byte we were searching for
                    {
                        return i; //returns the index in the host array of the host we searched for
                    }
                }
            }
            return -1;// if host not found return -1
        }

        public void printNetworkInfo()
        {

            Console.WriteLine("Network Address . . . . . . . . . : " + this.network);
            Console.WriteLine("Subnet Mask . . . . . . . . . . . : " + this.subnetMask);
            Console.WriteLine("Broadcast . . . . . . . . . . . . : " + this.broadcast);
            Console.WriteLine("");
        }
    }
}
