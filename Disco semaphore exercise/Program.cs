using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Disco_semaphore_exercise {
    class Program {

        static Thread[] threads = new Thread[21];
        static Semaphore guestSem = new Semaphore(15, 15);
        static Mutex m = new Mutex();
        static Random rnd = new Random();
        static int guests = 0;

        static void Main(string[] args) {
            for (int i = 1; i < threads.Length; i++) {
                threads[i] = new Thread(ThreadProcess);
                threads[i].Name = i.ToString();
                threads[i].Start();
            }
            Console.Read();
        }

        private static void ThreadProcess() {
            Thread.Sleep(1000);
            Console.WriteLine("Guest '" + Thread.CurrentThread.Name + "' arrives at the disco");
            //Console.WriteLine("there are: " + guestSem.);
            guestSem.WaitOne();
            m.WaitOne();
            guests++;
            Console.WriteLine("Guest '" + Thread.CurrentThread.Name + "' enter the disco\nThere are now " + guests + " inside the disco");
            m.ReleaseMutex();
            Thread.Sleep(rnd.Next(1000,3000));
            guestSem.Release(1);
            m.WaitOne();
            guests--;
            Console.WriteLine("Guest '" + Thread.CurrentThread.Name + "' leaves the disco\nThere are now " + guests + " inside the disco");
            m.ReleaseMutex();
        }
    }
}
