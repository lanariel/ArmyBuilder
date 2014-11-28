using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public enum UnitCategory
    {
        Lord,
        Hero,
        Core,
        Special,
        Rare,
    }

    public class Unit
    {
        private int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        private UnitCategory category;

        public UnitCategory Category
        {
            get { return category; }
            set { category = value; }
        }


    }
}
