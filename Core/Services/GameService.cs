using ConsoleDiablo.App.Entities.Abilities;
using ConsoleDiablo.App.Entities.Abilities.BarbarianAbilities;
using ConsoleDiablo.App.Entities.Abilities.Factory;
using ConsoleDiablo.App.Entities.Abilities.SorceressAbilities;
using ConsoleDiablo.App.Entities.Characters.Factory;
using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.App.Entities.Items.Factory;
using ConsoleDiablo.App.Entities.Weapons;
using ConsoleDiablo.Data;
using ConsoleDiablo.DataModels.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDiablo.App.Core
{
    public class GameService : IGameService
    {
        private ConsoleDiabloData gameData;
        private ICharacterService characterService;
        private IItemService itemService;

        public GameService(ConsoleDiabloData gameData, ICharacterService characterService, IItemService itemService,
            IEnumerable<int> poolItemIds, IEnumerable<int> partyIds, IEnumerable<int> shopItemIds)
        {
            this.gameData = gameData;
            this.characterService = characterService;
            this.itemService = itemService;

            this.ItemPool = new List<int>(poolItemIds);
            this.PartyIds = new List<int>(partyIds);
            this.Shop = new List<int>(shopItemIds);
        }

        public ICollection<int> ItemPool { get; set; }
        public ICollection<int> PartyIds { get; set; }
        public ICollection<int> Shop { get; set; }

        public void StartGame(params int[] characterIds)
        {
            foreach (var id in characterIds)
            {
                this.PartyIds.Add(id);
            }
        }


        //    public string GetStats()
        //    {
        //        var sortedCharacters = party
        //            .OrderByDescending(c => c.IsAlive)
        //            .ThenByDescending(c => c.Life);
        //        var result = string.Join(Environment.NewLine, sortedCharacters);

        //        return result;
        //    }

        //    public string EndTurn()
        //    {
        //        var aliveCharacters = party.Where(c => c.IsAlive).ToArray();

        //        var sb = new StringBuilder();

        //        foreach (var character in aliveCharacters)
        //        {
        //            var previousHealth = character.Life;

        //            character.Recover();

        //            var currentHealth = character.Life;
        //            sb.AppendLine($"{character.Name} rests ({previousHealth} => {currentHealth}).");
        //        }

        //        if (aliveCharacters.Length <= 1)
        //        {
        //            lastSurviverRounds++;
        //        }

        //        var result = sb.ToString().TrimEnd('\r', '\n');

        //        return result;
        //    }

        //    public bool IsGameOver()
        //    {
        //        var oneOrZeroSurvivorsLeft = party.Count(c => c.IsAlive) <= 1;

        //        var lastSurviverSurvivedTooLong = this.lastSurviverRounds > 1;

        //        return oneOrZeroSurvivorsLeft && lastSurviverSurvivedTooLong;
        //    }
        //}
    }
}
