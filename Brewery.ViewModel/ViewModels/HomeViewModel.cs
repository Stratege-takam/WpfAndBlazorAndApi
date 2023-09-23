﻿using Brewery.ViewModel.Enums;

namespace Brewery.ViewModel.ViewModels;

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
        Register<PageEnum>(MessageEnum.MsgDisplayRightHomePage, (PageEnum page) =>
        {
            CurrentPage = page;
        });
    }

}