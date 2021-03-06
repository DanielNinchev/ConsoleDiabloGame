using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Models;
using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Menus
{
    public class MainMenu : Menu
    {
        private ISession session;
        private ILabelFactory labelFactory;
        private ICommandFactory commandFactory;

        public MainMenu(ISession session, ILabelFactory labelFactory, ICommandFactory commandFactory)
        {
            this.session = session;
            this.labelFactory = labelFactory;
            this.commandFactory = commandFactory;
            this.BackMenu = "MainMenu";

            this.Open();
        }

        public override IMenu ExecuteCommand()
        {
            string commandName = string.Join("", this.CurrentOption.Text.Split()) + "Menu";

            IMenuCommand command = this.commandFactory.CreateCommand(commandName);

            IMenu menu = command.Execute();

            return menu;
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            string[] buttonContents = new string[] { "Log In", "Sign Up", "Exit"};

            if (session?.IsLoggedIn ?? false)
            {

                buttonContents[0] = "Single Player";
                buttonContents[1] = "Multiplayer";
                buttonContents[2] = "Log Out";
            }

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left  - buttonContents[0].Length / 2, consoleCenter.Top + 4),
                new Position(consoleCenter.Left  - buttonContents[1].Length / 2, consoleCenter.Top + 6),
                new Position(consoleCenter.Left  - buttonContents[2].Length / 2, consoleCenter.Top + 8),
            };

            this.Buttons = new IButton[buttonContents.Length];

            for (int i = 0; i < this.Buttons.Length; i++)
            {
                this.Buttons[i] = labelFactory.CreateButton(buttonContents[i], buttonPositions[i]);
            }
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            string[] labelContents = new string[]
            {
                "CONSOLE DIABLO",
                string.Format("Welcome, {0}", this.session?.AccountName)
            };

            Position[] labelPositions = new Position[]
            {
                new Position(consoleCenter.Left - labelContents[0].Length / 2, consoleCenter.Top - 6),
                new Position(consoleCenter.Left - labelContents[1].Length / 2, consoleCenter.Top - 7),
            };

            this.Labels = new ILabel[labelContents.Length];

            int lastIndex = this.Labels.Length - 1;

            for (int i = 0; i < lastIndex; i++)
            {
                this.Labels[i] = labelFactory.CreateLabel(labelContents[i], labelPositions[i]);
            }

            this.Labels[lastIndex] = labelFactory.CreateLabel(labelContents[lastIndex], labelPositions[lastIndex], !session?.IsLoggedIn ?? true);
        }
    }
}
