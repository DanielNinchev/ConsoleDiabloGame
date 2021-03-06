using ConsoleDiablo.App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Models
{
    public interface IPositionable
    {
        Position Position { get; }
    }
}
