using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts.Characters;
using System;

namespace ConsoleDiablo.App.Entities.Characters
{
    public class Paladin : Character, IPaladin
    {
        public Paladin(int id, string name, string type, bool isDeleted, int gearId, int inventoryId, int accountId) : base
            (id, name, "Paladin", isDeleted,
                  damage: 30,
                  defense: 50,
                  baseLife: 150,
                  baseMana: 90,
                  gearId,
                  inventoryId,
                  accountId)
        {

        }

        public Paladin(int id,
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

        //public void UsePaladinAbility(Character character, PaladinAbility palAbility)
        //{
        //    EnsureAlive();

        //    if (palAbility is Zealotry & character.Mana >= palAbility.ManaCost)
        //    {
        //        palAbility.AffectCharacter(character);
        //    }
        //    else if (palAbility is Charge & character.Mana >= palAbility.ManaCost)
        //    {
        //        palAbility.AffectCharacter(character);
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"{character.Name} does not have enough mana to perform this action.");
        //    }
        //}
    }
}
