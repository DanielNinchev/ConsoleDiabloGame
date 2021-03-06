using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class ChooseAnExistingCharacterMenuCommand : IMenuCommand
    {
        private ISession session;
        private IMenuFactory menuFactory;

        public ChooseAnExistingCharacterMenuCommand(ISession session, IMenuFactory menuFactory)
        {
            this.session = session;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            int accountId = this.session.AccountId;

            string menuName = "ChooseAnExistingCharacterMenu";

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu(menuName);
            menu.SetId(accountId);

            return menu;
        }
    }
}
