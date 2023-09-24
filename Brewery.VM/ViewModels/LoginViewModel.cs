using System;
using System.Net.Mail;
using System.Windows.Input;
using Brewery.BL.Client.Business.Users;
using Brewery.BL.Client.Contracts.Inputs.Users;
using Brewery.VM.Enums;
using Elia.Core.Utils;
using Elia.Share.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.VM.ViewModels;

public class LoginViewModel : ViewModelCommon
{
    
    #region Private properties

    private UserService _bl;

    #endregion
    
    #region Properties Notify
    
    private string _errorServer;
    public string ErrorServer
    {
        get => _errorServer;
        set => SetProperty(ref _errorServer, value); 
    }

    private string _email = "test@elia.be";
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value , () => Validate()); 
    }
    
    private string _password = "azerty";
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value, () => Validate()) ; 
    }

    #endregion
    
    public  ICommand OnSubmit { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public  LoginViewModel(): base()
    {
        _bl = ServiceProvider.GetRequiredService<UserService>();
        OnSubmit = new RelayCommand(async () =>
        {
            if (Validate())
            {
                Loading = DefaultTextLoad;
                ErrorServer = "";
               var response = await  _bl.LoginAsync(new LoginInput()
                {
                    Email = Email,
                    Password = Password
                });
               
               // Login success. Set token
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
        ClearErrors();
        
        if ( string.IsNullOrEmpty(Email))
            AddError(nameof(Email), "Email is required");
        else if (!IsValid(Email))
            AddError(nameof(Email), "The email address is not valid");
        if (string.IsNullOrEmpty(Password))
            AddError(nameof(Password), "Password is required");
        else if ( Password.Length < 5)
            AddError(nameof(Password), "length must be at least 4");

        return !HasErrors;
    }



    private bool IsValid(string email)
    {
        try
        {
            MailAddress m = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}