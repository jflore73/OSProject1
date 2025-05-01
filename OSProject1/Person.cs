using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject1
{
    internal class Person
    {
        private String name;
        private int time;
        private int money;
        public Person()
        {
            this.name = "Person";
            this.money = 0;
        }
        public Person(String n, int m)
        {
            this.name = n;
            this.money = m;
        }
        public void Run()
        {
            Random rand = new Random();
            int i = 0;
            while (i < 5)
            {
                try
                {
                    this.time = rand.Next(15000);
                    Thread.Sleep(this.time);
                    Console.WriteLine("Hello! I am " + this.name + ". This is transaction #" + i);
                    Console.WriteLine(this.name + " is entering Critical Region");
                    Thread.BeginCriticalRegion();
                    Console.WriteLine(this.name + " is exiting Critical Region");
                    Thread.EndCriticalRegion();
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.Message);
                }
                i++;
            }
        }
        public override String ToString() { return (this.name + "," + this.money); }
    }
}
