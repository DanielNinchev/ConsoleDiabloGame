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
    public class CreateCharacterMenu : Menu, IIdHoldingMenu
    {
        private ILabelFactory labelFactory;
        private ICharacterService characterService;
        private ICommandFactory commandFactory;
        private ICharacterViewModel characterViewModel;

        private bool error;

        public CreateCharacterMenu(ILabelFactory labelFactory, ICharacterService characterService, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.characterService = characterService;
            this.commandFactory = commandFactory;
            this.BackMenu = "SinglePlayerMenu";
        }

        public int Id { get; set; } //characterId
        private string ErrorMessage { get; set; }

        public override IMenu ExecuteCommand()
        {
            try
            {
                string commandName = string.Join("", this.CurrentOption.Text.Split()) + "Menu";

                IMenuCommand command = this.commandFactory.CreateCommand(commandName);

                IMenu menu = command.Execute(this.Id.ToString());

                return menu;
            }
            catch (Exception e)
            {
                this.error = true;
                this.ErrorMessage = e.Message;

                this.Open();
                return this;
            }
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            string[] buttonContents = new string[]
            {
                "Gear", "Inventory", "Skills", "Fight Monster", "Fight Boss", "Delete Character", "Back"
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left + 40 - buttonContents[0].Length / 2, consoleCenter.Top - 9),    //Gear
                new Position(consoleCenter.Left + 40 - buttonContents[1].Length / 2, consoleCenter.Top - 8),    //Inventory
                new Position(consoleCenter.Left + 40 - buttonContents[2].Length / 2, consoleCenter.Top - 7),    //Skills
                new Position(consoleCenter.Left + 40 - buttonContents[3].Length / 2, consoleCenter.Top - 4),    //Fight Monster
                new Position(consoleCenter.Left + 40 - buttonContents[3].Length / 2, consoleCenter.Top - 3),    //Fight Boss
                new Position(consoleCenter.Left + 40 - buttonContents[4].Length / 2, consoleCenter.Top - 1),    //Delete Character
                new Position(consoleCenter.Left - buttonContents[5].Length / 2, consoleCenter.Top + 14)     //Back
            };

            this.Buttons = new IButton[buttonContents.Length];

            for (int i = 0; i < this.Buttons.Length; i++)
            {
                string buttonContent = buttonContents[i];
                bool isField = string.IsNullOrWhiteSpace(buttonContent);
                this.Buttons[i] = this.labelFactory.CreateButton(buttonContent, buttonPositions[i], false, isField);
            }
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            string[] labelContents = new string[] 
            { 
                this.characterViewModel.Name, 
                this.characterViewModel.Type.ToUpper(), 
                "Level: ", this.characterViewModel.Level.ToString(),
                "Life: ", this.characterViewModel.BaseLife.ToString(),
                "Mana: ", this.characterViewModel.BaseMana.ToString(),
                "Damage: ", this.characterViewModel.Damage.ToString(),
                "Defense: ", this.characterViewModel.Defense.ToString(),
                "Fire Resistance: ", this.characterViewModel.FireResistance.ToString(),
                "Cold Resistance: ", this.characterViewModel.ColdResistance.ToString(),
                "Lightning Resistance: ", this.characterViewModel.LightningResistance.ToString(),
                "Poison Resistance: ", this.characterViewModel.PoisonResistance.ToString(),
                "Money Balance: ", this.characterViewModel.MoneyBalance.ToString(),
                this.ErrorMessage
            };

            Position[] labelPositions = new Position[]
            {
                new Position(consoleCenter.Left - labelContents[0].Length / 2, consoleCenter.Top - 16), //Name
                new Position(consoleCenter.Left - labelContents[1].Length / 2, consoleCenter.Top - 14), //characterType
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 9), //Level:
                new Position(consoleCenter.Left - 40 + labelContents[2].Length, consoleCenter.Top - 9), //characterLevel
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 8), //Life:
                new Position(consoleCenter.Left - 40 + labelContents[4].Length, consoleCenter.Top - 8), //chracterBaseLife
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 7), //Mana:
                new Position(consoleCenter.Left - 40 + labelContents[6].Length, consoleCenter.Top - 7), //chracterBaseMana
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 6), //Damage:
                new Position(consoleCenter.Left - 40 + labelContents[8].Length, consoleCenter.Top - 6), //characterDamage
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 5), //Defense:
                new Position(consoleCenter.Left - 40 + labelContents[10].Length, consoleCenter.Top - 5), //characterDefense
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 4), //FR:
                new Position(consoleCenter.Left - 40 + labelContents[12].Length, consoleCenter.Top - 4), //characterFR
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 3), //CR:
                new Position(consoleCenter.Left - 40 + labelContents[14].Length, consoleCenter.Top - 3), //characterCR
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 2), //LR:
                new Position(consoleCenter.Left - 40 + labelContents[16].Length, consoleCenter.Top - 2), //LR
                new Position(consoleCenter.Left - 40, consoleCenter.Top - 1), //PR:
                new Position(consoleCenter.Left - 40 + labelContents[18].Length, consoleCenter.Top - 1), //cPR
                new Position(consoleCenter.Left - 40, consoleCenter.Top), //MoneyBalance:
                new Position(consoleCenter.Left - 40 + labelContents[20].Length, consoleCenter.Top), //cMB

                new Position(consoleCenter.Left - this.ErrorMessage?.Length / 2 ?? 0, consoleCenter.Top + 12), //error
            };

            this.Labels = new ILabel[labelContents.Length];

            this.Labels[^1] = new Label(labelContents[^1], labelPositions[^1], !this.error);

            for (int i = 0; i < this.Labels.Length - 1; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
            }
        }

        private void LoadCharacterInfo()
        {
            this.characterViewModel = this.characterService.GetCharacterViewModel(this.Id);
        }

        public void SetId(params int[] ids)
        {
            this.Id = ids[0];

            Open();
        }

        public override void Open()
        {
            this.LoadCharacterInfo();

            base.Open();
        }
    }
}
