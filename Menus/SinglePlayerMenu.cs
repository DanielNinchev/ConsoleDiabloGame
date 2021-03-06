using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Menus
{
    public class SinglePlayerMenu : Menu
    {
        private ILabelFactory labelFactory;
        private ICommandFactory commandFactory;

        public SinglePlayerMenu(ILabelFactory labelFactory, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.commandFactory = commandFactory;
            this.BackMenu = "MainMenu";

            this.Open();
        }

        public override IMenu ExecuteCommand()
        {
            string commandName = string.Join("", this.CurrentOption.Text.Split()) + "Menu";

            if (commandName == "BackMenu")
            {
                commandName = "Back";
            }

            IMenuCommand command = this.commandFactory.CreateCommand(commandName);

            IMenu menu = command.Execute();

            return menu;
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            string[] buttonContents = new string[]
            {
                "Choose An Existing Character", "Create A New Character", "Back"
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top - 4),
                new Position(consoleCenter.Left - buttonContents[1].Length / 2, consoleCenter.Top - 2),
                new Position(consoleCenter.Left - buttonContents[2].Length / 2, consoleCenter.Top + 2) //Back
            };

            this.Buttons = new IButton[buttonContents.Length];

            for (int i = 0; i < Buttons.Length; i++)
            {
                string buttonContent = buttonContents[i];
                this.Buttons[i] = this.labelFactory.CreateButton(buttonContent, buttonPositions[i]);
            }
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            string[] labelContents = new string[] { "SINGLE PLAYER" };

            Position[] labelPositions = new Position[]
            {
                new Position(consoleCenter.Left - labelContents[0].Length / 2, consoleCenter.Top - 10),
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
