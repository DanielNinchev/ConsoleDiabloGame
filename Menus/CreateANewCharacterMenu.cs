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
    public class CreateANewCharacterMenu : Menu
    {
        private ILabelFactory labelFactory;
        private ICommandFactory commandFactory;

        public CreateANewCharacterMenu(ILabelFactory labelFactory, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.commandFactory = commandFactory;
            this.BackMenu = "SinglePlayerMenu";

            Open();
        }

        public override IMenu ExecuteCommand()
        {
            chosenOptions.Push(this.CurrentOption.Text);

            string commandName = string.Join("", this.CurrentOption.Text.Split()) + "Menu";

            IMenuCommand command = this.commandFactory.CreateCommand(commandName);

            IMenu menu = command.Execute();

            return menu;
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            string[] buttonContents = new string[]
            {
                "Amazon", "Assassin", "Barbarian", "Druid", "Necromancer", "Paladin", "Sorceress", "Back"
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top - 8),     //Amazon
                new Position(consoleCenter.Left - buttonContents[1].Length / 2, consoleCenter.Top - 6),     //Assassin
                new Position(consoleCenter.Left - buttonContents[2].Length / 2, consoleCenter.Top - 4),     //Barbarian
                new Position(consoleCenter.Left - buttonContents[3].Length / 2, consoleCenter.Top - 2),     //Druid
                new Position(consoleCenter.Left - buttonContents[4].Length / 2, consoleCenter.Top),         //Necromancer
                new Position(consoleCenter.Left - buttonContents[5].Length / 2, consoleCenter.Top + 2),     //Paladin
                new Position(consoleCenter.Left - buttonContents[6].Length / 2, consoleCenter.Top + 4),     //Sorceress
                new Position(consoleCenter.Left - buttonContents[7].Length / 2, consoleCenter.Top + 8),     //Back
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

            string[] labelContents = new string[] {"Choose a character type:" };

            Position[] labelPositions = new Position[]
            {
                new Position(consoleCenter.Left - labelContents[0].Length / 2, consoleCenter.Top - 13),
            };

            this.Labels = new ILabel[labelContents.Length];

            this.Labels[0] = new Label(labelContents[0], labelPositions[0]);

            for (int i = 1; i < this.Labels.Length; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
            }
        }
    }
}
