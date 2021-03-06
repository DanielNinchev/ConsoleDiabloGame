using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.ViewModels
{
    public interface IMainController
    {
        void MarkOption();

        void UnmarkOption();

        void NextOption();

        void PreviousOption();

        void Back();

        void Execute();
    }
}
