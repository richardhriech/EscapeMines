using System;
using System.Collections.Generic;
using Common;

namespace Data
{
    public class GameConfigParserFromFile : IGameConfigParser
    {
        private const int RequiredLineCount = 5;
        private const int StartDataCount = 3;
        private const string SpaceSeparator = " ";
        private const string CommaSeparator = ",";

        private readonly IDataProvider dataProvider;

        public GameConfigParserFromFile(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public GameConfig ParseConfig()
        {
            List<string> rawData = dataProvider.GetRawData();
            var config = new GameConfig();

            if (rawData.Count != RequiredLineCount)
            {
                throw new ArgumentException(
                    "Invalid data format. The config file should " +
                    $"contain exactly {RequiredLineCount} lines");
            }

            config.BoardSize = ParsePosition(rawData[0]);
            config.MinePositions = ParseMinePositions(rawData[1]);
            config.ExitPosition = ParsePosition(rawData[2]);

            string[] startRawData = GetStartRawData(rawData[3]);
            config.StartPosition = ParsePosition(startRawData[0] + SpaceSeparator + startRawData[1]);
            config.StartDirection = ParseDirection(startRawData[2]);

            config.Moves = GetMoves(rawData[4]);

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

            return new Position()
            {
                X = x,
                Y = y
            };
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

                minePositions.Add(new Position(){ X = x, Y = y });
            }

            return minePositions;
        }

        private Direction ParseDirection(string rawDirection)
        {
            bool isSuccess = Enum.TryParse(rawDirection, out Direction direction);

            /*if (!isSuccess)
            {
                throw new ArgumentException("Invalid direction.");
            }*/

            return direction;
        }

        private Move ParseMove(string rawMove)
        {
            bool isSuccess = Enum.TryParse(rawMove, out FileMove fileMove);

            if (!isSuccess)
            {
                throw new ArgumentException("Invalid move.");
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
            string[] splitValues = rawData.Split(SpaceSeparator);
            List<Move> moves = new List<Move>();

            foreach (string value in splitValues)
            {
                moves.Add(ParseMove(value));
            }

            return moves;
        }
    }
}