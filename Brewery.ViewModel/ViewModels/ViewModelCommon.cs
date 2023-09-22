using Brewery.BL.Contracts.Responses.Users;
using Elia.Share.WPF.BaseClasses;

namespace Brewery.ViewModel.ViewModels;

public class ViewModelCommon:ViewModelBase<CreateUserOutput>
{
    public string CurrentLang => ApplicationRoot.CurrentLang;
}