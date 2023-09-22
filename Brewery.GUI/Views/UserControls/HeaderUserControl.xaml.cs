using System;
using System.Windows;
using System.Windows.Controls;
using Elia.Share.WPF.BaseClasses;
using Elia.Shares.Controls;

namespace Brewery.GUI.Views.Layout;

public partial class HeaderUserControl : UserControlBase
{
    public HeaderUserControl()
    {
        InitializeComponent();

    }
    
    
    private void SwitchLanguage(string lang = null)
    {
        NotifyColleagues(App.Messages.MSG_SWITCH_LANG, lang);
    }

    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var item = sender as MenuItem;
        SwitchLanguage(item.Uid);
    }
}