using ConsoleDiablo.App.Entities.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class SignUpMenuCommand : MenuCommand
    {
        public SignUpMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {
        }
    }
}
