using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class SelectCharacterTypeMenuCommand : MenuCommand
    {
        public SelectCharacterTypeMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {
        }
    }
}
