using Brewery.BL.Business.Users;
using Brewery.Context;
using Brewery.Context.Seeds;

namespace Brewery.API;

/// <summary>
/// 
/// </summary>
public class SeeManager
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public static void Run(IApplicationBuilder app, IConfiguration configuration)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<BreweryContext>();

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
                        var token = UserBl.GenerateJwt(configuration["Jwt:Key"], configuration["Jwt:Issuer"]);
                        UserSeed.Run(context,token);
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
}