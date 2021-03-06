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
    public class GearMenu : Menu, IIdHoldingMenu, IReturningMenu
    {
        private ILabelFactory labelFactory;
        private ICharacterService characterService;
        private IItemService itemService;
        private ICommandFactory commandFactory;

        private ICharacterViewModel characterViewModel;
        private List<IItemViewModel> itemViewModels;
        private IItemViewModel armorViewModel;

        private List<IButton> gearMenuButtons;
        private List<ILabel> gearMenuLabels;

        private int itemId = 0;

        private bool error;

        public GearMenu(ILabelFactory labelFactory, ICharacterService characterService, IItemService itemService, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.characterService = characterService;
            this.itemService = itemService;
            this.commandFactory = commandFactory;
            this.BackMenu = "CreateCharacterMenu";
        }

        public int Id { get; set; } //characterId

        private string ErrorMessage { get; set; }

        public override IMenu ExecuteCommand()
        {
            try
            {
                string commandName = this.CurrentOption.Text;

                foreach (var viewModel in this.itemViewModels)
                {
                    if (commandName.Contains(viewModel.Name))
                    {
                        var itemId = this.itemService.GetItemIdByItsName(viewModel.Name);
                        this.itemId = itemId;
                    }
                }

                IMenuCommand command = this.commandFactory.CreateCommand("SellItem");

                IMenu menu = command.Execute(this.itemId.ToString(), this.Id.ToString());

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

        private void FillMainButtonsArray(Position consoleCenter)
        {
            string[] buttonContents = new string[]
            {
                "Inventory",
                "Back"
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top + 14), //Inventory
                new Position(consoleCenter.Left - buttonContents[1].Length / 2, consoleCenter.Top + 16), //Back
            };

            FillGearMenuButtons(buttonContents, buttonPositions);
        }

        private void FillRightHandItemButtonsArray(Position consoleCenter)
        {
            var gear = this.characterService.GetCharactersGearByHisId(this.Id);
            string itemName = null;

            foreach (var viewModel in this.itemViewModels)
            {
                int itemId = this.itemService.GetItemIdByItsName(viewModel.Name);
                if (gear.RightHandItemId == itemId)
                {
                    itemName = viewModel.Name;
                }
            }

            string[] buttonContents = new string[]
            {
                $"Move {itemName} To Inventory",
                $"Sell {itemName}",
                $"Drop {itemName}",
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - this.itemViewModels[0].Name.Length / 2 - 50, consoleCenter.Top), //Move
                new Position(consoleCenter.Left - this.itemViewModels[0].Name.Length / 2 - 50, consoleCenter.Top + 2), //Sell
                new Position(consoleCenter.Left - this.itemViewModels[0].Name.Length / 2 - 50, consoleCenter.Top + 4), //Drop
            };

            FillGearMenuButtons(buttonContents, buttonPositions);
        }

        private void FillLeftHandItemButtonsArray(Position consoleCenter)
        {
            var gear = this.characterService.GetCharactersGearByHisId(this.Id);
            string itemName = null;

            foreach (var viewModel in this.itemViewModels)
            {
                int itemId = this.itemService.GetItemIdByItsName(viewModel.Name);
                if (gear.LeftHandItemId == itemId)
                {
                    itemName = viewModel.Name;
                }
            }

            string[] buttonContents = new string[]
            {
                $"Move {itemName} To Inventory",
                $"Sell {itemName}",
                $"Drop {itemName}",
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - this.itemViewModels[1].Name.Length / 2 + 50, consoleCenter.Top), //Move
                new Position(consoleCenter.Left - this.itemViewModels[1].Name.Length / 2 + 50, consoleCenter.Top + 2), //Sell
                new Position(consoleCenter.Left - this.itemViewModels[1].Name.Length / 2 + 50, consoleCenter.Top + 4), //Drop
            };

            FillGearMenuButtons(buttonContents, buttonPositions);
        }

        private void FillArmorButtonsArray(Position consoleCenter)
        {
            string[] buttonContents = new string[]
            {
                "Move To Inventory",
                "Sell Item",
                "Drop Item",
            };

            Position[] buttonPositions = new Position[]
            {
               new Position(consoleCenter.Left - this.armorViewModel.Name.Length / 2, consoleCenter.Top), //Move
               new Position(consoleCenter.Left - this.armorViewModel.Name.Length / 2, consoleCenter.Top + 2), //Sell
               new Position(consoleCenter.Left - this.armorViewModel.Name.Length / 2, consoleCenter.Top + 4), //Drop
            };

            FillGearMenuButtons(buttonContents, buttonPositions);
        }

        private void FillGearMenuButtons(string[] buttonContents, Position[] buttonPositions)
        {        
            this.Buttons = new IButton[buttonContents.Length];

            for (int i = 0; i < this.Buttons.Length; i++)
            {
                string buttonContent = buttonContents[i];
                bool isField = string.IsNullOrWhiteSpace(buttonContent);

                this.Buttons[i] = this.labelFactory.CreateButton(buttonContent, buttonPositions[i], false, isField);

                this.gearMenuButtons.Add(this.Buttons[i]);
            }
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            this.gearMenuButtons = new List<IButton>();

            if (this.itemViewModels.Count == 0 && this.armorViewModel is null)
            {
                FillMainButtonsArray(consoleCenter);
            }
            else if (this.itemViewModels.Count == 1 && this.armorViewModel is null)
            {
                FillRightHandItemButtonsArray(consoleCenter);
                FillMainButtonsArray(consoleCenter);
            }
            else if (this.itemViewModels.Count == 2 && this.armorViewModel is null)
            {
                FillRightHandItemButtonsArray(consoleCenter);
                FillLeftHandItemButtonsArray(consoleCenter);
                FillMainButtonsArray(consoleCenter);
            }
            else if (this.itemViewModels.Count == 1 && this.armorViewModel != null)
            {
                FillRightHandItemButtonsArray(consoleCenter);
                FillArmorButtonsArray(consoleCenter);
                FillMainButtonsArray(consoleCenter);
            }
            else if (this.itemViewModels.Count == 2 && this.armorViewModel != null)
            {
                FillRightHandItemButtonsArray(consoleCenter);
                FillArmorButtonsArray(consoleCenter);
                FillLeftHandItemButtonsArray(consoleCenter);
                FillMainButtonsArray(consoleCenter);
            }
            else
            {
                FillArmorButtonsArray(consoleCenter);
                FillMainButtonsArray(consoleCenter);
            }

            this.Buttons = new IButton[this.gearMenuButtons.Count];

            for (int i = 0; i < this.Buttons.Length; i++)
            {
                this.Buttons[i] = this.gearMenuButtons[i];
            }
        }

        private List<string[]> InitializeHandItemLabelContents()
        {
            List<string[]> labelContents = new List<string[]>();

            string[] handItemLabelContents = null;

            foreach (var itemViewModel in this.itemViewModels)
            {
                handItemLabelContents = new string[]
                {
                    $"{this.characterViewModel.Name } wears the following items: ",

                    $"\"{itemViewModel.Name}\"",
                    "Damage Bonus: " + itemViewModel.BonusValues[0].ToString(),
                    "Inventory Space Required: " + itemViewModel.InventoryLoad.ToString(),
                    "Sell Value: " + itemViewModel.SellValue.ToString(),
                };

                labelContents.Add(handItemLabelContents);
            }

            return labelContents;
        }

        private void FillLeftHandItemLabelsArray(Position consoleCenter)
        {
            List<string[]> labelContents = InitializeHandItemLabelContents();

            string[] leftHandLabelContents = labelContents[1];

            Position[] leftHandLabelPositions = new Position[]
            {
                    new Position(consoleCenter.Left - leftHandLabelContents[0].Length / 2, consoleCenter.Top - 16), //Character's gear
                                       
                    new Position(consoleCenter.Left - leftHandLabelContents[0].Length / 2 + 40, consoleCenter.Top - 12), //Name
                    new Position(consoleCenter.Left - leftHandLabelContents[0].Length / 2 + 40, consoleCenter.Top - 8), //Damage Bonus
                    new Position(consoleCenter.Left - leftHandLabelContents[0].Length / 2 + 40, consoleCenter.Top - 6), //Space Required
                    new Position(consoleCenter.Left - leftHandLabelContents[0].Length / 2 + 40, consoleCenter.Top - 4), //Sell Value
            };

            FillLabelsArray(leftHandLabelContents, leftHandLabelPositions);
        }

        private void FillArmorLabelsArray(Position consoleCenter)
        {
            string[] armorLabelContents = new string[]
            {
                $"{this.characterViewModel.Name }'s gear has the following items in his gear: ",

                $"\"{this.armorViewModel.Name}\"",
                "Defense Bonus: " + this.armorViewModel.BonusValues[0].ToString(),
                "Inventory Space Required: " + this.armorViewModel.InventoryLoad.ToString(),
                "Sell Value: " + this.armorViewModel.SellValue.ToString()
            };

            Position[] armorLabelPositions = new Position[]
            {
                new Position(consoleCenter.Left - armorLabelContents[0].Length / 2, consoleCenter.Top - 16), //Character's gear
                                                
                new Position(consoleCenter.Left - armorLabelContents[0].Length / 2, consoleCenter.Top - 12), //Name
                new Position(consoleCenter.Left - armorLabelContents[0].Length / 2, consoleCenter.Top - 8), //Defense Bonus
                new Position(consoleCenter.Left - armorLabelContents[0].Length / 2, consoleCenter.Top - 6), //Space Required
                new Position(consoleCenter.Left - armorLabelContents[0].Length / 2, consoleCenter.Top - 4), //Sell Value
            };

            FillLabelsArray(armorLabelContents, armorLabelPositions);
        }

        private void FillRightHandItemLabelsArray(Position consoleCenter)
        {
            List<string[]> labelContents = InitializeHandItemLabelContents();

            string[] rightHandLabelContents = labelContents[0];

            Position[] rightHandLabelPositions = new Position[]
            {
               new Position(consoleCenter.Left - rightHandLabelContents[0].Length / 2, consoleCenter.Top - 16), //Character's gear
               
               new Position(consoleCenter.Left - rightHandLabelContents[0].Length / 2 - 40, consoleCenter.Top - 12), //Name
               new Position(consoleCenter.Left - rightHandLabelContents[0].Length / 2 - 40, consoleCenter.Top - 8), //Damage Bonus
               new Position(consoleCenter.Left - rightHandLabelContents[0].Length / 2 - 40, consoleCenter.Top - 6), //Space Required
               new Position(consoleCenter.Left - rightHandLabelContents[0].Length / 2 - 40, consoleCenter.Top - 4), //Sell Value
            };

            FillLabelsArray(rightHandLabelContents, rightHandLabelPositions);
        }

        private void FillLabelsArray(string[] labelContents, Position[] labelPositions)
        {
            this.Labels = new ILabel[labelContents.Length];

            for (int i = 0; i < this.Labels.Length; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);

                this.gearMenuLabels.Add(this.Labels[i]);
            }
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            this.gearMenuLabels = new List<ILabel>();

            if (this.itemViewModels.Count == 0 && this.armorViewModel is null)
            {
                string labelContent = $"{this.characterViewModel.Name} doesn't wear any items at the moment.";

                Position labelPosition = new Position(consoleCenter.Left - labelContent.Length / 2, consoleCenter.Top - 8);

                this.Labels = new ILabel[] { new Label(labelContent, labelPosition) };
            }

            else if (this.itemViewModels.Count == 1 && this.armorViewModel is null)
            {
                FillRightHandItemLabelsArray(consoleCenter);
                FillLabelsArray(consoleCenter);
            }
            else if(this.itemViewModels.Count == 2 && this.armorViewModel is null)
            {
                FillRightHandItemLabelsArray(consoleCenter);
                FillLeftHandItemLabelsArray(consoleCenter);
                FillLabelsArray(consoleCenter);
            }
            else if (this.itemViewModels.Count == 1 && this.armorViewModel != null)
            {
                FillRightHandItemLabelsArray(consoleCenter);
                FillArmorLabelsArray(consoleCenter);
                FillLabelsArray(consoleCenter);
            }
            else if (this.itemViewModels.Count == 2 && this.armorViewModel != null)
            {
                FillRightHandItemLabelsArray(consoleCenter);
                FillArmorLabelsArray(consoleCenter); 
                FillLeftHandItemLabelsArray(consoleCenter);
                FillLabelsArray(consoleCenter);
            }
            else
            {
                FillArmorLabelsArray(consoleCenter);
                FillLabelsArray(consoleCenter);
            }
        }

        private void FillLabelsArray(Position consoleCenter)
        {
            this.Labels = new ILabel[this.gearMenuLabels.Count + 1];

            this.Labels[^1] = new Label(this.ErrorMessage, new Position(consoleCenter.Left - this.ErrorMessage?.Length / 2 ?? 0, consoleCenter.Top + 10), !this.error);

            for (int i = 0; i < this.Labels.Length - 1; i++)
            {
                this.Labels[i] = this.gearMenuLabels[i];
            }
        }

        private void LoadViewModels()
        {
            this.characterViewModel = this.characterService.GetCharacterViewModel(this.Id);
            List<IItemViewModel> viewModels = this.itemService.GetItemViewModelsForGear(this.Id);

            this.itemViewModels = new List<IItemViewModel>();

            foreach (var viewModel in viewModels)
            {
                if (viewModel != null)
                {
                    this.itemViewModels.Add(viewModel);
                }
            }

            foreach (var itemViewModel in this.itemViewModels)
            {
                if (itemViewModel.Name.Contains("Armor"))
                {
                    this.armorViewModel = itemViewModel;
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
            this.LoadViewModels();

            base.Open();
        }
    }
}
