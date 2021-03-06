using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Models
{
    public class Button : IButton
    {
        public Button(string text, Position position, bool isHidden = false, bool isField = false)
        {
            this.Text = text;
            this.Position = position;
            this.IsHidden = isHidden;
            this.IsField = isField;
        }
        public bool IsField { get; }

        public string Text { get; }

        public bool IsHidden { get; }

        public Position Position { get; }
    }
}
