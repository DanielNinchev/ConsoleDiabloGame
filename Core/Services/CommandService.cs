using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleDiablo.App.Core.Services
{
    public class CommandService :ICommandService
    {
        private ICharacterService characterService;
        private Type[] commandTypes;

        public string ProcessCommand(string commandName, IList<string> commandArgs)
        {
            Type command = this.commandTypes.FirstOrDefault(ct => ct.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

            if (command == null)
            {
                throw new ArgumentException($"Command {commandName} does not exist!");
            }

            ConstructorInfo ctor = command.GetConstructor(new Type[] { commandArgs.GetType(), this.characterService.GetType() });

            ITextCommand commandInstance = (ITextCommand)ctor.Invoke(new object[] { commandArgs, this.characterService });

            return commandInstance.Execute();
        }
    }
}
