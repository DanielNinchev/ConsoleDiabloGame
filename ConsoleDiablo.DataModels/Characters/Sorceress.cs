using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts.Characters;
using System;

namespace ConsoleDiablo.App.Entities.Characters.Factory
{
    public class Sorceress : Character, ISorceress
    {
        public Sorceress(int id, string name, string type, bool isDeleted, int gearId, int inventoryId, int accountId) : base
            (id, name, "Sorceress", isDeleted,
                  damage: 10,
                  defense: 10,
                  baseLife: 120,
                  baseMana: 180,
                  gearId,
                  inventoryId,
                  accountId)
        {

        }

        public Sorceress(int id,
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

        //public void UseSorceressAbility(Character character, SorceressAbility sorcAbility)
        //{
        //    EnsureAlive();
        //    if (sorcAbility is Blizzard & character.Mana >= sorcAbility.ManaCost)
        //    {
        //        sorcAbility.AffectCharacter(character);
        //    }
        //    else if (sorcAbility is Lightning & character.Mana >= sorcAbility.ManaCost)
        //    {
        //        sorcAbility.AffectCharacter(character);
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"{character.Name} does not have enough mana to perform this action.");
        //    }
        //}
    }
}
