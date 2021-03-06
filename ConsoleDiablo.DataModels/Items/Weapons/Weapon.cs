using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Weapons
{
    public abstract class Weapon : Item, IWeapon
    {
        protected Weapon(int id, string type, int inventoryLoad, double sellValue, int droppingFactor, int characterId) : base(id, type, inventoryLoad, sellValue, droppingFactor, characterId)
        {
            this.IsTwoHanded = this.IsTheItemTwoHanded();
        }

        public double DamageBonus { get; set; }

        public bool IsTwoHanded { get; set; }

        public override void AffectCharacter(Character character)
        {
            character.Damage += this.DamageBonus;
        }

        public bool IsTheItemTwoHanded()
        {
            if (this.InventoryLoad >= 4)
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
            this.RandomNames = new List<string>() { " of doom", " of power", " of strength", " of grudge", " of rage", " of brutality", " of vengeance", " of aggression", " of fury", " of anger" };

            int index = this.Number.Next(0, (RandomNames.Count - 1));

            string name = this.RandomNames[index];

            return name;
        }
    }
}
