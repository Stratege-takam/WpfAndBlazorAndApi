﻿using Elia.Share.WPF.BaseClasses;

namespace Elia.Share.WPF.Controls {
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
