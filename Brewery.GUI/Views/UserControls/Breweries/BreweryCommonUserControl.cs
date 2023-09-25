using System.Windows;
using Brewery.VM.ViewModels.Breweries;
using Elia.Share.WPF.Controls;

namespace Brewery.GUI.Views.UserControls.Breweries;

public abstract class BreweryCommonUserControl: UserControlBase
{

    #region ViewModel dependancy

     // Declare a read-write property wrapper.
        public BreweryViewModel ViewModel
        {
            get => (BreweryViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    
    
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(BreweryViewModel), typeof(BreweryCommonUserControl), new
                PropertyMetadata(null, new PropertyChangedCallback(OnViewModelChanged)));
    
        private static void OnViewModelChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            BreweryCommonUserControl? control =  d as BreweryCommonUserControl;
            control?.OnSetViewModelChanged(e);
        }


        public abstract void OnSetViewModelChanged(DependencyPropertyChangedEventArgs e);
        /* {
              Vm = e.NewValue as ManagerAccountViewModel;
         }*/

        #endregion

}