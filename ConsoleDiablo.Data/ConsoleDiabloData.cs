using ConsoleDiablo.App.Entities.Gears;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels;
using ConsoleDiablo.DataModels.Characters;
using System.Collections.Generic;

namespace ConsoleDiablo.Data
{
    public class ConsoleDiabloData
    {
        public ConsoleDiabloData()
        {
            this.Accounts = DataMapper.LoadAccounts();
            this.Characters = DataMapper.LoadCharacters();
            this.Items = DataMapper.LoadItems();
            this.Gears = DataMapper.LoadGears();
            this.Inventories = DataMapper.LoadInventories();
            this.Monsters = DataMapper.LoadMonsters();
        }

        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Gear> Gears { get; set; } = new List<Gear>();
        public List<Inventory> Inventories { get; set; } = new List<Inventory>();
        public List<Monster> Monsters { get; set; } = new List<Monster>();

        public void SaveChanges()
        {
            DataMapper.SaveAccounts(this.Accounts);
            DataMapper.SaveCharacters(this.Characters);
            DataMapper.SaveItems(this.Items);
            DataMapper.SaveGears(this.Gears);
            DataMapper.SaveInventories(this.Inventories);
        }
    }
}
