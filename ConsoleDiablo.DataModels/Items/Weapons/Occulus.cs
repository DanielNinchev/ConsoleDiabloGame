using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Items
{
    public class Occulus : Weapon
    {
        public Occulus(int id, int characterId) : base(id, type: "Occulus", inventoryLoad: 3, sellValue: 1500, droppingFactor: 55, characterId)
        {
            Random number = new Random();

            this.Name = type + this.GenerateRandomName();
            this.DamageBonus = number.Next(10, 15);
        }
    }
}
