using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Items.Armor
{
    public abstract class Armor : Item, IArmorEquipment
    {
        protected Armor(int id, string type, int inventoryLoad, double sellValue, int droppingFactor, int characterId) : base(id, type, inventoryLoad, sellValue, droppingFactor, characterId)
        {

        }

        public double DefenseBonus { get; set; }
        public double FireResistanceBonus { get; set; }
        public double LightningResistanceBonus { get; set; }
        public double ColdResistanceBonus { get; set; }
        public double PoisonResistanceBonus { get; set; }

        public override void AffectCharacter(Character character)
        {
            character.Defense += this.DefenseBonus;
            character.FireResistance += this.FireResistanceBonus;
            character.ColdResistance += this.ColdResistanceBonus;
            character.PoisonResistance += this.PoisonResistanceBonus;
            character.LightningResistance += this.LightningResistanceBonus;
        }

        protected string GenerateRandomName()
        {
            this.RandomNames = new List<string>() { " of security", " of protection", " of defense", " of dexterity", " of hope", " of safety" };

            int index = this.Number.Next(0, (RandomNames.Count - 1));

            string name = this.RandomNames[index];

            return name;
        }
    }
}
