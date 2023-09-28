using Brewery.BO.Entities;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class BrewerySeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbContext"></param>
    public static void Run(BreweryContext bbContext)
        {
            var entities = new List<BreweryEntity>
            {
                new BreweryEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb7"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Leffe Blonde"
                },
                
                new BreweryEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb8"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Brewery 2"
                },
               
                new BreweryEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb9"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Brewery 3"
                },
                
                new BreweryEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c1cb4"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Brewery 4"
                },
                
                new BreweryEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c2cb5"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Brewery 5"
                },
                
                new BreweryEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c3cb6"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Brewery 6"
                },
            };
            bbContext.AddRange(entities);
            bbContext.SaveChanges();
        }
}