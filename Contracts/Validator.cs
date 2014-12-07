﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public enum InvalidReason
    {
        NullUnit,
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
        Other,
    }

    public class Validator
    {
        ValidatorConfiguration config = new ValidatorConfiguration();

        public ValidatorConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        List<string> duplicatesSpecial = new List<string>();
        List<string> duplicatesRare = new List<string>();

        public IEnumerable<string> DuplicateSpecial { get { return duplicatesSpecial; } }

        public IEnumerable<string> DuplicateRare { get { return duplicatesRare; } }

        public ISet<InvalidReason> Validate(IEnumerable<Unit> Units)
        {
            ISet<InvalidReason> errors = new HashSet<InvalidReason>();

            if (Units == null || Units.Contains(null))
            {
                errors.Add(InvalidReason.NullUnit);
                return errors;
            }

            if (!ValidateNumberOfUnits(Units))
            {
                errors.Add(InvalidReason.FewUnits);
            }

            if (!ValidateCharacterExists(Units))
            {
                errors.Add(InvalidReason.NoCharacter);
            }

            ValidatePoints(Units, errors);

            if (!ValidateSpecialDuplicte(Units))
            {
                errors.Add(InvalidReason.DuplicateSpecial);
            }

            if (!ValidateRareDuplicate(Units))
            {
                errors.Add(InvalidReason.DuplicateRare);
            }

            return errors;
        }

        protected virtual bool ValidateRareDuplicate(IEnumerable<Unit> Units)
        {
            var rare = Units.Where(u => u.Category == UnitCategory.Rare);
            duplicatesRare.Clear();
            bool b = DuplicateCheck(rare, config.RareDuplicates, ref duplicatesRare);
            return b;
        }

        protected virtual bool ValidateSpecialDuplicte(IEnumerable<Unit> Units)
        {
            var special = Units.Where(u => u.Category == UnitCategory.Special);
            duplicatesSpecial.Clear();
            bool b = DuplicateCheck(special, config.SpecialDuplicates, ref duplicatesSpecial);
            return b;
        }

        protected bool DuplicateCheck(IEnumerable<Unit> Units, int choises, ref List<string> models)
        {
            
            Dictionary<string, List<Unit>> duplicates = new Dictionary<string, List<Unit>>();
            if (config.Points >= config.GrandArmyLimit)
                choises *= 2;
            foreach (var u in Units)
            {
                if (duplicates.ContainsKey(u.UnitName))
                {
                    duplicates[u.UnitName].Add(u);
                }
                else
                {
                    duplicates.Add(u.UnitName, new List<Unit>() { u });
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

        private void ValidatePoints(IEnumerable<Unit> Units, ISet<InvalidReason> errors)
        {
            int totalpoints;
            if (config.Points == 0)
            {
                totalpoints = (from u in Units select u.Points).Sum();
            }
            else
            {
                totalpoints = config.Points;
            }

            if (!ValidateCorePercent(Units, totalpoints))
            {
                errors.Add(InvalidReason.CorePoint);
            }

            if (!ValidateSpecialPercent(Units, totalpoints))
            {
                errors.Add(InvalidReason.SpecialPoint);
            }

            if (!ValidateRarePercent(Units, totalpoints))
            {
                errors.Add(InvalidReason.RarePoint);
            }

            if (!ValidateHeroPercent(Units, totalpoints))
            {
                errors.Add(InvalidReason.HeroPoint);
            }

            if (!ValidateLordPercent(Units, totalpoints))
            {
                errors.Add(InvalidReason.LordPoint);
            }

            if (!ValidateTotalPoints(Units, totalpoints))
            {
                errors.Add(InvalidReason.TotalPoint);
            }
        }

        protected virtual bool ValidateTotalPoints(IEnumerable<Unit> Units, int totalpoints)
        {
            int total = (from u in Units select u.Points).Sum();
            return total <= totalpoints;
        }

        protected virtual bool ValidateHeroPercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int hero = (from u in Units where u.Category == UnitCategory.Hero select u.Points).Sum();
            return (double)hero / TotalPoints <= config.HeroPercent;
        }

        protected virtual bool ValidateLordPercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int lord = (from u in Units where u.Category == UnitCategory.Lord select u.Points).Sum();
            return (double)lord / TotalPoints <= config.LordPercent;
        }

        protected virtual bool ValidateRarePercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int rare = (from u in Units where u.Category == UnitCategory.Rare select u.Points).Sum();
            return (double)rare / TotalPoints <= config.RarePercent;
        }

        protected virtual bool ValidateSpecialPercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int special = (from u in Units where u.Category == UnitCategory.Special select u.Points).Sum();
            return (double)special / TotalPoints <= config.SpecialPercent;
        }

        protected virtual bool ValidateCorePercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int core = (from u in Units where u.Category == UnitCategory.Core select u.Points).Sum();
            return (double)core/TotalPoints >= config.CorePercent;
        }

        protected virtual bool ValidateCharacterExists(IEnumerable<Unit> Units)
        {
            return Units.Any(u => u.Category == UnitCategory.Hero || u.Category == UnitCategory.Lord);
        }

        protected virtual bool ValidateNumberOfUnits(IEnumerable<Unit> Units)
        {
            return Units.Count(u => u.Category != UnitCategory.Hero && u.Category != UnitCategory.Lord) >= 3;
        }
    }
}
