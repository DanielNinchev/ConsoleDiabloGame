using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Basic_Items
{
    public class Wand : Weapon, IBaseItem
    {
        public Wand(int id, int characterId) : base(id, type: "Wand", inventoryLoad: 2, sellValue: 0, droppingFactor: 0, characterId)
        {
            this.Name = this.Type;
            this.DamageBonus = 1;
        }
    }
}
