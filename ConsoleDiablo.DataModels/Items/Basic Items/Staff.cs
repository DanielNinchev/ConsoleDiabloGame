using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Basic_Items
{
    public class Staff : Weapon, IBaseItem
    {
        public Staff(int id, int characterId) : base(id, type: "Staff", inventoryLoad: 6, sellValue: 0, droppingFactor: 0, characterId)
        {
            this.Name = this.Type;
            this.DamageBonus = 3;
        }
    }
}
