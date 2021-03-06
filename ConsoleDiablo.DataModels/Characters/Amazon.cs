using ConsoleDiablo.DataModels.Contracts.Characters;
using System;

namespace ConsoleDiablo.DataModels.Characters
{
    public class Amazon : Character, IAmazon
    {
        public Amazon(int id, string name, string type, bool isDeleted, int gearId, int inventoryId, int accountId) 
            : base(id, name, type : "Amazon", isDeleted,
                  damage : 30, 
                  defense : 50, 
                  baseLife : 200, 
                  baseMana : 40,
                  gearId, inventoryId, accountId)
        {
        }

        public Amazon(int id,
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
