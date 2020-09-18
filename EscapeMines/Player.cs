using Common.Enums;

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

        public override bool Equals(object? obj)
        {
            var other = obj as Player;

            return Equals(Position, other.Position)
                && Equals(Direction, other.Direction);
        }
    }
}
