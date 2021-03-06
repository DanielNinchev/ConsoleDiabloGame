using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Basic_Items
{
    public class ShortSword : Weapon, IBaseItem
    {
        public ShortSword(int id, int characterId) : base(id, type: "ShortSword", inventoryLoad: 3, sellValue: 0, droppingFactor: 0, characterId)
        {
            this.Name = "Short Sword";
            this.DamageBonus = 10;
        }
    }
}
