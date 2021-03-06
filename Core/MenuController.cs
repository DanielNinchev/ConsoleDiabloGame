using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Menus;
using System;
using Microsoft.Extensions.DependencyInjection;
using ConsoleDiablo.App.Entities.Contracts.Models;

namespace ConsoleDiablo.App.Core
{
    internal class MenuController : IMainController
    {
        private IServiceProvider serviceProvider;
        private IGameViewEngine viewEngine;
        private ISession session;

        public MenuController(IServiceProvider serviceProvider, IGameViewEngine viewEngine, ISession session)
        {
            this.serviceProvider = serviceProvider;
            this.viewEngine = viewEngine;
            this.session = session;

            this.InitializeSession();
        }

        private IMenu CurrentMenu { get => this.session.CurrentMenu; }

        private void InitializeSession()
        {
            var menu = new MainMenu(this.session, this.serviceProvider.GetService<ILabelFactory>(),
                this.serviceProvider.GetService<ICommandFactory>());

            this.session.PushView(menu);

            this.RenderCurrentView(menu);
        }

        private void RenderCurrentView(IMenu menu)
        {
            this.viewEngine.RenderMenu(menu);
        }

        public void Back()
        {
            var menu = this.session.Back();
            RenderCurrentView(menu);
        }

        public void Execute()
        {
            this.session.PushView(this.CurrentMenu.ExecuteCommand());
            this.RenderCurrentView(this.CurrentMenu);
        }

        public void MarkOption()
        {
            this.viewEngine.Mark(this.CurrentMenu.CurrentOption);
        }

        public void NextOption()
        {
            this.CurrentMenu.NextOption();
        }

        public void PreviousOption()
        {
            this.CurrentMenu.PreviousOption();
        }

        public void UnmarkOption()
        {
            this.viewEngine.Mark(this.CurrentMenu.CurrentOption, false);
        }
    }
}
