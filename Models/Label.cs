using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Models
{
    public class Label : ILabel
    {
        public Label(string text, Position position, bool isHidden = false)
        {
            this.Text = text;
            this.Position = position;
            this.IsHidden = isHidden;
        }
        public string Text { get; }

        public bool IsHidden { get; }

        public Position Position { get; }
    }
}
