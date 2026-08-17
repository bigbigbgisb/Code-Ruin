using Avalonia.Controls.Platform;
using Code_Ruins.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CSScriptLib;

namespace Code_Ruins.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public ChattingResource ChattingResource { get; } = new();

        [ObservableProperty]
        ThemeViewModel _themeViewModel = new();

        [ObservableProperty]
        bool _isFpsVisible = true;

        [ObservableProperty]
        Code_Ruins.Views.Settings _settings = new();

        [ObservableProperty]
        WikiContentResource _wikiContentResource = new();

        [ObservableProperty]
        SnackBarViewModel _snackBarViewModel = new();

        [ObservableProperty]
        StartPage _startPage = new();

        [ObservableProperty]
        object? _recentPage;

        [ObservableProperty]
        private CodeWiki_HomePage codeWiki_HomePage = new();

        [ObservableProperty]
        private CodeWiki_QuestionsPage _codeWiki_QuestionsPage = new();

        [ObservableProperty]
        MainGamePage _mainGamePage = new();

        [ObservableProperty]
        CodeEditor _codeEditor = new();

        [ObservableProperty]
        ChipCodeViewModel _chipCodeViewModel = new();

        [ObservableProperty]
        CodeWiki _codeWiki = new();

        [ObservableProperty]
        BaseSettingsViewModel _baseSettingsViewModel = new();

        [ObservableProperty]
        IntroducePage introducePage = new();

        


        public MainWindowViewModel()
        {
            StartPage.DataContext = this;
            CodeWiki.DataContext = this;
            CodeWiki_QuestionsPage.DataContext = this;
            CodeWiki_HomePage.DataContext = this;
            IntroducePage.DataContext = this;
            MainGamePage.DataContext = this;
            CodeEditor.DataContext = this;
            RecentPage = StartPage;
            CodeEditor.Topmost = true;
            CodeWiki.Topmost = true;


        }

        
        public void ShowCodeEditor()
        {
            CodeEditor.DataContext = this;
            CodeEditor.Topmost = true;
            CodeEditor.WindowState = Avalonia.Controls.WindowState.Normal;
            CodeEditor.Show();
        }

        public void HideCodeEditor()
        {
            CodeEditor.WindowState = Avalonia.Controls.WindowState.Minimized;
        }
        public void ShowWiki()
        {
            CodeWiki.DataContext = this;
            CodeWiki.Topmost = true;
            CodeWiki.WindowState = Avalonia.Controls.WindowState.Normal;
            CodeWiki.Show();
        }

        public void HideWiki()
        {
            CodeWiki.WindowState = Avalonia.Controls.WindowState.Minimized;
        }
    }
    
    
}
