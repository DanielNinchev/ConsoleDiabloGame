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
    public class ChooseAnExistingCharacterMenu : Menu, IIdHoldingMenu
    {
        private ILabelFactory labelFactory;
        private IAccountService accountService;
        private ICharacterService characterService;
        private ICommandFactory commandFactory;
        private List<ICharacterViewModel> characterViewModels;
        private int accountId;

        public ChooseAnExistingCharacterMenu(ILabelFactory labelFactory, IAccountService accountService, 
            ICharacterService characterService, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.accountService = accountService;
            this.characterService = characterService;
            this.commandFactory = commandFactory;
            this.BackMenu = "SinglePlayerMenu";
            this.Id = accountId;    
        }

        public int Id { get; set; } //accountId

        public override IMenu ExecuteCommand()
        {
            try
            {
                string commandName = string.Join("", this.CurrentOption.Text.Split());
                IMenuCommand command = null;
                IMenu menu = null;

                if (this.characterViewModels.Count == 0)
                {
                    command = this.commandFactory.CreateCommand(commandName + "Menu");
                    menu = command.Execute();
                }
                else
                {
                    int characterId = 0;

                    for (int i = 0; i < this.characterViewModels.Count; i++)
                    {
                        if (commandName.Contains(characterViewModels[i].Name))
                        {
                            characterId = this.characterService.GetCharacterIdByHisName(characterViewModels[i].Name);
                        }
                    }

                    command = this.commandFactory.CreateCommand("SelectCharacter");

                    menu = command.Execute(characterId.ToString());
                }

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
            string[] buttonContents = new string[this.characterViewModels.Count];
            Position[] buttonPositions = new Position[buttonContents.Length];

            if (this.characterViewModels.Count != 0)
            {
                for (int i = 0; i < buttonContents.Length; i++)
                {
                    buttonContents[i] =
                        characterViewModels[i].Name + "                     " +
                        characterViewModels[i].Type + "                     " +
                        characterViewModels[i].Level.ToString() + "                     " +
                        characterViewModels[i].DateCreated.ToString();
                }

                int yCoordinateCounter = 0;

                for (int i = 0; i < buttonPositions.Length; i++)
                {
                    buttonPositions[i] = new Position(consoleCenter.Left - buttonContents[i].Length / 2, consoleCenter.Top - 8 + yCoordinateCounter);

                    yCoordinateCounter++;
                }
            }
            else
            {
                buttonContents = new string[] { "Create A New Character" };

                buttonPositions = new Position[] { new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top) };
            }
                
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
            if (this.characterViewModels.Count != 0)
            {
                string[] labelContents = new string[]
                {
                     "Name:", "Type:", "Level:", "Date Created:", 
                };

                Position[] labelPositions = new Position[]
                {
                new Position(consoleCenter.Left - 45, consoleCenter.Top - 12), //Name
                new Position(consoleCenter.Left - 20, consoleCenter.Top - 12), //Type
                new Position(consoleCenter.Left + 5, consoleCenter.Top - 12), //Level
                new Position(consoleCenter.Left + 30, consoleCenter.Top - 12), //Date Created 
                };

                this.Labels = new ILabel[labelContents.Length];

                for (int i = 0; i < this.Labels.Length; i++)
                {
                    this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
                }
            }
            else
            {
                string labelContent = "You have not created any characters yet.";

                Position labelPosition = new Position(consoleCenter.Left - labelContent.Length / 2, consoleCenter.Top - 2);

                this.Labels = new ILabel[] { new Label(labelContent, labelPosition) };
            }

        }

        private List<ICharacterViewModel> LoadCharacterViewModels()
        {
            var account = this.accountService.GetAccountById(accountId);

            this.characterViewModels = new List<ICharacterViewModel>();

            for (int i = 0; i < account.Characters.Count; i++)
            {
                var viewModel = this.characterService.GetCharacterViewModel(account.Characters[i]);

                if (viewModel != null && !this.characterViewModels.Contains(viewModel))
                {
                    this.characterViewModels.Add(viewModel);
                }
            }

            return this.characterViewModels;
        }

        public void SetId(params int[] ids)
        {
            this.accountId = ids[0];
 
            Open();
        }

        public override void Open()
        {
            this.LoadCharacterViewModels();

            base.Open();
        }
    }
}
