using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Armor
{
    public class PlateMailArmor : Armor
    {
        public PlateMailArmor(int id, int characterId) : base(id, type: "PlateMailArmor", inventoryLoad: 6, sellValue: 100, droppingFactor: 10, characterId)
        {
            this.Name = "Plate Mail Armor" + this.GenerateRandomName();
            this.DefenseBonus = Number.Next(30, 35);
            this.FireResistanceBonus = Number.Next(20, 25);
            this.ColdResistanceBonus = Number.Next(10, 15);
            this.PoisonResistanceBonus = Number.Next(20, 25);
            this.LightningResistanceBonus = Number.Next(0, 3);
        }
    }
}
