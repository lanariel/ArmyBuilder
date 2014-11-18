using Core.MagicItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Core
{
    [Serializable]
    public class Unit : INotifyPropertyChanged
    {
        class ConstStrings
        {
            internal const string Name = "Name";
            internal const string MagicItem = "MagicItem";
        }
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify(ConstStrings.Name);
            }
        }


        /// <summary>
        /// Helpermethod to trigger PropertyChangedEventArgs
        /// </summary>
        /// <param name="Property">Name of changed Property</param>
        protected void Notify(string Property)
        {

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Adds the item to the Unit
        /// </summary>
        /// <param name="mi">The item to add to the Unit. (Ignores null)</param>
        public void AddMagicItem(MagicItem mi)
        {
            if (mi != null)
                magicItem = mi;
        }


        public MagicItem magicItem;
        public MagicItem MagicItem
        {
            get { return magicItem; }
            private set
            {
                magicItem = value;
                   Notify(ConstStrings.MagicItem);
            }
        }

        public void RemoveItem()
        {
            MagicItem = null;
        }
    }
}
