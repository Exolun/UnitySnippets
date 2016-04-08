using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selection
{
    public class CurrentSelection
    {

        #region Singleton Implementation
        private static CurrentSelection instance;
        public static CurrentSelection GetInstance()
        {
            return instance;
        }

        static CurrentSelection()
        {
            instance = new CurrentSelection();
        }
        #endregion


        private List<SelectableUnit> selectedUnits = new List<SelectableUnit>();
        public List<SelectableUnit> SelectedUnits { get { return this.selectedUnits; } }

        public void Add(SelectableUnit unit)
        {
            this.selectedUnits.Add(unit);
        }

        public void Remove(SelectableUnit unit)
        {
            this.selectedUnits.Remove(unit);
        }
    }
}
