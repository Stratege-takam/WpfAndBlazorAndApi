using Brewery.BO.Entities;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class StockBeerWholesalerSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbContext"></param>
     public static void Run(BreweryContext bbContext)
            {
                var entities = new List<StockBeerWholesalerEntity>
                {
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924870c9cb7"),
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8663-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
                        Stock = 100,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    },
                    
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("08d9c3d4-bd3b-6129-8153-4924870c9cb8"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8563-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
                        Stock = 20,
                    },
                   
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("08d9c4d4-bd3b-7129-8153-4924870c9cb9"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8463-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
                        Stock = 23,
                    },
                    
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("08d9c5d4-bd3b-8129-8153-4924870c1cb4"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8363-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb3"),
                        Stock = 35,
                    },
                    
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("08d9c6d4-bd3b-9129-8153-4924870c2cb5"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8263-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb3"),
                        Stock = 62,
                    },
                    
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("08d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8193-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb2"),
                        Stock = 18,
                    },
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("01d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8183-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb2"),
                        Stock = 12,
                    },
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("02d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8173-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
                        Stock = 7,
                    },
                    new StockBeerWholesalerEntity()
                    {
                        BeerId = Guid.Parse("03d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8163-4924870c3cb6"),
                        WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
                        Stock = 324,
                    },
                };
                bbContext.AddRange(entities);
                bbContext.SaveChanges();
            }
}