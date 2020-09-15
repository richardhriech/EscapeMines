﻿using System.Collections.Generic;

namespace Common
{
    class GameConfig
    {
        public Position BoardSize { get; set; }

        public List<Position> MinePositions { get; set; }

        public Position ExitPosition { get; set; }

        public Position StartPosition { get; set; }

        public Direction StartDirection { get; set; }

        public List<Move> Moves { get; set; }
    }
}