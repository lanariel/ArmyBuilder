using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Army
    {
        public Army()
        {
            Units = new List<Unit>();
            Points = 0;
        }
        public List<Unit> Units { get; set; }

        public int Points { get; set; }

        public Unit General { get; set; }

        public Unit BsB { get; set; }

    }
}
