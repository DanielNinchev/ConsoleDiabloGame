using ConsoleDiablo.App.Entities.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class ExitMenuCommand : MenuCommand
    {
        public ExitMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {
        }
    }
}
