using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;



namespace UnitTests
{
    using Constants = MiscDefines;

    [TestClass]
    public class TestArmy : Army
    {
        #region AddUnit

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassAddUnit)]
        [TestCategory(Constants.Army)]
        public void AddUnitWhenNoneExist()
        {
            Army a = new TestArmy();
            Unit u = new Unit();

            a.AddUnit(u);

            CollectionAssert.AreEqual(new Unit[] { u }, a.Units);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassAddUnit)]
        [TestCategory(Constants.Army)]
        public void AddUnitNullWhenNoneExist()
        {
            Army a = new TestArmy();

            a.AddUnit(null);

            CollectionAssert.AreEqual(new Unit[] { }, a.Units);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassAddUnit)]
        [TestCategory(Constants.Army)]
        public void AddUnitNullWhenOneExist()
        {
            Army a = new TestArmy();
            Unit u = new Unit();

            a.AddUnit(u);
            a.AddUnit(null);

            CollectionAssert.AreEqual(new Unit[] { u }, a.Units);
        }


        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassAddUnit)]
        [TestCategory(Constants.Army)]
        public void AddUnitWhenOneExists()
        {
            Army a = new TestArmy();
            Unit u1 = new Unit();
            Unit u2 = new Unit();

            a.AddUnit(u1);
            a.AddUnit(u2);

            CollectionAssert.AreEqual(new Unit[] { u1, u2 }, a.Units);
        }

        #endregion

        #region RemoveUnit

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassRemoveUnit)]
        [TestCategory(Constants.Army)]
        public void RemoveUnitWhenNonePresent()
        {
            Army a = new TestArmy();
            Unit u = new Unit();

            a.RemoveUnit(u);

            CollectionAssert.AreEqual(new Unit[] { }, a.Units);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassRemoveUnit)]
        [TestCategory(Constants.Army)]
        public void RemoveUnitNullWhenNonePresent()
        {
            Army a = new TestArmy();

            a.RemoveUnit(null);

            CollectionAssert.AreEqual(new Unit[] { }, a.Units);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassRemoveUnit)]
        [TestCategory(Constants.Army)]
        public void RemoveUnitNullWhenOnePresent()
        {
            Army a = new TestArmy();
            Unit u = new Unit();

            a.AddUnit(u);
            a.RemoveUnit(null);

            CollectionAssert.AreEqual(new Unit[] { u }, a.Units);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassRemoveUnit)]
        [TestCategory(Constants.Army)]
        public void RemoveUnitWhenOnePresent()
        {
            Army a = new TestArmy();
            Unit u = new Unit();

            a.AddUnit(u);
            a.RemoveUnit(u);

            CollectionAssert.AreEqual(new Unit[] { }, a.Units);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.ArmyPassRemoveUnit)]
        [TestCategory(Constants.Army)]
        public void RemoveUnitWhenTwoPresent()
        {
            Army a = new TestArmy();
            Unit u1 = new Unit();
            Unit u2 = new Unit();

            a.AddUnit(u1);
            a.AddUnit(u2);
            a.RemoveUnit(u1);

            CollectionAssert.AreEqual(new Unit[] { u2 }, a.Units);
        }

        #endregion
    }
}
