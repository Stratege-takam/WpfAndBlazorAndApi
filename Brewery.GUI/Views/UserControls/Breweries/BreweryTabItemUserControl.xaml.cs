using System.Windows.Controls;
using Brewery.VM.ViewModels.Breweries;
using Elia.Core.Attributes;
using Elia.Share.WPF.Controls;

namespace Brewery.GUI.Views.UserControls.Breweries;

[Injectable]
public partial class BreweryTabItemUserControl : UserControlBase
{
    public BreweryDetailViewModel ViewModel { get; set; }
    public BreweryTabItemUserControl()
    {
        InitializeComponent();
    }

    public void SetViewModel(BreweryDetailViewModel viewModel)
    {
        ViewModel = viewModel ?? new BreweryDetailViewModel();
        DataContext = ViewModel;

    }
}