using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts
{
    public interface IMonster
    {
        int Id { get; set; }
        public string Name { get; set; }
        double Damage { get; set; }
        double Life { get; set; }
        double BaseLife { get; set; }
        string DamageType { get; set; }
        double Defense { get; set; }
        double FireResistance { get; set; }
        double ColdResistance { get; set; }
        double LightningResistance { get; set; }
        double PoisonResistance { get; set; }
        bool IsAlive { get; set; }
    }
}
