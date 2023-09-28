using Brewery.BO.Entities;
using Elia.Core.Utils;

namespace Brewery.Tests.API.FakeData;

public class FakeModel
{
    /// <summary>
    /// Beer 
    /// </summary>
    public virtual BeerEntity Beer { get; set; }
        
    /// <summary>
    /// Brewery 
    /// </summary>
    public virtual BreweryEntity Brewery { get; set; }
        
    /// <summary>
    /// Client 
    /// </summary>
    public virtual ClientEntity Client { get; set; }
        
    /// <summary>
    /// Order beer 
    /// </summary>
    public virtual OrderBeerEntity OrderBeer { get; set; }
        
    /// <summary>
    /// Order 
    /// </summary>
    public virtual OrderEntity Order { get; set; }
     
        
    /// <summary>
    /// Stock beer wholesaler 
    /// </summary>
    public virtual StockBeerWholesalerEntity StockBeerWholesaler { get; set; }
        
    /// <summary>
    /// Wholesaler 
    /// </summary>
    public virtual WholesalerEntity Wholesaler { get; set; }

    public static FakeModel GetData()
    {
        return new FakeModel()
        {
            Brewery = new BreweryEntity()
            {
                Id = new Guid("08d9c2d4-ad3b-4129-8153-4924870c9cb7"),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Name = "Leffe green"
            },
            Beer = new BeerEntity() 
            {
                Id = new Guid("18d9c1d4-bd3b-5129-8153-4924870c9cb7"),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Name = "Leffe Blonde",
                Price = 2.20,
                Degree = 6.6,
                OwnerId = new Guid("08d9c2d4-ad3b-4129-8153-4924870c9cb7")
            },
            Client = new ClientEntity()
            {
                Id = new Guid("28d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Name = "Danick Takam"
            },
            Wholesaler = new WholesalerEntity()
            {
                Id = new Guid("38d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Name = "GeneDrinks"
            },
            Order = new OrderEntity()
            {
                ClientId  = new Guid("28d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                Id  = new Guid("48d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                WholesalerId = new Guid("38d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                CommandNumber = Generator.GenerateDigit()
            },
            OrderBeer = new OrderBeerEntity()
            {
                BeerId  = new Guid("18d9c1d4-bd3b-5129-8153-4924870c9cb7"),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Id  = new Guid("58d9c1d4-bd3b-5129-8153-4924872c9cb7"),
                OrderId  = new Guid("48d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                Count = 2
            },
            StockBeerWholesaler =new StockBeerWholesalerEntity()
            {
                BeerId = new Guid("18d9c1d4-bd3b-5129-8153-4924870c9cb7"),
                Id = new Guid("68d9c1d4-bd3b-5129-8153-4924872c9cb7"),
                WholesalerId = new Guid("38d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                Stock = 140,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            },
        };
    }
}