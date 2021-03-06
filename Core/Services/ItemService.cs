using ConsoleDiablo.App.Core.ViewModels;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Gears;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.Data;
using ConsoleDiablo.DataModels;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using ConsoleDiablo.DataModels.Contracts.Factories;
using ConsoleDiablo.DataModels.Contracts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleDiablo.App.Core.Services
{
    public class ItemService : IItemService
    {
        private ConsoleDiabloData gameData;
        private ICharacterService characterService;
        private IItemFactory itemFactory;

        public ItemService(ConsoleDiabloData gameData, IAccountService accountService, ICharacterService characterService, IItemFactory itemFactory)
        {
            this.gameData = gameData;
            this.characterService = characterService;
            this.itemFactory = itemFactory;
        }

        public int CreateNewItem(string itemType)
        {
            int itemId = this.gameData.Items.Any() ? this.gameData.Items.Last().Id + 1 : 1;

            Item item = this.itemFactory.CreateItem(itemType, itemId, 0);

            this.gameData.Items.Add(item);
            this.gameData.SaveChanges();

            return item.Id;
        }

        //Adds an item of a specified item type N times in a list, where N = item.droppingFactor. Then returns the list
        public List<string> GetItemDroppingFactors()
        {
            IEnumerable<Item> items = GetEnumerableOfType<Item>();

            List<string> itemTypesByDroppingFactor = new List<string>();

            foreach (var item in items)
            {
                if (item != null)
                {
                    if (item.DroppingFactor != 0)
                    {
                        for (int i = 0; i < item.DroppingFactor; i++)
                        {
                            itemTypesByDroppingFactor.Add(item.Type);
                        }
                    }              
                }              
            }
            Random random = new Random();

            ShuffleAList(itemTypesByDroppingFactor, random);

            return itemTypesByDroppingFactor;
        }

        public void ShuffleAList(List<string> list, Random rnd)
        {
            for (var i = list.Count; i > 0; i--)
            {
                Swap(list, 0, rnd.Next(0, i));
            }
        }

        public void Swap(List<string> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        //Returns a list of all the types of T
        public IEnumerable<T> GetEnumerableOfType<T>() where T : class
        {
            List<T> types = new List<T>();

            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                types.Add((T)Activator.CreateInstance(type, this.gameData.Items.Any() ? this.gameData.Items.Last().Id + 1 : 1, 0));
            }

            return types;
        }

        public List<IItemViewModel> GetItemViewModelsForInventory(int characterId)
        {
            var character = this.gameData.Characters.FirstOrDefault(ch => ch.Id == characterId);
            var inventory = this.characterService.GetCharactersInventoryByHisId(characterId);

            List<IItemViewModel> itemViewModels = new List<IItemViewModel>();

            foreach (var itemId in inventory.ItemIds)
            {
                IItemViewModel item = InitializeItemBonuses(itemId);

                itemViewModels.Add(item);
            }

            return itemViewModels;
        }

        public List<IItemViewModel> GetItemViewModelsForGear(int characterId)
        {
            var character = this.gameData.Characters.FirstOrDefault(ch => ch.Id == characterId);
            var gear = this.characterService.GetCharactersGearByHisId(characterId);

            List<IItemViewModel> itemViewModels = new List<IItemViewModel>();

            foreach (var itemId in gear.ItemIds)
            {
                IItemViewModel item = InitializeItemBonuses(itemId);

                itemViewModels.Add(item);
            }

            return itemViewModels;
        }

        //Initiealizes the viewModel properties of the item
        public IItemViewModel InitializeItemBonuses(int itemId)
        {
            var item = this.GetItemById(itemId);

            if (item != null)
            {
                var itemViewModel = new ItemViewModel(item.Name, item.InventoryLoad, item.SellValue);

                if (item is IWeapon)
                {
                    var weapon = (IWeapon)item;

                    itemViewModel.BonusValues.Add(weapon.DamageBonus);
                }
                else if (item is IShield)
                {
                    var shield = (IShield)item;

                    itemViewModel.BonusValues.Add(shield.Defense);
                }
                else if (item is IArmorEquipment)
                {
                    var armor = (IArmorEquipment)item;

                    itemViewModel.BonusValues.Add(armor.DefenseBonus);
                    itemViewModel.BonusValues.Add(armor.FireResistanceBonus);
                    itemViewModel.BonusValues.Add(armor.LightningResistanceBonus);
                    itemViewModel.BonusValues.Add(armor.ColdResistanceBonus);
                    itemViewModel.BonusValues.Add(armor.PoisonResistanceBonus);
                }

                return itemViewModel;
            }
            else
            {
                return null;
            }    
        }

        //Gives the character his starting weapon (different for each type of character)
        public void GiveCharacterHisBasicGear(int characterId)
        {
            var character = this.characterService.GetCharacterById(characterId);

            if (character.Level == 1)
            {
                switch (character.Type)
                {
                    case "Amazon":
                        int javelinId = this.CreateNewItem("Javelin");
                        this.PutHandItemOn(javelinId, characterId);
                        break;
                    case "Assassin":
                        int wristBladeId = this.CreateNewItem("WristBlade");
                        this.PutHandItemOn(wristBladeId, characterId);
                        break;
                    case "Barbarian":
                        int itemId = this.CreateNewItem("HandAxe");
                        this.PutHandItemOn(itemId, characterId);
                        break;
                    case "Druid":
                        int clubId = this.CreateNewItem("Club");
                        this.PutHandItemOn(clubId, characterId);
                        break;
                    case "Necromancer":
                        int wandId = this.CreateNewItem("Wand");
                        this.PutHandItemOn(wandId, characterId);
                        break;
                    case "Paladin":
                        int swordId = this.CreateNewItem("ShortSword");
                        this.PutHandItemOn(swordId, characterId);
                        break;
                    case "Sorceress":
                        int staffId = this.CreateNewItem("Staff");
                        this.PutHandItemOn(staffId, characterId);
                        break;
                }
            }
        }

        public IItem GetItemById(int itemId)
        {
            var item = this.gameData.Items.FirstOrDefault(i => i.Id == itemId);

            return item;
        }

        public int GetItemIdByItsName(string itemName)
        {
            IItem item = this.gameData.Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                throw new ArgumentException("There is no such item!");
            }

            return item.Id;
        }

        public void BuyItem(int itemId, int characterId)
        {
            var item = GetItemById(itemId);
            var character = this.characterService.GetCharacterById(characterId);
            var inventory = this.characterService.GetCharactersInventoryByHisId(characterId);

            if (character.MoneyBalance >= item.SellValue )
            {
                if (inventory.Load + item.InventoryLoad <= inventory.Capacity)
                {
                    inventory.ItemIds.Add(item.Id);
                }
                else
                {
                    throw new ArgumentException("Not enough space in the inventory.");
                }
            }
            else
            {
                throw new ArgumentException("This item is too expensive!");
            }
        }

        public void DropItemFromGear(int itemId, int characterId)
        {
            var gear = this.characterService.GetCharactersGearByHisId(characterId);

            if (gear.ItemIds.Contains(itemId))
            {
                gear.ItemIds.Remove(itemId);
            }
        }

        public void DropItemFromInventory(int itemId, int characterId)
        {
            var inventory = this.characterService.GetCharactersInventoryByHisId(characterId);

            if (inventory.ItemIds.Contains(itemId))
            {
                inventory.ItemIds.Remove(itemId);
            }
        }

        //Checks if the inventory of a character has enough capacity, then adds an item to it
        public void PickItem(int itemId, int characterId)
        {
            var inventory = this.characterService.GetCharactersInventoryByHisId(characterId);
            var item = this.GetItemById(itemId);

            if (inventory.Capacity < item.InventoryLoad + inventory.Load)
            {
                throw new ArgumentException("Not enough space in inventory!");
            }
            else
            {
                inventory.Load += item.InventoryLoad;
                inventory.ItemIds.Add(item.Id);
                item.CharacterId = characterId;
            }

            this.gameData.SaveChanges();
        }

        //Checks if the character is wearing an other item on the specified slot for the current item. If not - adds the item to the 
        //specified slot
        public void PutHandItemOn(int itemId, int characterId)
        {
            var gear = (Gear)this.characterService.GetCharactersGearByHisId(characterId);
            var item = (IHandItem)this.GetItemById(itemId);

            var rightHandItem = (IHandItem)this.GetItemById(gear.RightHandItemId);
            var leftHandItem = (IHandItem)this.GetItemById(gear.LeftHandItemId);

            if (gear.RightHandItemId == 0 && gear.LeftHandItemId == 0)
            {
                gear.RightHandItemId = item.Id;
                
                this.MakeGearChanges(characterId, itemId);
            }
            else if (((gear.RightHandItemId != 0 && gear.LeftHandItemId == 0) && (!rightHandItem.IsTwoHanded && !item.IsTwoHanded)))
            {
                gear.LeftHandItemId = item.Id;

                this.MakeGearChanges(characterId, itemId);
            }
            else if (((gear.RightHandItemId == 0 && gear.LeftHandItemId != 0) && (!item.IsTwoHanded && !leftHandItem.IsTwoHanded)))
            {
                gear.RightHandItemId = item.Id;

                this.MakeGearChanges(characterId, itemId);
            }
            else
            {
                throw new ArgumentException("Cannot hold more items than that at the same time!");
            }
        }

        //Affects the properties of the character, the inventory and the gear, after the item has been put on a specified character
        public void MakeGearChanges(int characterId, int itemId)
        {
            var gear = (Gear)this.characterService.GetCharactersGearByHisId(characterId);
            var inventory = (Inventory)this.characterService.GetCharactersInventoryByHisId(characterId);
            var item = (Item)this.GetItemById(itemId);
            var character = (Character)this.characterService.GetCharacterById(characterId);

            gear.ItemIds.Add(item.Id);
            item.CharacterId = characterId;
            item.AffectCharacter(character);
            inventory.ItemIds.Remove(itemId);
            inventory.Load = Math.Max(inventory.Load - item.InventoryLoad, 0);

            this.gameData.SaveChanges();
        }

        public void PutArmorOn(int itemId, int characterId)
        {
            var gear = this.characterService.GetCharactersGearByHisId(characterId);
            var item = (IArmorEquipment)this.GetItemById(itemId);

            if (gear.ArmorId == 0)
            {
                gear.ArmorId = item.Id;

                MakeGearChanges(characterId, itemId);
            }
            else
            {
                throw new ArgumentException("Characters cannot wear multiple armors.");
            }
        }
            
        public void SellItem(int itemId, int characterId)
        {
            var item = GetItemById(itemId);
            var character = this.characterService.GetCharacterById(characterId);
            var inventory = this.characterService.GetCharactersInventoryByHisId(characterId);
            var gear = this.characterService.GetCharactersGearByHisId(characterId);

            if (item.DroppingFactor == 0)
            {
                throw new ArgumentException("Items from the starting kits of the characters cannot be sold.");
            }

            if (inventory.ItemIds.Contains(itemId))
            {
                character.MoneyBalance += item.SellValue;

                inventory.ItemIds.Remove(itemId);
                inventory.Load -= item.InventoryLoad;
            }
            else if (gear.ItemIds.Contains(item.Id))
            {
                character.MoneyBalance += item.SellValue;

                if (item is IWeapon)
                {
                    var weapon = (IWeapon)item;
                    character.Damage -= weapon.DamageBonus;

                    AffectGearItemIds(gear, itemId);
                }
                else if (item is IShield)
                {
                    var shield = (IShield)item;
                    character.Defense -= shield.Defense;

                    AffectGearItemIds(gear, itemId);
                }
                else
                {
                    var armor = (IArmorEquipment)item;
                    character.Defense -= armor.DefenseBonus;

                    AffectGearItemIds(gear, itemId);
                }

                gear.ItemIds.Remove(itemId);
            }
            else
            {
                throw new ArgumentException("You do not possess such item!");
            }

            this.gameData.SaveChanges();
        }

        public void AffectGearItemIds(IGear gear, int itemId)
        {
            if (gear.RightHandItemId == itemId)
            {
                gear.RightHandItemId = 0;
            }
            else if (gear.ArmorId == itemId)
            {
                gear.ArmorId = 0;
            }
            else
            {
                gear.LeftHandItemId = 0;
            }
        }
    }
}
