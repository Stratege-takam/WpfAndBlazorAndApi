using Brewery.BL.Business.Beers;
using Brewery.BL.Business.OrderBeers;

namespace Brewery.Tests.API.Provides;

public class FakeBl
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static BeerBl GetBeerBl() => new(FakeRepository.GetBeerRepository(),
        FakeRepository.GetStockBeerWholesalerRepository(), FakeRepository.GetOrderBeerRepository(),
        FakeRepository.GetBreweryRepository()
        );
    
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static OrderBeerBl GetOrderBeer() => new(FakeRepository.GetOrderBeerRepository(),
        FakeRepository.FakeBreweryContext, FakeRepository.GetWholesalerRepository(),
        FakeRepository.GetStockBeerWholesalerRepository(), FakeRepository.GetClientRepository(),
        FakeRepository.GetBeerRepository(), FakeRepository.GetOrderRepository()
    );
    
}