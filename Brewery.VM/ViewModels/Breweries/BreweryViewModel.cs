using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Brewery.BL.Client.Business.Beers;
using Brewery.BL.Client.Business.Breweries;
using Brewery.BL.Client.Business.Wholesalers;
using Brewery.BL.Contracts.Requests.Beers;
using Brewery.VM.Enums;
using Elia.Core.Utils;
using Elia.Share.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.VM.ViewModels.Breweries;

public class BreweryViewModel: ViewModelCommon
{
    
    #region Private properties

    private BeerService _beerBl;
    private BreweryService _breweryBl;
    private WholesalerService _wholesalerBl;

    #endregion
    
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

    private int _take = 20;
    public int Take
    {
        get => _take;
        set
        {
            SetProperty(ref _take, value); 
            
            InitMethod(async () =>
            {
                await FillTreview();
            });
        }
    }
    
    private ObservableCollection<int> _takes;
    public ObservableCollection<int> Takes
    {
        get => _takes;
        set => SetProperty(ref _takes, value); 
    }
    
    private int _page = 1;
    public int Page
    {
        get => _page;
        set
        {
            SetProperty(ref _page, value); 
            
            InitMethod(async () =>
            {
                await FillTreview();
            });
        }
    }
    
    private ObservableCollection<int> _pages;
    public ObservableCollection<int> Pages
    {
        get => _pages;
        set => SetProperty(ref _pages, value); 
    }
    
    private bool _isExpanded = true;
    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetProperty(ref _isExpanded, value); 
    }

    private int _isChecked = 1;
    public int IsChecked
    {
        get => _isChecked;
        set
        {
            IsExpanded = value == 1;
            SetProperty(ref _isChecked, value); 
        }
    }

    #endregion End for footer

    #region For header
    
    private List<CompanyCreateViewModel> _brewerySearch;
    public List<CompanyCreateViewModel> BrewerySearch
    {
        get => _brewerySearch;
        set=> SetProperty(ref _brewerySearch, value); 
    }

    private List<CompanyCreateViewModel>  _wholesalerSearch;
   
    public List<CompanyCreateViewModel>  WholesalerSearch
    {
        get => _wholesalerSearch;
        set => SetProperty(ref _wholesalerSearch, value); 
    }

    
    private ObservableCollection<CompanyCreateViewModel> _breweries;
    public ObservableCollection<CompanyCreateViewModel> Breweries
    {
        get => _breweries;
        set => SetProperty(ref _breweries, value); 
    }

    private ObservableCollection<CompanyCreateViewModel> _wholesalers;
   
    public ObservableCollection<CompanyCreateViewModel> Wholesalers
    {
        get => _wholesalers;
        set => SetProperty(ref _wholesalers, value); 
    }
    

    #endregion For header


    #region For treview

    private BeerCreateViewModelBase _currentBeer;
    public BeerCreateViewModelBase CurrentBeer
    {
        get => _currentBeer;
        set
        {
            SetProperty(ref _currentBeer, value); 
            
            NotifyColleagues(MessageEnum.MsgOpenDetailBeer, value);
        }
    }
    
    
    private ObservableCollection<KeyValue> _trees;
    public ObservableCollection<KeyValue> Trees
    {
        get => _trees;
        set => SetProperty(ref _trees, value); 
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
        _beerBl = ServiceProvider.GetRequiredService<BeerService>();
        _breweryBl = ServiceProvider.GetRequiredService<BreweryService>();
        _wholesalerBl = ServiceProvider.GetRequiredService<WholesalerService>();
        
        InitMethod( async () =>
        {
            await RefreshBreweriesAsync();
            await RefreshWholesalersAsync();
            await FillTreview();
        });
        
        OnSearchCommand = new RelayCommand(async () =>
        {
            await FillTreview();
        });
        
        
        
        OnOpenCreateBeerCommand = new RelayCommand( () =>
        {
            NotifyColleagues(MessageEnum.MsgOpenCreateBeer);
        });

    }
    
    #endregion

    #region Methods private

    private async Task RefreshBreweriesAsync()
    {
        var response = await _breweryBl.GetBreweriesAsync(null, int.MaxValue, 0 );
        if (response.ResultStatus == BaseResultStatus.Success)
        {
            Breweries = new ObservableCollection<CompanyCreateViewModel>(response.Data.Results.Select(b => new CompanyCreateViewModel()
            {
                Id = b.Id,
                Name = b.Name,
            }));
        }
    }
    
    private async Task RefreshWholesalersAsync()
    {
        var response = await _wholesalerBl.GetWholesalerAsync(null, int.MaxValue, 0 );
        if (response.ResultStatus == BaseResultStatus.Success)
        {
            Wholesalers = new ObservableCollection<CompanyCreateViewModel>(response.Data.Results.Select(b => new CompanyCreateViewModel()
            {
                Id = b.Id,
                Name = b.Name,
            }));
        }
    }

    private async Task FillTreview()
    {
        var wholesalers = WholesalerSearch?.Select(c => c.Id.ToString()).ToList() ?? new List<string>() ;
        var breweries = BrewerySearch?.Select(c => c.Id.ToString()).ToList() ?? new List<string>();
        var skip = (Page -1)* Take;
        var response =! wholesalers.Any() && !breweries.Any() ?
            await _beerBl.GetAllAsync(skip, Take) : 
            await _beerBl.GetBeerByWholesalersOrBreweriesAsync(new SearchBeerByWholesalerAndBreweryInput()
        {
            Breweries = breweries,
            Wholesalers = wholesalers,
            Skip = skip,
            Take = Take
        });
        
        if (response.ResultStatus == BaseResultStatus.Success)
        {
            var r =  response.Data.Count % Take;
            var q =  response.Data.Count / Take;
            q = r > 0 ? q + 1 : q;
            
            Takes = new ObservableCollection<int>()
            {
                5,
                10,
                20,
                30,
                50,
                100,
                500
            };
            
            var pages = new List<int>();

            if (q == 0)
            {
                pages.Add(1);
            }
            else
            {
                for (var i = 1; i <= q; i++)
                {
                    pages.Add(i);
                }
            }

            Pages = new ObservableCollection<int>(pages);

            var group = response.Data.Results.GroupBy(b => b.Owner, (be, g) => 
               new KeyValue()
               {
                   DisplayCompany = be.Name,
                   Beers = new ObservableCollection<KeyValue>(g.Select(b =>  
                      new KeyValue()
                      {
                          DisplayBeer = b.Name,
                          IsBeer = true,
                          Beer =  new BeerCreateViewModel()
                          {
                              IdOfBeer = b.Id,
                              NameOfBeer = b.Name,
                              DegreeOfBeer = double.Parse(b.Degree?.Replace('%', ' ').Trim()) ,
                              PriceOfBeer = b.Price,
                              DescriptionOfBeer = b.Description,
                              Owner = new CompanyCreateViewModel()
                              {
                                  Id = be.Id,
                                  Name = be.Name
                              },
                          }
                      }
                   ))
               }).ToList();

            Trees = new ObservableCollection<KeyValue>(group);
        }
    }
    
    #endregion
    
    
    public class KeyValue: ViewModelCommon
    {

        public bool IsBeer { get; set; }
        public BeerCreateViewModelBase Beer { get; set; }
        
        private string _displayCompany;
        public string DisplayCompany
        {
            get => _displayCompany;
            set => SetProperty(ref _displayCompany, value); 
        }

        
        private string _displayBeer;
        public string DisplayBeer
        {
            get => _displayBeer;
            set => SetProperty(ref _displayBeer, value); 
        }
        
        private ObservableCollection<KeyValue> _beers;
        public ObservableCollection<KeyValue> Beers
        {
            get => _beers;
            set => SetProperty(ref _beers, value); 
        }

    }
}