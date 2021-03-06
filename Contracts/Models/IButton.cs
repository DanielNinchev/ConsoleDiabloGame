using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IButton : ILabel
    {
        bool IsField { get; }
    }
}
