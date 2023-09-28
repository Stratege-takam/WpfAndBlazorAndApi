using Brewery.Contract.Contracts.Requests.Orders;
using Brewery.Tests.API.Provides;
using Elia.Core.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.Tests.API.API.Controllers;


/// <summary>
/// 
/// </summary>
[TestClass]
public class OrderBeerControllerUnitTest: TestBase
{
    
    /// <summary>
    /// 
    /// </summary>
    private ServiceProvider _provider;
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestCleanup]
    public async Task TestClean()
    {
        await _provider.DisposeAsync();
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestInitialize]
    public override async Task Setup()
    {
        // Arrange
        await base.Setup();

        _provider = FakeServiceProvider.GetServiceProvider();
    }
       
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetEstimation_SUCCESS()
    {
        
        //Arrange
        var request = new EstimationOrderRequest()
        {
            WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
            Beers = new List<EstimationOrderItemRequest>()
            {
                new EstimationOrderItemRequest()
                {
                    Name = "Leffe Blonde",
                    Count = 4
                },
                new EstimationOrderItemRequest()
                {
                    Name = "08d9c3d4-bd3b-6129-8153-4924870c9cb8",
                    Count = 8
                }
            }
        };
        
        
        
        //Act
        var response = await FakeController.GetOrderBeerController(_provider).GetEstimation(request);

        //Assert
        Assert.AreEqual(BaseResultStatus.Success, response.ResultStatus);
        Assert.IsNull(response.Reason);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(response.Data.Total, 808.8);
        Assert.AreEqual(response.Data.TotalToPay, 727.92);
        Assert.AreEqual(response.Data.Discount, 0.1);
        Assert.AreEqual(response.Data.DiscountAmount, 80.88);
    }
    
       
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetEstimation_Order_cannot_be_empty()
    {
        
        //Arrange
        var request = new EstimationOrderRequest()
        {
            WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
            Beers = new List<EstimationOrderItemRequest>()
        };
        
        
        
        //Act
        var response = await FakeController.GetOrderBeerController(_provider).GetEstimation(request);

        //Assert
        Assert.AreEqual(BaseResultStatus.BadParams, response.ResultStatus);
        Assert.IsNotNull(response.Reason);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Reason, "Order cannot be empty");
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetEstimation_The_wholesaler_must_exist()
    {
        
        //Arrange
        var request = new EstimationOrderRequest()
        {
            WholesalerId = Guid.Parse("12d9c2d4-bd3b-4129-8153-4924870c9cb1"),
            Beers = new List<EstimationOrderItemRequest>()
            {
                new EstimationOrderItemRequest()
                {
                    Name = "Leffe Blonde",
                    Count = 2
                },
                new EstimationOrderItemRequest()
                {
                    Name = "08d9c3d4-bd3b-6129-8153-4924870c9cb8",
                    Count = 25
                }
            }
        };

        
        
        //Act
        var response = await FakeController.GetOrderBeerController(_provider).GetEstimation(request);

        //Assert
        Assert.AreEqual(BaseResultStatus.BadParams, response.ResultStatus);
        Assert.IsNotNull(response.Reason);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Reason, "The wholesaler must exist");
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetEstimation_There_cannot_be_a_duplicate_in_the_order()
    {
        
        //Arrange
        var request = new EstimationOrderRequest()
        {
            WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
            Beers = new List<EstimationOrderItemRequest>()
            {
                new EstimationOrderItemRequest()
                {
                    Name = "Leffe Blonde",
                    Count = 2
                },
                new EstimationOrderItemRequest()
                {
                    Name = "Beer 2",
                    Count = 13
                },
                new EstimationOrderItemRequest()
                {
                    Name = "Leffe Blonde",
                    Count = 5
                },
                
            }
        };
        
        
        
        //Act
        var response = await FakeController.GetOrderBeerController(_provider).GetEstimation(request);

        //Assert
        Assert.AreEqual(BaseResultStatus.BadParams, response.ResultStatus);
        Assert.IsNotNull(response.Reason);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Reason, "There cannot be a duplicate in the order");
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetEstimation_The_beer_must_be_sold_by_the_wholesaler()
    {
        
        //Arrange
        var request = new EstimationOrderRequest()
        {
            WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
            Beers = new List<EstimationOrderItemRequest>()
            {
                new EstimationOrderItemRequest()
                {
                    Name = "Leffe Blonde",
                    Count = 2
                },
                new EstimationOrderItemRequest()
                {
                    Name = "Leffe green",
                    Count = 25
                }
            }
        };
        
        
        
        //Act
        var response = await FakeController.GetOrderBeerController(_provider).GetEstimation(request);

        //Assert
        Assert.AreEqual(BaseResultStatus.BadParams, response.ResultStatus);
        Assert.IsNotNull(response.Reason);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Reason, "The beer (Leffe green) must be sold by the wholesaler");
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public async Task GetEstimation_The_number_of_beers_ordered_must_not_exceed_the_wholesaler_s_stock()
    {
        
        //Arrange
        var request = new EstimationOrderRequest()
        {
            WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
            Beers = new List<EstimationOrderItemRequest>()
            {
                new EstimationOrderItemRequest()
                {
                    Name = "Leffe Blonde",
                    Count = 250
                },
                new EstimationOrderItemRequest()
                {
                    Name = "08d9c3d4-bd3b-6129-8153-4924870c9cb8",
                    Count = 25
                }
            }
        };
        
        
        
        //Act
        var response = await FakeController.GetOrderBeerController(_provider).GetEstimation(request);

        //Assert
        Assert.AreEqual(BaseResultStatus.BadParams, response.ResultStatus);
        Assert.IsNotNull(response.Reason);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Reason, $"The number of beers (Leffe Blonde) ordered must not exceed the wholesaler's stock");
    }
}