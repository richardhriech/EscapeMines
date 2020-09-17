using System;
using System.Collections.Generic;
using Common;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Data
{
    [TestClass]
    public class GameConfigParserFromFileTest
    {
        [TestMethod]
        public void ParseConfig_EmptyList_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(new List<string>());

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_InvalidLineCount_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "This",
                        "is",
                        "Only three lines"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_InvalidPositionFormat_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5,4",
                        "1,1 1,3 3,3",
                        "4 2",
                        "0 1 N",
                        "R M L M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_InvalidPositionCharacter_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5 4",
                        "1,1 1,3 3,3",
                        "4a 2",
                        "0 1 N",
                        "R M L M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_InvalidCharInMinePosition_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5 4",
                        "1,asd 1,3 3,3",
                        "4 2",
                        "0 1 N",
                        "R M L M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_InvalidMinePositionFormat_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5 4",
                        "1,1,1,3 3,3",
                        "4 2",
                        "0 1 N",
                        "R M L M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_StartPositionArgMissing_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5 4",
                        "1,1 1,3 3,3",
                        "4 2",
                        "0 1",
                        "R M L M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_StartPositionInvalidDirection_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5 4",
                        "1,1 1,3 3,3",
                        "4 2",
                        "0 1 K",
                        "R M L M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_InvalidMove_ShouldThrowArgumentException()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5 4",
                        "1,1 1,3 3,3",
                        "4 2",
                        "0 1 N",
                        "R M K M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => configParser.ParseConfig());
        }

        [TestMethod]
        public void ParseConfig_ValidData_ShouldReturnValidConfig()
        {
            var dataProvider = new Mock<IDataProvider>();
            dataProvider
                .Setup(provider => provider.GetRawData())
                .Returns(
                    new List<string>()
                    {
                        "5 4",
                        "1,1 1,3 3,3",
                        "4 2",
                        "0 1 N",
                        "R M L M M"
                    }
                );

            IGameConfigParser configParser = new GameConfigParserFromFile(dataProvider.Object);

            GameConfig actualConfig = configParser.ParseConfig();

            GameConfig expectedConfig =
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
                    Moves =
                        new List<Move>()
                        {
                            Move.TurnRight,
                            Move.Move,
                            Move.TurnLeft,
                            Move.Move,
                            Move.Move
                        }
                };

            Assert.AreEqual(expectedConfig, actualConfig);
        }
    }
}
