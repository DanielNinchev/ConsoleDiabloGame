using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Menus
{
    public interface ICommandAreaMenu : IMenu
    {
        ICommandInputArea CommandArea { get; }
    }
}
