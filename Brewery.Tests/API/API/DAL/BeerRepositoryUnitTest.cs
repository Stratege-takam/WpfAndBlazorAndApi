using Brewery.Tests.API.Provides;
using Elia.Core.Utils;

namespace Brewery.Tests.API.API.DAL;


/// <summary>
/// 
/// </summary>
[TestClass]
public class BeerRepositoryUnitTest: TestBase
{
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetBeerByWholesalersOrBreweries_Bold_Search_Result()
    {
        //Arrange
        IList<string> breweries = new List<string>()
        {
            "08d9c2d4-bd3b-4129-8153-4924870c9cb7",
            "Leffe Blonde"
        };
        IList<string> wholesalers = new List<string>()
        {
            "08d9c2d4-bd3b-4129-8153-4924870c9cb2", 
            "GeneDrinks"
        };
        int skip = 0;
        int take = 30;
        
        //Act
        var response = await FakeRepository.GetBeerRepository()
            .GetBeerByWholesalersOrBreweries(breweries, wholesalers, skip, take);
        
        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(7, response.Data.Count);
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetBeerByWholesalersOrBreweries_05_Result()
    {
        //Arrange
        IList<string> breweries = new List<string>()
        {
            "08d9c2d4-bd3b-4129-8153-4924870c9cb7",
            "Leffe Blonde"
        };
        IList<string> wholesalers = new List<string>()
        {
            "08d9c2d4-bd3b-4129-8153-4924870c9cb2", 
            "GeneDrinks"
        };
        int skip = 0;
        int take = 5;
        
        //Act
        var response = await FakeRepository.GetBeerRepository()
            .GetBeerByWholesalersOrBreweries(breweries, wholesalers, skip, take);
        
        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(7, response.Data.Count);
        Assert.AreEqual(5, response.Data.Results.Count());
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetBeerByWholesalersOrBreweries_Only_Breweries()
    {
        //Arrange
        IList<string> breweries = new List<string>()
        {
            "Leffe Blonde"
        };
        IList<string> wholesalers = new List<string>()
        {
           
        };
        int skip = 0;
        int take = 30;
        
        //Act
        var response = await FakeRepository.GetBeerRepository()
            .GetBeerByWholesalersOrBreweries(breweries, wholesalers, skip, take);
        
        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(5, response.Data.Count);
        Assert.AreEqual(5, response.Data.Results.Count());
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetBeerByWholesalersOrBreweries_Not_Found_Result()
    {
        //Arrange
        IList<string> breweries = new List<string>()
        {
           
        };
        IList<string> wholesalers = new List<string>()
        {
           
        };
        int skip = 0;
        int take = 30;
        
        //Act
        var response = await FakeRepository.GetBeerRepository()
            .GetBeerByWholesalersOrBreweries(breweries, wholesalers, skip, take);
        
        //Assert
        Assert.IsTrue(response.IsNotSuccess);
        Assert.AreEqual(BaseResultStatus.NotFound, response.Status);
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetAllBeerIdByNameOrId_Success()
    {
        //Arrange
        var names = new List<string>()
        {
            "08d9c3d4-bd3b-6129-8153-4924870c9cb8",
            "Leffe Blonde"
        };
        
        
        //Act
        var response = await FakeRepository.GetBeerRepository()
            .GetAllBeerIdByNameOrId(names);
        
        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(2, response.Data.Count);
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetAllBeerIdByNameOrId_Not_Found_Result()
    {
        //Arrange
        var names = new List<string>()
        {
            "Leffe black"
        };
        
        
        //Act
        var response = await FakeRepository.GetBeerRepository()
            .GetAllBeerIdByNameOrId(names);
        
        //Assert
        Assert.IsTrue(response.IsNotSuccess);
        Assert.AreEqual(BaseResultStatus.NotFound, response.Status);
    }

}