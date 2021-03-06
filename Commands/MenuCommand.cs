using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public abstract class MenuCommand : IMenuCommand
    {
        private IMenuFactory menuFactory;

        public MenuCommand(IMenuFactory menuFactory)
        {
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            string commandName = this.GetType().Name;
            string menuName = commandName.Substring(0, commandName.Length - "Command".Length);

            IMenu menu = menuFactory.CreateMenu(menuName);

            return menu;
        }
    }
}
