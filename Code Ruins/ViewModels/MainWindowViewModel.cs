using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Avalonia.Controls.Platform;
using Avalonia.Platform;

using Code_Ruins.Views;

using CommunityToolkit.Mvvm.ComponentModel;

using CSScriptLib;

namespace Code_Ruins.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        ChattingResource _chattingResource = new();

        [ObservableProperty]
        int? _screenHeight = null;

        [ObservableProperty]
        int _curtainHeight = 0;

        [ObservableProperty]
        bool _curtainIsVisible = false;

        [ObservableProperty]

        ThemeViewModel _themeViewModel = new();

        [ObservableProperty]
        ChattingBoxViewModel _chattingBoxViewModel = new();

        [ObservableProperty]
        ChattingBox _chattingBox = new();

        [ObservableProperty]
        TaskTipViewModel _taskTipViewModel = new();


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
        CodeEditor _codeEditor = new();

        [ObservableProperty]
        ChipCodeViewModel _chipCodeViewModel = new();

        [ObservableProperty]
        CodeWiki _codeWiki = new();

        [ObservableProperty]
        EndPage _endPage = new();

        [ObservableProperty]
        BaseSettingsViewModel _baseSettingsViewModel = new();





        public MainWindowViewModel()
        {
            StartPage.DataContext = this;
            EndPage.DataContext = this;
            CodeWiki.DataContext = this;
            CodeWiki_QuestionsPage.DataContext = this;
            CodeWiki_HomePage.DataContext = this;
            CodeEditor.DataContext = this;
            ChattingBox.DataContext = this;
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

        public async Task ShowCurtainAsync()
        {
            if (ScreenHeight is null) { return; }


            CurtainIsVisible = true;
            while (!(CurtainHeight > ScreenHeight))
            {
                CurtainHeight += 30;
                await Task.Delay(16);
            }

        }

        public async Task HideCurtainAsync()
        {
            if (!CurtainIsVisible) { return; }
            if (ScreenHeight is null) { return; }

            while (CurtainHeight >= 30)
            {
                CurtainHeight -= 30;
                await Task.Delay(16);
            }
            CurtainHeight = 0;
            CurtainIsVisible = false;

        }

        public async Task ShowThenHideCurtainAsync(int interval, Action? action = null)
        {
            await ShowCurtainAsync();
            if (action is not null) action();
            await Task.Delay(interval);
            await HideCurtainAsync();
        }

        public async void ShowThenHideSnackBar(string message)
        {//显示成就
            SnackBarViewModel.RecentAchievement = message;
            SnackBarViewModel.ShowSnackBarCommand.Execute(null);
            //隐藏成就
            await Task.Delay(4000);
            SnackBarViewModel.HideSnackBarCommand.Execute(null);
            SnackBarViewModel.RecentAchievement = "";
        }
    }


}
