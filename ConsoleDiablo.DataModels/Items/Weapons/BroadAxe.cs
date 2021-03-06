using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Weapons
{
    public class BroadAxe : Weapon
    {
        public BroadAxe(int id, int characterId) : base(id, type: "BroadAxe", inventoryLoad: 6, sellValue: 500, droppingFactor: 25, characterId)
        {
            Random number = new Random();

            this.Name = "Broad Axe" + this.GenerateRandomName();
            this.DamageBonus = number.Next(12, 17);
        }
    }
}
