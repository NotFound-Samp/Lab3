using System.Diagnostics;
using System.Threading;

namespace Lab3
{
    class Program
    {
        private static readonly string filePath =
            Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\Alex\\VS\\Lab3\\Lab3\\writerText.txt");
        private static int currentThread = 1;
        private static object lockObject = new object();
        static void Main()
        {
            for (int i = 1; i <= 10; i++)
            {
                Thread thread = new Thread(PrintData);
                thread.Start(i);
            }
        }
        static void PrintData(object threadNumber)
        {
            int number = (int)threadNumber;
            while (true)
            {
                lock (lockObject)
                {
                    if (number == currentThread)
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine($"Поток {(int)threadNumber} запустился. Текущее время: {DateTime.Now}");
                        }
                        currentThread = (currentThread % 10) + 1;
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}