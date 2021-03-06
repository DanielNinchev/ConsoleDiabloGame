using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Menus
{
    public class ExitMenu : Menu
    {
        public override Contracts.Models.IMenu ExecuteCommand()
        {
            Environment.Exit(-1);

            return null;
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
           
        }
    }
}
