using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Contracts
{
    public interface IInventory
    {
        int Id { get; set; }

        int CharacterId { get; set; }

        int Load { get; set; }

        ICollection<int> ItemIds { get; set; }

        int Capacity { get; }
    }
}
