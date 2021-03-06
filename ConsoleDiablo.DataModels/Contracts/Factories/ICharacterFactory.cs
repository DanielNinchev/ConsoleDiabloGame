using ConsoleDiablo.DataModels.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.DataModels.Contracts
{
    public interface ICharacterFactory
    {
        Character CreateCharacter(string type, string name, int characterId, int gearId, int inventoryId, int accountId);
    }
}
