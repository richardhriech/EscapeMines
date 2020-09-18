using System;

namespace EscapeMines
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path for the game config file: ");
            string gameConfigFilePath = Console.ReadLine();

            var game = new Game();
            game.Setup(gameConfigFilePath);

            game.PlayMoves();

            Console.WriteLine("Type anything to quit.");
            Console.ReadLine();
        }
    }
}
