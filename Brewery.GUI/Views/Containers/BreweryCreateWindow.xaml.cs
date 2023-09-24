using System.Text.RegularExpressions;
using System.Windows.Input;
using Brewery.VM.ViewModels.Breweries;
using Elia.Core.Attributes;
using Elia.Share.WPF.Controls;

namespace Brewery.GUI.Views.Containers;

[Injectable]
public partial class BreweryCreateWindow : DialogWindowBase
{
    public BreweryCreateWindow()
    {
        InitializeComponent();
        Vm = new BeerCreateViewModel();
    }
    
    
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
}