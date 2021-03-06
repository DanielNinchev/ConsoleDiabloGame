using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Shields
{
    public abstract class Shield : Item, IShield
    {
        protected Shield(int id, string type, int inventoryLoad, double sellValue, int droppingFactor, int characterId) : base(id, type, inventoryLoad, sellValue, droppingFactor, characterId)
        {
            this.IsTwoHanded = this.IsTheItemTwoHanded();
        }

        public bool IsTwoHanded { get; set; }

        public double Defense { get; set; }

        public override void AffectCharacter(Character character)
        {
            character.Defense += this.Defense;
        }

        public bool IsTheItemTwoHanded()
        {
            if (this.InventoryLoad >= 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string GenerateRandomName()
        {
            this.RandomNames = new List<string>() { " of security", " of protection", " of defense", " of dexterity", " of hope", " of safety"};

            int index = this.Number.Next(0, (RandomNames.Count - 1));

            string name = this.RandomNames[index];

            return name;
        }
    }
}
