using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public enum InvalidReason
    {
        FewUnits,
        NoCharacter,
        CorePoint,
        SpecialPoint,
        RarePoint,
        HeroPoint,
        LordPoint,
        TotalPoint,
        DuplicateSpecial,
        DuplicateRare,
        GeneralIsBSB,
        GeneralIsNotCharacter,
        Other,
    }

    public class Validator
    {
        ValidatorConfiguration config = new ValidatorConfiguration();

        public Validator()
        {
            foreach(UnitCategory category in (UnitCategory[])Enum.GetValues(typeof(UnitCategory)))
            {
                UnitList.Add(category, new List<Unit>());
            }
        }

        public ValidatorConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        protected List<ITroopData> duplicatesSpecial = new List<ITroopData>();
        protected List<ITroopData> duplicatesRare = new List<ITroopData>();
        protected ISet<InvalidReason> errors = new HashSet<InvalidReason>();
        protected Dictionary<UnitCategory, List<Unit>> UnitList = new Dictionary<UnitCategory, List<Unit>>();

        public IEnumerable<ITroopData> DuplicateSpecial { get { return duplicatesSpecial; } }

        public IEnumerable<ITroopData> DuplicateRare { get { return duplicatesRare; } }

        public ISet<InvalidReason> Validate(Army Army)
        {
            PreValidationSetup(Army);

            if (!ValidateNumberOfUnits(Army))
            {
                errors.Add(InvalidReason.FewUnits);
            }

            int totalpoints;
            if (Army.Points == 0)
            {
                totalpoints = (from u in Army.Units select u.Points).Sum();
            }
            else
            {
                totalpoints = Army.Points;
            }

            ValidatePoints(Army, totalpoints);

            if (!ValidateSpecialDuplicte(Army, totalpoints))
            {
                errors.Add(InvalidReason.DuplicateSpecial);
            }

            if (!ValidateRareDuplicate(Army, totalpoints))
            {
                errors.Add(InvalidReason.DuplicateRare);
            }

            ValidateGeneral(Army);

            return errors;
        }

        private void PreValidationSetup(Army Army)
        {
            errors.Clear();
            duplicatesSpecial.Clear();
            duplicatesRare.Clear();
            foreach (UnitCategory category in (UnitCategory[])Enum.GetValues(typeof(UnitCategory)))
            {
                UnitList[category].Clear();
            }

            foreach (var unit in Army.Units)
            {
                UnitList[unit.Category].Add(unit);
            }
        }

        private void ValidateGeneral(Army Army)
        {

            if (Army.General == null )
            {
                errors.Add(InvalidReason.NoCharacter);
                return;
            }

            if (!ValidateGeneralIsCharacter(Army))
            {
                errors.Add(InvalidReason.GeneralIsNotCharacter);
            }

            if (!ValidateGeneralIsNotBSB(Army))
            {
                errors.Add(InvalidReason.GeneralIsBSB);
            }
            
        }

        protected virtual bool ValidateGeneralIsNotBSB(Army a)
        {
            if (a.BsB == null)
                return true;
            return a.General != a.BsB;
        }

        protected bool ValidateGeneralIsCharacter(Army a)
        {
            return a.General.Category == UnitCategory.Hero || a.General.Category == UnitCategory.Lord;
        }

        protected virtual bool ValidateRareDuplicate(Army a, int totalPoints)
        {
            bool b = DuplicateCheck(UnitList[UnitCategory.Rare], config.RareDuplicates, ref duplicatesRare, totalPoints);
            return b;
        }

        protected virtual bool ValidateSpecialDuplicte(Army a, int totalPoints)
        {
            bool b = DuplicateCheck(UnitList[UnitCategory.Special], config.SpecialDuplicates, ref duplicatesSpecial, totalPoints);
            return b;
        }

        protected bool DuplicateCheck(IEnumerable<Unit> Units, int choises, ref List<ITroopData> models, int ArmyPoints)
        {

            Dictionary<ITroopData, List<Unit>> duplicates = new Dictionary<ITroopData, List<Unit>>();
            if (ArmyPoints >= config.GrandArmyLimit)
                choises *= 2;
            foreach (var u in Units)
            {
                if (duplicates.ContainsKey(u.TroopData))
                {
                    duplicates[u.TroopData].Add(u);
                }
                else
                {
                    duplicates.Add(u.TroopData, new List<Unit>() { u });
                }
            }
            bool isValid = true;
            foreach (var d in duplicates)
            {
                if (d.Value.Count > choises)
                {
                    isValid = false;
                    models.Add(d.Key);
                }
            }

            return isValid;
        }

        private void ValidatePoints(Army a, int totalpoints)
        {
            

            if (!ValidateCorePercent( a, totalpoints))
            {
                errors.Add(InvalidReason.CorePoint);
            }

            if (!ValidateSpecialPercent( a, totalpoints))
            {
                errors.Add(InvalidReason.SpecialPoint);
            }

            if (!ValidateRarePercent( a, totalpoints))
            {
                errors.Add(InvalidReason.RarePoint);
            }

            if (!ValidateHeroPercent( a, totalpoints))
            {
                errors.Add(InvalidReason.HeroPoint);
            }

            if (!ValidateLordPercent( a, totalpoints))
            {
                errors.Add(InvalidReason.LordPoint);
            }

            if (!ValidateTotalPoints( a, totalpoints))
            {
                errors.Add(InvalidReason.TotalPoint);
            }
        }

        protected virtual bool ValidateTotalPoints(Army a, int totalpoints)
        {
            int total = (from u in a.Units select u.Points).Sum();
            return total <= totalpoints;
        }

        protected virtual bool ValidateHeroPercent(Army a, int TotalPoints)
        {
            int hero = UnitList[UnitCategory.Hero].Sum(s => s.Points);
            return (double)hero / TotalPoints <= config.HeroPercent;
        }

        protected virtual bool ValidateLordPercent(Army a, int TotalPoints)
        {
            int lord = UnitList[UnitCategory.Lord].Sum(s => s.Points);
            return (double)lord / TotalPoints <= config.LordPercent;
        }

        protected virtual bool ValidateRarePercent(Army a, int TotalPoints)
        {
            int rare = UnitList[UnitCategory.Rare].Sum(s => s.Points);
            return (double)rare / TotalPoints <= config.RarePercent;
        }

        protected virtual bool ValidateSpecialPercent(Army a, int TotalPoints)
        {
            int special = UnitList[UnitCategory.Special].Sum(s => s.Points);
            return (double)special / TotalPoints <= config.SpecialPercent;
        }

        protected virtual bool ValidateCorePercent(Army a, int TotalPoints)
        {
            int core = UnitList[UnitCategory.Core].Sum(s => s.Points);
            return (double)core/TotalPoints >= config.CorePercent;
        }

        protected virtual bool ValidateNumberOfUnits(Army a)
        {
            return a.Units.Count(u => u.Category != UnitCategory.Hero && u.Category != UnitCategory.Lord) >= 3;
        }
    }
}
