using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Basic_Items
{
    public class WristBlade : Weapon, IBaseItem
    {
        public WristBlade(int id, int characterId) : base(id, type: "WristBlade", inventoryLoad: 3, sellValue: 0, droppingFactor: 0, characterId)
        {
            this.Name = "Wrist Blade";
            this.DamageBonus = 8;
        }
    }
}
