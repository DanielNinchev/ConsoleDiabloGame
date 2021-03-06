using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface ICommandInputArea
    {
        string Text { get; }

        void Write();

        void Render();
    }
}
