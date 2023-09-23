using System;
using System.Net.Mail;
using System.Windows.Input;
using Brewery.BL.Client.Business.Users;
using Brewery.BL.Client.Contracts.Inputs.Users;
using Brewery.ViewModel.Enums;
using Elia.Core.Utils;
using Elia.Share.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.ViewModel.ViewModels;

public class LoginViewModel : ViewModelCommon
{
    
    #region Private properties

    private UserService _bl;

    #endregion
    
    #region Properties Notify

    private string _email;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value , () => Validate(nameof(Email))); 
    }
    
    private string _password;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value, () => Validate(nameof(Password)) ); 
    }

    #endregion
    
    public  ICommand OnSubmit { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bl"></param>
    public  LoginViewModel(): base()
    {
        _bl = ServiceProvider.GetRequiredService<UserService>();
        OnSubmit = new RelayCommand(async () =>
        {
            if (Validate())
            {
               var response = await  _bl.LoginAsync(new LoginInput()
                {
                    Email = Email,
                    Password = Password
                });
               
               if (response.ResultStatus == BaseResultStatus.Success)
               {
                   FormatResult.Token = response.Data.Token;
                   NotifyColleagues(MessageEnum.MsgDisplayBrewery, response.Data);
               }
            }
        });
    }

    public override bool Validate(string currentField)
    {
        ClearErrors();
        
        if (nameof(Email) == currentField && string.IsNullOrEmpty(Email))
            AddError(nameof(Email), "Email is required");
        else if (nameof(Email) == currentField && !IsValid(Email))
            AddError(nameof(Email), "The email address is not valid");
        if (nameof(Email) == currentField && string.IsNullOrEmpty(Password))
            AddError(nameof(Password), "Password is required");
        else if (nameof(Email) == currentField && Password.Length < 5)
            AddError(nameof(Password), "length must be at least 4");

        return !HasErrors;
    }

    public override bool Validate()
    {
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