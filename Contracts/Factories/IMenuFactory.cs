using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Factories
{
    public interface IMenuFactory
    {
        IMenu CreateMenu(string menuName);
    }
}
