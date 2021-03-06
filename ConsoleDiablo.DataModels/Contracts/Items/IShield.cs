using ConsoleDiablo.DataModels.Contracts.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IShield : IHandItem
    {
        double Defense { get; }
    }
}
