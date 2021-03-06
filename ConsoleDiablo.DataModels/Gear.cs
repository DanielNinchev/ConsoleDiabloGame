using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using ConsoleDiablo.DataModels.Contracts.Items;
using ConsoleDiablo.DataModels.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleDiablo.App.Entities.Gears
{
    public class Gear : IGear
    {
        private int id;
        private int characterId;
        private string type;
        private int leftHandItemId;
        private int rightHandItemId;
        private int armorId;
        private List<int> itemIds;

        public Gear(int id, int characterId)
        {
            this.Id = id;
            this.CharacterId = characterId;
            this.RightHandItemId = 0;
            this.LeftHandItemId = 0;
            this.ArmorId = 0;
            this.itemIds = new List<int>() { this.RightHandItemId, this.LeftHandItemId, this.ArmorId };
        }

        public Gear(int id, int characterId, int rightHandItemId, int leftHandItemId, int armorId)
        {
            this.Id = id;
            this.CharacterId = characterId;
            this.RightHandItemId = rightHandItemId;
            this.LeftHandItemId = leftHandItemId;
            this.ArmorId = armorId;
            this.ItemIds = new List<int>() { this.RightHandItemId, this.LeftHandItemId, this.ArmorId };
        }

        public List<int> ItemIds
        {
            get { return itemIds; }
            set { itemIds = value; }
        }

        public int ArmorId
        {
            get { return armorId; }
            set { armorId = value; }
        }

        public int RightHandItemId
        {
            get { return rightHandItemId; }
            set { rightHandItemId = value; }
        }

        public int LeftHandItemId
        {
            get { return leftHandItemId; }
            set { leftHandItemId = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public int CharacterId
        {
            get { return characterId; }
            set { characterId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        //public void PutItemOn(string type, string name, Character character)
        //{
        //    var itemType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(it => it.Name == type);

        //    if (itemType == null)
        //    {
        //        throw new ArgumentNullException("The item cannot be null!");
        //    }

        //    if (!typeof(IItem).IsAssignableFrom(itemType))
        //    {
        //        throw new ArgumentException($"{itemType} is not an item!");
        //    }

        //    //if (this.gear.Contains(itemType))
        //    //{
        //    //    throw new InvalidOperationException("This character cannot carry anymore!");
        //    //}

        //    //this.gear.Add(item);
        //}

        //private void EnsureItemExists(string name)
        //{
        //    if (!this.gear.Any())
        //    {
        //        throw new InvalidOperationException("This character has not picked any items yet.");
        //    }

        //    var itemExists = this.gear.Any(i => i.GetType().Name == name);

        //    if (!itemExists)
        //    {
        //        throw new ArgumentException($"There is no {name} in this gear!");
        //    }
        //}

        //public Item ChooseItemFromGear(string name)
        //{
        //    EnsureItemExists(name);

        //    var chosenItem = this.gear.First(i => i.GetType().Name == name);

        //    return chosenItem;
        //}
    }
}
