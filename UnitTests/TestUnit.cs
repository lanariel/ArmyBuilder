using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Core.MagicItems;

namespace UnitTests
{
    using Constants = MiscDefines;

    [TestClass]
    public class TestUnit
    {
        #region AddMagicItem
        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.UnitPassAddMagicItem)]
        [TestCategory(Constants.Unit)]
        public void AddMagicItemWhenNoneExists()
        {
            MagicItem mi = new MagicItem();
            Unit u = new Unit();
           
            u.AddMagicItem(mi);
            
            Assert.AreEqual(mi, u.MagicItem);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.UnitPassAddMagicItem)]
        [TestCategory(Constants.Unit)]
        public void AddMagicItemNullWhenNoneExists()
        {
            Unit u = new Unit();
            
            u.AddMagicItem(null);
            
            Assert.IsNull(u.MagicItem);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.UnitPassAddMagicItem)]
        [TestCategory(Constants.Unit)]
        public void AddMagicItemNullWhenOneExists()
        {
            MagicItem mi = new MagicItem();
            Unit u = new Unit();
            
            u.AddMagicItem(mi);
            u.AddMagicItem(null);
            
            Assert.AreEqual(mi, u.MagicItem);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.UnitPassAddMagicItem)]
        [TestCategory(Constants.Unit)]
        public void AddMagicItemWhenOneExists()
        {
            MagicItem mi1 = new MagicItem();
            MagicItem mi2 = new MagicItem();
            Unit u = new Unit();
            
            u.AddMagicItem(mi1);
            u.AddMagicItem(mi2);

            Assert.AreEqual(mi2, u.MagicItem);
        }

        #endregion

        #region RemoveMagicItem
        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.UnitPassRemoveMagicItem)]
        [TestCategory(Constants.Unit)]
        public void RemoveMagicItemWithItemSet()
        {
            MagicItem mi = new MagicItem();
            Unit u = new Unit();

            u.AddMagicItem(mi);
            u.RemoveItem();

            Assert.IsNull(u.MagicItem);
        }

        [TestMethod]
        [TestProperty(Constants.TestPass, Constants.UnitPassRemoveMagicItem)]
        [TestCategory(Constants.Unit)]
        public void RemoveMagicItemWithNoneSet()
        {
            Unit u = new Unit();

            u.RemoveItem();

            Assert.IsNull(u.MagicItem);
        }

        #endregion
    }
}
