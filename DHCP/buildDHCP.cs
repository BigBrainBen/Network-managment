using System;
using System.Collections.Generic;
using System.Text;

namespace DHCP
{
    class buildDHCP
    {
        private string network;
        private string subnetMask;
        private string broadcast;
        private int first, second, third;
        private int[] pool; 

        public buildDHCP(int num)
        {
            pool = new int[num]; 
            this.setPool();
        }
        public string getNetworkIP()
        {
            return this.network;
        }
        public void setFirst(int num)
        {
            this.first = num;
        }
        public void setSecond(int num)
        {
            this.second = num;

        }
        public void setThird(int num)
        {
            this.third = num;
        }
        public int getFirst()
        {
            return this.first;
        }
        public int getSecond()
        {
            return this.second;
        }
        public int getThird()
        {
            return this.third;
        }
        public void setNetwork(int subnetMask)
        {
            string firstInt = this.first.ToString();
            string secondInt = this.second.ToString();
            string thirdInt = this.third.ToString();
            this.network = firstInt + "." + secondInt + "." + thirdInt + "." + "0";
            this.subnetMask = "255.255.255." + subnetMask.ToString();
            this.broadcast = this.network.Remove(this.network.Length - 1, 1) + (255 - subnetMask).ToString();
        }
        public string getSubnetMask()
        {
            return this.subnetMask;
        }
        public string getBroadcast()
        {
            return this.broadcast;
        }
        private void setPool()
        {
            int num = 1;
            for(int i = 0; i < this.pool.Length; i++, num++)
            {
                this.pool[i] = num;
            }

        }
        public int getIP()
        {
      
            int Byte;
            int index;
            Random rnd = new Random();
            while (true)
            {
                index = rnd.Next(0, pool.Length);
                if (this.pool[index] != 0)
                {
                    Byte = this.pool[index];                  
                    this.pool[index] = 0;
                    return Byte;
                }
            }
        }
        public void restoreIPToPool(Host host)
        {
            int num = host.getForthByte();
            for (int i = 0; i < this.pool.Length; i++)
            {
                if (this.pool[i] == 0)
                {
                    this.pool[i] = num;
                    return;
                }
            }
        }

        public bool poolEmpty()
        {
            for(int i = 0; i < this.pool.Length; i++)
            {
                if (this.pool[i] != 0)
                {
                    return false;
                }
            }
            return true;
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
