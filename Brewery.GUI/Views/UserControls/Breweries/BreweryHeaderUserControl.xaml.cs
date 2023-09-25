using System.Windows;
using Brewery.VM.ViewModels.Breweries;

namespace Brewery.GUI.Views.UserControls.Breweries;

public partial class BreweryHeaderUserControl : BreweryCommonUserControl
{
    public BreweryHeaderUserControl()
    {
        InitializeComponent();
    }

    public override void OnSetViewModelChanged(DependencyPropertyChangedEventArgs e)
    {
        var  vm = e.NewValue as BreweryViewModel;
        DataContext = vm;
    }
}