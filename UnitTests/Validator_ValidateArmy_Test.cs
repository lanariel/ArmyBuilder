﻿using System;
using NUnit.Framework;
using Contracts;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class Validator_ValidateArmy_Test
    {
        #region Null

        [Test]
        [Category("Null")]
        public void NullArmy()
        {
            Validator v = new Validator();

            var res = v.Validate(null);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.NullUnit));
        }

        [Test]
        [TestCase(1, 0, Category = "Null")]
        [TestCase(2, 0, Category = "Null")]
        [TestCase(2, 1, Category = "Null")]
        [TestCase(3, 1, Category = "Null")]
        public void NullUnit(int NrOfUnits, int NullUnit)
        {
            Validator v = new Validator();
            List<Unit> units = new List<Unit>();
            for (int i = 0; i < NrOfUnits; i++)
            {
                if (i == NullUnit)
                {
                    units.Add(null);
                    continue;
                }
                units.Add(TestHelper.CreateUnit());
            }

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.NullUnit));
            Assert.True(res.Count(r => r == InvalidReason.NullUnit) == res.Count());
        }

        #endregion

        #region FewUnits

        [Test]
        [TestCase(0,0,0, Category = "FewUnits")]
        [TestCase(1,0,0, Category = "FewUnits")]
        [TestCase(2,0,0, Category = "FewUnits")]

        [TestCase(0, 1, 0, Category = "FewUnits")]
        [TestCase(1, 5, 0, Category = "FewUnits")]
        [TestCase(2, 100, 0, Category = "FewUnits")]

        [TestCase(0, 0, 1, Category = "FewUnits")]
        [TestCase(1, 0, 5, Category = "FewUnits")]
        [TestCase(2, 0, 100, Category = "FewUnits")]

        [TestCase(0, 1, 1, Category = "FewUnits")]
        [TestCase(1, 5, 5, Category = "FewUnits")]
        [TestCase(2, 100, 100, Category = "FewUnits")]
        public void TooFewUnits(int NrOfUnits, int NrOfHeroes = 0, int NrOfLords = 0)
        {
            Validator v = new Validator();
            List<Unit> units = new List<Unit>();
            for (int i = 0; i < NrOfUnits; i++)
            {
                units.Add(TestHelper.CreateUnit());
            }
            for (int i = 0; i < NrOfHeroes; i++)
            {
                units.Add(TestHelper.CreateUnit(Category: UnitCategory.Hero));
            }

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.FewUnits));
        }

        #endregion

        #region Character

        #endregion

        #region Points
        #region Core
        [Test]
        [Category("CorePoint")]
        public void NoCore()
        {
            Validator v = new Validator();
            var units = TestHelper.CreateArmy(Core: 0);

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.CorePoint));
        }

        [Test]
        [Category("CorePoint")]
        public void FiveUnits()
        {
            Validator v = new Validator();
            var units = TestHelper.CreateArmy(Core: 1, Special: 1, Rare: 1, Hero: 1, Lord: 1);

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.CorePoint));
        }
        #endregion
        #region Special
        [Test]
        [Category("SpecialPoint")]
        public void SpecialPoint()
        {
            Validator v = new Validator();
            var units = TestHelper.CreateArmy(Core: 3, Special: 6, Rare: 0, Hero: 1, Lord: 0);

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.SpecialPoint));
        }
        #endregion
        #region Rare
        [Test]
        [Category("RarePoint")]
        public void RarePoint()
        {
            Validator v = new Validator();
            var units = TestHelper.CreateArmy(Core:1, Special:0, Rare:2, Hero:1, Lord:0);

            var res = v.Validate(units);

            Assert.True(res.Any(), "valid");
            Assert.True(res.Any(r => r == InvalidReason.RarePoint));
        }
        #endregion
        #region Hero
        [Test]
        [Category("HeroPoint")]
        public void HeroPoint()
        {
            Validator v = new Validator();
            var units = TestHelper.CreateArmy(Core: 2, Special: 0, Rare: 0, Hero: 2, Lord: 0);

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.HeroPoint));
        }
        #endregion
        #region Lord
        [Test]
        [Category("LordPoint")]
        public void LordPoint()
        {
            Validator v = new Validator();
            var units = TestHelper.CreateArmy(Core: 2, Special: 0, Rare: 0, Hero: 0, Lord: 2);

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.LordPoint));
        }
        #endregion
        #endregion

        #region Valid

        [Test]
        [Category("Valid")]
        public void CoreHero()
        {
            Validator v = new Validator();

            var res = v.Validate(TestHelper.CreateArmy());

            Assert.False(res.Any());
        }

        [Test]
        [Category("Valid")]
        public void NoLord()
        {
            Validator v = new Validator();
            IEnumerable<Unit> units = TestHelper.CreateArmy(1, 1, 1, 1);

            var res = v.Validate(units);

            Assert.False(res.Any());
        }

        #endregion

    }
}
