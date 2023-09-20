using Brewery.BO.Entities;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class OrderBeerSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbContext"></param>
     public static void Run(BreweryContext bbContext)
            {
                var entities = new List<OrderBeerEntity>
                {
                    new OrderBeerEntity()
                    {
                        BeerId  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924870c9cb7"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924872c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8158-4924870c9cb5"),
                        Count = 2
                    },
                    
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("08d9c3d4-bd3b-6129-8153-4924870c9cb8"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924873c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8158-4924870c9cb5"),
                        Count = 1
                    },
                   
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("08d9c4d4-bd3b-7129-8153-4924870c9cb9"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924874c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8157-4924870c9cb5"),
                        Count = 4
                    },
                    
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("08d9c5d4-bd3b-8129-8153-4924870c1cb4"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924875c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8157-4924870c9cb5"),
                        Count = 3
                    },
                    
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("08d9c6d4-bd3b-9129-8153-4924870c2cb5"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924876c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8157-4924870c9cb5"),
                        Count = 2
                    },
                    
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("08d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924877c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8156-4924870c9cb5"),
                        Count = 3
                    },
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("01d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924878c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8156-4924870c9cb5"),
                        Count = 1
                    },
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("02d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924879c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8158-4924870c9cb5"),
                        Count = 4
                    },
                    new OrderBeerEntity()
                    {
                        BeerId = Guid.Parse("03d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Id  = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924861c9cb7"),
                        OrderId  = Guid.Parse("08d9c2d4-bd3b-4129-8158-4924870c9cb5"),
                        Count = 20
                    },
                };
                bbContext.AddRange(entities);
                bbContext.SaveChanges();
            }
}