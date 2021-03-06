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
    public class SelectItemCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;

        public SelectItemCommand(IMenuFactory menuFactory, IItemService itemService)
        {
            this.menuFactory = menuFactory;
        }
        public IMenu Execute(params string[] args)
        {
            int itemId = int.Parse(args[0]);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("ItemMenu");

            menu.SetId(itemId);

            return menu;
        }
    }
}
