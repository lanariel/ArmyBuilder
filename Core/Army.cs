using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Core
{
    [Serializable]
    public abstract class Army : INotifyPropertyChanged
    {
        private int pointCost;

        public int PointCost
        {
            get { return pointCost; }
            protected set
            {
                pointCost = value;
                Notify("PointCost");
            }
        }

        List<Unit> unit = new List<Unit>();

        public Unit[] Units { get { return unit.ToArray(); } }

        /// <summary>
        /// Adds the unit to the army list.
        /// </summary>
        /// <param name="u">The unit to add. (Ignores null Units)</param>
        public void AddUnit(Unit u)
        {
            if (u == null)
                return;
            unit.Add(u);
            Notify("Units");
        }



        /// <summary>
        /// Helpermethod to trigger PropertyChangedEventArgs
        /// </summary>
        /// <param name="Property">Name of changed Property</param>
        private void Notify(string Property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Removes the specified unit if found in the army
        /// </summary>
        /// <param name="u">The unit to remove</param>
        public void RemoveUnit(Unit u)
        {
            unit.Remove(u);
        }
    }
}
