using Brewery.BO.Entities;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class ClientSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bbContext"></param>
     public static void Run(BreweryContext bbContext)
            {
                var entities = new List<ClientEntity>
                {
                    new ClientEntity()
                    {
                        Id = Guid.Parse("08d9c1d4-bd3b-4129-8153-4924870c9cb7"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Danick Takam"
                    },
                    
                    new ClientEntity()
                    {
                        Id = Guid.Parse("08d9c3d4-bd3b-4129-8153-4924870c9cb8"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Client 2"
                    },
                   
                    new ClientEntity()
                    {
                        Id = Guid.Parse("08d9c4d4-bd3b-4129-8153-4924870c9cb9"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Client 3"
                    },
                    
                    new ClientEntity()
                    {
                        Id = Guid.Parse("08d9c5d4-bd3b-4129-8153-4924870c1cb4"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Client 4"
                    },
                    
                    new ClientEntity()
                    {
                        Id = Guid.Parse("08d9c6d4-bd3b-4129-8153-4924870c2cb5"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Client 5"
                    },
                    
                    new ClientEntity()
                    {
                        Id = Guid.Parse("08d9c7d4-bd3b-4129-8153-4924870c3cb6"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Name = "Client 6"
                    },
                };
                bbContext.AddRange(entities);
                bbContext.SaveChanges();
            }
}