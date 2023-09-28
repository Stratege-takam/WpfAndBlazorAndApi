using System;

namespace Elia.Share.WPF.BaseClasses
{
    public abstract class DialogViewModelBase : ViewModelBase, IDialogViewModelBase
    {

        private object _dialogResult;
        public object DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                DoClose.Invoke();
            }
        }

        public event Action DoClose;
    } 
    public abstract class DialogViewModelBase<U> : ViewModelBase<U>, IDialogViewModelBase
        where U : class, new()
    {

        private object _dialogResult;
        public object DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                DoClose.Invoke();
            }
        }

        public event Action DoClose;
    }
}
