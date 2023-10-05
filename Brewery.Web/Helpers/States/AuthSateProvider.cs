using System.Security.Claims;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Brewery.Contract.Contracts.Responses.Users;
using Brewery.Services.Services.Users;
using Brewery.Web.Helpers.ViewModels;
using Elia.Core.Attributes;
using Elia.Core.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace Brewery.Web.Helpers.States;

[Injectable(serviceLifetime:ServiceLifetime.Scoped)]
public class AuthSateProvider: AuthenticationStateProvider
{
    #region Privates Attributes

    private readonly ISessionStorageService _sessionStorage;

    private readonly UserService _userService;

    #region Constructor

    #region Properties
    public ApplicationUserViewModel CurrentUserViewModel {
        get;
        set;
    }
    #endregion

    public AuthSateProvider(ISessionStorageService sessionStorage, UserService userService)
    {
        _sessionStorage = sessionStorage;
        _userService = userService;
    }

    #endregion

    #endregion
  
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        if (CurrentUserViewModel!= null)
        {
            var json = JsonConvert.SerializeObject(CurrentUserViewModel.User);
           await _sessionStorage.SetItemAsStringAsync("brewery", json); // bad pratice hash
        }
        else
        {
            await SetToken();
        }
        
        if (CurrentUserViewModel!= null  && CurrentUserViewModel.IsAuthenticated)
        {
            var currentClaims = CurrentUserViewModel.Claims == null ? null : CurrentUserViewModel.Claims.Select(c => new Claim(c.Key, c.Value));

            var claims = currentClaims is null
                ? new[] { new Claim(ClaimTypes.Name, CurrentUserViewModel.User.Email) }
                : new[] { new Claim(ClaimTypes.Name, CurrentUserViewModel.User.Email) }.Concat(currentClaims);
            
            identity = new ClaimsIdentity(claims, "Server authentication");
        }
        
        return  new AuthenticationState(new ClaimsPrincipal(identity));
    }
    
    public void Logout()
    {
        CurrentUserViewModel = null;
        NotifyStateChanged();
    }

    public async Task SetToken()
    {
        if (CurrentUserViewModel == null)
        {
            try
            {
                var json = await _sessionStorage.GetItemAsStringAsync("brewery");
                if (json != null)
                {
                    var user = JsonConvert.DeserializeObject<CreateUserResponse>(json);
                    CurrentUserViewModel = new ApplicationUserViewModel()
                    {
                        IsAuthenticated = true,
                        User = user
                    };
                    _userService.SetToken(CurrentUserViewModel.User.Token);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }
      
    }

    public void NotifyStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}