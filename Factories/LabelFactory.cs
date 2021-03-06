using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Factories
{
    public class LabelFactory : ILabelFactory
    {
        public IButton CreateButton(string content, Position position, bool isHidden = false, bool isField = false)
        {
            return new Button(content, position, isHidden, isField);
        }

        public ILabel CreateLabel(string content, Position position, bool isHidden = false)
        {
            return new Label(content, position, isHidden);
        }
    }
}
