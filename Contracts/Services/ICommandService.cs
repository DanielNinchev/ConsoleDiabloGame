using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Services
{
    public interface ICommandService
    {
        string ProcessCommand(string commandName, IList<string> commandArgs);
    }
}
