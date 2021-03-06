using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class LogInCommand : IMenuCommand
    {
        private IAccountService accountService;
        private IMenuFactory menuFactory;

        public LogInCommand(IAccountService accountService, IMenuFactory menuFactory)
        {
            this.accountService = accountService;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)  
         {
            var accountName = args[0];
            var password = args[1];

            bool success = this.accountService.TryLoggingIn(accountName, password);

            if (!success)
            {
                throw new InvalidOperationException("Invalid account name or password!");
            }

            return this.menuFactory.CreateMenu("MainMenu");
        }
    }
}
