using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class MiscDefines
    {
        public const string TestPass = "TestPass";
        public const string Unit = "Unit";
        public const string Army = "Army";
        public const string CoreSystem = "CoreSystem";

        #region CoreSystemPasses
        public const string CoreSystemSaveLoad = "CoreSystemPassSaveLoad";
        public const string CoreSystemFileName = "TestArmy.army";
        public const string CoreSystemShowUnits = "CoreSystemPassShow";
        #endregion

        #region ArmyPasses
        public const string ArmyPassAddUnit = "ArmyPassAddUnit";
        public const string ArmyPassRemoveUnit = "ArmyPassRemoveUnit";
        #endregion

        #region UnitPasses
        public const string UnitPassAddMagicItem = "UnitPassAddMagicItem";
        public const string UnitPassRemoveMagicItem = "UnitPassRemoveMagicItem";
        #endregion
    }
}
