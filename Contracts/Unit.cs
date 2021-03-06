﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public enum UnitCategory 
    {
        Lord,
        Hero,
        Core,
        Special,
        Rare,
    }

    public class Unit : ITroopData
    {
        private int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public virtual UnitCategory Category
        {
            get { return TroopData.Category; }
            set { TroopData.Category = value; }
        }

        private string companyName;

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }


        public ITroopData TroopData { get; set; }
        public virtual bool IsUnique
        {
            get
            {
                return TroopData.IsUnique;
            }
            set
            {
                TroopData.IsUnique = value;
            }
        }

        public virtual string Name
        {
            get
            {
                return TroopData.Name;
            }
            set
            {
                TroopData.Name = value;
            }
        }

        public virtual int PointsPerModel
        {
            get
            {
                return TroopData.PointsPerModel;
            }
            set
            {
                TroopData.PointsPerModel = value;
            }
        }

        public virtual int MinSize
        {
            get
            {
                return TroopData.MinSize;
            }
            set
            {
                TroopData.MinSize = value;
            }
        }

        public virtual int MaxSize
        {
            get
            {
                return TroopData.MaxSize;
            }
            set
            {
                TroopData.MaxSize = value;
            }
        }

        public virtual TroopType TroopType
        {
            get
            {
                return TroopData.TroopType;
            }
            set
            {
                TroopData.TroopType = value;
            }
        }
    }
}
