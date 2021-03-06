using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Menus
{
    public class FightMonsterMenu : Menu, IIdHoldingMenu, IReturningMenu
    {
        private ILabelFactory labelFactory;
        private ICharacterService characterService;
        private IMonsterService monsterService;
        private ICommandFactory commandFactory;

        private ICharacterViewModel characterViewModel;
        private IBeingViewModel monsterViewModel;

        private int monsterId = 0;

        public FightMonsterMenu(ILabelFactory labelFactory, ICharacterService characterService, IMonsterService monsterService,
            IGameService gameService, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.characterService = characterService;
            this.monsterService = monsterService;
            this.commandFactory = commandFactory;
            this.BackMenu = "CreateCharacterMenu";
        }

        public int Id { get; set; } //characterId

        public override IMenu ExecuteCommand()
        {
            try
            {
                string commandName = string.Join("", this.CurrentOption.Text.Split()) + "Menu";

                IMenuCommand command = this.commandFactory.CreateCommand(commandName);

                IMenu menu = command.Execute(this.Id.ToString(), this.monsterId.ToString());

                return menu;
            }
            catch (Exception)
            {
                this.Open();
                return this;
            }

        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            var character = this.characterService.GetCharacterById(this.Id);
            var monster = this.monsterService.GetMonsterById(this.monsterId);

            string[] buttonContents = null;
            Position[] buttonPositions = null;

            if (character.IsAlive && monster.IsAlive)
            {
                buttonContents = new string[]
                {
                    "Melee Attack", "Skill Attack", "Quit Game"
                };

                buttonPositions = new Position[]
                {
                    new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top + 5),    //Melee attack
                    new Position(consoleCenter.Left - buttonContents[1].Length / 2, consoleCenter.Top + 6),    //Skill attack
                    new Position(consoleCenter.Left - buttonContents[2].Length / 2, consoleCenter.Top + 9),    //Quit
                };
            }
            else if (!character.IsAlive)
            {
                buttonContents = new string[]
                {
                    "Back To Menu"
                };

                buttonPositions = new Position[]
                {
                    new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top + 9),
                };
            }

            else
            {
                buttonContents = new string[]
                {
                    "Continue"
                };

                buttonPositions = new Position[]
                {
                    new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top + 9),   
                };
            }

            this.Buttons = new IButton[buttonContents.Length];

            for (int i = 0; i < this.Buttons.Length; i++)
            {
                string buttonContent = buttonContents[i];
                bool isField = string.IsNullOrWhiteSpace(buttonContent);
                this.Buttons[i] = this.labelFactory.CreateButton(buttonContent, buttonPositions[i], false, isField);
            }
        }

        public List<string> DetermineWinner()
        {
            List<string> gameResult = new List<string>();

            var character = this.characterService.GetCharacterById(this.Id);
            var monster = this.monsterService.GetMonsterById(this.monsterId);

            if (character.IsAlive && !monster.IsAlive)
            {
                gameResult.Add(character.Name);
                gameResult.Add(monster.Name);
            }
            else if (!character.IsAlive && monster.IsAlive)
            {
                gameResult.Add(monster.Name);
                gameResult.Add(character.Name);
            }

            return gameResult;
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            string conditionalLabel = null;
            bool conditionalLabelIsHidden = (this.characterViewModel.BaseLife == this.characterViewModel.Life &&
                this.monsterViewModel.BaseLife == this.monsterViewModel.Life);

            if (this.characterViewModel.Mana == characterViewModel.BaseMana)
            {
                conditionalLabel = $"{this.characterViewModel.Name} attacked the {monsterViewModel.Name.ToLower()} with a melee attack!" +
                 $" The {monsterViewModel.Name.ToLower()} fought back with a {this.monsterViewModel.DamageType.ToLower()} attack!";
            }
            else
            {
                conditionalLabel = conditionalLabel = $"{this.characterViewModel.Name} attacked the {monsterViewModel.Name.ToLower()} with a skill attack!" +
                 $" The {monsterViewModel.Name.ToLower()} fought back with a {this.monsterViewModel.DamageType.ToLower()} attack!";
            }

            List<string> gameResult = DetermineWinner();

            if (gameResult.Count != 0)
            {
                conditionalLabel = $"{gameResult[1]} was slain by {gameResult[0]}.";
            }

            string[] labelContents = new string[]
            {
                 $"\"{this.characterViewModel.Name}\" vs \"{this.monsterViewModel.Name}\"",

                 $"\"{this.characterViewModel.Name}\"",
                 $"Life: {Math.Floor(this.characterViewModel.Life)}/{this.characterViewModel.BaseLife}",
                 $"Mana: {Math.Floor(this.characterViewModel.Mana)}/{this.characterViewModel.BaseMana}",
                 $"Damage: {Math.Floor(this.characterViewModel.Damage)}",
                 $"Defense: {Math.Floor(this.characterViewModel.Defense)}",
                 $"Fire Resistance: {this.characterViewModel.FireResistance}",
                 $"Lightning Resistance: {this.characterViewModel.LightningResistance}",
                 $"Cold Resistance: {this.characterViewModel.ColdResistance}",
                 $"Poison Resistance: {this.characterViewModel.PoisonResistance}",

                 $"\"{this.monsterViewModel.Name}\"",
                 $"Life: {Math.Floor(this.monsterViewModel.Life)}/{Math.Floor(this.monsterViewModel.BaseLife)}",
                 $"Damage: {Math.Floor(this.monsterViewModel.Damage)}",
                 $"Damage Type: {this.monsterViewModel.DamageType}",
                 $"Defense: {Math.Floor(this.monsterViewModel.Defense)}",
                 $"Fire Resistance: {this.monsterViewModel.FireResistance}",
                 $"Lightning Resistance: {this.monsterViewModel.LightningResistance}",
                 $"Cold Resistance: {this.monsterViewModel.ColdResistance}",
                 $"Poison Resistance: {this.monsterViewModel.PoisonResistance}",

                 conditionalLabel
            };

            Position[] labelPositions = new Position[]
            {
               new Position(consoleCenter.Left - labelContents[0].Length / 2, consoleCenter.Top - 18),
          
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 14), //Name
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 13), //Life
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 12), //Mana
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 11), //Damage
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 10), //Defense
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 9), //FR
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 8), //LR
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 7), //CR
               new Position(consoleCenter.Left - labelContents[0].Length / 2 - 40, consoleCenter.Top - 6), //PR

               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 14), //Name
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 13), //Life
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 12), //Damage
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 11), //DamageType
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 10), //Defense
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 9), //FR
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 8), //LR
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 7), //CR
               new Position(consoleCenter.Left - labelContents[0].Length / 2 + 40, consoleCenter.Top - 6), //PR

               new Position(consoleCenter.Left - labelContents[19].Length / 2, consoleCenter.Top), //conditional label
            };

            this.Labels = new ILabel[labelContents.Length];

            this.Labels[^1] = new Label(conditionalLabel, labelPositions[^1], conditionalLabelIsHidden);

            for (int i = 0; i < this.Labels.Length - 1; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
            }
        }

        private void LoadViewModels()
        {
            if (this.monsterId == 0)
            {
                this.monsterId = this.monsterService.CreateMonster(this.Id);
            }           

            this.monsterViewModel = this.monsterService.GetMonsterViewModel(this.monsterId);
            this.characterViewModel = this.characterService.GetCharacterViewModel(this.Id);       
        }

        public void SetId(params int[] ids)
        {
            this.Id = ids[0];

            if (ids.Length > 1)
            {
                this.monsterId = ids[1];
            }   

            Open();
        }

        public override void Open()
        {
            LoadViewModels();

            base.Open();
        }
    }
}
