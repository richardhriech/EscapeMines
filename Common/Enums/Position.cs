namespace Common.Enums
{
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Position otherPosition && otherPosition.X == X && otherPosition.Y == Y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
