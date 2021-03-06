﻿using ConsoleDiablo.DataModels.Characters;
using ConsoleDiablo.DataModels.Contracts.Characters;
using System;

namespace ConsoleDiablo.App.Entities.Characters
{
    public class Assassin : Character, IAssassin
    {
        public Assassin(int id, string name, string type, bool isDeleted, int gearId, int inventoryId, int accountId) 
            : base(id, name, "Assassin", isDeleted,
                  damage: 40,
                  defense: 60,
                  baseLife: 150,
                  baseMana: 70,
                  gearId,
                  inventoryId,
                  accountId)
        {
        }

        public Assassin(int id,
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
