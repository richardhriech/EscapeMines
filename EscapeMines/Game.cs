using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Enums;
using Data;

namespace EscapeMines
{
    public sealed class Game
    {
        public Game()
        {
        }

        public Board Board { get; set; }

        public List<Move> Moves { get; set; }

        public bool IsGameOver { get; set; }

        public Result Result { get; set; }


        public void Setup(string path)
        {
            try
            {
                IDataProvider dataProvider = new DataProviderFromFile(path);
                IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider);
                GameConfig config = configParser.ParseConfig();
                Board = new Board(config);
                Board.BuildBoard();
                Moves = config.Moves;
            }
            catch (ArgumentException ex)
            {
                ErrorHandler(ex);
            }
            catch (FileNotFoundException ex)
            {
                ErrorHandler(ex);
            }
        }

        public void PlayMoves()
        {
            WritePlayerPosition();
            SetResult();
            WriteResult();

            foreach (Move move in Moves)
            {
                if (IsGameOver)
                {
                    break;
                }

                switch (move)
                {
                    case Move.TurnLeft:
                        Board.Player.Direction = Board.Player.Direction.PreviousEnumValue();
                        Console.WriteLine("You turned left.");
                        WritePlayerDirection();
                        WritePlayerPosition();

                        break;

                    case Move.TurnRight:
                        Board.Player.Direction = Board.Player.Direction.NextEnumValue();
                        Console.WriteLine("You turned right.");
                        WritePlayerDirection();
                        WritePlayerPosition();

                        break;

                    case Move.Move:

                        switch (Board.Player.Direction)
                        {
                            case Direction.North:
                                Board.Player.Position.Y += 1;

                                break;

                            case Direction.East:
                                Board.Player.Position.X += 1;

                                break;

                            case Direction.South:
                                Board.Player.Position.Y -= 1;

                                break;

                            case Direction.West:
                                Board.Player.Position.X -= 1;

                                break;
                        }

                        if (Board.Player.Position.X < Board.MinPosition.X
                         || Board.Player.Position.Y < Board.MinPosition.Y
                         || Board.Player.Position.X > Board.MaxPosition.X
                         || Board.Player.Position.Y > Board.MaxPosition.Y)
                        {
                            IsGameOver = true;
                            Result = Result.InvalidMove;

                            break;
                        }

                        Console.WriteLine("You moved ahead.");
                        WritePlayerPosition();

                        SetResult();

                        break;
                }

                WriteResult();
            }


        }

        private void WriteResult()
        {
            switch (Result)
            {
                case Result.Success:
                    Console.WriteLine("Congratulations! :) You've found the exit!\n");

                    break;

                case Result.MineHit:
                    Console.WriteLine("You hit a mine. :( Rest in pieces.\n");

                    break;

                case Result.Danger:
                    Console.WriteLine("Good move, but you are still in danger!\n");

                    break;

                case Result.InvalidMove:
                    Console.WriteLine("Invalid move. :( You went outside the map.\n");

                    break;
            }
        }

        private void SetResult()
        {
            Field currentField =
                Board.Fields
                    .FirstOrDefault(field => Equals(field?.Position, Board.Player.Position));

            if (currentField != null)
            {
                switch (currentField.FieldType)
                {
                    case FieldType.Mine:
                        Result = Result.MineHit;
                        IsGameOver = true;

                        break;

                    case FieldType.Exit:
                        Result = Result.Success;
                        IsGameOver = true;

                        break;
                }
            }
            else
            {
                Result = Result.Danger;
            }
        }

        private void ErrorHandler(Exception ex)
        {
            Console.WriteLine("Error occurred.");
            Console.WriteLine(ex.Message);
            Environment.Exit(-1);
        }

        private void WritePlayerPosition()
        {
            Console.WriteLine($"Your coordinates are: {Board.Player.Position}");
        }

        private void WritePlayerDirection()
        {
            Console.WriteLine($"You are looking {Board.Player.Direction.ToString().ToLower()}.");
        }
    }
}
