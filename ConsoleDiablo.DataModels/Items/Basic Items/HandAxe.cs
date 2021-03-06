using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Basic_Items
{
    public class HandAxe : Weapon, IBaseItem
    {
        public HandAxe(int id, int characterId) : base(id, type: "HandAxe", inventoryLoad : 3, sellValue : 0, droppingFactor : 0, characterId)
        {
            this.Name = "Hand Axe";
            this.DamageBonus = 10;
        }
    }
}
