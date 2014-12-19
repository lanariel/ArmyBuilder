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
        static TestHelper()
        {
            foreach (UnitCategory category in (UnitCategory[]) Enum.GetValues(typeof(UnitCategory)))
            {
                TroopDatas.Add(category, new TroopData() { Name = category.ToString(), IsUnique = false, Category = category });
            }
        }
        static Dictionary<UnitCategory, TroopData> TroopDatas = new Dictionary<UnitCategory,TroopData>();
        public static Unit CreateUnit(UnitCategory Category = UnitCategory.Core, int Points = 1, ITroopData TroopData = null)
        {
            if (TroopData == null)
                TroopData = TroopDatas[Category];
            Unit u = new Unit();
            u.TroopData = TroopData;
            u.Points = Points;
            return u;
        }

        public static IEnumerable<Unit> CreateUnits(UnitCategory Category= UnitCategory.Core, int Points=1, int Amount = 1, ITroopData TroopData = null)
        {
            List<Unit> units = new List<Unit>();
            for (int i = 0; i < Amount; i++)
            {
                units.Add(CreateUnit(Category, Points, TroopData));
            }
            return units;
        }

        public static Army CreateArmy(int Core = 3, int Special = 0, int Rare = 0, int Hero = 1, int Lord = 0, int GeneralIndex = -1)
        {
            Army a = new Army();
            List<Unit> units = a.Units;

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

            if (GeneralIndex == -1)
            {
                int i = Core + Special + Rare + Hero + Lord - 1;
                if (i > -1)
                {
                    a.General = a.Units[i];
                }
                else
                {
                    a.General = null;
                }
            }
            else
            {
                a.General = a.Units[GeneralIndex];
            }
            return a;
        }
    }
}
