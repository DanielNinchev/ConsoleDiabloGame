using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Models
{
    public class CommandArea : ICommandInputArea
    {
        private const int commandLineLength = 37;

        private int x;
        private int y;
        private int height;
        private int maxLength;
        private int textCursorPosition;
        private Position displayCursor;

        public string Text => throw new NotImplementedException();

        public void Render()
        {
            throw new NotImplementedException();
        }

        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
