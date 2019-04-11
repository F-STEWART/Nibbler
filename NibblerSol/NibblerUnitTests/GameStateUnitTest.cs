using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NibblerBackEnd;

namespace NibblerUnitTests
{
    /// <summary>
    /// Summary description for GameStateUnitTest
    /// </summary>
    [TestClass]
    public class GameStateUnitTest
    {
        public GameStateUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ConstructorTest()
        {
            GameState gameState = new GameState();
            if (gameState is GameState)
                Assert.IsTrue(true);
            else
                Assert.Fail();

            if (gameState.Caterpillar is Caterpillar)
                Assert.IsTrue(true);
            else
                Assert.Fail();

            if (gameState.Grid is Grid)
                Assert.IsTrue(true);
            else
                Assert.Fail();
            if (gameState.ScoreAndLives is ScoreAndLives)
                Assert.IsTrue(true);
            else
                Assert.Fail();

            bool IsThereAToken = false;
            for (int i = 0; i < gameState.Grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < gameState.Grid.tiles.GetLength(1); j++)
                {
                    if (gameState.Grid.tiles[i, j] is CaterpillarGrower || gameState.Grid.tiles[i, j] is CaterpillarShrinker)
                        IsThereAToken = true;
                }
            }
            Assert.IsTrue(IsThereAToken);
            int MiddleX = gameState.Grid.tiles.GetLength(0) / 2;
            int MiddleY = gameState.Grid.tiles.GetLength(1) / 2;
            Assert.IsTrue(new Point(MiddleX, MiddleY) == gameState.Caterpillar.GetHead());
        }
        [TestMethod]
        public void RandomTokenUnitTest()
        {
            GameState gameState = new GameState();
            Assert.IsTrue(GameState.RandomToken(gameState.Grid, gameState.Caterpillar) is ICollidable);
        }
    }
}
