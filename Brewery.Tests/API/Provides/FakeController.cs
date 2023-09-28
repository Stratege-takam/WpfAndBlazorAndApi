using Brewery.API.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Brewery.Tests.API.Provides;

public class FakeController
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static BeerController GetBeerController(ServiceProvider provider) => 
        new( FakeServiceProvider.GetLoggerFactory(provider).CreateLogger<BeerController>(),
        FakeBl.GetBeerBl()
    );
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static OrderBeerController GetOrderBeerController(ServiceProvider provider) => 
        new( FakeServiceProvider.GetLoggerFactory(provider).CreateLogger<OrderBeerController>(),
            FakeBl.GetOrderBeer()
        );
}