using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.ViewModels
{
    public interface IGameViewEngine
    {
        void RenderMenu(IMenu menu);

        void Mark(ILabel label, bool highlighted = true);

        void SetBufferHeight(int rows);

        void ResetBuffer();
    }
}
