using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Brewery.BL.Client.Contracts.Outputs.Beers;
using Brewery.VM.Enums;
using Elia.Share.WPF.Helpers;

namespace Brewery.VM.ViewModels.Breweries;

public class BreweryViewModel: ViewModelCommon
{
    #region Properties Notify

    #region For tabControl

    private Guid _currentBrewery;
    public Guid CurrentBrewery
    {
        get => _currentBrewery;
        set => SetProperty(ref _currentBrewery, value); 
    }

    #endregion End for tabControl
    

    #region For footer

    private int _take;
    public int Take
    {
        get => _take;
        set => SetProperty(ref _take, value); 
    }
    
    private ObservableCollection<int> _takes;
    public ObservableCollection<int> Takes
    {
        get => _takes;
        set => SetProperty(ref _takes, value); 
    }
    
    private int _skip;
    public int Skip
    {
        get => _skip;
        set => SetProperty(ref _skip, value); 
    }
    
    private ObservableCollection<int> _skips;
    public ObservableCollection<int> Skips
    {
        get => _skips;
        set => SetProperty(ref _skips, value); 
    }

    #endregion End for footer

    #region For header
    private string _brewerySearch;
    public string BrewerySearch
    {
        get => _brewerySearch;
        set => SetProperty(ref _brewerySearch, value); 
    }

    private string _wholesalerSearch;
   
    public string WholesalerSearch
    {
        get => _wholesalerSearch;
        set => SetProperty(ref _wholesalerSearch, value); 
    }

    
    
    
    
    private ObservableCollection<BreweryCreateBeerOutput> _breweries;
    public ObservableCollection<BreweryCreateBeerOutput> Breweries
    {
        get => _breweries;
        set => SetProperty(ref _breweries, value); 
    }

    private ObservableCollection<BreweryCreateBeerOutput> _wholesalers;
   
    public ObservableCollection<BreweryCreateBeerOutput> Wholesalers
    {
        get => _wholesalers;
        set => SetProperty(ref _wholesalers, value); 
    }
    

    #endregion For header


    #region For treview

    private ObservableCollection<BreweryDetailViewModel> _treeBreweries;
    public ObservableCollection<BreweryDetailViewModel> TreeBreweries
    {
        get => _treeBreweries;
        set => SetProperty(ref _treeBreweries, value); 
    }
    #endregion

    #endregion End for treview
    

    #region Commands

    public  ICommand OnSearchCommand { get; set; }

    public  ICommand OnOpenCreateBeerCommand { get; set; }
    #endregion

    #region Constructor

    public BreweryViewModel()
    {
        OnSearchCommand = new RelayCommand(() =>
        {
            
        });
        
        OnOpenCreateBeerCommand = new RelayCommand( () =>
        {
            NotifyColleagues(MessageEnum.MsgOpenCreateBeer);
        });
    }


    #endregion

    
}