using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts
{
    public interface IMenuCommand
    {
        IMenu Execute(params string[] args);
    }
}
