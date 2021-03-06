using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IArmorEquipment : IItem
    {
        double DefenseBonus { get; }
        double FireResistanceBonus { get; }
        double LightningResistanceBonus { get; }
        double ColdResistanceBonus { get; }
        double PoisonResistanceBonus { get; }
    }
}
