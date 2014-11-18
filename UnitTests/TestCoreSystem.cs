using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;

namespace UnitTests
{
    using Constants = MiscDefines;

    [TestClass]
    public class TestCoreSystem
    {
        [TestMethod]
        public void TestLoadAssembly()
        {
            
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.CoreSystemSaveLoad)]
        [TestCategory(Constants.CoreSystem)]
        public void SaveAndLoadUnit()
        {
            Army a1 = new TestArmy();
            Unit u1 = new Unit();
            Unit u2 = new Unit();

            u1.AddMagicItem(new Core.MagicItems.MagicItem());
            a1.AddUnit(u1);
            a1.AddUnit(u2);

            CoreSystem.Save(a1,Constants.CoreSystemFileName);

            Army a2 = CoreSystem.Load(Constants.CoreSystemFileName);

            Assert.IsNotNull(a2);
            Assert.AreEqual(2, a2.Units.Length);
            Assert.IsNotNull(a2.Units[0].MagicItem);
        }
    }
}
