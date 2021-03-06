using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Armor
{
    public class LeatherArmor : Armor
    {
        public LeatherArmor(int id, int characterId) : base(id, type: "LeatherArmor", inventoryLoad: 6, sellValue: 500, droppingFactor: 10, characterId)
        {
            this.Name = "Leather Armor" + this.GenerateRandomName();
            this.DefenseBonus = Number.Next(5, 10);
            this.FireResistanceBonus = Number.Next(5, 8);
            this.ColdResistanceBonus = Number.Next(10, 15);
            this.PoisonResistanceBonus = Number.Next(5, 8);
            this.LightningResistanceBonus = Number.Next(3, 6);
        }
    }
}
