using ConsoleDiablo.App.Entities.Models;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Factories
{
    public interface ILabelFactory
    {
        ILabel CreateLabel(string content, Position position, bool isHidden = false);

        IButton CreateButton(string content, Position position, bool isHidden = false, bool isField = false);
    }
}
