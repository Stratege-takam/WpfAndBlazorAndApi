using Brewery.Contract.Contracts.Requests.Beers;
using Brewery.Contract.Contracts.Responses.Companies;
using Brewery.Services.Services.Beers;
using Brewery.Services.Services.Breweries;
using Brewery.Services.Services.Wholesalers;
using Brewery.Web.Helpers.ViewModels;
using Elia.Core.Attributes;
using Elia.Core.Utils;

namespace Brewery.Web.Helpers.States;


[Injectable(serviceLifetime: ServiceLifetime.Singleton)]
public class BreweryState: NotifyPropertyChanged
{
   
    #region Private properties

    private readonly BeerService _beerService;
    private readonly BreweryService _breweryService;
    private readonly WholesalerService _wholesalerService;

    #endregion
    
    #region Properties Notify
    private bool _isAlreadyInitialize;
    public bool IsAlreadyInitialize
    {
        get => _isAlreadyInitialize;
        set => SetProperty(ref _isAlreadyInitialize, value); 
    }
    

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

            if (_take != value)
            {
                SetProperty(ref _take, value); 
                InvokeMethod(async () =>  await FillTreview());
            }
            
        }
    }
    
    public IList<int> Takes { get; set; }
    
    private int _skip = 1;
    public int Skip
    {
        get => _skip;
        set
        {
            
            if (_skip != value)
            {
                SetProperty(ref _skip, value); 
                InvokeMethod(async () =>  await FillTreview());
            }
        }
    }
    
    private IList<int> _skips;
    public IList<int> Skips { get; set; }
    
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
    
    private string _brewerySearch;
    public string BrewerySearch
    {
        get => _brewerySearch;
        set=> SetProperty(ref _brewerySearch, value); 
    }

    private string  _wholesalerSearch;
   
    public string  WholesalerSearch
    {
        get => _wholesalerSearch;
        set => SetProperty(ref _wholesalerSearch, value); 
    }

    
    public List<KeyValueViewModel> Breweries { get ; set ; }

    public List<KeyValueViewModel> Wholesalers { get; set; }
    

    #endregion For header


    #region For treview
    
    public List<BreweryViewModel> Trees { get ; set; }
    
    
    #endregion

    #endregion End for treview
    
    #region For Content
    
    public IList<BeerDetailViewModel> Details { get; set ; }
    
    #endregion

    #region Constructor

    public BreweryState(BeerService beerService, BreweryService breweryService, WholesalerService wholesalerService)
    {
        _beerService = beerService;
        _breweryService = breweryService;
        _wholesalerService = wholesalerService;
    }
    
    #endregion

    #region Methods

    public async Task InitDataAsync()
    {
        await RefreshBreweriesAsync();
        await RefreshWholesalersAsync();
        await FillTreview();
        IsAlreadyInitialize = true;
    }

    private async Task RefreshBreweriesAsync()
    {
        var response = await _breweryService.GetBreweriesAsync(null, int.MaxValue, 0 );
        if (response.ResultStatus == BaseResultStatus.Success)
        {
            Breweries= response.Data.Results.Select(b => new KeyValueViewModel()
            {
                Key = b.Id.ToString(),
                Value = b.Name,
            }).ToList();
        }
    }
    
    private async Task RefreshWholesalersAsync()
    {
        var response = await _wholesalerService.GetWholesalerAsync(null, int.MaxValue, 0 );
        if (response.ResultStatus == BaseResultStatus.Success)
        {
            Wholesalers =response.Data.Results.Select(b => new KeyValueViewModel()
            {
                Key = b.Id.ToString(),
                Value = b.Name,
            }).ToList();
        }
    }

    public void AddDetail(BeerDetailViewModel beerDetail)
    {
        if (Details == null) Details = new List<BeerDetailViewModel>();

        // add only single details
        if (!Details.Any(d => d.Id == beerDetail.Id))
        {
            Details.Insert(0, beerDetail);
            RaisePropertyChanged(null);
        }
    }
    
    public void RemoveDetail(BeerDetailViewModel beerDetail)
    {
        Details.Remove( beerDetail);
        RaisePropertyChanged(null);
    }

    public async Task FillTreview()
    {
        var wholesalers = WholesalerSearch?.Split(",").ToList() ?? new List<string>() ;
        var breweries = BrewerySearch?.Split(",").ToList() ?? new List<string>();
        var skip = (Skip -1)* Take;
        var response =! wholesalers.Any() && !breweries.Any() ?
            await _beerService.GetAllAsync(skip, Take) : 
            await _beerService.GetBeerByWholesalersOrBreweriesAsync(new SearchBeerByWholesalerAndBreweryRequest()
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
            
            Takes = new List<int>()
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

            Skips = pages;

            Trees = response.Data.Results.GroupBy(b => b.Owner, (be, g) =>
                new BreweryViewModel()
                {
                    Name = be.Name,
                    Id = be.Id,
                    Beers = g.Select(b =>
                        new BeerDetailViewModel()
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Degree = double.Parse(b.Degree.Replace("%", "")),
                            Description = b.Description,
                            Price = b.Price,
                            Owner = new GetCompanyResponse()
                            {
                                Name = be.Name,
                                Id = be.Id
                            }
                        }
                    ).ToList()

                }).ToList();
        }
        
        RaisePropertyChanged(null);
    }
    
    #endregion
}