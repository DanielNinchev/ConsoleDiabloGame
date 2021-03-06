using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.DataModels.Contracts.Items;
using System.Collections.Generic;

namespace ConsoleDiablo.DataModels.Contracts
{
    public interface IGear
    {
        int Id { get; set; }

        int ArmorId { get; set; }

        int RightHandItemId { get; set; }

        int LeftHandItemId { get; set; }

        int CharacterId { get; set; }

        string Type { get; set; }

        List<int> ItemIds { get; set; }
    }
}
