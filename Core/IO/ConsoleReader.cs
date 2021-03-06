using ConsoleDiablo.App.Core.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDiablo.App.Core.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine().Trim();
        }
    }
}
