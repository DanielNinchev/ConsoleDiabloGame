using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Weapons
{
    public class Flail : Weapon
    {
        public Flail(int id, int characterId) : base(id, type: "Flail", inventoryLoad: 6, sellValue: 500, droppingFactor: 25, characterId)
        {
            this.Name = type + this.GenerateRandomName();
            this.DamageBonus = Number.Next(10, 15);
        }
    }
}
