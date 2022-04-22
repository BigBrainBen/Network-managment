using System;
using System.Collections.Generic;

namespace DHCP
{
    class Program
    {
        static void Main(string[] args)
        {

            int userInput = -1;
            string userString="";
            List<Network> networksList = new List<Network>();
           // bool Predicate<Network>(Network obj);


            while (true) 
            {
                start:
                Console.WriteLine("\nEnter your choice");
                Console.WriteLine("1. Add network");
                Console.WriteLine("2. Print networks");
                Console.WriteLine("3. Enter a specific network");
                Console.WriteLine("4. Delete a network");
                Console.WriteLine("5. Clear screen");
                Console.WriteLine("6. Exit\n");
                while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 6 || userInput < 1)//checks if the choise is valid
                {
                    Console.WriteLine("Enter a valid choise.");                    
                }

                if (userInput == 1)
                {
                    addNetwork(networksList); //heavy algoritm- main program
                }

                if (userInput == 2) //prints all the networks
                {
                    Console.WriteLine("Current networks:\n");
                    foreach (Network i in networksList)
                    {
                        i.printNetworkInfo();
                    }
                    Console.WriteLine("\n\n");
                }

                if(userInput == 3)//enters the wanted network
                {
                    Console.WriteLine("Enter network address");
                    userString = Console.ReadLine();
                    foreach (Network i in networksList)
                    {
                        if(i.getNetworkIP() == userString)
                        {
                            while (NetworkSettings(i.getDHCP(), i)) ;
                            goto start;
                        }                    
                    }
                    Console.WriteLine("\nIP Adress of the network not found!");
                }

                if(userInput == 4)
                {
                    Console.WriteLine("Enter network address");
                    userString = Console.ReadLine();
                 //   if (networksList.Exists(Predicate<Network>);)
                  //  networksList.Exists(Predicate<Network>);
                    foreach (Network i in networksList)
                    {
                        if (i.getNetworkIP() == userString)
                        {
                            
                            networksList.Remove(i);
                            Console.WriteLine("\nNetwork " + userString + " deleted successfully!");
                            goto start;
                        }
                    }
                    Console.WriteLine("\nIP Adress of the network not found!");
                }

                if(userInput == 5)
                {
                    Console.Clear();
                }

                if (userInput == 6) Environment.Exit(0); //exits the program

            }
              
        }


        static void addNetwork(List<Network> networksList)
        {
            int userInput = -1, hostsNum, subnetMask;
            Console.WriteLine("How many hosts do you want in the network ?\n");
            while (!int.TryParse(Console.ReadLine(), out hostsNum) || hostsNum > 254 || hostsNum < 0)//checks if the number is valid
            {
                Console.WriteLine("Enter a valid hosts number.");
            }

            subnetMask = calcSubnetMask(hostsNum);//calcs the subnet mask from the amount of users
            buildDHCP myDHCP = new buildDHCP(hostsNum); //the DHCP server

            Console.WriteLine("Set the first Byte");
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 255 || userInput < 1)//checks if the number is valid
            {
                Console.WriteLine("Enter a valid byte number.");
            }

       
            myDHCP.setFirst(userInput);

            Console.WriteLine("Set the second Byte");
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 255 || userInput < 1)//checks if the number is valid
            {
                Console.WriteLine("Enter a valid byte number.");
            }

            myDHCP.setSecond(userInput);

            Console.WriteLine("Set the third Byte");
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 255 || userInput < 1)//checks if the input is valid
            {
                Console.WriteLine("Enter a valid byte number.");
            }

            myDHCP.setThird(userInput);

            myDHCP.setNetwork(subnetMask); //sets network from user inputs

            foreach (Network i in networksList)
            {
                if(i.getNetworkIP() == myDHCP.getNetworkIP())
                {
                    Console.WriteLine("This Network already exists!");
                    return;
                }
            }

            Network network = new Network(hostsNum, myDHCP.getNetworkIP(),myDHCP.getSubnetMask(),myDHCP.getBroadcast(),myDHCP); //represents the network


            networksList.Add(network);//adding the current host array to the list 

            while (NetworkSettings(myDHCP, network)) ;

        }
        static void networkMenu()
        {
            Console.WriteLine("\nNetwork Menu - Enter your choice");
            Console.WriteLine("1. Add a host");
            Console.WriteLine("2. Print hosts");
            Console.WriteLine("3. Print network information");
            Console.WriteLine("4. Delete a host");
            Console.WriteLine("5. Exit this network");
            Console.WriteLine("6. Clear screen");
            Console.WriteLine("7. Exit\n");
        }
        static bool NetworkSettings(buildDHCP myDHCP, Network network)
        {
            int userInput=-1;
            networkMenu();
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 7 || userInput < 1)//checks if the choise is valid
            {
                Console.WriteLine("Enter a valid choise.");
            }

            if (userInput == 1)
            {
                if (myDHCP.poolEmpty())
                {
                    Console.WriteLine("Can't make more hosts, IP pool is empty, please delete a host \n");
                    return true;
                }
                Host host = new Host(myDHCP, network);
            }

            if (userInput == 2) //prints info about hosts
            {
                Console.WriteLine("");
                for (int i = 0; i < network.getHostList().Length; i++) //passing all the array 
                {
                    if (network.getHostList()[i] != null) //untill getting to the needed host
                        network.getHostList()[i].printHostInfo(); //host.printInfo
                }
                return true;

            }

            if (userInput == 3)
            {
                myDHCP.printNetworkInfo();
                return true;
            }

            if (userInput == 4)
            {
                Console.WriteLine("Enter his last byte");

                while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 254 || userInput < 1)//checks if the input is valid
                {
                    Console.WriteLine("Enter a valid byte number.");
                }

                int hostIndex = network.hostIndexFromIP(userInput); //getting host index in the array from his IP. if IP not found return -1
                if (hostIndex == -1)    /** user inputs his last byte **/
                {
                    Console.WriteLine("\nIP doesnt exist ! \n");
                    return true;
                }
                myDHCP.restoreIPToPool(network.getHostList()[hostIndex]); //putting his last byte back in the pool
                network.deleteHostFromArray(hostIndex); //putting null in the host array
                Console.WriteLine("\nHost deleted successfully! \n ");

            }

            if (userInput == 5)
            {
                return false;
            }

            if(userInput == 6)
            {
                Console.Clear();
            }

            if (userInput == 7)
            {
                Environment.Exit(0);
            }
            return true;
        }
        static int calcSubnetMask(int hosts)
        {
            if (hosts == 0) return 255;

            double num = 0;
            for (double i = 0; num < hosts + 2; i++)
            {
                num = Math.Pow(2, i);
            }
            return 256 - Convert.ToInt32(num);
        }


    }
}
