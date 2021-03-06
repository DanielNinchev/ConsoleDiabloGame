using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.ViewModels
{
    public interface IBeingViewModel
    {
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
