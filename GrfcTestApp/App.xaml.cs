using GrfcTestApp.Data;
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
        private static IHost _hosting;

        public static IHost Hosting => _hosting ??
            Host.
            CreateDefaultBuilder(Environment.GetCommandLineArgs()).
            ConfigureServices(ConfigureServices).
            Build();

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var db =new AppDBContext())
            {
                AppDBContextInitializer.Initialize(db,false);
            }
            Hosting.Start();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            await Hosting.StopAsync().ConfigureAwait(false);
            Hosting.Dispose()
        }

    }
}
