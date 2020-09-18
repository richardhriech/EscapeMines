using System;
using System.Collections.Generic;
using Common;
using Common.Enums;
using EscapeMines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.EscapeMines
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void PlayMoves_MineOnStart_ShouldBeDeadImmediately()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(3, 3),
                Player = new Player(new Position(0, 0), Direction.North),
                Fields = new List<Field>() { new Field(new Position(0, 0), FieldType.Mine) }
            };

            var game = new Game
            {
                Board = board,
                Moves = new List<Move>() { Move.Move }
            };

            game.PlayMoves();

            Assert.AreEqual(Result.MineHit, game.Result);
        }

        [TestMethod]
        public void PlayMoves_ExitOnStart_ShouldBeSuccessImmediately()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(3, 3),
                Player = new Player(new Position(0, 0), Direction.North),
                Fields = new List<Field>() { new Field(new Position(0, 0), FieldType.Exit) }
            };

            var game = new Game
            {
                Board = board,
                Moves = new List<Move>() { Move.Move }
            };

            game.PlayMoves();

            Assert.AreEqual(Result.Success, game.Result);
        }

        [TestMethod]
        public void PlayMoves_NorthRotateLeft_PlayerDirectionShouldBeWest()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(0, 0), Direction.North)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.TurnLeft } };

            game.PlayMoves();

            Assert.AreEqual(Direction.West, board.Player.Direction);
        }

        [TestMethod]
        public void PlayMoves_EastRotateLeft_PlayerDirectionShouldBeNorth()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(0, 0), Direction.East)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.TurnLeft } };

            game.PlayMoves();

            Assert.AreEqual(Direction.North, board.Player.Direction);
        }

        [TestMethod]
        public void PlayMoves_WestRotateRight_PlayerDirectionShouldBeNorth()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(0, 0), Direction.West)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.TurnRight } };

            game.PlayMoves();

            Assert.AreEqual(Direction.North, board.Player.Direction);
        }

        [TestMethod]
        public void PlayMoves_SouthRotateRight_PlayerDirectionShouldBeWest()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(0, 0), Direction.South)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.TurnRight } };

            game.PlayMoves();

            Assert.AreEqual(Direction.West, board.Player.Direction);
        }

        [TestMethod]
        public void PlayMoves_MovingOutsideTheMap_ShouldBeGameOver()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(5, 5), Direction.North)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.IsTrue(game.IsGameOver);
        }

        [TestMethod]
        public void PlayMoves_ValidMoveNorth_PlayerYCoordShouldBeIncreasedBy1()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(3, 3), Direction.North)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.AreEqual(new Position(3, 4), board.Player.Position);
        }

        [TestMethod]
        public void PlayMoves_ValidMoveEast_PlayerXCoordShouldBeIncreasedBy1()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(3, 3), Direction.East)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.AreEqual(new Position(4, 3), board.Player.Position);
        }

        [TestMethod]
        public void PlayMoves_ValidMoveSouth_PlayerYCoordShouldBeDecreasedBy1()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(3, 3), Direction.South)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.AreEqual(new Position(3, 2), board.Player.Position);
        }

        [TestMethod]
        public void PlayMoves_ValidMoveWest_PlayerXCoordShouldBeDecreasedBy1()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(3, 3), Direction.West)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.AreEqual(new Position(2, 3), board.Player.Position);
        }

        [TestMethod]
        public void PlayMoves_MineFieldAhead_ResultShouldBeMineHit()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(3, 3), Direction.North),
                Fields = new List<Field>() { new Field(new Position(3, 4), FieldType.Mine) }
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.AreEqual(Result.MineHit, game.Result);
        }

        [TestMethod]
        public void PlayMoves_ExitAhead_ResultShouldBeSuccess()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(5, 5),
                Player = new Player(new Position(3, 3), Direction.North),
                Fields = new List<Field>() { new Field(new Position(3, 4), FieldType.Exit) }
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.AreEqual(Result.Success, game.Result);
        }

        [TestMethod]
        public void PlayMoves_OutOfBoundsAhead_ResultShouldBeInvalidMove()
        {
            GameConfig config = null;

            var board = new Board(config)
            {
                MaxPosition = new Position(3, 3),
                Player = new Player(new Position(3, 3), Direction.North)
            };

            var game = new Game { Board = board, Moves = new List<Move>() { Move.Move } };

            game.PlayMoves();

            Assert.AreEqual(Result.InvalidMove, game.Result);
        }
    }
}
