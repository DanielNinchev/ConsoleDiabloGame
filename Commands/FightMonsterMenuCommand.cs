using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class FightMonsterMenuCommand : IMenuCommand
    {
        private ISession session;
        private IMenuFactory menuFactory;

        public FightMonsterMenuCommand(ISession session, IMenuFactory menuFactory)
        {
            this.session = session;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            var currentMenu = (IIdHoldingMenu)this.session.CurrentMenu;

            int characterId = currentMenu.Id;

            string menuName = "FightMonsterMenu";

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu(menuName);
            menu.SetId(characterId);

            return menu;
        }
    }
}
