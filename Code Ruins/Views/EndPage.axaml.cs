using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using Code_Ruins.ViewModels;

using System.Threading.Tasks;

namespace Code_Ruins.Views
{
    public partial class EndPage : UserControl
    {
        public EndPage()
        {
            InitializeComponent();
            Loaded += EndPage_Loaded;
        }

        private async void EndPage_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Delay(3000);
            for (int i = 0; i < Message.Bounds.Height; i++)
            {
                Message.Margin = new Thickness(50, 50 - i, 50, 50);
                await Task.Delay(16);
            }
        }

        private void ScrollViewer_PointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
        {
            e.Handled = true;
        }

        async void Quit()
        {
            await (DataContext as MainWindowViewModel)!.ShowThenHideCurtainAsync(1000, () => { (DataContext as MainWindowViewModel)!.RecentPage = (DataContext as MainWindowViewModel)!.StartPage; });
        }

        private void Quit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Quit();
        }

        private void UserControl_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == Avalonia.Input.Key.Escape)
            {
                Quit();
            }
        }
    }
}