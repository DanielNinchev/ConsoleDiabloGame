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
    public class InventoryMenu : Menu, IIdHoldingMenu, IReturningMenu
    {
        private ILabelFactory labelFactory;
        private ICharacterService characterService;
        private IItemService itemService;
        private ICommandFactory commandFactory;

        private ICharacterViewModel characterViewModel;
        private List<IItemViewModel> itemViewModels;

        public InventoryMenu(ILabelFactory labelFactory, ICharacterService characterService, IItemService itemService, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.characterService = characterService;
            this.itemService = itemService;
            this.commandFactory = commandFactory;
            this.BackMenu = "CreateCharacterMenu";
        }

        public int Id { get; set; } //itemId

        public override IMenu ExecuteCommand()
        {
            try
            {
                string commandName = this.CurrentOption.Text;
                int itemId = 0;

                foreach (var itemViewModel in this.itemViewModels)
                {
                    if (itemViewModel.Name.Equals(commandName))
                    {
                        itemId = this.itemService.GetItemIdByItsName(itemViewModel.Name);
                    }
                }

                IMenuCommand command = this.commandFactory.CreateCommand("SelectItem");

                IMenu menu = command.Execute(itemId.ToString(), this.characterViewModel.Name);

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
            string[] buttonContents = new string[this.itemViewModels.Count + 1];

            buttonContents[0] = "Back";

            Position[] buttonPositions = new Position[buttonContents.Length];

            buttonPositions[0] = new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top + 18);

            this.Buttons = new IButton[buttonContents.Length];

            this.Buttons[0] = this.labelFactory.CreateButton(buttonContents[0], buttonPositions[0]);

            if (this.itemViewModels.Count > 0)
            {
                for (int i = 1; i < buttonContents.Length; i++)
                {
                    buttonContents[i] = this.itemViewModels[i - 1].Name;
                }

                int yCoordinateCounter = 0;

                for (int i = 1; i < buttonPositions.Length; i++)
                {
                    buttonPositions[i] = new Position(consoleCenter.Left - buttonContents[i].Length / 2, consoleCenter.Top - 14 + yCoordinateCounter);

                    yCoordinateCounter++;
                }

                for (int i = 1; i < this.Buttons.Length; i++)
                {
                    string buttonContent = buttonContents[i];
                    bool isField = string.IsNullOrWhiteSpace(buttonContent);
                    this.Buttons[i] = this.labelFactory.CreateButton(buttonContent, buttonPositions[i], false, isField);
                }
            }         
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            string labelContent = null;

            if (this.itemViewModels.Count > 0)
            {
                labelContent = $"{this.characterViewModel.Name} keeps the following items in his inventory:";
            }
            else
            {
                labelContent = $"{this.characterViewModel.Name} doesn't keep any items in his inventory at the moment.";
            }

            Position labelPosition = new Position(consoleCenter.Left - labelContent.Length / 2, consoleCenter.Top - 18);

            this.Labels = new ILabel[] { new Label(labelContent, labelPosition) };
        }

        private void LoadViewModels()
        {
            this.characterViewModel = this.characterService.GetCharacterViewModel(this.Id);
            List<IItemViewModel> viewModels = this.itemService.GetItemViewModelsForInventory(this.Id);

            this.itemViewModels = new List<IItemViewModel>();

            foreach (var viewModel in viewModels)
            {
                if (viewModel != null)
                {
                    this.itemViewModels.Add(viewModel);
                }
            }
        }

        public void SetId(params int[] ids)
        {
            this.Id = ids[0];

            Open();
        }

        public override void Open()
        {
            LoadViewModels();

            base.Open();
        }
    }
}
