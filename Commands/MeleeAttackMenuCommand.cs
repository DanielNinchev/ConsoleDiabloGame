using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Commands
{
    public class MeleeAttackMenuCommand : IMenuCommand
    {
        private ISession session;
        private IMenuFactory menuFactory;
        private ICharacterService characterService;
        private IMonsterService monsterService;

        public MeleeAttackMenuCommand(ISession session, IMenuFactory menuFactory, ICharacterService characterService, IMonsterService monsterService)
        {
            this.session = session;
            this.menuFactory = menuFactory;
            this.characterService = characterService;
            this.monsterService = monsterService;
        }

        public IMenu Execute(params string[] args)
        {
            int characterId = int.Parse(args[0]);
            int monsterId = int.Parse(args[1]);

            this.monsterService.AttackMonster(characterId, monsterId);
            this.monsterService.Attack(monsterId, characterId);

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu("FightMonsterMenu");
            menu.SetId(characterId, monsterId);

            return menu;
        }
    }
}
