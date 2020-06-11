using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace TestThread
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<string, Timer> concurrent = new ConcurrentDictionary<string, Timer>();

            string[] arrKey = new string[]
            {
                "A","B","C","D","E"
            };

            foreach(string item in arrKey )
            {
                Timer timer = new Timer(TimeCB ,null, -1,3000);
                concurrent.TryAdd(item, timer);
            }

            bool outloop = true;
            while(outloop){
                Console.WriteLine("請輸入A-E");
                string key = Console.ReadLine();
                Timer getTimer = null;
                if ( arrKey.Contains(key))
                {
                    if(concurrent.TryGetValue(key, out getTimer))
                    {
                        getTimer.Change(0,3000);
                        Console.WriteLine("GetTimer");
                    }        
                }

                Console.WriteLine($"Timer Count : {concurrent.Count}");
                Console.WriteLine("是否繼續(Y/N)");
                var ynkey = Console.ReadLine();

                if(ynkey.ToLower() != "y")
                {
                    getTimer.Change(-1,3000);
                    var outkey = Console.ReadKey();
                    outloop = !(outkey.Key == ConsoleKey.Enter);
                }
            }
        }

        private static void TimeCB(object sender)
        {
            List<string> list = new List<string>();
            Console.WriteLine("Run");
            System.Threading.Thread.Sleep(10000);
            try{
                for (int i = 1; i <= 1000; i++)
                {
                    if(i % 100 == 0)
                    {
                        Console.WriteLine("ERROR!");
                        throw new ArgumentNullException();
                    }

                    list.Add(i.ToString());
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message);
            }
        }
    }
}
