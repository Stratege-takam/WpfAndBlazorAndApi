using Brewery.Context;
using Brewery.DAL.Repositories.Beers;
using Brewery.DAL.Repositories.Breweries;
using Brewery.DAL.Repositories.Clients;
using Brewery.DAL.Repositories.OrderBeers;
using Brewery.DAL.Repositories.Orders;
using Brewery.DAL.Repositories.StockBeerWholesalers;
using Brewery.DAL.Repositories.Wholesalers;

namespace Brewery.Tests.API.Provides;

public class FakeRepository
{
    /// <summary>
    /// 
    /// </summary>
    public static BreweryContext FakeBreweryContext { get; set; }

    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static BeerRepository GetBeerRepository() => new(FakeBreweryContext);
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static BreweryRepository GetBreweryRepository() => new(FakeBreweryContext);
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static ClientRepository GetClientRepository() => new(FakeBreweryContext);
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static OrderRepository GetOrderRepository() => new(FakeBreweryContext);
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static OrderBeerRepository GetOrderBeerRepository() => new(FakeBreweryContext);
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static StockBeerWholesalerRepository GetStockBeerWholesalerRepository() => new(FakeBreweryContext);
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static WholesalerRepository GetWholesalerRepository() => new(FakeBreweryContext);
    
}