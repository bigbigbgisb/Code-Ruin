using Avalonia.Controls.Platform;
using Code_Ruins.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Code_Ruins.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        ThemeViewModel _themeViewModel = new();

        [ObservableProperty]
        ChattingResource _chattingResource = new();

        [ObservableProperty]
        SnackBarViewModel _snackBarViewModel = new();

        [ObservableProperty]
        StartPage _startPage = new();

        [ObservableProperty]
        object? _recentPage;

        [ObservableProperty]
        MainGamePage _mainGamePage = new();

        [ObservableProperty]
        CodeEditor _codeEditor = new();

        [ObservableProperty]
        ChipCodeViewModel _chipCodeViewModel = new();

        [ObservableProperty]
        IntroducePage introducePage = new();

        


        public MainWindowViewModel()
        {
            StartPage.DataContext = this;
            IntroducePage.DataContext = this;
            MainGamePage.DataContext = this;
            RecentPage = StartPage;
            
            
        }
    }
    
    
}
