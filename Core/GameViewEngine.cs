using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDiablo.App.Core
{
    public class GameViewEngine : IGameViewEngine
    {
        private ConsoleColor backgroundColor;
        private ConsoleColor highlightColor;
        private ConsoleColor hardCodedBgColor = ConsoleColor.Red;
        private ConsoleColor hardCodedHlColor = ConsoleColor.DarkMagenta;
        private ConsoleColor hardCodedFontColor = ConsoleColor.White;

        public GameViewEngine()
        {
            this.InitializeConsole();
        }

        private ConsoleColor BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
            }
        }

        private ConsoleColor HiglightColor
        {
            get
            {
                return this.highlightColor;
            }
            set
            {
                ConsoleColor maxColor = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().Last();

                if (value > maxColor)
                {
                    value = (ConsoleColor)((int)value % (int)maxColor);
                }

                this.highlightColor = value;
            }
        }
        private void InitializeConsole()
        {
            this.BackgroundColor = hardCodedBgColor;
            this.HiglightColor = hardCodedHlColor;

            Console.BackgroundColor = this.BackgroundColor;
            Console.ForegroundColor = hardCodedFontColor;

            Console.CursorVisible = false;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            this.Clear();
        }

        private void Clear()
        {
            Console.Clear();
            Console.CursorVisible = false;
        }

        public void Mark(ILabel label, bool highlighted = true)
        {
            SetCursorPosition(label.Position.Left, label.Position.Top);

            if (highlighted)
            {
                Console.BackgroundColor = this.HiglightColor;
            }

            Console.Write(label.Text);

            Console.BackgroundColor = this.BackgroundColor;
        }

        private void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void RenderMenu(IMenu menu)
        {
            this.Clear();

            if (menu.Labels != null)
            {
                foreach (var label in menu.Labels.Where(l => !l.IsHidden))
                {
                    this.DisplayLabel(label);
                }
            }

            foreach (var button in menu.Buttons.Where(b=>!b.IsHidden))
            {
                this.DisplayLabel(button);
            }

            if (menu is ICommandAreaMenu commandAreaMenu)
            {
                commandAreaMenu.CommandArea.Render();
            }
        }

        private void DisplayLabel(ILabel label)
        {
            SetCursorPosition(label.Position.Left, label.Position.Top);
            Console.WriteLine(label.Text);
        }

        public void ResetBuffer()
        {
            this.Clear();
            Console.BufferHeight = 30;
        }

        public void SetBufferHeight(int rows)
        {
            Console.BufferHeight = rows;
        }
    }
}
