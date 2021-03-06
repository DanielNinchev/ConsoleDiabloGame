using ConsoleDiablo.App.Entities.Gears;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.DataModels;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleDiablo.Data
{
    internal static class DataMapper
    {
        private const string DATA_PATH = "../../../../data/";
        private const string CONFIG_PATH = "config.ini";

        private const string DEFAULT_CONFIG = "accounts=accounts.csv\r\n" +
            "characters=characters.csv\r\n" +
            "items=items.csv\r\n" +
            "gears=gears.csv\r\n" +
            "inventories=inventories.csv\r\n" +
            "monsters=monsters.csv";

        private static readonly Dictionary<string, string> config;
        static DataMapper()
        {
            Directory.CreateDirectory(DATA_PATH);
            config = LoadConfig(DATA_PATH + CONFIG_PATH);
        }

        private static Dictionary<string, string> LoadConfig(string configPath)
        {
            EnsureConfigFile(configPath);

            var contents = ReadLines(configPath);

            var config = contents.Select(l => l.Split('=')).ToDictionary(t => t[0], t => DATA_PATH + t[1]);

            return config;
        }

        public static List<Account> LoadAccounts()
        {
            List<Account> accounts = new List<Account>();
            var dataLines = ReadLines(config["accounts"]);

            foreach (var line in dataLines)
            {
                string[] args = line.Split(';');
                int id = int.Parse(args[0]);
                string accountName = args[1];
                string password = args[2];
                var characterIds = args[3]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                Account account = new Account(id, accountName, password);
                accounts.Add(account);
            }

            return accounts;
        }

        public static void SaveAccounts(List<Account> accounts)
        {
            List<string> lines = new List<string>();

            foreach (var account in accounts)
            {
                const string accountFormat = "{0};{1};{2};{3}";
                string line = string.Format(
                    accountFormat,
                    account.Id,
                    account.AccountName,
                    account.Password,
                    string.Join(",", account.Characters)
                    );

                lines.Add(line);
            }

            WriteLines(config["accounts"], lines.ToArray());
        }

        public static List<Character> LoadCharacters()
        {
            List<Character> characters = new List<Character>();
            var dataLines = ReadLines(config["characters"]);

            foreach (var line in dataLines)
            {
                var args = line.ToString().Split(";", StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(args[0]);
                var name = args[1];
                var type = args[2];
                var isDeleted = bool.Parse(args[3]);
                var dateCreated = DateTime.Parse(args[4]);
                var damage = double.Parse(args[5]);
                var defense = double.Parse(args[6]);
                var baseLife = double.Parse(args[7]);
                var life = double.Parse(args[8]);
                var lifeRegenerationMultiplier = double.Parse(args[9]);
                var baseMana = double.Parse(args[10]);
                var mana = double.Parse(args[11]);
                var manaRegenerationMultiplier = double.Parse(args[12]);
                var experience = double.Parse(args[13]);
                var level = int.Parse(args[14]);
                var fireResistance = double.Parse(args[15]);
                var lightningResistance = double.Parse(args[16]);
                var coldResistance = double.Parse(args[17]);
                var poisonResistance = double.Parse(args[18]);
                var gearId = int.Parse(args[19]);
                var inventoryId = int.Parse(args[20]);
                var moneyBalance = double.Parse(args[21]);
                var accountId = int.Parse(args[22]);

                var characterType = Assembly.Load("ConsoleDiablo.DataModels").GetTypes().FirstOrDefault(t => t.Name == type);

                if (characterType == null)
                {
                    throw new InvalidOperationException("Invalid character type \"{type}\"!");
                }

                var character = (Character)Activator.CreateInstance(characterType,
                    id,
                    name,
                    type,
                    isDeleted,
                    dateCreated,
                    damage,
                    defense,
                    baseLife,
                    life,
                    lifeRegenerationMultiplier,
                    baseMana,
                    mana,
                    manaRegenerationMultiplier,
                    experience,
                    level,
                    fireResistance,
                    lightningResistance,
                    coldResistance,
                    poisonResistance,
                    gearId,
                    inventoryId,
                    moneyBalance,
                    accountId);

                characters.Add(character);
            }

            return characters;
        }

        public static void SaveCharacters(List<Character> characters)
        {
            List<string> lines = new List<string>();
            foreach (var character in characters)
            {
                const string characterFormat = "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22}";
                string line = string.Format(characterFormat,
                    character.Id,
                    character.Name,
                    character.Type,
                    character.IsDeleted,
                    character.DateCreated,
                    character.Damage,
                    character.Defense,
                    character.BaseLife,
                    character.Life,
                    character.LifeRegenerationMultiplier,
                    character.BaseMana,
                    character.Mana,
                    character.ManaRegenerationMultiplier,
                    character.Experience,
                    character.Level,
                    character.FireResistance,
                    character.LightningResistance,
                    character.ColdResistance,
                    character.PoisonResistance,
                    character.GearId,
                    character.InventoryId,
                    character.MoneyBalance,
                    character.AccountId
                    );

                lines.Add(line);
            }

            WriteLines(config["characters"], lines.ToArray());
        }

        public static List<Item> LoadItems()
        {
            List<Item> items = new List<Item>();
            var dataLines = ReadLines(config["items"]);

            foreach (var line in dataLines)
            {
                string[] args = line.Split(';');
                int id = int.Parse(args[0]);
                string type = args[1];
                int inventoryLoad = int.Parse(args[2]);
                double sellValue = double.Parse(args[3]);
                int droppingFactor = int.Parse(args[4]);
                int characterId = int.Parse(args[5]);

                var itemType = Assembly.Load("ConsoleDiablo.DataModels").GetTypes().FirstOrDefault(t => t.Name == type);

                if (itemType == null)
                {
                    throw new InvalidOperationException($"Invalid item type \"{itemType}\"!");
                }

                var item = (Item)Activator.CreateInstance(itemType, id, characterId);

                items.Add(item);
            }

            return items;
        }

        public static void SaveItems(List<Item> items)
        {
            List<string> lines = new List<string>();

            foreach (var item in items)
            {
                const string itemFormat = "{0};{1};{2};{3};{4};{5}";
                string line = string.Format(
                    itemFormat,
                    item.Id,
                    item.Type,
                    item.InventoryLoad,
                    item.SellValue,
                    item.DroppingFactor,
                    item.CharacterId);

                lines.Add(line);
            }

            WriteLines(config["items"], lines.ToArray());
        }

        public static List<Gear> LoadGears()
        {
            List<Gear> gears = new List<Gear>();
            var dataLines = ReadLines(config["gears"]);

            foreach (var line in dataLines)
            {
                string[] args = line.Split(';');
                int id = int.Parse(args[0]);
                int characterId = int.Parse(args[1]);
                int rightHandItemId = int.Parse(args[2]);
                int leftHandItemId = int.Parse(args[3]);
                int armorId = int.Parse(args[4]);
                string type = args[5];

                var gear = new Gear(id, characterId, rightHandItemId, leftHandItemId, armorId);

                gears.Add(gear);
            }

            return gears;
        }
        public static void SaveGears(List<Gear> gears)
        {
            List<string> lines = new List<string>();

            foreach (var gear in gears)
            {
                const string gearFormat = "{0};{1};{2};{3};{4};{5}";
                string line = string.Format(
                    gearFormat,
                    gear.Id,
                    gear.CharacterId,
                    gear.RightHandItemId,
                    gear.LeftHandItemId,
                    gear.ArmorId,
                    gear.Type);

                lines.Add(line);
            }

            WriteLines(config["gears"], lines.ToArray());
        }

        internal static List<Inventory> LoadInventories()
        {
            List<Inventory> inventories = new List<Inventory>();
            var dataLines = ReadLines(config["inventories"]);

            foreach (var line in dataLines)
            {
                string[] args = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(args[0]);
                int characterId = int.Parse(args[1]);
                int load = int.Parse(args[2]);         
                var itemIds = args[3]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var inventory = new Inventory(id, characterId, load, itemIds);

                inventories.Add(inventory);
            }

            return inventories;
        }

        internal static void SaveInventories(List<Inventory> inventories)
        {
            List<string> lines = new List<string>();

            foreach (var inventory in inventories)
            {
                const string inventoryFormat = "{0};{1};{2};{3}";
                string line = string.Format(
                    inventoryFormat,
                    inventory.Id,
                    inventory.CharacterId,
                    inventory.Load,
                    string.Join(",", inventory.ItemIds));

                lines.Add(line);
            }

            WriteLines(config["inventories"], lines.ToArray());
        }

        public static List<Monster> LoadMonsters()
        {
            List<Monster> monsters = new List<Monster>();
            var dataLines = ReadLines(config["monsters"]);

            foreach (var line in dataLines)
            {
                string[] args = line.Split(';');
                int id = int.Parse(args[0]);

                var monster = new Monster(id);

                monsters.Add(monster);
            }

            return monsters;
        }

        private static void WriteLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }

        private static string[] ReadLines(string path)
        {
            EnsureFile(path);
            var lines = File.ReadAllLines(path);
            return lines;
        }

        private static void EnsureFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        private static void EnsureConfigFile(string configPath)
        {
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, DEFAULT_CONFIG);
            }
        }
    }
}
