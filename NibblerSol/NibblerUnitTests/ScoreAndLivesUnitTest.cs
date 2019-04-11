using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NibblerBackEnd;

namespace NibblerUnitTests
{
    /// <summary>
    /// Summary description for ScoreAndLivesUnitTest
    /// </summary>
    [TestClass]
    public class ScoreAndLivesUnitTest
    {
        [TestMethod]
        public void TestConstructorMethod1()
        {
            ScoreAndLives testyboi = new ScoreAndLives();

            Assert.AreEqual(testyboi.Lives, 3);
            Assert.AreEqual(testyboi.Score, 0);
        }

        [TestMethod]
        public void AddPointsAndLivesTest()
        {
            ScoreAndLives testyboi = new ScoreAndLives();
            CaterpillarGrower HelpyMcHelpful = new CaterpillarGrower(1000000, 1000000);

            testyboi.AddPointsAndLives(HelpyMcHelpful, null);

            Assert.AreEqual(1000000, testyboi.Score);
            Assert.AreEqual(1000003, testyboi.Lives);
        }
    }
}
