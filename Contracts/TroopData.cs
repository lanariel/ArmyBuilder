using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class TroopData : ITroopData
    {
        public TroopData()
        {
            IsUnique = false;
            Name = "TestUnit";
        }
        public bool IsUnique { get; set; }

        public string Name { get; set; }

        public UnitCategory Category { get; set; }
    }
}
