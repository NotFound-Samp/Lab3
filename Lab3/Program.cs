using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Lab3
{
    class Program
    {
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\Alex\\VS\\Lab3\\Lab3\\writerText.txt");
        private static Barrier barrier = new Barrier(11);
        private static Stopwatch stopwatch = new Stopwatch();

        static void Main()
        {
            for (int i = 1; i <= 10; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(PerformTask));
                thread.Start(i);
            }

            barrier.SignalAndWait();
            Console.WriteLine("Все потоки завершили работу");
        }

        static void PerformTask(object threadNumber)
        {
            
            stopwatch.Start();
            barrier.SignalAndWait();
            stopwatch.Stop();
            double executionTime = stopwatch.Elapsed.TotalMilliseconds;

            try
            {
                lock(typeof(Program))
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                        writer.WriteLine($"Поток {(int)threadNumber} завершил работу. Длительность выполнения: {executionTime} мс. Текущее время: {DateTime.Now}");
                Thread.Sleep(1000);
            }
            finally
            {
                barrier.SignalAndWait();
            }
        }
    }
}
