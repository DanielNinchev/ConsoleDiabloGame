using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IWeapon : IHandItem
    {
        double DamageBonus { get; }
    }
}
