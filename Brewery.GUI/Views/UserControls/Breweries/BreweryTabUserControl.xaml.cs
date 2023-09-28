using System;
using System.Windows;
using Brewery.VM.Enums;
using Brewery.VM.ViewModels.Breweries;
using Elia.Share.WPF.BaseClasses;
using Elia.Share.WPF.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.GUI.Views.UserControls.Breweries;

public partial class BreweryTabUserControl : BreweryCommonUserControl
{
    
    public BreweryTabUserControl()
    {
        InitializeComponent();
        Register<BeerCreateViewModelBase>(MessageEnum.MsgOpenDetailBeer, DoDisplayDetails);
    }
    
   


    public override void OnSetViewModelChanged(DependencyPropertyChangedEventArgs e)
    {
        var  vm = e.NewValue as BreweryViewModel;
        DataContext = vm;
    }
    
    
    private void DoDisplayDetails(BeerCreateViewModelBase beer)
    {
        if (beer != null)
        {
            var userControl = (BreweryTabItemUserControl)ViewModelBase.ServiceProvider.GetRequiredService<BreweryTabItemUserControl>();
            var viewModel= new BreweryDetailViewModel()
            {
                Id = beer.Owner.Id,
                Name = beer.Owner.Name,
                NameOfBeer = beer.NameOfBeer,
                DegreeOfBeer = beer.DegreeOfBeer,
                PriceOfBeer = beer.PriceOfBeer,
                IdOfBeer =  beer.IdOfBeer,
                DescriptionOfBeer = beer.DescriptionOfBeer
            };

            userControl.SetViewModel(viewModel);
            OpenTab( viewModel.NameOfBeer ,  viewModel.IdOfBeer.ToString(), () => userControl);
        }
    }
    private void OpenTab(string header, string tag, Func<UserControlBase> createView)
    {
        var tab = tabControl.FindByTag(tag);
        if (tab == null)
            tabControl.Add(createView.Invoke(), header, tag);
        else
            tabControl.SetFocus(tab);
    }
    
    private void DoCloseDetailTab(string name)
    {
        tabControl.CloseByTag(name);
    }
}