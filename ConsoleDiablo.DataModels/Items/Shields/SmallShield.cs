using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Shields
{
    public class SmallShield : Shield
    {
        public SmallShield(int id, int characterId) : base(id, type: "SmallShield", inventoryLoad: 4, sellValue: 400, droppingFactor: 20, characterId)
        {
            Random number = new Random();

            this.Name = "Small Shield" + this.GenerateRandomName();
            this.Defense = number.Next(10, 15);
        }
    }
}
