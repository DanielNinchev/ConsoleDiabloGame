using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.DataModels.Contracts.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class PutItemOnCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;
        private ICharacterService characterService;
        private IItemService itemService;

        public PutItemOnCommand(IMenuFactory menuFactory, ICharacterService characterService, IItemService itemService)
        {
            this.menuFactory = menuFactory;
            this.characterService = characterService;
            this.itemService = itemService;
        }
        public IMenu Execute(params string[] args)
        {
            int itemId = int.Parse(args[0]);
            var item = this.itemService.GetItemById(itemId);

            if (item is IHandItem)
            {
                this.itemService.PutHandItemOn(itemId, item.CharacterId);
            }
            else
            {
                this.itemService.PutArmorOn(itemId, item.CharacterId);
            }
            
            var menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("GearMenu");
            menu.SetId(item.CharacterId);

            return menu;
        }
    }
}
