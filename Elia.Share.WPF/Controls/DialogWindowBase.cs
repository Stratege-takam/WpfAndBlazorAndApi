using System.Windows.Controls.Primitives;
using Elia.Share.WPF.BaseClasses;
using Elia.Share.WPF.Controls;

namespace Elia.Shares.Controls {
    public class DialogWindowBase : WindowBase {

        public DialogWindowBase() {
            DataContextChanged += (o, e) => {
                if (e.OldValue != null)
                    (e.OldValue as IDialogViewModelBase).DoClose -= DialogWindowBase_DoClose;
                if (e.NewValue != null)
                    (e.NewValue as IDialogViewModelBase).DoClose += DialogWindowBase_DoClose;
            };
        }

        private void DialogWindowBase_DoClose() {
            Close();
        }

        public DialogViewModelBase<U> GetViewModel<U>() where U : class, new()
        {
            return (DialogViewModelBase<U>)DataContext;
        }
    }
}
