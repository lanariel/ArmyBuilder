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
        [TestProperty(Constants.TestPass, Constants.ArmyPassAddUnit)]
        [TestCategory(Constants.Army)]
        public void RemoveUnitNullWhenOnePresent()
        {

        }
    }
}
