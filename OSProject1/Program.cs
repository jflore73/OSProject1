using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using OSProject1;
class Program
{
    public static void Main()
    {
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
        }
    }

}