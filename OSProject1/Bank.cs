using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject1
{
    internal class Bank
    {
        private int[] resources;
        private List<int[]> clients = new List<int[]>();
        private int[,] maxClients, alloClients;
        public Bank()
        {
            this.resources = new int[1];
            resources[0] = 0;
            maxClients = new int[1, 1];
            alloClients = new int[1, 1];
        }
        public Bank(int r)
        {
            this.resources = new int[r];
            for (int i = 0; i < this.resources.Length; i++)
            {
                this.resources[i] = 10;
            }
            Console.WriteLine("This bank has 10 resources of " + r + " types.");
            maxClients = new int[1, r];
            alloClients = new int[1, r];
        }
        public void newClient(int[] max)
        {
            clients.Add(max);
            maxClients = new int[clients.Count, max.Length];
            alloClients = new int[clients.Count, max.Length];
            for (int i = 0; i < maxClients.GetLength(0); i++)
            {
                for (int j = 0; j < maxClients.GetLength(1); j++)
                {
                    maxClients[i, j] = clients.ElementAt(i)[j];
                    alloClients[i, j] = 0;
                }
            }
        }

        public void requestResource(int id, int[] req, int[] allo)
        {
            bool available = true;
            int[,] needClients = new int[maxClients.GetLength(0), maxClients.GetLength(1)];
            for (int i = 0; i < needClients.GetLength(0); i++)
            {
                for (int j = 0; j < needClients.GetLength(1); j++)
                {
                    needClients[i, j] = maxClients[i, j] - alloClients[i, j];
                }
            }
            for (int i = 0; i < resources.Length; i++)
            {
                if (req[i] > resources[i])
                {
                    available = false;
                    break;
                }
            }
            if (available)
            {
                for (int i = 0; i < resources.Length; i++)
                {
                    resources[i] -= req[i];
                    alloClients[id, i] += req[i];
                    needClients[id, i] -= req[i];
                }
                if (safetyCheck(resources, needClients, alloClients))
                {
                    Console.WriteLine("Request granted.");
                    for (int i = 0; i < resources.Length; i++)
                    {
                        allo[i] += req[i];
                    }
                }
                else
                {
                    Console.WriteLine("No safe state. Request denied");
                    for (int i = 0; i < resources.Length; i++)
                    {
                        resources[i] += req[i];
                        alloClients[id, i] -= req[i];
                    }
                }
            }
            else
            {
                Console.WriteLine("Not enough resources, person must wait");
            }
        }
        public static bool safetyCheck(int[] able, int[,] need, int[,] alloc)
        {
            int[] work = new int[able.Length];
            bool[] finish = new bool[need.GetLength(0)];
            bool safe = true;
            bool index = true;
            bool satisfy = false;
            for (int i = 0; i < work.Length; i++)
            {
                work[i] = able[i];
            }
            for (int i = 0; i < finish.Length; i++)
            {
                finish[i] = false;
            }
            while (index)
            {
                index = false;
                for (int i = 0; i < finish.Length; i++)
                {
                    if (finish[i] == false)
                    {
                        satisfy = true;
                        for (int j = 0; j < work.Length; j++)
                        {
                            if (need[i, j] > work[j])
                            {
                                satisfy = false;
                                break;
                            }
                        }
                        if (satisfy)
                        {
                            index = true;
                            for (int j = 0; j < work.Length; j++)
                            {
                                work[j] += alloc[i, j];
                            }
                            finish[i] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < finish.Length; i++)
            {
                if (finish[i] == false)
                {
                    safe = false;
                    break;
                }
            }
            return safe;
        }
        public void release(int id, int[] allo)
        {
            Console.WriteLine("Person with ID " + id + " has released resources");
            for (int i = 0;i < allo.Length; i++)
            {
                resources[i] += allo[i];
                allo[i] = 0;
                alloClients[id, i] = 0;
            }
        }
        public override string ToString()
        {
            String result = "";
            for(int i = 0; i < this.resources.Length; i++)
            {
                result += ("Resource " + (i + 1) + ": " + this.resources[i] + ". ");
            }
            return result;
        }
    }
}
