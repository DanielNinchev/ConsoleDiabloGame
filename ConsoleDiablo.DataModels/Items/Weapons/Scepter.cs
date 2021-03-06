using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ConsoleDiablo.App.Entities.Weapons
{
    public class Scepter : Weapon
    {
        public Scepter(int id, int characterId) : base(id, type: "Scepter", inventoryLoad: 3, sellValue: 1000, droppingFactor: 45, characterId)
        {
            Random number = new Random();

            this.Name = type + this.GenerateRandomName();
            this.DamageBonus = number.Next(10, 15);
        }
    }
}
