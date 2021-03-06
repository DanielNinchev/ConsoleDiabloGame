using ConsoleDiablo.App.Entities.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class CreateANewCharacterMenuCommand : MenuCommand
    {
        public CreateANewCharacterMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {

        }
    }
}
