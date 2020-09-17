using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Data;
using EscapeMines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.EscapeMines
{
    [TestClass]
    public class BoardTest
    {

        [TestMethod]
        public void BuildBoard_ConfigBoardSizeZero_ShouldReturnArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position() { X = 0, Y = 0 },
                    MinePositions = new List<Position>() { new Position() { X = 1, Y = 1 } },
                    ExitPosition = new Position() { X = 4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
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
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions = new List<Position>() { new Position() { X = 1, Y = 1 } },
                    ExitPosition = new Position() { X = 4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);
            board.BuildBoard();

            Assert.AreEqual(new Position() { X = 4, Y = 3 }, board.MaxPosition);
        }

        [TestMethod]
        public void BuildBoard_MinePoisitionsSetInConfig_ShouldReturnValidMineFieldCount()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions =
                        new List<Position>()
                        {
                            new Position() { X = 1, Y = 1 },
                            new Position() { X = 1, Y = 3 },
                            new Position() { X = 3, Y = 3 }
                        },
                    ExitPosition = new Position() { X = 4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
                    StartDirection = Direction.North,
                    Moves = new List<Move>() { Move.TurnRight }
                };

            var board = new Board(config);
            board.BuildBoard();
            List<Field> mines = board.Fields.Where(field => field.FieldType == FieldType.Mine).ToList();

            Assert.AreEqual(config.MinePositions.Count, mines.Count);
        }

        [TestMethod]
        public void BuildBoard_MinePositionInConfigSmallerThanMin_ShouldThrowArgumentException()
        {
            var config =
                new GameConfig()
                {
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions = new List<Position>() { new Position() { X = 1, Y = -1 }, },
                    ExitPosition = new Position() { X = 4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
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
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions = new List<Position>() { new Position() { X = 5, Y = 1 }, },
                    ExitPosition = new Position() { X = 4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
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
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions = new List<Position>() { new Position() { X = 1, Y = 1 }, },
                    ExitPosition = new Position() { X = -4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
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
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions = new List<Position>() { new Position() { X = 1, Y = 1 }, },
                    ExitPosition = new Position() { X = 5, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
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
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions = new List<Position>() { new Position() { X = 1, Y = 1 }, },
                    ExitPosition = new Position() { X = 4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = -1 },
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
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions = new List<Position>() { new Position() { X = 1, Y = 1 }, },
                    ExitPosition = new Position() { X = 3, Y = 2 },
                    StartPosition = new Position() { X = 5, Y = 1 },
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
                    BoardSize = new Position() { X = 5, Y = 4 },
                    MinePositions =
                        new List<Position>()
                        {
                            new Position() { X = 1, Y = 1 },
                            new Position() { X = 1, Y = 3 },
                            new Position() { X = 3, Y = 3 }
                        },
                    ExitPosition = new Position() { X = 4, Y = 2 },
                    StartPosition = new Position() { X = 0, Y = 1 },
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
