using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class SinglePlayerMenuCommand : MenuCommand
    {
        public SinglePlayerMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {
        }
    }
}
