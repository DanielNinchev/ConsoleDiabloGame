using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class ContinueMenuCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;
        private ICharacterService characterService;
        private IMonsterService monsterService;

        public ContinueMenuCommand(IMenuFactory menuFactory, ICharacterService characterService, IMonsterService monsterService)
        {
            this.menuFactory = menuFactory;
            this.characterService = characterService;
            this.monsterService = monsterService;
        }

        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);
            int monsterId = int.Parse(args[1]);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("ContinueMenu");

            this.characterService.Recover(characterId);

            int itemId = this.monsterService.DropPrize();

            menu.SetId(characterId, itemId, monsterId);
                      
            return menu;
        }
    }
}
