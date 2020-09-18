using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Enums;
using EscapeMines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.EscapeMines
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void BuildBoard_ConfigIsNull_ShouldThrowArgumentException()
        {
            GameConfig config = null;

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_ConfigBoardSizeZero_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(0, 0),
                    MinePositions = new List<Position>() { new Position(1, 1) },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_ConfigBoardSizeGiven_ShouldReturnProperMaxCoords()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions = new List<Position>() { new Position(1, 1) },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);
            board.BuildBoard();

            Assert.AreEqual(new Position(4, 3), board.MaxPosition);
        }

        [TestMethod]
        public void BuildBoard_MinePositionsSetInConfig_ShouldReturnValidMineFieldCount()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions =
                        new List<Position>()
                        {
                            new Position(1, 1),
                            new Position(1, 3),
                            new Position(3, 3)
                        },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);
            board.BuildBoard();
            List<Field> mines = board.Fields.Where(field => field.FieldType == FieldType.Mine).ToList();

            Assert.AreEqual(config.MinePositions.Count, mines.Count);
        }

        [TestMethod]
        public void BuildBoard_OneMinePositionSetInConfig_ShouldReturnValidMineField()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions =
                        new List<Position>()
                        {
                            new Position(1, 1)
                        },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);
            board.BuildBoard();
            List<Field> mines = board.Fields.Where(field => field.FieldType == FieldType.Mine).ToList();

            Assert.AreEqual(new Field(config.MinePositions.First(), FieldType.Mine), mines.First());
        }

        [TestMethod]
        public void BuildBoard_MinePositionInConfigSmallerThanMin_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions = new List<Position>() { new Position(1, -1) },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_MinePositionInConfigHigherThanMax_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions = new List<Position>() { new Position(5, 1) },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_ExitPositionInConfigSmallerThanMin_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions = new List<Position>() { new Position(1, 1) },
                    ExitPosition = new Position(-4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_ExitPositionInConfigHigherThanMax_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions = new List<Position>() { new Position(1, 1) },
                    ExitPosition = new Position(5, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_StartPositionInConfigSmallerThanMin_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions = new List<Position>() { new Position(1, 1) },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, -1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_StartPositionInConfigHigherThanMax_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions = new List<Position>() { new Position(1, 1) },
                    ExitPosition = new Position(3, 2),
                    StartPosition = new Position(5, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);

            Assert.ThrowsException<ArgumentException>(() => board.BuildBoard());
        }

        [TestMethod]
        public void BuildBoard_ValidStartPositionInConfig_ShouldReturnValidPlayer()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position(5, 4),
                    MinePositions =
                        new List<Position>()
                        {
                            new Position(1, 1),
                            new Position(1, 3),
                            new Position(3, 3)
                        },
                    ExitPosition = new Position(4, 2),
                    StartPosition = new Position(0, 1),
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);
            board.BuildBoard();
            var expectedPlayer = new Player(config.StartPosition, config.StartDirection);

            Assert.AreEqual(expectedPlayer, board.Player);
        }
    }
}
