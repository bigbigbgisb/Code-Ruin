using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Labs.Gif;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;

using Code_Ruins.ViewModels;
using Code_Ruins.Views;

using MsBox.Avalonia;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Ursa.Controls;

using static System.Net.Mime.MediaTypeNames;

namespace Code_Ruins;

public partial class StartPage : UserControl
{
    public StartPage()
    {
        InitializeComponent();
    }

    private async void Start_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        await (DataContext as MainWindowViewModel)!.ShowThenHideCurtainAsync(1000, () =>
        {
            (DataContext as MainWindowViewModel)!.ChattingResource.RecentStage = "Introduce";
            (DataContext as MainWindowViewModel)!.RecentPage = new IntroducePage() { DataContext = this.DataContext };
        });

    }

    private async void LoadSave_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (Path.Exists(Path.Combine(AppContext.BaseDirectory, "CodeRuinsSave", "Save.txt")))
        {
            await (DataContext as MainWindowViewModel)!.ShowThenHideCurtainAsync(1000, async () =>
            {
                (DataContext as MainWindowViewModel)!.ChattingResource.RecentStage = "Introduce";
                (DataContext as MainWindowViewModel)!.RecentPage = new MainGamePage(true) { DataContext = this.DataContext };
            });
            
        }
        else
        {
            if (LoadSave.Tag is not bool)
            {
                LoadSave.Tag = false;
            }
            if (LoadSave.Tag is bool b && b)
            {
                return;
            }
            LoadSave.Content = "你还没有存档~";
            LoadSave.Tag = true;
            await Task.Delay(1000);
            LoadSave.Tag = false;
            LoadSave.Content = "再续前缘";
        }
    }

    private void Grid_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        var point = e.GetPosition(StartGrid);
        double centerX = Bounds.Width / 2;
        double centerY = Bounds.Height / 2;
        double offsetX = (point.X - centerX);
        double offsetY = (point.Y - centerY);
        ImageBackground4.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        ImageBackground4.RenderTransform = new TranslateTransform(offsetX / 10, offsetY / 10);
        ImageBackground3.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        ImageBackground3.RenderTransform = new TranslateTransform(offsetX / 20, offsetY / 20);
        ImageBackground2.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        ImageBackground2.RenderTransform = new TranslateTransform(offsetX / 30, offsetY / 30);
        ImageBackground1.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        ImageBackground1.RenderTransform = new TranslateTransform(offsetX / 40, offsetY / 40);
    }

    private async void Settings_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if ((App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow is not MainWindow mainWindow) { return; }
        await MessageBoxManager.GetMessageBoxStandard("警告 - 进入命令行", "程序崩溃。退出码:0xffffffff。请进入命令行修改具体设置。\n错误编号: 0xF1A7n错误类型: 系统调用失败 (NT_STATUS_INVALID_SYSTEM_SERVICE)").ShowWindowDialogAsync(mainWindow);
        OverridePage.Content = (DataContext as MainWindowViewModel)!.Settings;
    }

    private void Quit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if ((App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime) is not IClassicDesktopStyleApplicationLifetime lifetime) { Log.Warning("异常退出，生命周期未找到，使用Environment.Exit"); Environment.Exit(0); return; }
        lifetime.Shutdown();
    }

    
}