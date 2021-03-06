using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class BackCommand : IMenuCommand
    {
        private ISession session;

        public BackCommand(ISession session)
        {
            this.session = session;
        }
        public IMenu Execute(params string[] args)
        {
            IMenu menu = this.session.Back();

            return menu;
        }
    }
}
