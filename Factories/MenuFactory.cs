using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Menus;
using ConsoleDiablo.DataModels.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleDiablo.App.Entities.Factories
{
    public class MenuFactory : IMenuFactory
    {
        private IServiceProvider serviceProvider;
        private ISession session;

        public MenuFactory(IServiceProvider serviceProvider, ISession session)
        {
            this.serviceProvider = serviceProvider;
            this.session = session;
        }
        public IMenu CreateMenu(string menuName)
        {
            //If the chosen option is "Log Out", return to the main menu
            if (menuName == "LogOutMenu")
            {
                menuName = "MainMenu";

                this.session.LogOut();
            }

            //Finding a type with the given menu plus "Menu" suffix within the executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type menuType = assembly.GetTypes().FirstOrDefault(t => t.Name == menuName);

            if (menuType == null)
            {
                throw new InvalidOperationException("Menu not found!");
            }

            //Checking if the type inherits IMenu
            if (!typeof(IMenu).IsAssignableFrom(menuType))
            {
                throw new InvalidFilterCriteriaException($"{menuType} is not a menu!");
            }

            //Get the parameters that the constructor of the menu needs
            ParameterInfo[] parameters = menuType.GetConstructors().First().GetParameters();

            //Create an array of objects that are going to be taken from the service provider.
            object[] args = new object[parameters.Length];

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = this.serviceProvider.GetService(parameters[i].ParameterType);
            }

            //Creating an instance of the type with the given argument and returning it
            IMenu menu = (IMenu)Activator.CreateInstance(menuType, args);

            return menu;
        }
    }
}
