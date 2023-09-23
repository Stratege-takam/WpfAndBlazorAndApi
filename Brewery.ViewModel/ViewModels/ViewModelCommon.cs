using Brewery.BL.Contracts.Responses.Users;
using Elia.Share.WPF.BaseClasses;

namespace Brewery.ViewModel.ViewModels;

public class ViewModelCommon:ViewModelBase<CreateUserOutput>
{
    #region Properties Notify

    private string _currentLang;
    public string CurrentLang
    {
        get => _currentLang ?? ApplicationRoot.CurrentLang;
        set => SetProperty(ref _currentLang, value ); 
    }
    
    
    private string _userFirstname;
    public string UserFirstname
    {
        get => _userFirstname ?? CurrentUser?.Firstname;
        set => SetProperty(ref _userFirstname, value ); 
    }

    #endregion
}