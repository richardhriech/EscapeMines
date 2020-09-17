using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Common;
using Data;

namespace EscapeMines
{
    public sealed class Game
    {
        private const int configFileLineCount = 5;

        static Game()
        {
        }

        private Game()
        {
        }

        public static Game Instance { get; } = new Game();

        public Board Board { get; set; }
        public List<Move> Moves { get; set; }

        public void Setup(string path)
        {
            IDataProvider dataProvider = new DataProviderFromFile(path);
            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider);
            GameConfig config = configParser.ParseConfig();
            Board = new Board(config);
            Moves = config.Moves;
        }

        public void PlayMoves()
        {
            foreach (Move move in Moves)
            {
                
            }
        }

    }
}
