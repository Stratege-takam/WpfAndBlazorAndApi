using Brewery.BO.Entities;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class WholesalerSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbContext"></param>
     public static void Run(BreweryContext bbContext)
        {
            var entities = new List<WholesalerEntity>
            {
                new WholesalerEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "GeneDrinks"
                },
                
                new WholesalerEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb2"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Wholesaler 2"
                },
               
                new WholesalerEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb3"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Wholesaler 3"
                },
                
                new WholesalerEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb4"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Wholesaler 4"
                },
                
                new WholesalerEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb5"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Wholesaler 5"
                },
                
                new WholesalerEntity()
                {
                    Id = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb6"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Name = "Wholesaler 6"
                },
            };
            bbContext.AddRange(entities);
            bbContext.SaveChanges();
        }
}