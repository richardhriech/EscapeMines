using System;

using Data;



namespace EscapeMines
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path for the game config file: ");
            string gameConfigFilePath = Console.ReadLine();
            var game = Game.Instance;
            game.Setup(gameConfigFilePath);
        }
    }
}
