namespace EscapeMines
{
    public sealed class Game
    {
        static Game()
        {
        }

        private Game()
        {
        }

        public static Game Instance { get; } = new Game();
    }
}