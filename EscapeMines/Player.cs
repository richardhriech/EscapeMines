using Common;

namespace EscapeMines
{
    public class Player
    {
        public Player(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        public Position Position { get; set; }

        public Direction Direction { get; set; }
    }
}