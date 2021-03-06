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
    public class DeleteCharacterMenuCommand : IMenuCommand
    {
        private ISession session;
        private ICharacterService characterService;
        private IMenuFactory menuFactory;

        public DeleteCharacterMenuCommand(ISession session, ICharacterService characterService, IMenuFactory menuFactory)
        {
            this.session = session;
            this.characterService = characterService;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);

            this.characterService.DeleteCharacter(characterId);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("ChooseAnExistingCharacterMenu");

            menu.SetId(this.session.AccountId);

            return menu;
        }
    }
}
