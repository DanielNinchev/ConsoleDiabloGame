using ConsoleDiablo.App.Entities.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class LogInMenuCommand : MenuCommand
    {
        public LogInMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {
        }
    }
}
