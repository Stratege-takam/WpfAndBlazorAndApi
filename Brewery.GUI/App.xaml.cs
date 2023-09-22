
using System;
using System.IO;
using System.Windows;
using Brewery.BL.Contracts.Responses.Users;
using Brewery.GUI.Helpers;
using Brewery.GUI.Views;
using Elia.Core.Containers;
using Elia.Core.Utils;
using Elia.Share.WPF.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase<CreateUserOutput>
    {

        #region Properties
        public static string CurrentLang { get;  set; }
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

            SwitchLanguage();
            ListernerChange();

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


        #region Listener change

        public enum Messages
        {
            MSG_LOGIN,
            MSG_SIGN_UP_ACTION,
            MSG_SIGN_UP,
            MSG_LOGOUT,
            MSG_SWITCH_LANG,
            MSG_DISPLAY_ACCOUNT_DETAIL,
        }


        public void ListernerChange()
        {
            Register(this, Messages.MSG_SWITCH_LANG, (string lang) =>
            {
                SwitchLanguage(lang);
            });
        }
        
        
        private void SwitchLanguage(string lang = null)
        {
            ApplicationRoot.CurrentLang = lang;
            ResourceDictionary dictionary = new ResourceDictionary();
            lang = string.IsNullOrEmpty(lang) || lang == "fr" ? "" : $"{lang}.";
            dictionary.Source = new Uri($"..\\Resources\\Home\\HomeTranslationResource.{lang}xaml", UriKind.Relative);
            
            Resources.MergedDictionaries.Add(dictionary);
        }

        #endregion
    }
}