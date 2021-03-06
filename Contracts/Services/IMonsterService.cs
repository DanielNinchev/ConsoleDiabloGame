using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Contracts.Services
{
    public interface IMonsterService
    {
        void Attack(int monsterId, int characterId);

        void AttackMonster(int characterId, int monsterId);

        int CreateMonster(int characterId);

        void KillMonster(int monsterId);

        int DropPrize();

        IMonster GetMonsterById(int monsterId);

        IBeingViewModel GetMonsterViewModel(int monsterId);

        void SpecialAttack(int characterId);
    }
}
