using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Core.ViewModels
{
    public class ItemViewModel : IItemViewModel
    {
        public ItemViewModel(string name, int inventoryLoad, double sellValue)
        {
            this.Name = name;
            this.InventoryLoad = inventoryLoad;
            this.SellValue = sellValue;
            this.BonusValues = new List<double>();
        }

        public string Name { get; set; }
        public int InventoryLoad { get; set; }
        public double SellValue { get; set; }
        public List<double> BonusValues { get; set; }
    }
}
