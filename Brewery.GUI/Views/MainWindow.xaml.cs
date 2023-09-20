using System.Windows;
using Elia.Core.Attributes;

namespace Brewery.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    [Injectable]
    public partial class MainWindow : Window
    {
        public MyTestService MyTestService { get; set; }
        public MainWindow(MyTestService myTestService)
        {
            MyTestService = myTestService;
            InitializeComponent();
        }
    }
}