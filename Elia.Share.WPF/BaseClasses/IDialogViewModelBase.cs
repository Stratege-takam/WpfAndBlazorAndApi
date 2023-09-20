using System;

namespace Elia.Share.WPF.BaseClasses
{
    public interface IDialogViewModelBase {
        event Action DoClose;
        object DialogResult { get; set; }
    }
}
