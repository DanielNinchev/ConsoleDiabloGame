using ConsoleDiablo.App.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Factories
{
    public interface ICommandFactory
    {
        IMenuCommand CreateCommand(string commandName);
    }
}
