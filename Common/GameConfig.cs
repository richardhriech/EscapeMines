using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class GameConfig
    {
        public Position BoardSize { get; set; }

        public List<Position> MinePositions { get; set; }

        public Position ExitPosition { get; set; }

        public Position StartPosition { get; set; }

        public Direction StartDirection { get; set; }

        public List<Move> Moves { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as GameConfig;

            return Equals(BoardSize, other.BoardSize)
                && Equals(ExitPosition, other.ExitPosition)
                && Equals(StartPosition, other.StartPosition)
                && StartDirection == other.StartDirection
                && MinePositions.SequenceEqual(other.MinePositions)
                && Moves.SequenceEqual(other.Moves);
        }
    }
}
