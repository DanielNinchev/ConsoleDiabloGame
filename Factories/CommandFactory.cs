using ConsoleDiablo.App.Entities.Contracts;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleDiablo.App.Entities.Factories
{
    public class CommandFactory : ICommandFactory
    {
        //IServiceProvider is the interface behind ServiceProvider, which is the built-in implementation of the Service Locator Pattern.
        private IServiceProvider serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Finds the command with the given name from the executing assembly, performs validations,
        /// gets the parameters needed for the command, creates an instance of it and returns it.
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns>The instance of the command.</returns>
		public IMenuCommand CreateCommand(string commandName)
        {
            //TODO - MAKE IT MORE ABSTRACT!
            switch (commandName)
            {
                case "AmazonMenu":
                case "AssassinMenu":
                case "BarbarianMenu":
                case "DruidMenu":
                case "NecromancerMenu":
                case "PaladinMenu":
                case "SorceressMenu":
                    commandName = "SelectCharacterTypeMenu";
                    break;
            }

            //Finding the command with the given name from the executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type commandType = assembly.GetTypes().FirstOrDefault(t => t.Name == commandName + "Command");

            //Performing validations
            if (commandType == null)
            {
                throw new InvalidOperationException("Command not found!");
            }

            if (!typeof(IMenuCommand).IsAssignableFrom(commandType))
            {
                throw new InvalidFilterCriteriaException($"{commandType} is not а command!");
            }

            //Gets the parameters that the constructor of the command needs
            ParameterInfo[] parameters = commandType.GetConstructors().First().GetParameters();

            //Creating an array of objects and taking them from the service provider
            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                args[i] = this.serviceProvider.GetService(parameters[i].ParameterType);
            }

            //Creating an instance of that type with the given argument and returning it
            IMenuCommand command = (IMenuCommand)Activator.CreateInstance(commandType, args);

            return command;
        }
    }
}
