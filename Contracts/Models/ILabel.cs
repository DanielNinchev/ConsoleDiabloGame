using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface ILabel : IPositionable
    {
        string Text { get; }

        bool IsHidden { get; }
    }
}
