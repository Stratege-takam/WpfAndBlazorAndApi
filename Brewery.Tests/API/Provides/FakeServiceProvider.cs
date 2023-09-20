using Elia.Core.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Brewery.Tests.API.Provides;

/// <summary>
/// 
/// </summary>
public class FakeServiceProvider
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static ServiceProvider GetServiceProvider()
    {
        return new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static ILoggerFactory GetLoggerFactory(ServiceProvider provider)
    {
        return provider.GetService<ILoggerFactory>();
    }
        
   
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IWebHostEnvironment GetHostingEnvironment()
    {
        var mockEnvironment = new Mock<IWebHostEnvironment>();
        //...Setup the mock as needed
        mockEnvironment
            .Setup(m => m.EnvironmentName)
            .Returns(EnvironmentEnum.Test.ToString());
        //.Returns("Hosting:UnitTestEnvironment");
          
        return mockEnvironment.Object;
    }
}