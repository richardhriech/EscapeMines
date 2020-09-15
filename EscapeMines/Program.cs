using System;

using Data;



namespace EscapeMines
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileReader fr = new FileReader();
            var asd = fr.ReadLines(@"C:\Users\richa\OneDrive - Mortoff\Backup\Desktop\asd.txt");

        }
    }
}
