using Brewery.BL.Contracts.Responses.Users;
using Elia.Share.WPF.BaseClasses;

namespace Brewery.VM.ViewModels;

public class DialogViewModelCommon: DialogViewModelBase<CreateUserOutput>
{
    public static string DefaultTextLoad = "Loading...";
    
    #region Properties Notify

    private string _currentLang;
    public string CurrentLang
    {
        get => _currentLang ?? ApplicationRoot.CurrentLang;
        set => SetProperty(ref _currentLang, value ); 
    }
    
    private string _loading;
    public string Loading
    {
        get => _loading ;
        set => SetProperty(ref _loading, value ); 
    }

    
    private string _userFirstname;
    public string UserFirstname
    {
        get => _userFirstname ?? CurrentUser?.Firstname;
        set => SetProperty(ref _userFirstname, value ); 
    }

    #endregion
}