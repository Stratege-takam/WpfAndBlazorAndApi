using Brewery.Context.Seeds;

namespace Brewery.Tests.API.Provides;

/// <summary>
/// 
/// </summary>
public class FakeSeedManager
{
   /// <summary>
   /// 
   /// </summary>
    public static void Run()
    {
        var context = FakeRepository.FakeBreweryContext;

        if (!context.Beers.Any())
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ClientSeed.Run(context);
                    WholesalerSeed.Run(context);
                    BrewerySeed.Run(context);
                    BeerSeed.Run(context);
                    OrderSeed.Run(context);
                    OrderBeerSeed.Run(context);
                    StockBeerWholesalerSeed.Run(context);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                }
            }
        }
    }
}