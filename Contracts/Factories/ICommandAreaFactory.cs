using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Factories
{
    public interface ICommandAreaFactory
    {
        ICommandInputArea CreateCommandArea(IGameReader reader, int x, int y, bool isCharacter = true);
    }
}
