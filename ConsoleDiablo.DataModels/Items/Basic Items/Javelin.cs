using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Basic_Items
{
    public class Javelin : Weapon, IBaseItem
    {
        public Javelin(int id, int characterId) : base(id, type: "Javelin", inventoryLoad: 3, sellValue: 0, droppingFactor: 0, characterId)
        {
            this.Name = this.Type;
            this.DamageBonus = 10;
        }
    }
}
