
using System;
using System.IO;
using System.Windows;
using Brewery.Contract.Contracts.Responses.Users;
using Brewery.GUI.Helpers;
using Brewery.GUI.Views.Containers;
using Brewery.VM.Enums;
using Brewery.VM.ViewModels;
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
    public partial class App : ApplicationBase<CreateUserResponse>
    {

        #region Properties
        /// <summary>
        /// This properties is container services
        /// </summary>
        public  IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// This properties represent the configuration
        /// </summary>
        public IConfiguration Configuration { get; private set; } 
        /* install
                Microsoft.Extensions.Configuration.Binder
                 Microsoft.Extensions.Configuration.Json
                Microsoft.Extensions.DependencyInjection
                Microsoft.Extensions.Options.ConfigurationExtensions
         */

        #endregion
        
        #region Protected Methods
        

        protected override void OnStartup(StartupEventArgs e)
        {
            var env = Environments.Current.ToString();
            
            try
            {
                var appsettings = $"Appsettings/appsettings.{env}.json";

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("Appsettings/appsettings.json", false, true)
                    .AddJsonFile(appsettings, true, true);

                Configuration = builder.Build();

                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                ServiceProvider = serviceCollection.BuildServiceProvider();

                ViewModelCommon.ServiceProvider = ServiceProvider;

                SwitchLanguage();
                ListernerChange();

                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
           
        }

        #endregion


        #region Privates methods

        private void ConfigureServices(IServiceCollection services)
        {
           // var config = Configuration.GetSection("Server");
            services.Configure<AppSettings.Server>(Configuration.GetSection(nameof(AppSettings.Server)));

            services.AutoInject(SolutionAssembly.GetAllAssemblies);
        }
        #endregion


        #region Listener change

       

        public void ListernerChange()
        {
            // Siwtchlanguage
            Register(this, MessageEnum.MsgSwitchLang, (string lang) =>
            {
                SwitchLanguage(lang);
            });
            
            // After login or register, first, we set user
            Register(this, MessageEnum.MsgDisplayBrewery, (CreateUserResponse user) =>
            {
                if (user != null)
                {
                    CurrentUser = user;
                    NavigateTo<BreweryWindow>();
                }
            }); 
            
            // After click to button open beer
            Register(this, MessageEnum.MsgOpenCreateBeer, () =>
            {
                ShowDialog<BreweryCreateWindow>();
            });
            
            // Switch to home page
            Register(this, MessageEnum.MsgNavigationPage, (PageEnum page) =>
            {
                switch (page)
                {
                    case PageEnum.BreweryPage:
                        NavigateTo<BreweryWindow>();
                        break;
                    case PageEnum.HomePage:
                    case PageEnum.LoginPage:
                    case PageEnum.RegisterPage:
                        NavigateTo<MainWindow>();
                        break;
                }
              
            });
            
        }
        
        
        private void SwitchLanguage(string? lang = null)
        {
            ApplicationRoot.CurrentLang = lang ?? "fr";
            ResourceDictionary dictionary = new ResourceDictionary();
            lang = string.IsNullOrEmpty(lang) || lang == "fr" ? "" : $"{lang}.";

            try
            {
                dictionary.Source = new Uri($"..\\Resources\\Home\\HomeTranslationResource.{lang}xaml", UriKind.Relative);
                Resources.MergedDictionaries.Add(dictionary);
            }
            catch (Exception)
            {
               // Resource not found
            } 
          
        }

        #endregion
    }
}