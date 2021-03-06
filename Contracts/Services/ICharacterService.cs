using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts;
using ConsoleDiablo.DataModels.Contracts.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Services
{
    public interface ICharacterService
    {
        void KillCharacter(int characterId);

        int CreateNewCharacter(int accountId, string characterName, string characterType);

        int GetCharacterIdByHisName(string characterName);

        ICharacterViewModel GetCharacterViewModel(int characterId);

        void DeleteCharacter(int characterId);

        IInventory GetCharactersInventoryByHisId(int characterId);

        IGear GetCharactersGearByHisId(int characterId);

        ICharacter GetCharacterById(int characterId);

        void Recover(int characterId);

        void Regenerate(int characterId);
    }
}
