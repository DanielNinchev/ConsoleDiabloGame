using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts.Characters;
using System;

namespace ConsoleDiablo.App.Entities.Characters
{
    public class Necromancer : Character, INecromancer
    {
        public Necromancer(int id, string name, string type, bool isDeleted, int gearId, int inventoryId, int accountId) 
            : base(id, name, "Necromancer", isDeleted,
                  damage : 10,
                  defense : 20,
                  baseLife : 120,
                  baseMana : 170,
                  gearId,
                  inventoryId,
                  accountId)
        {
        }

        public Necromancer(int id,
                    string name,
                    string type,
                    bool isDeleted,
                    DateTime dateCreated,
                    double damage,
                    double defense,
                    double baseLife,
                    double life,
                    double lifeRegenerationMultiplier,
                    double baseMana,
                    double mana,
                    double manaRegenerationMultiplier,
                    double experience,
                    int level,
                    double fireResistance,
                    double lightningResistance,
                    double coldResistance,
                    double poisonResistance,
                    int gearId,
                    int inventoryId,
                    double moneyBalance,
                    int accountId) : base(
                                         id,
                                         name,
                                         type,
                                         isDeleted,
                                         dateCreated,
                                         damage,
                                         defense,
                                         baseLife,
                                         life,
                                         lifeRegenerationMultiplier,
                                         baseMana,
                                         mana,
                                         manaRegenerationMultiplier,
                                         experience,
                                         level,
                                         fireResistance,
                                         lightningResistance,
                                         coldResistance,
                                         poisonResistance,
                                         gearId,
                                         inventoryId,
                                         moneyBalance,
                                         accountId)
        {

        }
    }
}
