using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class LogOutCommand : IMenuCommand
    {
        private ISession session;
        private IMenuFactory menuFactory;

        public LogOutCommand(ISession session, IMenuFactory menuFactory)
        {
            this.session = session;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            this.session.Reset();

            return this.menuFactory.CreateMenu("MainMenu");
        }
    }
}
