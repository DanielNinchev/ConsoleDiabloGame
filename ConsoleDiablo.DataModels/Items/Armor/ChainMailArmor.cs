using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Armor
{
    public class ChainMailArmor : Armor
    {
        public ChainMailArmor(int id, int characterId) : base(id, type: "ChainMailArmor", inventoryLoad: 6, sellValue: 500, droppingFactor: 10, characterId)
        {
            this.Name = "Chain Mail Armor" + this.GenerateRandomName();
            this.DefenseBonus = Number.Next(20, 30);
            this.FireResistanceBonus = Number.Next(15, 20);
            this.ColdResistanceBonus = Number.Next(3, 5);
            this.PoisonResistanceBonus = Number.Next(10, 15);
            this.LightningResistanceBonus = Number.Next(0, 3);
        }
    }
}
