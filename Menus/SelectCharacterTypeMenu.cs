using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Menus
{
    public class SelectCharacterTypeMenu : Menu
    {
        private bool error;

        private ILabelFactory labelFactory;
        private IGameReader gameReader;
        private ICommandFactory commandFactory;

        public SelectCharacterTypeMenu(ILabelFactory labelFactory, IGameReader gameReader, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.gameReader = gameReader;
            this.commandFactory = commandFactory;
            this.BackMenu = "SinglePlayerMenu";

            Open();
        }

        private string CharacterNameInput => this.Buttons[0].Text.TrimStart();
        private string ErrorMessage { get; set; }

        public override IMenu ExecuteCommand()
        {
            if (this.CurrentOption.IsField)
            {
                string fieldInput = " " + this.gameReader.ReadLine(this.CurrentOption.Position.Left + 1, this.CurrentOption.Position.Top);

                this.Buttons[this.currentIndex] = this.labelFactory.CreateButton(fieldInput, this.CurrentOption.Position,
                    this.CurrentOption.IsHidden, this.CurrentOption.IsField);

                return this;
            }

            try
            {
                string commandName = string.Join("", this.CurrentOption.Text.Split());

                IMenuCommand command = this.commandFactory.CreateCommand(commandName);

                IMenu menu = command.Execute(this.CharacterNameInput, chosenOptions.Peek());

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
                " ", "Create Character", "Back"
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - 10, consoleCenter.Top - 8),  //Name
                new Position(consoleCenter.Left - buttonContents[1].Length / 2, consoleCenter.Top + 8),    //Create Character
                new Position(consoleCenter.Left - buttonContents[2].Length / 2, consoleCenter.Top + 10)    //Back
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
            string[] labelContents = new string[] { this.ErrorMessage, "Name:"};

            Position[] labelPositions = new Position[]
            {
                new Position(consoleCenter.Left - this.ErrorMessage?.Length / 2 ?? 0, consoleCenter.Top - 13),   // Error: 
                new Position(consoleCenter.Left - labelContents[1].Length - 10, consoleCenter.Top - 8),
            };

            this.Labels = new ILabel[labelContents.Length];

            this.Labels[0] = new Label(labelContents[0], labelPositions[0], !this.error);

            for (int i = 1; i < this.Labels.Length; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
            }
        }
    }
}
