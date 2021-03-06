using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Shields
{
    public class KiteShield : Shield
    {
        public KiteShield(int id, int characterId) : base(id, type: "KiteShield", inventoryLoad: 6, sellValue: 600, droppingFactor: 20, characterId)
        {
            Random number = new Random();

            this.Name = "Kite Shield" + this.GenerateRandomName();
            this.Defense = number.Next(20, 25);
        }
    }
}
