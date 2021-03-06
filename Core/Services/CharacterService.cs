using ConsoleDiablo.App.Core.IO;
using ConsoleDiablo.App.Core.ViewModels;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Gears;
using ConsoleDiablo.Data;
using ConsoleDiablo.DataModels;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using ConsoleDiablo.DataModels.Contracts.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDiablo.App.Core
{
    public class CharacterService : ICharacterService
    {
        private ConsoleDiabloData gameData;
        private IAccountService accountService;
        private ICharacterFactory characterFactory;

        public CharacterService(ConsoleDiabloData gameData, IAccountService accountService, ICharacterFactory characterFactory)
        {
            this.gameData = gameData;
            this.accountService = accountService;
            this.characterFactory = characterFactory;
        }

        public int GetCharacterIdByHisName(string characterName)
        {
            ICharacter character = this.gameData.Characters.FirstOrDefault(h => h.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                throw new ArgumentException(string.Format(Constants.InvalidCharacterMessage, characterName));
            }

            return character.Id;
        }

        public int CreateNewCharacter(int accountId, string characterName, string characterType)
        {
            char[] forbiddenSymbols = new char[] { '!', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '@', '#', '$', '%', '^', '&', '*', '(', ')',
            ',', '-', '=', '_', '+', '[', ']', '{', '}', ':', ';', '/', '\'', '|', '.', '?', '<', '>', '~', '`'};

            bool validCharacterName = !string.IsNullOrEmpty(characterName) && characterName.Length > 2;

            if (!validCharacterName)
            {
                throw new ArgumentException("The name of the character must be at least 3 letters long!");
            }

            foreach (var symbol in forbiddenSymbols)
            {
                if (characterName.Contains(symbol))
                {
                    throw new ArgumentException("The character's name can include only letters (no numbers and symbols allowed)!");
                }
            }

            bool characterAlreadyExists = this.gameData.Characters.Any(ch => ch.Name.Equals(characterName));

            if (characterAlreadyExists)
            {
                throw new ArgumentException("There is already a character with this name!");
            }

            if (characterType == null)
            {
                throw new ArgumentException($"Character {characterType} does not exist!");
            }

            int characterId = this.gameData.Characters.Any() ? this.gameData.Characters.Last().Id + 1 : 1;
            int gearId = this.gameData.Gears.Any() ? this.gameData.Gears.Last().Id + 1 : 1;
            int inventoryId = this.gameData.Inventories.Any() ? this.gameData.Inventories.Last().Id + 1 : 1;

            Account owner = this.accountService.GetAccountById(accountId);

            Character character = this.characterFactory.CreateCharacter(characterType, characterName, characterId, gearId, inventoryId, accountId);

            Gear gear = new Gear(gearId, characterId);
            Inventory inventory = new Inventory(inventoryId, characterId, 0, new List<int>() {0});

            this.gameData.Characters.Add(character);
            this.gameData.Gears.Add(gear);
            this.gameData.Inventories.Add(inventory);

            owner.Characters.Add(characterId);

            this.gameData.SaveChanges();

            return character.Id;
        }

        public void DeleteCharacter(int characterId)
        {
            var character = (Character)GetCharacterById(characterId);

            var account = this.accountService.GetAccountById(character.AccountId);

            account.Characters.Remove(characterId);

            character.IsDeleted = true;

            this.gameData.SaveChanges();
        }

        public ICharacter GetCharacterById(int characterId)
        {
            var character = this.gameData.Characters.FirstOrDefault(ch => ch.Id == characterId);

            return character;
        }

        public IGear GetCharactersGearByHisId(int characterId)
        {
            var gear = this.gameData.Gears.FirstOrDefault(g => g.CharacterId == characterId);

            return gear;
        }

        public IInventory GetCharactersInventoryByHisId(int characterId)
        {
            var inventory = this.gameData.Inventories.FirstOrDefault(i => i.CharacterId == characterId);

            return inventory;
        }

        public ICharacterViewModel GetCharacterViewModel(int characterId)
        {
            var character = this.gameData.Characters.FirstOrDefault(ch => ch.Id == characterId);

            if (!character.IsDeleted)
            {
                ICharacterViewModel characterViewModel = new CharacterViewModel(
                character.Name,
                character.Type,
                character.DateCreated,
                character.Level,
                character.Damage,
                character.Defense,
                character.BaseLife,
                character.Life,
                character.BaseMana,
                character.Mana,
                character.FireResistance,
                character.LightningResistance,
                character.ColdResistance,
                character.PoisonResistance,
                character.MoneyBalance);

                return characterViewModel;
            }
            else
            {
                return null;
            }         
        }

        public void KillCharacter(int characterId)
        {
            var character = GetCharacterById(characterId);
            var gear = GetCharactersGearByHisId(characterId);

            character.Experience = Math.Min((character.Level * 1000) + 1, character.Experience - 300);

            character.IsAlive = false;

            this.gameData.SaveChanges();
        }

        public void Regenerate(int characterId)
        {
            var character = GetCharacterById(characterId);

            character.Life = Math.Min(character.Life + character.BaseLife * character.LifeRegenerationMultiplier, character.BaseLife);
            character.Mana = Math.Min(character.Mana + character.BaseMana * character.ManaRegenerationMultiplier, character.BaseMana);
        }

        public void Recover(int characterId)
        {
            var character = GetCharacterById(characterId);

            character.IsAlive = true;

            character.Life = character.BaseLife;
            character.Mana = character.BaseMana;

            this.gameData.SaveChanges();
        }
    }
}
