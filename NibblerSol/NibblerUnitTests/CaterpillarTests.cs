using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NibblerBackEnd;

namespace NibblerUnitTests
{
    [TestClass]
    public class CaterpillarTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Caterpillar testyboi = new Caterpillar(new Point(5,5));

            Point head = new Point(5, 5);
            Point tail = new Point(4, 5);

            Assert.AreEqual(Direction.RIGHT, testyboi.Direction);
            Assert.AreEqual(head.X, testyboi.GetHead().X);
            Assert.AreEqual(head.Y, testyboi.GetHead().Y);
            Assert.AreEqual(tail.X, testyboi.GetTail().X);
            Assert.AreEqual(tail.Y, testyboi.GetTail().Y);
        }

        [TestMethod]
        public void MoveTest()
        {
            Caterpillar testyboi = new Caterpillar(new Point(5, 5));

            testyboi.Update();
            testyboi.Update();
            testyboi.Update();

            Point head = new Point(8,5);

            Assert.AreEqual(head.X, testyboi.GetHead().X);
            Assert.AreEqual(head.Y, testyboi.GetHead().Y);
        }

        [TestMethod]
        public void ChangeDirectionTests()
        {
            Caterpillar testyboi = new Caterpillar(new Point(5, 5));

            testyboi.ChangeDirection(Direction.LEFT);

            Assert.AreEqual(Direction.RIGHT, testyboi.Direction);

            testyboi.ChangeDirection(Direction.UP);

            Assert.AreEqual(Direction.UP, testyboi.Direction);
        }
    }
}
