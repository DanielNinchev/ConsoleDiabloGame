using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IItem
    {
        int Id { get; set; }
        double SellValue { get; set; }
        int InventoryLoad { get; set; }
        int DroppingFactor { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        int CharacterId { get; set; }

        void AffectCharacter(Character character);
    }
}
