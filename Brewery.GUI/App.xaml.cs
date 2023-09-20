
using System;
using System.IO;
using System.Windows;
using Brewery.GUI.Helpers;
using Brewery.GUI.Views;
using Elia.Core.Containers;
using Elia.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Properties

        /// <summary>
        /// This properties is container services
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// This properties represent the configuration
        /// </summary>
        public IConfiguration Configuration { get; private set; }

        #endregion

        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            var env = Environments.Current.ToString();
            var appsettings = $"Appsettings/appsettings.{env}.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Appsettings/appsettings.json", true, true)
                .AddJsonFile(appsettings, true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        #endregion


        #region Privates methods

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings.Server>(Configuration.GetSection(nameof(AppSettings.Server)));

            services.AutoInject(SolutionAssembly.GetAllAssemblies);
        }
        #endregion
    }
}