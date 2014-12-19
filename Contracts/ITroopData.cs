using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITroopData
    {
        bool IsUnique { get; set;}
        string Name { get; set; }
        UnitCategory Category { get; set; }
        
    }
}
