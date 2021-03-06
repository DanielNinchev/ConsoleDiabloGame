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
    public class GearMenuCommand : IMenuCommand
    {
        private ISession session;
        private IMenuFactory menuFactory;

        public GearMenuCommand(ISession session, IMenuFactory menuFactory)
        {
            this.session = session;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("GearMenu");
            menu.SetId(characterId);

            return menu;
        }
    }
}
