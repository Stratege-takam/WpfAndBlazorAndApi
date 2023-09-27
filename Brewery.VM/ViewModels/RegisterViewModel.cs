using Brewery.BL.Contracts.Requests.Users;
using Brewery.Services.Services.Users;
using Brewery.VM.Enums;
using Elia.Core.Attributes;
using Elia.Core.Utils;
using Elia.Share.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.VM.ViewModels;

[Injectable]
public class RegisterViewModel : LoginViewModel
{
    #region Private properties

    private UserService _bl;

    #endregion
    #region Properties Notify

    private string _firstname = "test";
    public string Firstname
    {
        get => _firstname;
        set => SetProperty(ref _firstname, value, () => Validate());
    }
    
    #endregion
   
    
    public  RegisterViewModel(): base()
    {
        _bl = ServiceProvider.GetRequiredService<UserService>();
        OnSubmit = new RelayCommand(async () =>
        {
            ErrorServer = null;
            if (Validate())
            {
                Loading = DefaultTextLoad;
               
                var response = await  _bl.CreateuserAsync(new CreateUserRequest()
                {
                    Email = Email,
                    Firstname = Firstname,
                    Password = Password
                });
               
                // register success. Set token
                if (response.ResultStatus == BaseResultStatus.Success)
                {
                    FormatResult.Token = response.Data.Token;
                    NotifyColleagues(MessageEnum.MsgDisplayBrewery, response.Data);
                   
                    Loading = null;
                    return;
                }

                Loading = null;
                ErrorServer = response.Reason;
            }
        }, () => Validate());
    }
    
    public override bool Validate()
    {
        base.Validate();
        if ( string.IsNullOrEmpty(Firstname))
            AddError(nameof(Firstname), "Name is required");
        else if (Firstname.Length < 2)
            AddError(nameof(Firstname), "length must be at least 2");
        
        return !HasErrors;
    }
}