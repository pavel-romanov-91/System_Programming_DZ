using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modul_3_part_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] directory = Directory.GetFiles(@"E:\Работа", "*.txt");


            Console.WriteLine("input number of threads");
            int threads = int.Parse(Console.ReadLine());
            streams.StreamsStart(threads, directory);
            // endofprogram
            Console.ReadKey();

        }
    }
    class streams
    {
        public static void StreamsStart(int threads, string[] directory)
        {
            Thread[] threadArray;
            int filesCount = directory.Length;
            //если количество файлов и потоков равное - каждому потоку по файлу
            if (threads == filesCount)
            {
                for (int i = 0; i < (threadArray = new Thread[threads]).Length; i++)
                {
                    threadArray[i] = new Thread(new ThreadStart(() => { ReadFiles(i, directory[filesCount - 1]); })) { IsBackground = true };
                    threadArray[i].Start();
                    filesCount--;
                }
            }

            //если количество файлов меньше, чем потоков - потоки делим между файлами, 
            //вначале раздаём всем файлам по 1 потоку
            //потом снова всем (или кому хватит) по 1 потоку и т.д. 
            else if (threads > filesCount)
            {
                for (int i = 0; i < (threadArray = new Thread[threads]).Length; i++)
                {
                    if (filesCount < 1)
                    {
                        filesCount = directory.Length;
                    }
                    threadArray[i] = new Thread(new ThreadStart(() => { ReadFiles(i, directory[filesCount - 1]); })) { IsBackground = true };
                    threadArray[i].Start();
                    filesCount--;

                }
            }

            // если количество файлов больше, чем потоков - файлы делим между потоками, так: Например 26 файлов, 3 потока.
            // 26/3 = 8
            // и 2 в остатке.
            // 2 потока получают 9 файлов, 1 поток 8 файлов.     
            else
            {
                // целое
                int withoutResidue = filesCount / threads;
                // остаток
                int residue = filesCount % threads;

                for (int i = 0; i < (threadArray = new Thread[threads]).Length; i++)
                {
                    threadArray[i] = new Thread(new ThreadStart(() =>
                    {
                        //раздаём гарантировано всем потокам по целому набору файлов
                        for (int j = 0; j < withoutResidue; j++)
                        {
                            ReadFiles(i, directory[filesCount - 1]);
                            filesCount--;
                        }
                        //если есть остаток, даём каждому потоку по 1 файлу, пока остаток не исчерпается
                        if (residue != 0)
                        {
                            ReadFiles(i, directory[filesCount - 1]);
                            filesCount--;
                            residue--;
                        }
                    }
                    ))
                    { IsBackground = true };
                    threadArray[i].Start();
                }
            }
        }

        private static readonly object syncRoot = new object();

        static void ReadFiles(int thread, string fileName)
        {
            lock (syncRoot)
            {
                using (FileStream file = File.Open(fileName, FileMode.Open))
                using (StreamReader stream = new StreamReader(file))
                {
                    while (stream.Peek() >= 0)
                    {
                        stream.ReadLine();
                    }
                    Console.WriteLine("Thread {0}, Filename {1}", thread, file.Name);
                }
            }
        }
    }
}
   
