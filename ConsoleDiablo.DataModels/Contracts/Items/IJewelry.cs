using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IJewelry : IItem
    {
        double VitalityBonus { get; }
        double EnergyBonus { get; }
    }
}
