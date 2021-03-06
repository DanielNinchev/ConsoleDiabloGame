using ConsoleDiablo.App.Core.ViewModels;
using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.Data;
using ConsoleDiablo.DataModels;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDiablo.App.Core.Services
{
    public class MonsterService : IMonsterService
    {
        private ConsoleDiabloData gameData;
        private ICharacterService characterService;
        private IItemService itemService;
        private IItemFactory itemFactory;

        public MonsterService(ConsoleDiabloData gameData, ICharacterService characterService, IItemService itemService, IItemFactory itemFactory)
        {
            this.gameData = gameData;
            this.characterService = characterService;
            this.itemService = itemService;
            this.itemFactory = itemFactory;
        }

        public void Attack(int monsterId, int characterId)
        {
            var character = this.characterService.GetCharacterById(characterId);
            var monster = GetMonsterById(monsterId);

            if (monster.IsAlive)
            {
                switch (monster.DamageType)
                {
                    case "Fire":
                        character.Life = character.Life - monster.Damage * (1 + character.FireResistance / 100);
                        break;
                    case "Cold":
                        character.Life = character.Life - monster.Damage * (1 + character.ColdResistance / 100);
                        character.ManaRegenerationMultiplier = 0;
                        break;
                    case "Lightning":
                        character.Life = character.Life - monster.Damage * (1 + character.LightningResistance / 100);
                        break;
                    case "Poison":
                        character.Life = character.Life - monster.Damage * (1 + character.PoisonResistance / 100);
                        character.LifeRegenerationMultiplier = 0;
                        break;
                    case "Melee":
                        character.Life = character.Life - monster.Damage * (1 + character.Defense / 50);
                        break;
                }

                this.gameData.SaveChanges();

                if (character.Life <= 0)
                {
                    this.characterService.KillCharacter(characterId);
                }
                else
                {
                    this.characterService.Regenerate(characterId);
                }

                this.gameData.SaveChanges();
            }
        }

        public int CreateMonster(int characterId)
        {
            var character = this.characterService.GetCharacterById(characterId);
            var monsterId = this.gameData.Monsters.Any() ? this.gameData.Monsters.Last().Id + 1 : 1;

            var monster = new Monster(monsterId);
            Random number = new Random();
            List<string> randomNames = new List<string>() { "Zombie", "Demon", "Ghoul", "Spider", "Ogre", "Golem" };

            monster.Name = randomNames[number.Next(0, randomNames.Count - 1)];
            monster.Damage = character.Level * 10;
            monster.BaseLife = character.Level * 120;
            monster.Life = monster.BaseLife;
            monster.Defense = character.Level * 10;
            monster.FireResistance = Math.Min(character.Level * 10, 60);
            monster.ColdResistance = Math.Min(character.Level * 10, 60);
            monster.LightningResistance = Math.Min(character.Level * 10, 60);
            monster.PoisonResistance = Math.Min(character.Level * 10, 60);

            List<string> attackTypes = new List<string>() { "Fire", "Cold", "Lightning", "Poison", "Melee" };

            monster.DamageType = attackTypes[number.Next(0, 4)];

            this.gameData.Monsters.Add(monster);

            return monster.Id;
        }

        public void AttackMonster(int characterId, int monsterId)
        {
            var character = (Character)this.characterService.GetCharacterById(characterId);
            var monster = (Monster)GetMonsterById(monsterId);

            if (character.IsAlive)
            {
                monster.Life = Math.Floor(Math.Max(monster.Life - character.Damage * (1 - monster.Defense / 100), 0));

                if (monster.Life <= 0)
                {
                    KillMonster(monsterId);

                    character.Experience += 1000;

                    this.gameData.Characters.Remove(character);
                    this.gameData.Characters.Add(character);
                }
            }

            this.gameData.Monsters.Clear();
            this.gameData.Monsters.Add(monster);
            this.gameData.SaveChanges();
        }

        public void KillMonster(int monsterId)
        {
            var monster = GetMonsterById(monsterId);

            monster.IsAlive = false;
        }

        //Returns the dropped random itemId from the droppingFactor list from the itemService;
        public int DropPrize()
        {
            List<string> itemTypesByDroppingFactor = this.itemService.GetItemDroppingFactors();

            Random index = new Random();

            var randomItemType = itemTypesByDroppingFactor[index.Next(0, itemTypesByDroppingFactor.Count - 1)];

            var item = this.itemFactory.CreateItem(randomItemType, this.gameData.Items.Any() ? this.gameData.Items.Last().Id + 1 : 1, 0);

            this.gameData.Items.Add(item);
            this.gameData.SaveChanges();

            return item.Id;
        }

        public IMonster GetMonsterById(int monsterId)
        {
            var monster = this.gameData.Monsters.FirstOrDefault(m => m.Id == monsterId);

            return monster;
        }

        public IBeingViewModel GetMonsterViewModel(int monsterId)
        {
            var monster = this.gameData.Monsters.FirstOrDefault(m => m.Id == monsterId);

            IBeingViewModel monsterViewModel = new MonsterViewModel(
            monster.Name,
            monster.Damage,
            monster.Defense,
            monster.BaseLife,
            monster.Life,
            monster.FireResistance,
            monster.LightningResistance,
            monster.ColdResistance,
            monster.PoisonResistance);

            monsterViewModel.DamageType = monster.DamageType;

            return monsterViewModel;
        }

        public void SpecialAttack(int characterId)
        {
            //if (character.IsAlive)
            //{
            //    switch (character.DamageType)
            //    {
            //        case "Fire":
            //            monster.Life = monster.BaseLife - character.Damage * (1 + monster.FireResistance / 100);
            //            break;
            //        case "Cold":
            //            monster.Life = monster.BaseLife - character.Damage * (1 + monster.ColdResistance / 100);
            //            break;
            //        case "Lightning":
            //            monster.Life = monster.BaseLife - character.Damage * (1 + monster.LightningResistance / 100);
            //            break;
            //        case "Poison":
            //            monster.Life = monster.BaseLife - character.Damage * (1 + monster.PoisonResistance / 100);
            //            break;
            //        case "Melee":
            //            monster.Life = monster.BaseLife - character.Damage * (1 + monster.Defense / 50);
            //            break;
            //    }

            //    if (monster.Life <= 0)
            //    {
            //        KillMonster(monsterId);
            //    }
            //}
        }
    }
}
