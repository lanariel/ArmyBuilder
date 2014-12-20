using System;
using NUnit.Framework;
using Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    public class Validator_ValidateArmy_Test
    {
        #region Null

        [Test]
        [Category("Null")]
        [ExpectedException( typeof(NullReferenceException))]
        public void NullArmy()
        {
            Validator v = new Validator();

            var res = v.Validate(null);
        }

        [Test]
        [TestCase(1, 0, Category = "Null")]
        [TestCase(2, 0, Category = "Null")]
        [TestCase(2, 1, Category = "Null")]
        [TestCase(3, 1, Category = "Null")]
        [ExpectedException(typeof(NullReferenceException))]
        public void NullUnit(int NrOfUnits, int NullUnit)
        {
            Validator v = new Validator();
            Army a = new Army();
            for (int i = 0; i < NrOfUnits; i++)
            {
                if (i == NullUnit)
                {
                    a.Units.Add(null);
                    continue;
                }
                a.Units.Add(TestHelper.CreateUnit());
            }

            var res = v.Validate(a);
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
            Army a = new Army();
            List<Unit> units = a.Units;
            for (int i = 0; i < NrOfUnits; i++)
            {
                units.Add(TestHelper.CreateUnit());
            }
            for (int i = 0; i < NrOfHeroes; i++)
            {
                units.Add(TestHelper.CreateUnit(Category: UnitCategory.Hero));
            }

            var res = v.Validate(a);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.FewUnits));
        }

        #endregion

        #region Character
        [Test]
        [Category("Character")]
        public void GeneralIsNotACharacter()
        {
            Validator v = new Validator();
            Army a = TestHelper.CreateArmy();
            a.General = a.Units[0];

            var res = v.Validate(a);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.GeneralIsNotCharacter));
        }
        
        [Test]
        [Category("Character")]
        public void GeneralIsBSB()
        {
            Validator v = new Validator();
            Army a = TestHelper.CreateArmy();
            a.General = a.Units[3];
            a.BsB = a.Units[3];

            var res = v.Validate(a);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.GeneralIsBSB));
        }
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

        #region Total

        [Test]
        [Category("TotalPoints")]
        public void TotalPoints()
        {
            Validator v = new Validator();
            Army a = TestHelper.CreateArmy(Core:4);
            a.Points = 4;

            var res = v.Validate(a);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.TotalPoint));
        }

        #endregion

        #endregion

        #region Duplicates
        
        #region Special
        
        [Test]
        [Category("Duplicates")]
        public void SpecialAllDuplicates()
        {
            Validator v = new Validator();
            Army a = TestHelper.CreateArmy(4, 4, 0, 4, 4);

            var res = v.Validate(a);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.DuplicateSpecial));
        }

        [Test]
        [Category("Duplicates")]
        public void SpecialOneOfTwoDuplicates()
        {
            Validator v = new Validator();
            Army a = TestHelper.CreateArmy(20, 0, 0, 5, 5);
            List<Unit> units = a.Units;
            TroopData Unit1 = new TroopData() { Name = "Special1", Category = UnitCategory.Special };
            TroopData Unit2 = new TroopData() { Name = "Special2", Category = UnitCategory.Special };

            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Special, Amount: 3, TroopData: Unit1));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Special, Amount: 3, TroopData: Unit2));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Special, Amount: 1, TroopData: Unit1));

            var res = v.Validate(a);

            Assert.True(res.Any(), "No Error");
            Assert.True(res.Any(r => r == InvalidReason.DuplicateSpecial),"Wrong Error");
            Assert.True(v.DuplicateSpecial.Any(r => r == Unit1), "Wrong Unit");
        }

        #endregion

        #region Rare
        
        [Test]
        [Category("Duplicates")]
        public void RareAllDuplicates()
        {
            Validator v = new Validator();
            var a = TestHelper.CreateArmy(3, 0, 3, 3, 3);

            var res = v.Validate(a);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.DuplicateRare));
        }

        [Test]
        [Category("Duplicates")]
        public void RareOneOfTwoDuplicates()
        {
            Validator v = new Validator();
            var a = TestHelper.CreateArmy(20, 0, 0, 5, 5);
            List<Unit> units = a.Units;
            TroopData Unit1 = new TroopData() { Name = "Rare1", Category = UnitCategory.Rare };
            TroopData Unit2 = new TroopData() { Name = "Rare2", Category = UnitCategory.Rare };

            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Rare, Amount: 2, TroopData: Unit1));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Rare, Amount: 2, TroopData: Unit2));
            units.AddRange(TestHelper.CreateUnits(Category: UnitCategory.Rare, Amount: 1, TroopData: Unit1));

            var res = v.Validate(a);

            Assert.True(res.Any());
            Assert.True(res.Any(r => r == InvalidReason.DuplicateRare));
            Assert.True(v.DuplicateRare.Any(r => r == Unit1));
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

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var item in res)
            {
                sb.AppendLine(item.ToString());
            }
            Assert.False(res.Any(), sb.ToString());
        }

        [Test]
        [Category("Valid")]
        public void NoLord()
        {
            Validator v = new Validator();

            var res = v.Validate(TestHelper.CreateArmy(1, 1, 1, 1));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var item in res)
            {
                sb.AppendLine(item.ToString());
            }
            Assert.False(res.Any(), sb.ToString());
        }

        [Test]
        [Category("Valid")]
        public void WithDuplicates()
        {
            Validator v = new Validator();

            var res = v.Validate(TestHelper.CreateArmy(20, 3, 2, 1));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var item in res)
            {
                sb.AppendLine(item.ToString());
            }
            Assert.False(res.Any(), sb.ToString());
        }

        [Test]
        [Category("Valid")]
        public void GrandArmyWithDuplicates()
        {
            Validator v = new Validator();
            Army a = TestHelper.CreateArmy(3500, 6, 4, 400, 90);
            a.Points = 4000;

            var res = v.Validate(a);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var item in res)
            {
                sb.AppendLine(item.ToString());
            }
            Assert.False(res.Any(), sb.ToString());
        }

        #endregion

    }
}
