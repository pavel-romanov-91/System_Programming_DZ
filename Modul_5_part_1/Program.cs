using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modul_5_part_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Основной поток запущен");
            Task task = new Task(MyTask);
            task.Start();
            for (int i = 0; i < 60; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine("Основной поток завершен");
            Console.ReadLine();
        }
        static void MyTask()
        {
            Console.WriteLine("MyTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("В методе MyTask подсчет равен " + count);
            }
        }
    }
}
