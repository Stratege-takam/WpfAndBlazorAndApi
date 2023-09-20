using Brewery.BO.Entities;
using Elia.Core.Utils;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class OrderSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbContext"></param>
   public static void Run(BreweryContext bbContext)
        {
            var entities = new List<OrderEntity>
            {
                new OrderEntity()
                {
                    ClientId  = Guid.Parse("08d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                    Id  = Guid.Parse("08d9c2d4-bd3b-4129-8158-4924870c9cb5"),
                    WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb1"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    CommandNumber = Generator.GenerateDigit()
                },
                
                new OrderEntity()
                {
                    ClientId  = Guid.Parse("08d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                    Id  = Guid.Parse("08d9c2d4-bd3b-4129-8157-4924870c9cb5"),
                    WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb2"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    CommandNumber = Generator.GenerateDigit()
                },
               
                new OrderEntity()
                {
                    ClientId  = Guid.Parse("08d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                    Id  = Guid.Parse("08d9c2d4-bd3b-4129-8156-4924870c9cb5"),
                    WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb3"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    CommandNumber = Generator.GenerateDigit()
                },
                
                new OrderEntity()
                {
                    ClientId  = Guid.Parse("08d9c3d4-bd3b-4129-8153-4924870c9cb8"),
                    Id  = Guid.Parse("08d9c2d4-bd3b-4129-8155-4924870c9cb5"),
                    WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb4"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    CommandNumber = Generator.GenerateDigit()
                },
                
                new OrderEntity()
                {
                    ClientId  = Guid.Parse("08d9c3d4-bd3b-4129-8153-4924870c9cb8"),
                    Id  = Guid.Parse("08d9c2d4-bd3b-4129-8154-4924870c9cb5"),
                    WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb5"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    CommandNumber = Generator.GenerateDigit()
                },
                
                new OrderEntity()
                {
                    ClientId  = Guid.Parse("08d9c4d4-bd3b-4129-8153-4924870c9cb9"),
                    Id  = Guid.Parse("08d9c2d4-bd3b-4129-8159-4924870c9cb6"),
                    WholesalerId = Guid.Parse("08d9c2d4-bd3b-4129-8153-4924870c9cb6"),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    CommandNumber = Generator.GenerateDigit()
                },
            };
            bbContext.AddRange(entities);
            bbContext.SaveChanges();
        }
}