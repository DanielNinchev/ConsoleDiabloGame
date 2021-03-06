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
    public class SellItemCommand : IMenuCommand
    {
        private IItemService itemService;
        private IMenuFactory menuFactory;


        public SellItemCommand(IItemService itemService, IMenuFactory menuFactory, ISession session)
        {
            this.itemService = itemService;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            int itemId = int.Parse(args[0]);
            var item = this.itemService.GetItemById(itemId);

            this.itemService.SellItem(itemId, item.CharacterId);

            var menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("CreateCharacterMenu");
            menu.SetId(item.CharacterId);

            return menu;
        }
    }
}
