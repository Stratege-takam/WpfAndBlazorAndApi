using System;
using System.Windows;
using System.Windows.Controls;
using Elia.Core.Attributes;
using Elia.Share.WPF.BaseClasses;
using Elia.Share.WPF.Controls;

namespace Brewery.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    [Injectable]
    public partial class MainWindow : WindowBase
    {
        public MyTestService MyTestService { get; set; }
        public MainWindow(MyTestService myTestService)
        {
            MyTestService = myTestService;
            InitializeComponent();
        }


   
    }
}