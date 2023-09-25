using Brewery.VM.ViewModels.Breweries;
using Elia.Core.Attributes;
using Elia.Share.WPF.Controls;

namespace Brewery.GUI.Views.Containers;

[Injectable]
public partial class BreweryWindow : WindowBase
{
    public BreweryViewModel BreweryViewModel { get; set; }
    public BreweryWindow()
    {
        InitializeComponent();

        BreweryViewModel = new BreweryViewModel();
        DataContext = BreweryViewModel;
        
        BreweryFooterUserControl.ViewModel = BreweryViewModel;
        BreweryHeaderUserControl.ViewModel = BreweryViewModel;
        BreweryListUserControl.ViewModel = BreweryViewModel;
        BreweryTabUserControl.ViewModel = BreweryViewModel;
    }
}