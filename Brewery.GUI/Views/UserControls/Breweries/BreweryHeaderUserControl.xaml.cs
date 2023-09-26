using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Brewery.VM.ViewModels.Breweries;

namespace Brewery.GUI.Views.UserControls.Breweries;

public partial class BreweryHeaderUserControl : BreweryCommonUserControl
{
    public BreweryViewModel Vm { get; set; }
    public BreweryHeaderUserControl()
    {
        InitializeComponent();
    }

    public override void OnSetViewModelChanged(DependencyPropertyChangedEventArgs e)
    {
        Vm = e.NewValue as BreweryViewModel;
        DataContext = Vm;
    }

    private void SelectionComboBoxBreweries_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var obj = (MahApps.Metro.Controls.MultiSelectionComboBox)sender;

        ObservableCollection<object> selectedItems = (ObservableCollection<object> ) obj.SelectedItems;
  
        Vm.BrewerySearch = selectedItems?.Select(b => (CompanyCreateViewModel) b).ToList();
    }

    private void SelectionComboBoxWholesalers_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var obj = (MahApps.Metro.Controls.MultiSelectionComboBox)sender;
        ObservableCollection<object> selectedItems = (ObservableCollection<object> ) obj.SelectedItems;
  
        Vm.WholesalerSearch = selectedItems?.Select(b => (CompanyCreateViewModel) b).ToList();
    }
}