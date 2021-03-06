using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Basic_Items
{
    public class Club : Weapon, IBaseItem
    {
        public Club(int id, int characterId) : base(id, type: "Club", inventoryLoad: 3, sellValue: 0, droppingFactor: 0, characterId)
        {
            this.Name = this.Type;
            this.DamageBonus = 8;
        }
    }
}
