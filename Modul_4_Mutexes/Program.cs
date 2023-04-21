﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modul_4_Mutexes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IncThread mt1 = new IncThread("Inc thread", 5);
            Thread.Sleep(1);
            DecThread mt2 = new DecThread("Dec thread", 5);
            mt1.Thrd.Join();
            mt2.Thrd.Join();
            Console.ReadLine();
        }
    }

    // В этом классе содержится общий ресурс в виде переменной Count, а так же мьютекс mtx
    
    class SharedRes
    {
        public static int Count;
        public static Mutex mtx = new Mutex();
    }

    // В этом классе Count инкрементируется
    class IncThread
    {
        int num;
        public Thread Thrd;
        public IncThread(string name, int n)
        {
            Thrd = new Thread(this.Run);
            num = n;
            Thrd.Name = name;
            Thrd.Start();
        }

        // Точка входа в поток
        void Run()
        {
            Console.WriteLine(Thrd.Name + " ожидает мьютекс");

            // Получить мьютекс
            SharedRes.mtx.WaitOne();
            Console.WriteLine(Thrd.Name + " получает мьютекс");
            do
            {
                Thread.Sleep(500);
                SharedRes.Count++;
                Console.WriteLine("в потоке {0}, Count={1}", Thrd.Name, SharedRes.Count);
                num--;
            } while (num > 0);
            Console.WriteLine(Thrd.Name + " освобождает мьютекс");
            SharedRes.mtx.ReleaseMutex();
        }
    }
    class DecThread
    {
        int num;
        public Thread Thrd;
        public DecThread(string name, int n)
        {
            Thrd = new Thread(new ThreadStart(this.Run));
            num = n;
            Thrd.Name = name;
            Thrd.Start();
        }

        // Точка входа в поток
        void Run()
        {
            Console.WriteLine(Thrd.Name + " ожидает мьютекс");
            // Получить мьютекс
            SharedRes.mtx.WaitOne();
            Console.WriteLine(Thrd.Name + " получает мьютекс");
            do
            {
                Thread.Sleep(500);
                SharedRes.Count--;
                Console.WriteLine("в потоке {0}, Count={1}", Thrd.Name, SharedRes.Count);
                num--;
            } while (num > 0);
            Console.WriteLine(Thrd.Name + " освобождает мьютекс");
            SharedRes.mtx.ReleaseMutex();
        }
    }
}
