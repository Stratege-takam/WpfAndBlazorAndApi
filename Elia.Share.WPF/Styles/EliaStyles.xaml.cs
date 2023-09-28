using System.Windows.Controls;

namespace Elia.Share.WPF.Styles;

public partial class EliaStyles
{
    private void TextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) {
        ((TextBox)sender).SelectAll();
    }
}