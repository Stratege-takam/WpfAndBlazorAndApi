﻿using System.Windows.Input;
using Brewery.VM.Enums;
using Elia.Share.WPF.Helpers;

namespace Brewery.VM.ViewModels;

public class HeaderViewModel : ViewModelCommon
{
    public ICommand OnSwitchLanguageCommand { get; set; }
    public ICommand OnDisplayPage { get; set; }

    public PageEnum BreweryPage
    {
        get => PageEnum.BreweryPage;
    }
    public PageEnum HomePage
    {
        get => PageEnum.HomePage;
    }
    
    public PageEnum RegisterPage
    {
        get => PageEnum.RegisterPage;
    }
    
    public PageEnum LoginPage
    {
        get => PageEnum.LoginPage;
    }
    
    public  HeaderViewModel(): base()
    {
        OnSwitchLanguageCommand = new RelayCommand<string>(language =>
        {
            CurrentLang = language;
            NotifyColleagues(MessageEnum.MsgSwitchLang, language);
        });
        
        OnDisplayPage = new RelayCommand<PageEnum>(page =>
        {
            NotifyColleagues(MessageEnum.MsgNavigationPage, page);
        });
        
        
    }
}