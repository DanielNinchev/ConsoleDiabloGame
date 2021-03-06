using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class InventoryMenuCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;

        public InventoryMenuCommand(IMenuFactory menuFactory)
        {
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("InventoryMenu");
            menu.SetId(characterId);

            return menu;
        }
    }
}
