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
        public const string CoreSystemSave = "CoreSystemPassSave";
        public const string CoreSystemLoad = "CoreSystemPassLoad";
        public const string CoreSystemShow = "CoreSystemPassShow";
        #endregion

        #region ArmyPasses
        public const string ArmyPassAddUnit = "AddUnit";
        public const string ArmyPassRemoveUnit = "RemoveUnit";
        #endregion

        #region UnitPasses
        public const string UnitPassAddMagicItem = "AddMagicItem";
        public const string UnitPassRemoveMagicItem = "RemoveMagicItem";
        #endregion
    }
}
