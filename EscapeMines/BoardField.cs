using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace EscapeMines
{
    public class BoardField
    {
        public BoardField(Position position, bool isMine = false)
        {
            Position = position;
            IsMine = isMine;
        }

        public Position Position { get; set; }

        public Direction Direction { get; set; }

        public bool IsMine { get; set; }
    }
}