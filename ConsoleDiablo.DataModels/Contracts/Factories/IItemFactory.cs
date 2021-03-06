using ConsoleDiablo.App.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Contracts.Factories
{
    public interface IItemFactory
    {
        Item CreateItem(string type, int id, int characterId);
    }
}
