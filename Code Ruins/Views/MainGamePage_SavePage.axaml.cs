using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Code_Ruins.ViewModels;

namespace Code_Ruins.Views;

public partial class MainGamePage_SavePage : UserControl
{
    private MainWindowViewModel? mwvm;
    private readonly MainGamePage _mainGamePage;
    public MainGamePage_SavePage(MainGamePage mainGamePage)
    {
        InitializeComponent();
        Loaded += MainGamePage_SavePage_Loaded;
        _mainGamePage = mainGamePage;
    }

    private void MainGamePage_SavePage_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        mwvm = (DataContext as MainWindowViewModel)!;
    }

    private void Back_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (this.Parent is ContentControl contentControl)
        {
            contentControl.Content = null;
            if ((App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow is MainWindow mainWindow)
            {
                mainWindow.TaskTipBar.IsVisible = (DataContext as MainWindowViewModel)!.TaskTipViewModel.IsVisible;
            }

        }
    }

    private void Save_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _mainGamePage.SaveGame();
    }

    async void QuitToStart()
    {
        await (DataContext as MainWindowViewModel)!.ShowThenHideCurtainAsync(1000, () => { (DataContext as MainWindowViewModel)!.RecentPage = (DataContext as MainWindowViewModel)!.StartPage; });
    }

    private void Quit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        QuitToStart();
    }

    private void SaveAndQuit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _mainGamePage.SaveGame();
        QuitToStart();
    }
}