namespace Elia.Share.WPF.BaseClasses
{
    public abstract class ViewModelBase<U> : ObservableBase
        where U : class, new()
    {

        public ViewModelBase() : base() {
            ApplicationRoot.Register(this, ApplicationBaseMessages.MSG_REFRESH_DATA, OnRefreshData);
        }

        protected virtual void OnRefreshData() { }

        public virtual void SaveAction() { }

        public virtual void CancelAction() { }


        public static U CurrentUser
        {
            get => ApplicationBase<U>.CurrentUser;
        }


        public static bool IsLoggedIn => ApplicationBase<U>.IsLoggedIn;

    }
}
