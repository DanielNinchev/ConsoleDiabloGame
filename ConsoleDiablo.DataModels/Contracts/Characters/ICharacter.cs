using ConsoleDiablo.App.Entities.Gears;
using ConsoleDiablo.DataModels.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Contracts.Characters
{
    public interface ICharacter : IEntity
    {
        string Name { get; set; }
        string Type { get; set; }
        double Damage { get; set; }
        double Defense { get; set; }
        double Life { get; set; }
        double BaseLife { get; set; }
        double LifeRegenerationMultiplier { get; set; }
        double Mana { get; set; }
        double BaseMana { get; set; }
        double ManaRegenerationMultiplier { get; set; }
        double Experience { get; set; }
        int Level { get; set; }
        double FireResistance { get; set; }
        double LightningResistance { get; set; }
        double ColdResistance { get; set; }
        double PoisonResistance { get; set; }
        double MoneyBalance { get; set; }
        int GearId { get; set; }
        int InventoryId { get; set; }
        int AccountId { get; set; }
        public bool IsAlive { get; set; }
        string DamageType { get; set; }
    }
}
