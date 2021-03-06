using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Core.ViewModels
{
    public class CharacterViewModel : BeingViewModel, ICharacterViewModel
    {
        public CharacterViewModel(
            string name,
            string type,
            DateTime dateCreated,
            int level,
            double damage,
            double defense,
            double baseLife,
            double life,
            double baseMana,
            double mana,
            double fireResistance,
            double lightningResistance,
            double coldResistance,
            double poisonResistance, 
            double moneyBalance) : base(name, damage, defense, baseLife, life, fireResistance, lightningResistance, coldResistance, poisonResistance)
        {
            this.Type = type;
            this.DateCreated = dateCreated;
            this.Level = level;
            this.BaseMana = baseMana;
            this.Mana = mana;
            this.MoneyBalance = moneyBalance;
        }

        public string Type { get; }

        public int Level { get; }

        public double BaseMana { get; }

        public double Mana { get; set; }

        public double MoneyBalance { get; set; }

        public DateTime DateCreated { get; }
    }
}
