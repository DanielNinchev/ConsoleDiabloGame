using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Entities.Models
{
    public class GameConsoleReader : IGameReader
    {
        private bool CursorVisible { get => Console.CursorVisible; set => Console.CursorVisible = value; }
        public void HideCursor()
        {
            CursorVisible = false;
        }

        public string ReadLine()
        {
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            return this.ReadLine(cursorLeft, cursorTop);
        }

        public string ReadLine(int cursorLeft, int cursorTop)
        {
            ClearRow(cursorLeft, cursorTop);

            ShowCursor();

            string result = Console.ReadLine();

            HideCursor();

            return result;
        }

        private void ClearRow(int cursorLeft, int cursorTop)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new string(' ', Console.LargestWindowWidth - cursorLeft));

            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        public void ShowCursor()
        {
            CursorVisible = true;
        }
    }
}
