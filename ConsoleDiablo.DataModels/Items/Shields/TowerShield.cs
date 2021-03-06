using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Shields
{
    public class TowerShield : Shield
    {
        public TowerShield(int id, int characterId) : base(id, type: "KiteShield", inventoryLoad: 6, sellValue: 800, droppingFactor: 10, characterId)
        {
            Random number = new Random();

            this.Name = "Tower Shield" + this.GenerateRandomName();
            this.Defense = number.Next(30, 35);
        }
    }
}
