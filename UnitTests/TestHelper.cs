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
        public static Unit CreateUnit( UnitCategory Category = UnitCategory.Core, int Points= 1)
        {
            Unit u = new Unit();
            u.Category = Category;
            return u;
        }

        public static IEnumerable<Unit> CreateUnits(UnitCategory Category= UnitCategory.Core, int Points=1, int Amount = 1)
        {
            List<Unit> units = new List<Unit>();
            for (int i = 0; i < Amount; i++)
            {
                units.Add(CreateUnit(Category, Points));
            }
            return units;
        }

        public static IEnumerable<Unit> CreateArmy()
        {
            List<Unit> units = new List<Unit>();
            for (int i = 0; i < 3; i++)
            {
                units.Add(CreateUnit());
            }
            units.Add(CreateUnit(Category: UnitCategory.Hero));
            return units;
        }
    }
}
