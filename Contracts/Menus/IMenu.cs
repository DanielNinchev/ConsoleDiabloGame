using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IMenu
    {
        IButton CurrentOption { get; }

        ILabel[] Labels { get; }

        IButton[] Buttons { get; }

        string BackMenu { get; }

        void NextOption();

        void PreviousOption();

        IMenu ExecuteCommand();

        void Open();
    }
}
