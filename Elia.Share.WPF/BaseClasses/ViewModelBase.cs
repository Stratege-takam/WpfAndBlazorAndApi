using System;

namespace Elia.Share.WPF.BaseClasses
{

    public abstract class ViewModelBase : ObservableBase
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceProvider ServiceProvider { get;  set; }
        
        protected virtual void OnRefreshData() { }

        public virtual void SaveAction() { }

        public virtual void CancelAction() { }
    }
    
    public abstract class ViewModelBase<U> : ViewModelBase
        where U : class, new()
    {
        public ViewModelBase() : base() {
            ApplicationRoot.Register(this, ApplicationBaseMessages.MSG_REFRESH_DATA, OnRefreshData);
        }

        public static U CurrentUser
        {
            get => ApplicationBase<U>.CurrentUser;
        }

        public static bool IsLoggedIn => ApplicationBase<U>.IsLoggedIn;

    }
}
