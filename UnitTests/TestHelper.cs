using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    static class TestHelper
    {
        public static Unit CreateUnit(UnitCategory Category = UnitCategory.Core, int Points = 1, string UnitName = "")
        {
            Unit u = new Unit();
            u.Category = Category;
            u.Points = Points;
            u.UnitName = UnitName;
            return u;
        }

        public static IEnumerable<Unit> CreateUnits(UnitCategory Category= UnitCategory.Core, int Points=1, int Amount = 1, string UnitName="")
        {
            List<Unit> units = new List<Unit>();
            for (int i = 0; i < Amount; i++)
            {
                units.Add(CreateUnit(Category, Points, UnitName));
            }
            return units;
        }

        public static IEnumerable<Unit> CreateArmy(int Core = 3, int Special = 0, int Rare = 0, int Hero = 1, int Lord = 0)
        {
            List<Unit> units = new List<Unit>();

            for (int i = 0; i < Core; i++)
            {
                units.Add(CreateUnit(Category:  UnitCategory.Core));
            }

            for (int i = 0; i < Special; i++)
            {
                units.Add(CreateUnit(Category: UnitCategory.Special));
            }

            for (int i = 0; i < Rare; i++)
            {
                units.Add(CreateUnit(Category: UnitCategory.Rare));
            }

            for (int i = 0; i < Hero; i++)
            {
                units.Add(CreateUnit(Category: UnitCategory.Hero));
            }

            for (int i = 0; i < Lord; i++)
            {
                units.Add(CreateUnit(Category: UnitCategory.Lord));
            }
            return units;
        }
    }
}
