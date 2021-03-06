using ConsoleDiablo.App.Entities.Models;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Menus
{
    public abstract class Menu : IMenu
    {
        protected int currentIndex;
        public static Stack<string> chosenOptions = new Stack<string>();

        public Menu()
        {
            this.currentIndex = 0;
        }

        public IButton CurrentOption => this.Buttons[currentIndex];

        public ILabel[] Labels { get; protected set; }

        public IButton[] Buttons { get; protected set; }

        public string BackMenu { get; protected set; }

        public abstract IMenu ExecuteCommand();

        public void NextOption()
        {
            this.currentIndex++;

            int totalOptions = this.Buttons.Length;

            if (this.currentIndex >= totalOptions)
            {
                this.currentIndex -= totalOptions;
            }

            if (this.CurrentOption.IsHidden)
            {
                this.NextOption();
            }
        }

        public virtual void Open()
        {
            Position consoleCenter = Position.ConsoleCenter();

            this.InitializeStaticLabels(consoleCenter);

            this.InitializeButtons(consoleCenter);
        }

        protected abstract void InitializeButtons(Position consoleCenter);

        protected abstract void InitializeStaticLabels(Position consoleCenter);

        public void PreviousOption()
        {
            this.currentIndex--;

            if (this.currentIndex < 0)
            {
                this.currentIndex += this.Buttons.Length;
            }

            if (this.CurrentOption.IsHidden)
            {
                this.PreviousOption();
            }
        }
    }
}
