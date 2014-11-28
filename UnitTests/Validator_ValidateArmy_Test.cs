using System;
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
        const string CorePoints = "CorePoints";
        const string SpecialPoints = "SpecialPoints";
        const string RarePoints = "RarePoints";
        const string HeroPoints = "HeroPoints";
        const string LordPoints = "LordPoints";
        [Test]
        [TestCase(0, 0, 0, 0, 0, InvalidReason.CorePoint, TestName="Core: 0 Rest: 0", Category = CorePoints)]
        [TestCase(10, 0, 0, 0, 0, InvalidReason.CorePoint, TestName = "Core: 10 Rest: 0", Category = CorePoints)]
        [TestCase(24, 0, 0, 0, 0, InvalidReason.CorePoint, TestName = "Core: 24 Rest: 0 ", Category = CorePoints)]

        [TestCase(0, 10, 10, 10, 10, InvalidReason.CorePoint, TestName = "Core: 0 Rest: 10", Category = CorePoints)]
        [TestCase(10, 10, 10, 10, 10, InvalidReason.CorePoint, TestName = "Core: 10 Rest: 10", Category = CorePoints)]
        [TestCase(24, 10, 10, 10, 10, InvalidReason.CorePoint, TestName = "Core: 24 Rest: 10", Category = CorePoints)]

        [TestCase(0, 100, 100, 100, 100, InvalidReason.CorePoint, TestName = "Core: 0 Rest: 100", Category = CorePoints)]
        [TestCase(10, 100, 100, 100, 100, InvalidReason.CorePoint, TestName = "Core: 10 Rest: 100", Category = CorePoints)]
        [TestCase(24, 100, 100, 100, 100, InvalidReason.CorePoint, TestName = "Core: 24 Rest: 100", Category = CorePoints)]

        [TestCase(0, 26, 0, 0, 0, InvalidReason.SpecialPoint, TestName = "Special: 26 Rest: 0", Category=SpecialPoints)]
        [TestCase(0, 50, 0, 0, 0, InvalidReason.SpecialPoint, TestName = "Special: 50 Rest: 0", Category = SpecialPoints)]
        [TestCase(0, 100, 0, 0, 0, InvalidReason.SpecialPoint, TestName = "Special: 100 Rest: 0", Category = SpecialPoints)]

        public void PercentPerUnitCategory(int Core,int Special, int Rare, int Hero, int Lord, InvalidReason reason)
        {
            Validator v = new Validator();
            List<Unit> units = new List<Unit>();

            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Core, Points: Core));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Special, Points: Special));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Rare, Points: Rare));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Hero, Points: Hero));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Lord, Points: Lord));

            var res = v.Validate(units);

            Assert.True(res.Any());
            Assert.Contains(reason, res.ToList());
        }
        #endregion

        #region Valid

        [Test]
        [Category("Valid")]
        public void ValidArmy()
        {
            Validator v = new Validator();

            var res = v.Validate(TestHelper.CreateArmy());

            Assert.False(res.Any());
        }

        #endregion

    }
}
