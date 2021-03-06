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
    public class BackToMenuMenuCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;
        private ICharacterService characterService;

        public BackToMenuMenuCommand(IMenuFactory menuFactory, ICharacterService characterService)
        {
            this.menuFactory = menuFactory;
            this.characterService = characterService;
        }
        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);

            this.characterService.Recover(characterId);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("CreateCharacterMenu");

            menu.SetId(characterId);

            return menu;
        }
    }
}
