using Brewery.BO.Entities;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class BeerSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbContext"></param>
    public static void Run(BreweryContext bbContext)
            {
                var entities = new List<BeerEntity>
                {
                    new BeerEntity()
                    {
                        Id = Guid.Parse("08d9c1d4-bd3b-5129-8153-4924870c9cb7"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Leffe Blonde",
                        Price = 2.20,
                        Degree = 6.6,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb7")
                    },
                    
                    new BeerEntity()
                    {
                        Id = Guid.Parse("08d9c3d4-bd3b-6129-8153-4924870c9cb8"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 2",
                        Price = 100.00,
                        Degree = 63.6,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb7")
                    },
                   
                    new BeerEntity()
                    {
                        Id = Guid.Parse("08d9c4d4-bd3b-7129-8153-4924870c9cb9"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 3",
                        Price = 22.10,
                        Degree = 0.6,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb7")
                    },
                    
                    new BeerEntity()
                    {
                        Id = Guid.Parse("08d9c5d4-bd3b-8129-8153-4924870c1cb4"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 4",
                        Price = 1.00,
                        Degree = 0,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb9")
                    },
                    
                    new BeerEntity()
                    {
                        Id = Guid.Parse("08d9c6d4-bd3b-9129-8153-4924870c2cb5"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 5",
                        Price = 23.00,
                        Degree = 11.2,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb9")
                    },
                    
                    new BeerEntity()
                    {
                        Id = Guid.Parse("08d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 6",
                        Price = 12.12,
                        Degree = 8.88,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb8")
                    },
                    new BeerEntity()
                    {
                        Id = Guid.Parse("01d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 7",
                        Price = 1209.12,
                        Degree = 89.88,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb7")
                    },
                    new BeerEntity()
                    {
                        Id = Guid.Parse("02d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 8",
                        Price = 12.12,
                        Degree = 19.18,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb8")
                    },
                    new BeerEntity()
                    {
                        Id = Guid.Parse("03d9c7d4-bd3b-4229-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Beer 9",
                        Price = 08.33,
                        Degree = 30.00,
                        OwnerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb7")
                    },
                };
                bbContext.AddRange(entities);
                bbContext.SaveChanges();
            }
}