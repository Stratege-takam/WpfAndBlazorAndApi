using Brewery.BO.Entities;
using Brewery.Contract.Contracts.Requests.Beers;
using Brewery.Tests.API.Provides;
using Elia.Core.Utils;

namespace Brewery.Tests.API.API.BL;

/// <summary>
/// 
/// </summary>
[TestClass]
public class BeerBlUnitTest: TestBase
{
    
     /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetBeerByWholesalersOrBreweries_Bold_Search_Result()
    {
        //Arrange
        var request = new SearchBeerByWholesalerAndBreweryRequest()
        {
            Breweries = new List<string>()
            {
                "08d9c2d4-bd3b-4129-8153-4924870c9cb7",
                "Leffe Blonde"
            },
            Wholesalers = new List<string>()
            {
                "08d9c2d4-bd3b-4129-8153-4924870c9cb2", 
                "GeneDrinks"
            },
            Skip = 0,
            Take = 30
        };
       
        
        //Act
        var response = await FakeBl.GetBeerBl().GetBeerByWholesalersOrBreweries(request);
        
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
        var request = new SearchBeerByWholesalerAndBreweryRequest()
        {
            Breweries = new List<string>()
            {
                "08d9c2d4-bd3b-4129-8153-4924870c9cb7",
                "Leffe Blonde"
            },
            Wholesalers = new List<string>()
            {
                "08d9c2d4-bd3b-4129-8153-4924870c9cb2", 
                "GeneDrinks"
            },
            Skip = 0,
            Take = 5
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().GetBeerByWholesalersOrBreweries(request);
        
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
        var request = new SearchBeerByWholesalerAndBreweryRequest()
        {
            Breweries = new List<string>()
            {
                "Leffe Blonde"
            },
            Wholesalers = new List<string>()
            {
               
            },
            Skip = 0,
            Take = 5
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().GetBeerByWholesalersOrBreweries(request);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(5, response.Data.Results.Count());
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetBeerByWholesalersOrBreweries_Not_Found_Result()
    {
        //Arrange
        var request = new SearchBeerByWholesalerAndBreweryRequest()
        {
            Breweries = new List<string>()
            {
            },
            Wholesalers = new List<string>()
            {
               
            },
            Skip = 0,
            Take = 5
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().GetBeerByWholesalersOrBreweries(request);

        //Assert
        Assert.IsTrue(response.IsNotSuccess);
        Assert.AreEqual(BaseResultStatus.NotFound, response.Status);
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task CreateBeerAsync_SUCCESS()
    {
        //Arrange
        await FakeRepository.GetBreweryRepository().CreateAsync(_fakeModel.Brewery);
        var request = new CreateBeerRequest()
        {
            Degree = _fakeModel.Beer.Degree,
            Name = "New beer",
            Price = _fakeModel.Beer.Price,
            OwnerId = _fakeModel.Beer.OwnerId
        };

     
        //Act
        var response = await FakeBl.GetBeerBl().CreateBeerAsync(request);
        
        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(BaseResultStatus.Success, response.Status);
        Assert.IsNotNull(response.Data);
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task CreateBeerAsync_BREWERY_NOT_EXIST()
    {
        //Arrange
        var request = new CreateBeerRequest()
        {
            Degree = _fakeModel.Beer.Degree,
            Name = _fakeModel.Beer.Name,
            Price = _fakeModel.Beer.Price,
            OwnerId = _fakeModel.Beer.OwnerId
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().CreateBeerAsync(request);

        //Assert
        Assert.IsTrue(response.IsNotSuccess);
        Assert.AreEqual(BaseResultStatus.BadParams, response.Status);
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task CreateBeerAsync_BEER_ALREADY_EXIST()
    {
        //Arrange
        var request = new CreateBeerRequest()
        {
            Degree = _fakeModel.Beer.Degree,
            OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb7"),
            Name = "Leffe Blonde",
            Price = _fakeModel.Beer.Price,
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().CreateBeerAsync(request);

        //Assert
        Assert.IsTrue(response.IsNotSuccess);
        Assert.AreEqual(BaseResultStatus.BadParams, response.Status);
    }
    
    
    
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task RemoveBeerAsync_PERMANENTLY()
    {
        //Arrange
        await FakeRepository.GetBreweryRepository().CreateAsync(_fakeModel.Brewery);
        await FakeRepository.GetBeerRepository().CreateAsync(new BeerEntity()
        {
            Degree = 134,
            Name = "Remove permanent",
            Price = 12,
            OwnerId = _fakeModel.Brewery.Id
        });
        var request = new RemoveBeerRequest()
        {
            Name = "Remove permanent",
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().RemoveBeerAsync(request);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(BaseResultStatus.Success, response.Status);
        Assert.IsNotNull(response.Data);
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task RemoveBeerAsync_TEMPORARY()
    {
        //Arrange
        var request = new RemoveBeerRequest()
        {
            Name = "Leffe Blonde"
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().RemoveBeerAsync(request);

      
        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(BaseResultStatus.Success, response.Status);
        Assert.IsNotNull(response.Data);
       
        
        var ExistBeerResponse = await FakeRepository.GetBeerRepository().AnyAsync(b => b.Id == response.Data.Id, true);

        Assert.IsTrue(ExistBeerResponse.IsSuccess);
        Assert.IsTrue(ExistBeerResponse.Data);
    }
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task RemoveBeerAsync_NOT_FOUND()
    {
        //Arrange
        var request = new RemoveBeerRequest()
        {
            Name = "Not found beer",
        };
        
        //Act
        var response = await FakeBl.GetBeerBl().RemoveBeerAsync(request);

        //Assert
        Assert.IsTrue(response.IsNotSuccess);
        Assert.AreEqual(BaseResultStatus.NotFound, response.Status);
        Assert.IsNull(response.Data);
    }
}