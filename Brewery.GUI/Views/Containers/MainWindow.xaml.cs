using Elia.Core.Attributes;
using Elia.Core.Utils;
using Elia.Share.WPF.Controls;
using Microsoft.Extensions.Options;

namespace Brewery.GUI.Views.Containers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    [Injectable]
    public partial class MainWindow : WindowBase
    {
        // check if right load appsetting 
        public MainWindow(IOptions<AppSettings.Server> appSettingsClientSection)
        {
            var server = appSettingsClientSection.Value;
            InitializeComponent();
        }


   
    }
}