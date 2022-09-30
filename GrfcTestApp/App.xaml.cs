using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Engines;
using GrfcTestApp.Services.Base;
using GrfcTestApp.Services.Interfaces;
using GrfcTestApp.Services.RepositoryInDB;
using GrfcTestApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GrfcTestApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting;
            
        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddTransient<AppDBContext>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<IRepository<Automobile>, AutomobilesRepositoryInDb>();
            services.AddTransient<IRepository<CarMark>, CarMarksRepositoryInDb>();
            services.AddTransient<IRepository<EngineBase>, EnginesRepositoryInDb>();
            services.AddTransient<IRepository<Maintenance>, MaintenancesRepositoryInDb>();
            services.AddTransient<IRepository<CarModel>, CarModelsRepositoryInDb>();
            services.AddTransient<IRepository<Operation>, OperationsRepositoryInDb>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _Hosting=Host.
                CreateDefaultBuilder(Environment.GetCommandLineArgs()).
                ConfigureServices(ConfigureServices).
                Build();
            base.OnStartup(e);
            using (var db = new AppDBContext())
            {
                AppDBContextInitializer.Initialize(db, true);
            }

            Hosting.Start();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            await Hosting.StopAsync().ConfigureAwait(false);
            Hosting.Dispose();
        }

    }
}
