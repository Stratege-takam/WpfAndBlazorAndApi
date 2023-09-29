using System.Security.Claims;
using Blazored.SessionStorage;
using Brewery.Contract.Contracts.Responses.Users;
using Brewery.Web.ViewModels;
using Elia.Core.Attributes;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace Brewery.Web.States;

[Injectable(serviceLifetime:ServiceLifetime.Scoped)]
public class AuthSateProvider: AuthenticationStateProvider
{
    #region Privates Attributes

    private ISessionStorageService _sessionStorage;

    #region Constructor

    #region Properties
    public ApplicationUser CurrentUser {
        get;
        set;
    }
    #endregion

    public AuthSateProvider(ISessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    #endregion

    #endregion
  
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        if (CurrentUser!= null)
        {
            var json = JsonConvert.SerializeObject(CurrentUser.User);
           await _sessionStorage.SetItemAsync("brewery", json); // bad pratice hash
        }
        else
        {
            var json = await _sessionStorage.GetItemAsync<string>("brewery");
            if (json != null)
            {
                var user = JsonConvert.DeserializeObject<CreateUserResponse>(json);
                CurrentUser = new ApplicationUser()
                {
                    IsAuthenticated = true,
                    User = user
                };
            }
        }
        
        if (CurrentUser!= null  && CurrentUser.IsAuthenticated)
        {
            var currentClaims = CurrentUser.Claims == null ? null : CurrentUser.Claims.Select(c => new Claim(c.Key, c.Value));

            var claims = currentClaims is null
                ? new[] { new Claim(ClaimTypes.Name, CurrentUser.User.Email) }
                : new[] { new Claim(ClaimTypes.Name, CurrentUser.User.Email) }.Concat(currentClaims);
            
            identity = new ClaimsIdentity(claims, "Server authentication");
        }
        
        return  new AuthenticationState(new ClaimsPrincipal(identity));
    }
    
    public async Task Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}