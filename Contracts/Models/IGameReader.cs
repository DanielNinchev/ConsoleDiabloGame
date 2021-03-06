using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IGameReader
    {
        string ReadLine();

        string ReadLine(int left, int top);

        void HideCursor();

        void ShowCursor();
    }
}
