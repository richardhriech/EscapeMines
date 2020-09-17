using System;

namespace Common
{
    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Position otherPosition && otherPosition.X == X && otherPosition.Y == Y;
        }
    }
}
