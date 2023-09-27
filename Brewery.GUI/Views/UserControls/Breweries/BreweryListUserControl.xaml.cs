using System.Windows;
using Brewery.VM.ViewModels.Breweries;

namespace Brewery.GUI.Views.UserControls.Breweries;

public partial class BreweryListUserControl : BreweryCommonUserControl
{
    public BreweryViewModel Vm { get; set; }
    public BreweryListUserControl()
    {
        InitializeComponent();
    }

    public override void OnSetViewModelChanged(DependencyPropertyChangedEventArgs e)
    {
        Vm = e.NewValue as BreweryViewModel;
        DataContext = Vm;
    }

    private void MainTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        var item = e.NewValue as BreweryViewModel.KeyValue;

        if (item.IsBeer)
        {
            Vm.CurrentBeer = item.Beer;
        }

    }
}