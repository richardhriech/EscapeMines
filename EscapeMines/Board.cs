using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Common;

namespace EscapeMines
{
    public class Board
    {
        private readonly GameConfig config;

        private readonly Position MinPosition = new Position
            { X = 0, Y = 0 };

        public Board(GameConfig config)
        {
            this.config = config;
            Fields = new List<Field>();
        }

        public Position MaxPosition { get; set; }

        public List<Field> Fields { get; set; }

        public Player Player { get; set; }

        public void BuildBoard()
        {
            SetMaxPosition();

            SetMineFields();

            SetExitPosition();

            if (IsValidCoord(config.StartPosition.X, config.StartPosition.Y))
            {
                Player = new Player(config.StartPosition, config.StartDirection);
            }
            else
            {
                throw new ArgumentException("Player position must be inside the map");
            }
        }

        private void SetMaxPosition()
        {
            if (config.BoardSize.X < 1 || config.BoardSize.Y < 1)
                throw new ArgumentException("Board size must be at least 1x1.");

            MaxPosition = new Position() { X = config.BoardSize.X - 1, Y = config.BoardSize.Y - 1 };
        }

        private void SetMineFields()
        {
            foreach (Position minePosition in config.MinePositions)
                if (IsValidCoord(minePosition.X, minePosition.Y))
                    Fields.Add(new Field(minePosition, FieldType.Mine));
                else
                    throw new ArgumentException("Mine position must be inside the map.");
        }
        private void SetExitPosition()
        {
            if (IsValidCoord(config.ExitPosition.X, config.ExitPosition.Y))
            {
                Fields.Add(new Field(config.ExitPosition, FieldType.Exit));
            }
            else
            {
                throw new ArgumentException("Exit position must be inside the map");
            }
        }
        private bool IsValidCoord(int X, int Y)
        {

            return X <= MaxPosition.X
                && Y <= MaxPosition.Y
                && X >= MinPosition.X
                && Y >= MinPosition.Y;
        }
    }
}
