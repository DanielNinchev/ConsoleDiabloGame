using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.DataModels.Characters;
using System;
using System.Collections.Generic;

namespace ConsoleDiablo.App.Entities.Weapons
{
    public abstract class Item : IItem
    {
        private int id;
        protected string name;
        private int inventoryLoad;
        private double sellValue;
        private int droppingFactor;
        protected string type;
        private int characterId;

        private Random number = new Random();
        private List<string> randomNames;

        protected Item(int id, string type, int inventoryLoad, double sellValue, int droppingFactor, int characterId)
        {
            this.Id = id;
            this.Type = type;
            this.InventoryLoad = inventoryLoad;
            this.SellValue = sellValue;
            this.DroppingFactor = droppingFactor;
            this.CharacterId = characterId;
        }

        public int CharacterId
        {
            get { return characterId; }
            set { characterId = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Random Number
        {
            get { return number; }
            set { number = value; }
        }

        public List<string> RandomNames { get; set; }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int InventoryLoad
        {
            get { return inventoryLoad; }
            set { inventoryLoad = value; }
        }

        public double SellValue
        {
            get { return sellValue; }
            set { sellValue = value; }
        }

        public int DroppingFactor
        {
            get { return droppingFactor; }
            set { droppingFactor = value; }
        }

        public abstract void AffectCharacter(Character character);
    }
}
