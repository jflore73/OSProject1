using System;
//using System.Threading;
//using System.IO;
//using System.Collections.Generic;
using OSProject1;
class Program
{
    public static void Main()
    {
        //let n be the number of processes and m the number of available resources of each type
        //Setup of data structures
        int processes, resources;
        int[] available;
        int[,] max;
        int[,] allocation;
        int[,] need;
        Console.Write("Enter the number of processes: ");
        processes = Convert.ToInt32(Console.ReadLine());
        //Console.ReadLine();
        Console.Write("Enter the number of resources: ");
        resources = Convert.ToInt32(Console.ReadLine());
        //Console.ReadLine ();
        available=new int[resources];
        max=new int[processes, resources];
        allocation = new int[processes, resources];
        need=new int[processes, resources];

        for(int i=0; i<available.Length; i++)
        {
            Console.Write("Enter number of available resources of type " + (i + 1)+": ");
            available[i]=Convert.ToInt32(Console.ReadLine());
        }
        for (int i = 0; i < max.GetLength(0); i++)
        {
            for(int j=0; j<max.GetLength(1); j++)
            {
                Console.Write("Enter max of resources of type " + (j + 1) +" for process "+(i+1)+": ");
                max[i, j] = Convert.ToInt32(Console.ReadLine());
            }
        }
        for (int i = 0; i < allocation.GetLength(0); i++)
        {
            for (int j = 0; j < allocation.GetLength(1); j++)
            {
                Console.Write("Enter allocated resources of type " + (j + 1) + " for process " + (i + 1) + ": ");
                allocation[i, j] = Convert.ToInt32(Console.ReadLine());
                need[i, j] = max[i, j] - allocation[i, j];
                Console.WriteLine(need[i, j]+" = " + max[i, j]+" - " + allocation[i, j]);
            }
        }
        if (safetyCheck(available, need, allocation))
        {
            Console.WriteLine("Safe State :D");
        }
        else
        {
            Console.WriteLine("Not Safe State D:");
        }

        /*
        String[] data;
        List<Person> allPersons = new List<Person>();
        try
        {
            StreamReader sr = new StreamReader("Accounts.txt");
            while (!sr.EndOfStream)
            {
                data = sr.ReadLine().Split(",");
                allPersons.Add(new Person(data[0], Convert.ToInt32(data[1])));
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        Thread[] threads = new Thread[allPersons.Count];
        for (int i = 0; i < allPersons.Count; i++)
        {
            threads[i] = new Thread(allPersons.ElementAt<Person>(i).Run);
        }
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }*/
    }
    public static bool safetyCheck(int[] able, int[,] need, int[,] alloc) {
        int[] work = new int[able.Length];
        bool[] finish = new bool[need.GetLength(0)];
        bool safe = true;
        bool index = true;
        bool satisfy=false;
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
            Console.WriteLine("index is " + index);
            for (int i = 0; i < finish.Length; i++)
            {
                if (finish[i] == false)
                {
                    Console.WriteLine("Testing Process "+(i+1));
                    satisfy = true;
                    for (int j = 0; j < work.Length; j++)
                    {
                        Console.WriteLine("Need is " + need[i, j]+" and work is " + work[j]);
                        if (need[i, j] > work[j])
                        {
                            satisfy = false;
                            Console.WriteLine("Cannot be satisfied");
                            break;
                        }
                    }
                    if (satisfy)
                    {
                        index = true;
                        Console.WriteLine("Index is "+index);
                        for(int j = 0;j < work.Length; j++)
                        {
                            Console.WriteLine("Allocation for resource "+(j+1)+" for process "+(i+1)+" is " + alloc[i, j]);
                            Console.WriteLine("Work " + (j + 1) + " is " + work[j]);
                            work[j] += alloc[i,j];
                            Console.WriteLine("Work " + (j+1) + " is now " + work[j]);
                        }
                        finish[i] = true;
                        Console.WriteLine("Process is finished");
                    }
                }
            }
        }
        Console.WriteLine("Final Check");
        for(int i =0; i < finish.Length;i++)
        {
            if (finish[i] == false)
            {
                safe=false;
                break;
            }
        }
        return safe;

    }

}