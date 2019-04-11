using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NibblerBackEnd;

namespace NibblerUnitTests
{
    /// <summary>
    /// Summary description for GridTests
    /// </summary>
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorTests()
        {
            Grid testyboi = new Grid(new ICollidable[10,10], (Grid grid, Caterpillar caterpillar) => new CaterpillarGrower(10,10));

            Assert.IsNotNull(testyboi);
        }
    }
}
