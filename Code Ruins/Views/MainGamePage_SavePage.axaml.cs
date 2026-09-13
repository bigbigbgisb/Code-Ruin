using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Code_Ruins.ViewModels;

namespace Code_Ruins.Views;

public partial class MainGamePage_SavePage : UserControl
{
    public MainGamePage_SavePage()
    {
        InitializeComponent();
    }

    private void Back_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (this.Parent is ContentControl contentControl)
        {
            contentControl.Content = null;
            if ((App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow is  MainWindow mainWindow) 
            {
                mainWindow.TaskTipBar.IsVisible = (DataContext as MainWindowViewModel)!.TaskTipViewModel.IsVisible;
            }
            
        }
    }
}