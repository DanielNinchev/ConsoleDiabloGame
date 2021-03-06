
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using ConsoleDiablo.DataModels.Contracts.Characters;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleDiablo.DataModels
{
    public class CharacterFactory : ICharacterFactory
    {
        public Character CreateCharacter(string type, string name, int characterId, int gearId, int inventoryId, int accountId)
        {
            var characterType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == type);

            if (characterType == null)
            {
                throw new InvalidOperationException("Invalid character type \"{type}\"!");
            }

            if (!typeof(ICharacter).IsAssignableFrom(characterType))
            {
                throw new InvalidFilterCriteriaException($"{characterType} is not а character!");
            }

            var character = (Character)Activator.CreateInstance(characterType, characterId, name, type, false, gearId, inventoryId, accountId);

            return character;
        }
    }
}
