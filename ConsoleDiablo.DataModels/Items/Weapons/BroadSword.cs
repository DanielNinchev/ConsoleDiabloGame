using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Weapons
{
    public class BroadSword : Weapon
    {
        public BroadSword(int id, int characterId) : base(id, type: "BroadSword", inventoryLoad: 4, sellValue: 500, droppingFactor: 55, characterId)
        {
            Random number = new Random();

            this.Name = "Broad Sword" + this.GenerateRandomName();
            this.DamageBonus = number.Next(10, 15);
        }
    }
}
