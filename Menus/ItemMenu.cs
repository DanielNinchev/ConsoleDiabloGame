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
    public class ItemMenu : Menu, IIdHoldingMenu, IReturningMenu
    {
        private ILabelFactory labelFactory;
        private IItemService itemService;
        private ICommandFactory commandFactory;

        private IItemViewModel itemViewModel;

        private bool error;

        public ItemMenu(ILabelFactory labelFactory, IItemService itemService, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.itemService = itemService;
            this.commandFactory = commandFactory;
        }

        public int Id { get; set; } //itemId

        public string Error { get; set; } = " ";

        public override IMenu ExecuteCommand()
        {
            try
            {
                string commandName = string.Join("", this.CurrentOption.Text.Split());

                IMenuCommand command = this.commandFactory.CreateCommand(commandName);

                IMenu menu = command.Execute(this.Id.ToString());

                return menu;
            }
            catch (Exception e)
            {
                this.error = true;
                this.Error = e.Message;
                this.Open();
                return this;
            }
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            if (this.itemViewModel != null)
            {
                string[] buttonContents = new string[]
                {
                    "Sell Item", "Put Item On", "Drop Item", "Gear", "Back"
                };

                Position[] buttonPositions = new Position[]
                {
                    new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top + 10),    //Sell
                    new Position(consoleCenter.Left - buttonContents[1].Length / 2, consoleCenter.Top + 12),    //Put on
                    new Position(consoleCenter.Left - buttonContents[2].Length / 2, consoleCenter.Top + 14),    //Drop
                    new Position(consoleCenter.Left - buttonContents[3].Length / 2, consoleCenter.Top + 18),    //Gear
                    new Position(consoleCenter.Left - buttonContents[4].Length / 2, consoleCenter.Top + 20)    //Back
                };

                this.Buttons = new IButton[buttonContents.Length];

                for (int i = 0; i < this.Buttons.Length; i++)
                {
                    string buttonContent = buttonContents[i];
                    bool isField = string.IsNullOrWhiteSpace(buttonContent);
                    this.Buttons[i] = this.labelFactory.CreateButton(buttonContent, buttonPositions[i], false, isField);
                }
            }          
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            string[] labelContents = new string[]
            {
                $"\"{this.itemViewModel.Name}\"",
                "Damage Bonus: " + itemViewModel.BonusValues[0].ToString(),
                "Inventory Space Required: " + itemViewModel.InventoryLoad.ToString(),
                "Sell Value: " + itemViewModel.SellValue.ToString(),
                this.Error
            };            

            if (this.itemService.GetItemById(this.Id) is IWeapon == false)
            {
                labelContents[1] = "Defense Bonus: " + itemViewModel.BonusValues[0].ToString();
            }

            Position[] labelPositions = new Position[]
            {
                new Position(consoleCenter.Left - labelContents[0].Length / 2, consoleCenter.Top - 12),
                new Position(consoleCenter.Left - labelContents[1].Length / 2, consoleCenter.Top - 8),
                new Position(consoleCenter.Left - labelContents[2].Length / 2, consoleCenter.Top - 6),
                new Position(consoleCenter.Left - labelContents[3].Length / 2, consoleCenter.Top - 4),
                new Position(consoleCenter.Left - this.Error.Length / 2, consoleCenter.Top),
            };

            this.Labels = new ILabel[labelContents.Length];

            this.Labels[^1] = new Label(labelContents[^1], labelPositions[^1], !this.error);

            for (int i = 0; i < this.Labels.Length - 1; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
            }
        }

        private void LoadViewModels()
        {
            this.itemViewModel = this.itemService.InitializeItemBonuses(this.Id);
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
