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
    public class CreateCharacterCommand : IMenuCommand
    {
        private ISession session;
        private ICharacterService characterService;
        private IItemService itemService;
        private IMenuFactory menuFactory;

        public CreateCharacterCommand(ISession session, ICharacterService characterService, IItemService itemService, IMenuFactory menuFactory)
        {
            this.session = session;
            this.characterService = characterService;
            this.itemService = itemService;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            int accountId = this.session.AccountId;

            string characterName = args[0];
            string characterType = args[1];

            int characterId = this.characterService.CreateNewCharacter(accountId, characterName, characterType);

            this.itemService.GiveCharacterHisBasicGear(characterId);

            string commandName = this.GetType().Name;
            string menuName = commandName.Substring(0, (commandName.Length - "Command".Length)) + "Menu";

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu(menuName);
            menu.SetId(characterId);

            return menu;
        }
    }
}
