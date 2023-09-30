using Brewery.Contract.Contracts.Requests.Users;
using Brewery.Contract.Contracts.Responses.Users;
using Brewery.Services.Services.Users;
using Brewery.Web.Helpers.States;
using Brewery.Web.Shared.Enums;
using Brewery.Web.ViewModels;
using Elia.Core.Attributes;
using Elia.Core.Extensions;
using Elia.Core.Utils;
using Microsoft.AspNetCore.Components;

namespace Brewery.Web.States;
// install Install-package Microsoft.AspNetCore.Components.Authorization
[Injectable()]
public class UserState: NotifyPropertyChanged
{
    #region Private properties

    private UserService _service;
    private const string  DefaultTextLoad = "Loading...";
    private NavigationManager _navigationManager;
    private AuthSateProvider _authSateProvider;

    #endregion
    
    #region Observable properties

    private string _loading;
    public string Loading
    {
        get => _loading ;
        set => SetProperty(ref _loading, value );
    }
    
    private string _errorServer;
    public string ErrorServer
    {
        get => _errorServer ;
        set => SetProperty(ref _errorServer, value );
    }
    
    public LoginRequest LoginRequest{ get; set; }
    
    public CreateUserRequest CreateUserRequest { get; set; }
    
    #endregion

    #region Consytructor

    public UserState(UserService userService, NavigationManager navigationManager,
        AuthSateProvider authSateProvider)
    {
        LoginRequest = new LoginRequest();
        CreateUserRequest = new CreateUserRequest();
        
        
        _service = userService;
        _navigationManager = navigationManager;
        _authSateProvider = authSateProvider;
    }

    #endregion

    #region Methods

    private async Task Submit(Func<Task<BaseHttpResponse<CreateUserResponse>>> action)
    {
        ErrorServer = null;
        Loading = DefaultTextLoad;
        
        var response = await action.Invoke();
        
        // Operation is success. Set token
        if (response.ResultStatus == BaseResultStatus.Success)
        {
            FormatResult.Token = response.Data.Token;
            
            // Save user section 
            _authSateProvider.CurrentUser = new ApplicationUser()
            {
                IsAuthenticated = true,
                User = response.Data
            };
            Loading = null;
            
            _authSateProvider.NotifyStateChanged();
            
            _navigationManager.NavigateTo(AppRoutingEnum.Brewery.GetEnumDescription());
            return;
        }

        Loading = null;
        ErrorServer = response.Reason;
    }

    public async  Task SubmitLogin()=> await Submit(  () =>  _service.LoginAsync(LoginRequest));
    
    public async  Task  SubmitRegister() =>  await Submit(  () =>  _service.CreateuserAsync(CreateUserRequest));

    #endregion
   
}