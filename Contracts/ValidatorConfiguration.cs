using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class ValidatorConfiguration
    {
        private int points = 0;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        private double lordPercent = 0.25d;

        public double LordPercent
        {
            get { return lordPercent; }
            set { lordPercent = value; }
        }

        private double heroPercent = 0.25d;

        public double HeroPercent
        {
            get { return heroPercent; }
            set { heroPercent = value; }
        }

        private double rarePercent = 0.25d;

        public double RarePercent
        {
            get { return rarePercent; }
            set { rarePercent = value; }
        }

        private double specialPercent = 0.5d;

        public double SpecialPercent
        {
            get { return specialPercent; }
            set { specialPercent = value; }
        }

        private double corePercent = 0.25d;

        public double CorePercent
        {
            get { return corePercent; }
            set { corePercent = value; }
        }

        private int specialDuplicates = 3;

        public int SpecialDuplicates
        {
            get { return specialDuplicates; }
            set { specialDuplicates = value; }
        }

        private int rareDuplicates = 2;

        public int RareDuplicates
        {
            get { return rareDuplicates; }
            set { rareDuplicates = value; }
        }

        private int grandArmyLimit = 3000;

        public int GrandArmyLimit
        {
            get { return grandArmyLimit; }
            set { grandArmyLimit = value; }
        }


    }
}
