using Elia.Core.BaseModel;
using Elia.Shares.Controls;
using Elia.Shares.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using Elia.Share.WPF.Controls;
using Elia.Share.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Elia.Share.WPF.BaseClasses {
    public enum ApplicationBaseMessages {
        MSG_REFRESH_DATA
    }

    public abstract partial class ApplicationBase<U> : ApplicationRoot
        where U : class, new()
    { 
        public static U CurrentUser { get; protected set; }

        public static void SetUser(U user) {
            CurrentUser = user;
        }

        public static bool IsLoggedIn { get => CurrentUser != null; }

    }

    public partial class ApplicationRoot : Application {
        protected static readonly Messenger messenger = new();
        protected static readonly Dictionary<object, List<Tuple<Enum, Guid>>> ids = new();

        public static string CurrentLang { get;  set; }

        public static string IMAGE_PATH {
            get {
                // vérifie d'abord si on trouve un dossier images à côté de l'exécutable
                var path = Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/images");
                if (!Directory.Exists(path)) {
                    // Si ce n'est pas le cas, on feinte en se basant sur le stack trace pour retrouver le path du code source du projet
                    // C'est ce qui sera utilisé quand on est dans VS en mode design.
                    // (voir https://stackoverflow.com/a/20999702)
                    var trace = new StackTrace(true);
                    foreach (var frame in trace.GetFrames()) {
                        path = Path.GetFullPath(Path.GetDirectoryName(frame.GetFileName()) + "/images");
                        if (Directory.Exists(path))
                            return path;
                    }
                    path = null;
                }
                return path;
            }
        }

        public static string GetAbsolutePicturePath(string relativePath) {
            return Path.Combine(IMAGE_PATH, relativePath);
        }



       
        protected virtual void OnRefreshData() {
            //Console.WriteLine(GetType().FullName + "." + nameof(OnRefreshData));
        }

        public ApplicationRoot() {
            Register(this, ApplicationBaseMessages.MSG_REFRESH_DATA, () => OnRefreshData());
        }

     
        protected static Type MapViewModelToView(Type viewModelType) {
            var viewTypeName = viewModelType.FullName.Replace("ViewModel", "View");
            return viewModelType.Assembly.GetType(viewTypeName, true);
        }

        public static void NavigateTo<T, U>() where T : ViewModelBase<U> where U : class, new()
        {
            var win = Current.MainWindow;
            var newWindow = (WindowBase)Activator.CreateInstance(MapViewModelToView(typeof(T)));

            if (newWindow != win)
            {
                Current.MainWindow = newWindow;
                    Current.MainWindow.Show();
                if (win != null)
                    win.Close();
            }
        }
        
        public static void NavigateTo<T>()
            where T : WindowBase
        {
            var win = Current.MainWindow;
            var newWindow = (WindowBase)ViewModelBase.ServiceProvider.GetRequiredService<T>();
            
            if (newWindow?.Name != win?.Name)
            {
                Current.MainWindow = newWindow;
                Current.MainWindow.Show();
                if (win != null)
                    win.Close();
            }
        }

        public static object ShowDialog<T, U>(params object[] args)
            where U : Track, new()
            where T : DialogViewModelBase<U> 
        {
            var frm = (DialogWindowBase)Activator.CreateInstance(MapViewModelToView(typeof(T)), args);
            frm.ShowDialog();
            return frm.GetViewModel<U>().DialogResult;
        }

        public static void ChangeCulture(string culture) {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            var oldWindow = Current.MainWindow;
            if (oldWindow != null) {
                var type = oldWindow.GetType();
                Window newWindow = (Window)Activator.CreateInstance(type);
                newWindow.Show();
                Current.MainWindow = newWindow;
                oldWindow.Close();
            }
        }

        public static void Register(object owner, Enum message, Action callback) {
            var id = messenger.Register(message, callback);
            if (!ids.ContainsKey(owner))
                ids[owner] = new List<Tuple<Enum, Guid>>();
            ids[owner].Add(new Tuple<Enum, Guid>(message, id));
        }

        public static void Register<T>(object owner, Enum message, Action<T> callback) {
            var id = messenger.Register<T>(message, callback);
            if (!ids.ContainsKey(owner))
                ids[owner] = new List<Tuple<Enum, Guid>>();
            ids[owner].Add(new Tuple<Enum, Guid>(message, id));
        }

        public static void NotifyColleagues(Enum message, object parameter) {
            messenger.NotifyColleagues(message, parameter);
        }

        public static void NotifyColleagues(Enum message) {
            messenger.NotifyColleagues(message);
        }

        public static void UnRegister(object owner) {
            if (ids.ContainsKey(owner))
                foreach (var tuple in ids[owner])
                    messenger.UnRegister(tuple.Item1, tuple.Item2);
        }
    }
}
