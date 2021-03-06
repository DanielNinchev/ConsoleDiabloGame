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
    public class ContinueMenu : Menu, IIdHoldingMenu
    {
        private ILabelFactory labelFactory;
        private ICharacterService characterService;
        private IItemService itemService;
        private IMonsterService monsterService;
        private ICommandFactory commandFactory;
        private IItemViewModel itemViewModel;

        private bool itemHasBeenPicked = false;

        private int itemId = 0;
        private int monsterId = 0;

        public ContinueMenu(ILabelFactory labelFactory, ICharacterService characterService, IMonsterService monsterService,
            IItemService itemService, ICommandFactory commandFactory)
        {
            this.labelFactory = labelFactory;
            this.characterService = characterService;
            this.itemService = itemService;
            this.monsterService = monsterService;
            this.commandFactory = commandFactory;
            this.BackMenu = "CreateCharacterMenu";
        }

        public int Id { get; set; } //characterId

        public string Error { get; set; }

        public override IMenu ExecuteCommand()
        {
            try
            {
                string commandName = string.Join("", this.CurrentOption.Text.Split()) + "Menu";

                IMenuCommand command = this.commandFactory.CreateCommand(commandName);

                IMenu menu = command.Execute(this.Id.ToString(), this.itemId.ToString(), this.monsterId.ToString());

                return menu;
            }
            catch (Exception e)
            {
                this.Error = e.Message;
                this.Open();
                return this;             
            }
        }

        protected override void InitializeButtons(Position consoleCenter)
        {
            string[] buttonContents = new string[]
            {
                "Pick Item", "Back To Menu"
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - buttonContents[0].Length / 2, consoleCenter.Top - 9),    //Pick Item
                new Position(consoleCenter.Left - buttonContents[1].Length / 2, consoleCenter.Top),    //Back to menu
            };

            this.Buttons = new IButton[buttonContents.Length];
            this.Buttons[0] = new Button(buttonContents[0], buttonPositions[0], this.itemHasBeenPicked);

            for (int i = 1; i < this.Buttons.Length; i++)
            {
                string buttonContent = buttonContents[i];
                bool isField = string.IsNullOrWhiteSpace(buttonContent);
                this.Buttons[i] = this.labelFactory.CreateButton(buttonContent, buttonPositions[i], false, isField);
            }
        }

        protected override void InitializeStaticLabels(Position consoleCenter)
        {
            var monster = this.monsterService.GetMonsterById(this.monsterId);
            var inventory = this.characterService.GetCharactersInventoryByHisId(this.Id);
            var character = this.characterService.GetCharacterById(this.Id);
            
            string[] labelContents = new string[]
            {
                 $"{this.itemViewModel?.Name}",
                 $"The {monster.Name} dropped the following items: ",                
                 $"Current space in {character.Name}'s inventory: {inventory.Load}/{inventory.Capacity}",                
            };

            Position[] labelPositions = new Position[]
            {
               new Position(consoleCenter.Left - labelContents[0].Length / 2, consoleCenter.Top - 10),
               new Position(consoleCenter.Left - labelContents[1].Length / 2, consoleCenter.Top - 16),
               new Position(consoleCenter.Left - labelContents[2].Length / 2, consoleCenter.Top - 6),              
            };

            this.Labels = new ILabel[labelContents.Length];

            this.Labels[0] = new Label(labelContents[0], labelPositions[0], this.itemHasBeenPicked);

            for (int i = 1; i < this.Labels.Length; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
            }
        }

        private void LoadViewModels()
        {
            var item = this.itemService.GetItemById(this.itemId);

            if (item.CharacterId == 0)
            {
                this.itemHasBeenPicked = false;
                this.itemViewModel = this.itemService.InitializeItemBonuses(this.itemId);
            }
            else
            {
                this.itemHasBeenPicked = true;
            }          
        }

        public void SetId(params int[] ids)
        {

            this.Id = ids[0];
            this.itemId = ids[1];

            if (ids.Length > 2)
            {
                this.monsterId = ids[2];
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
