using ConsoleDiablo.App.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.ViewModels
{
    public interface ICharacterViewModel : IBeingViewModel
    {
        string Type { get; }

        DateTime DateCreated { get; }

        int Level { get; }

        public double BaseMana { get; }

        public double Mana { get; set; }

        double MoneyBalance { get; }
    }
}
