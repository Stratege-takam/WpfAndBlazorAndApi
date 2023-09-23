using Brewery.BL.Client.Business.Users;
using Brewery.ViewModel.Enums;
using Elia.Core.Attributes;
using Elia.Share.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.ViewModel.ViewModels;

[Injectable]
public class RegisterViewModel : LoginViewModel
{
    #region Private properties

    private UserService _bl;

    #endregion
    #region Properties Notify

    private string _firstname;
    public string Firstname
    {
        get => _firstname;
        set => SetProperty(ref _firstname, value, () => Validate(nameof(Email)));
    }
    
    #endregion
   
    
    public  RegisterViewModel(): base()
    {
        _bl = ServiceProvider.GetRequiredService<UserService>();
        OnSubmit = new RelayCommand(() =>
        {
           
            NotifyColleagues(MessageEnum.MsgDisplayBrewery, null);
        });
    }
    
    public override bool Validate(string currentField)
    {
        base.Validate();
        if ( nameof(Firstname) == currentField && string.IsNullOrEmpty(Firstname))
            AddError(nameof(Firstname), "Name is required");
        else if (Firstname.Length < 2)
            AddError(nameof(Firstname), "length must be at least 2");
        
        return !HasErrors;
    }
}