using ConsoleDiablo.App.Core;
using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class ExitCommand : IMenuCommand
    {
        public IMenu Execute(params string[] args)
        {
            Environment.Exit(-1);

            return null;
        }
    }
}
