using Brewery.Contract.Contracts.Responses.Users;

namespace Brewery.Web.Helpers.ViewModels;

public class ApplicationUserViewModel: NotifyPropertyChanged
{
    public CreateUserResponse User { get; set; }

    private bool _isAuthenticated;
    public bool IsAuthenticated
    {
        get => _isAuthenticated ;
        set => SetProperty(ref _isAuthenticated, value );
    }
    
    
    private Dictionary<string, string> _claims;
    public Dictionary<string, string> Claims  
    {
        get => _claims ;
        set => SetProperty(ref _claims, value );
    }
    
    
}