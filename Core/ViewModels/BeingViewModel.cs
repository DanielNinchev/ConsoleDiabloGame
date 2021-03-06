using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Core.ViewModels
{
    public abstract class BeingViewModel : IBeingViewModel
    {
        public BeingViewModel(string name,
            double damage,
            double defense,
            double baseLife,
            double life,
            double fireRes,
            double lightRes, 
            double coldRes, 
            double poisonRes)
        {
            this.Name = name;
            this.Damage = damage;
            this.Defense = defense;
            this.BaseLife = baseLife;
            this.Life = life;
            this.FireResistance = fireRes;
            this.LightningResistance = lightRes;
            this.ColdResistance = coldRes;
            this.PoisonResistance = poisonRes;
        }

        public string Name { get; }

        public double Damage { get; }

        public double Defense { get; }

        public double BaseLife { get; }

        public double Life { get; set; }

        public double FireResistance { get; }

        public double LightningResistance { get; }

        public double ColdResistance { get; }

        public double PoisonResistance { get; }

        public string DamageType { get; set; }
    }
}
