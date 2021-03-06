using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleDiablo.App.Entities.Factories
{
    public class CommandAreaFactory : ICommandAreaFactory
    {
        public ICommandInputArea CreateCommandArea(IGameReader reader, int x, int y, bool isCharacter = true)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type commandType = assembly.GetTypes().FirstOrDefault(t => typeof(ICommandInputArea).IsAssignableFrom(t));

            if (commandType == null)
            {
                throw new InvalidOperationException("CommandArea not found!");
            }

            object[] args = new object[] { reader, x, y, isCharacter };

            ICommandInputArea commandInstance = (ICommandInputArea)Activator.CreateInstance(commandType, args);

            return commandInstance;
        }
    }
}
