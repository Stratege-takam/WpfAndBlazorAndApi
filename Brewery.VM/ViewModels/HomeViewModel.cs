using Brewery.VM.Enums;

namespace Brewery.VM.ViewModels;

public class HomeViewModel : ViewModelCommon
{
    private PageEnum? _currentPage;
    public PageEnum CurrentPage
    {
        get => _currentPage ??  PageEnum.HomePage;
        set => SetProperty(ref _currentPage, value ); 
    }

    public HomeViewModel()
    {
        // Update right page in home (home, login, register)
        Register<PageEnum>(MessageEnum.MsgNavigationPage, (PageEnum page) =>
        {
            CurrentPage = page;
        });
    }

}