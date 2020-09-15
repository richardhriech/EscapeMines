using System.Collections.Generic;
using Common;

namespace EscapeMines
{
    public class Board
    {
        public List<BoardField> Fields { get; set; }

        public Player Player { get; set; }
    }
}