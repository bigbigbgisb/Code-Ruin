using Avalonia.Controls;
using Code_Ruins.ViewModels;
namespace Code_Ruins.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }
    }
}