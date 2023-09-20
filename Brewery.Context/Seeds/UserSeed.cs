using Brewery.BO.Entities;
using Elia.Core.Extensions;

namespace Brewery.Context.Seeds;

/// <summary>
/// 
/// </summary>
public class UserSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="token"></param>
    public static void Run(BreweryContext context, string token)
    {
       
        var user = new UserEntity()
        {
            Email = "admin23@gbrewery.be",
            Firstname = "user",
            Password = "DbTest2023".Hash(),
            Token = token
        };
        context.Users.Add(user);
        context.SaveChanges();
    }
}