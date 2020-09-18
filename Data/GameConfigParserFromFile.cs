using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Enums;

namespace Data
{
    public class GameConfigParserFromFile : IGameConfigParser
    {
        private const string SpaceSeparator = " ";
        private const string CommaSeparator = ",";
        private const short RequiredLineCount = 5;
        private const short StartDataCount = 3;
        private const short MovesStartingIndex = 4;
        private const short BoardSizeIndex = 0;
        private const short MinePositionsIndex = 1;
        private const short ExitPositionIndex = 2;
        private const short StartPositionIndex = 3;
        private readonly IDataProvider dataProvider;

        public GameConfigParserFromFile(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public GameConfig ParseConfig()
        {
            List<string> rawData = dataProvider.GetRawData();
            var config = new GameConfig();

            if (rawData.Count < RequiredLineCount)
            {
                throw new ArgumentException(
                    "Invalid data format. The config file should " + $"contain exactly {RequiredLineCount} lines"
                );
            }

            config.BoardSize = ParsePosition(rawData[BoardSizeIndex]);
            config.MinePositions = ParseMinePositions(rawData[MinePositionsIndex]);
            config.ExitPosition = ParsePosition(rawData[ExitPositionIndex]);

            string[] startRawData = GetStartRawData(rawData[StartPositionIndex]);
            config.StartPosition = ParsePosition(startRawData[0] + SpaceSeparator + startRawData[1]);
            config.StartDirection = ParseDirection(startRawData[2]);

            config.Moves = new List<Move>();

            for (int i = MovesStartingIndex; i < rawData.Count; i++)
            {
                config.Moves.AddRange(GetMoves(rawData[i]));
            }

            return config;
        }

        private Position ParsePosition(string rawBoardSize)
        {
            string[] splitValues = rawBoardSize.Split(SpaceSeparator);

            if (splitValues.Length != 2)
            {
                throw new ArgumentException("Invalid position format");
            }

            bool isXParseSuccessful = Int32.TryParse(splitValues[0], out int x);
            bool isYParseSuccessful = Int32.TryParse(splitValues[1], out int y);

            if (!isXParseSuccessful || !isYParseSuccessful)
            {
                throw new ArgumentException("Invalid position format");
            }

            return new Position(x, y);
        }


        private List<Position> ParseMinePositions(string rawPositions)
        {
            string[] splitRawValues = rawPositions.Split(SpaceSeparator);
            List<Position> minePositions = new List<Position>();

            foreach (string rawPair in splitRawValues)
            {
                string[] rawCoords = rawPair.Split(CommaSeparator);

                if (rawCoords.Length != 2)
                {
                    throw new ArgumentException("Invalid mine positions");
                }

                bool isXParseSuccessful = Int32.TryParse(rawCoords[0], out int x);
                bool isYParseSuccessful = Int32.TryParse(rawCoords[1], out int y);


                if (!isXParseSuccessful || !isYParseSuccessful)
                {
                    throw new ArgumentException("Invalid mine positions.");
                }

                minePositions.Add(new Position(x, y));
            }

            return minePositions;
        }

        private Direction ParseDirection(string rawDirection)
        {
            bool isSuccess = Enum.TryParse(rawDirection, out FileDirection fileDirection);

            if (!isSuccess)
            {
                throw new ArgumentException("Invalid direction.");
            }

            return fileDirection switch
            {
                FileDirection.N => Direction.North,
                FileDirection.E => Direction.East,
                FileDirection.S => Direction.South,
                FileDirection.W => Direction.West,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Move ParseMove(string rawMove)
        {
            bool isSuccess = Enum.TryParse(rawMove, out FileMove fileMove);

            if (!isSuccess)
            {
                throw new ArgumentException("Invalid move in file.");
            }

            return fileMove switch
            {
                FileMove.L => Move.TurnLeft,
                FileMove.R => Move.TurnRight,
                FileMove.M => Move.Move,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private string[] GetStartRawData(string rawData)
        {
            string[] startRawData = rawData.Split(SpaceSeparator);

            if (startRawData.Length != StartDataCount)
            {
                throw new ArgumentException("Invalid start position format.");
            }

            return startRawData;
        }

        private List<Move> GetMoves(string rawData)
        {
            if (rawData is null || rawData == String.Empty)
            {
                throw new ArgumentException("Moves can't be empty");
            }

            string[] splitValues = rawData.Split(SpaceSeparator);

            List<Move> moves = splitValues.Select(value => ParseMove(value)).ToList();

            return moves;
        }
    }
}
