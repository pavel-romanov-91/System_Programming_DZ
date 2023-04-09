using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_3_Multithreading_and_asynchrony
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProstyeChisla pch = new ProstyeChisla();
            Console.Write("Введите нижнюю границу простых чисел: ");
            pch.n_min = Convert.ToInt64(Console.ReadLine());
            Console.Write("Введите верхнюю границу простых чисел: ");
            pch.n_max = Convert.ToInt64(Console.ReadLine());
            long k = pch.poisk();
            pch.TimePoisk();
            long j = 0;
            for (int i = 0; i < k; i++)
                if (pch.prost[i] > pch.n_min)
                {
                    Console.Write(pch.prost[i].ToString() + "\t");
                    j++;
                }
            Console.WriteLine("\nВсего: {0}, в диапазоне: {1}", k, j);
            Console.ReadKey();
        }
    }
    class ProstyeChisla
    {
        public long[] prost = new long[10000000];
        public long n_min;
        public long n_max;

        public DateTime tstart, tfinish;

        public long poisk()
        {
            tstart = DateTime.Now;
            prost[0] = 2;
            prost[1] = 3;
            prost[2] = 5;
            prost[3] = 7;
            prost[4] = 11;
            long x = 13;
            long k = 5;
            while (x <= n_max)
            {
                if (yes_pr(x))
                {
                    prost[k] = x;
                    k++;
                }
                x = x + 2;
            }
            tfinish = DateTime.Now;
            return k;
        }
        bool yes_pr(long x)
        {
            bool b = true;
            double y = Math.Sqrt(x);
            long z = Convert.ToInt64(y + 0.5);
            long k = 1;
            while (prost[k] < z)
            {
                if (x % prost[k] == 0)
                {
                    b = false;
                    break;
                }
                k++;
            }
            return b;
        }
        public void TimePoisk()
        {
            int dt, ds, dm, dh;
            dt = tfinish.Hour * 3600 + tfinish.Minute * 60 + tfinish.Second - tstart.Hour * 3600 - tstart.Minute * 60 - tstart.Second;
            dh = dt / 3600;
            dm = (dt - dh * 3600) / 60;
            ds = (dt - dh * 3600 - dm * 60);
            if (dt < 60)
                Console.WriteLine("Время поиска: секунд - {0}", ds);
            else if (dt < 3600)
                Console.WriteLine("Время поиска: минут - {0}, секунд - {1}", dm, ds);
            else
                Console.WriteLine("Время поиска: часов - {0}, минут - {1}, секунд - {2}", dh, dm, ds);
        }
    }
}
