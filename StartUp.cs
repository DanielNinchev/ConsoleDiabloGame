using ConsoleDiablo.App.Core;
using ConsoleDiablo.App.Core.IO;
using ConsoleDiablo.App.Core.IO.Contracts;
using ConsoleDiablo.App.Core.Services;
using ConsoleDiablo.App.Entities.Contracts.Factories;
using ConsoleDiablo.App.Entities.Contracts.Models;
using ConsoleDiablo.App.Entities.Contracts.Services;
using ConsoleDiablo.App.Entities.Contracts.ViewModels;
using ConsoleDiablo.App.Entities.Factories;
using ConsoleDiablo.App.Entities.Items.Factory;
using ConsoleDiablo.App.Entities.Models;
using ConsoleDiablo.Data;
using ConsoleDiablo.DataModels;
using ConsoleDiablo.DataModels.Contracts;
using ConsoleDiablo.DataModels.Contracts.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleDiablo.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //var engine = new Engine(reader, writer);

            //engine.Run();

            IServiceProvider serviceProvider = ConfigureServices();
            IMainController menu = serviceProvider.GetService<IMainController>();

            Engine engine = new Engine(menu);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ICommandAreaFactory, CommandAreaFactory>();
            services.AddSingleton<ILabelFactory, LabelFactory>();
            services.AddSingleton<IMenuFactory, MenuFactory>();
            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<ICharacterFactory, CharacterFactory>();
            services.AddSingleton<IItemFactory, ItemFactory>();

            services.AddSingleton<ConsoleDiabloData>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IMonsterService, MonsterService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddSingleton<ISession, Session>();
            services.AddSingleton<IGameViewEngine, GameViewEngine>();
            services.AddSingleton<IMainController, MenuController>();

            services.AddTransient<IGameReader, GameConsoleReader>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
