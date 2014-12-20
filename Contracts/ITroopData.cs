using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public enum TroopType
    {
        Infantry,
        MonsrousInfantry,
        Cavalry,
        MonstrousCavalry,
        WarBeasts,
        MonstrousBeasts,
        Swarms,
        Monster,
    }
    public interface ITroopData
    {
        bool IsUnique { get; set;}
        string Name { get; set; }
        UnitCategory Category { get; set; }
        int PointsPerModel { get; set; }
        int MinSize { get; set; }
        int MaxSize { get; set; }
        TroopType TroopType { get; set; }
    }
}
