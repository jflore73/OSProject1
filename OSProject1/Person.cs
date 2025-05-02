using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject1
{
    internal class Person
    {
        private static Bank funds = new Bank(4);
        private static int count = 0;
        private int PersonID;
        private String name;
        private int time;
        private int[] maxResources=new int[4];
        private int[] alloResources=new int[4];
        private int[] reqResources = new int[4];
        public Person()
        {
            this.name = "Person";
            this.PersonID = count++;
            for (int i = 0; i<maxResources.Length; i++)
            {
                this.maxResources[i] = 0;
            }
            funds.newClient( this.maxResources);
        }

        public Person(string name, int[] m)
        {
            this.name = name;
            this.PersonID = count++;
            this.maxResources = m;
            for (int i = 0; i < alloResources.Length; i++)
            {
                this.alloResources[i] = 0;
            }
            funds.newClient(this.maxResources);
        }
        public Person(string name, int r1, int r2, int r3, int r4)
        {
            this.name = name;
            this.PersonID = count++;
            this.maxResources[0] = r1;
            this.maxResources[1] = r2;
            this.maxResources[2] = r3;
            this.maxResources[3] = r4;
            for (int i = 0; i < alloResources.Length; i++)
            {
                this.alloResources[i] = 0;
            }
            funds.newClient(this.maxResources);
        }
        public void Run()
        {
            Random rand = new Random();
            int i = 0;
            int[] needed= new int[4];
            bool complete=true;
            while (i < 5)
            {
                try
                {
                    complete = true;
                    this.time = rand.Next(10000);
                    Thread.Sleep(this.time);
                    Console.WriteLine(this.name + ". Is making a request for process run #" + i);
                    for (int j = 0; j < reqResources.Length; j++)
                    {
                        needed[j] = maxResources[j] - alloResources[j];
                        reqResources[j] = rand.Next(needed[j]+1);
                    }
                    Thread.BeginCriticalRegion();
                    funds.requestResource(PersonID, reqResources, alloResources);
                    for (int j = 0; j < alloResources.Length; j++)
                    {
                        if (alloResources[j] != maxResources[j])
                        {
                            complete=false;
                            break;
                        }
                        
                    }
                    if (complete)
                    {
                        i++;
                        funds.release(PersonID, alloResources);
                    }
                    Thread.EndCriticalRegion();
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
       
        public override String ToString() { return (this.name + ", the horse is here."); }
    }
}
