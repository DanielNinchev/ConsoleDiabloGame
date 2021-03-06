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
    public class SelectCharacterCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;

        public SelectCharacterCommand(IMenuFactory menuFactory, IItemService itemService)
        {
            this.menuFactory = menuFactory;
        }
        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("CreateCharacterMenu");

            menu.SetId(characterId);

            return menu;
        }
    }
}
