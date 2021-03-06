using ConsoleDiablo.App.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.ViewModels
{
    public interface IItemViewModel
    {
        string Name { get; set; }

        int InventoryLoad { get; set; }

        double SellValue { get; set; }

        List<double> BonusValues { get; set; }
    }
}
