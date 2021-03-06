using ConsoleDiablo.DataModels;
using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleDiablo.App.Entities.Contracts.Menus;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using System.Reflection;

namespace ConsoleDiablo.App.Entities.Models
{
    public class Session : ISession
    {
        private Account account;
        private Stack<IMenu> history;
        private IMenu currentMenu;
        private IServiceProvider serviceProvider;

        public Session(IServiceProvider serviceProvider)
        {
            history = new Stack<IMenu>();
            this.serviceProvider = serviceProvider;
        }

        public string AccountName => this.account?.AccountName;

        public int AccountId => this.account?.Id ?? 0;

        public bool IsLoggedIn => this.account != null;

        public IMenu CurrentMenu
        {
            get 
            {
                if (this.history.Count > 1)
                {
                    return currentMenu;
                }

                return this.history.Peek();
            }
            set { currentMenu = value; }
        }

        public IMenu Back()
        {
            IMenu menu = this.history.Peek();

            if (menu is IReturningMenu)
            {
                if (history.Count > 1)
                {
                    this.history.Pop();
                }

                menu = this.history.Peek();

                this.currentMenu = menu;

                menu.Open();

                return menu;
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type menuType = assembly.GetTypes().FirstOrDefault(t => t.Name == menu.BackMenu);

            //Get the parameters that the constructor of the menu needs
            ParameterInfo[] parameters = menuType.GetConstructors().First().GetParameters();

            //Create an array of objects that are going to be taken from the service provider.
            object[] args = new object[parameters.Length];

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = this.serviceProvider.GetService(parameters[i].ParameterType);
            }

            //Creating an instance of the type with the given argument and returning it
            menu = (IMenu)Activator.CreateInstance(menuType, args);

            this.CurrentMenu = menu;

            menu.Open();

            return menu;
        }

        public void LogIn(Account account)
        {
            this.account = account;
        }

        public void LogOut()
        {
            this.account = null;
        }

        public bool PushView(IMenu view)
        {
            if (!this.history.Any() || this.history.Peek() != view)
            {
                this.CurrentMenu = view;
                
                this.history.Push(view);

                return true;
            }

            return false;
        }

        public void Reset()
        {
            this.account = null;
            this.history = new Stack<IMenu>();
        }
    }
}
