using Brewery.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Brewery.Tests.API.Provides;

public class BreweryFakeContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Task<BreweryContext> GetDatabaseContextAsync()
    {
        var options = new DbContextOptionsBuilder<BreweryContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            // don't raise the error warning us that the in memory db doesn't support transactions
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        var dbContext = new BreweryContext(options);

        return  Task.Run(() => dbContext);
    }
}