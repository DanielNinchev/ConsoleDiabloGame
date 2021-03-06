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
    public class PickItemMenuCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;
        private ICharacterService characterService;
        private IItemService itemService;

        public PickItemMenuCommand(IMenuFactory menuFactory, ICharacterService characterService, IItemService itemService)
        {
            this.menuFactory = menuFactory;
            this.characterService = characterService;
            this.itemService = itemService;
        }
        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);
            int itemId = int.Parse(args[1]);
            int monsterId = int.Parse(args[2]);

            this.itemService.PickItem(itemId, characterId);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("ContinueMenu");

            menu.SetId(characterId, itemId, monsterId);

            return menu;
        }
    }
}
