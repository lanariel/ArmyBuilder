using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
        Other,
    }

    public class Validator
    {
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

            int totalpoints = (from u in Units select u.Points).Sum();

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

            return errors;
        }

        protected virtual bool ValidateHeroPercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int hero = (from u in Units where u.Category == UnitCategory.Hero select u.Points).Sum();
            return (double)hero / TotalPoints <= 0.25d;
        }

        protected virtual bool ValidateLordPercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int lord = (from u in Units where u.Category == UnitCategory.Lord select u.Points).Sum();
            return (double)lord / TotalPoints <= 0.25d;
        }

        protected virtual bool ValidateRarePercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int rare = (from u in Units where u.Category == UnitCategory.Rare select u.Points).Sum();
            return (double)rare / TotalPoints <= 0.25d;
        }

        protected virtual bool ValidateSpecialPercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int special = (from u in Units where u.Category == UnitCategory.Special select u.Points).Sum();
            return (double)special / TotalPoints <= 0.5d;
        }

        protected virtual bool ValidateCorePercent(IEnumerable<Unit> Units, int TotalPoints)
        {
            int core = (from u in Units where u.Category == UnitCategory.Core select u.Points).Sum();
            return (double)core/TotalPoints >= 0.25d;
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
