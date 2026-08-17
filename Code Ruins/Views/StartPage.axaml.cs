using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Labs.Gif;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Code_Ruins.ViewModels;
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


        Curtain.IsVisible = true;
        while (true)
        {
            Curtain.Height += 30;
            await Task.Delay(16);
            if (Curtain.Height > Bounds.Height)
            {
                break;
            }
        }
        await Task.Delay(2000);
        (DataContext as MainWindowViewModel).ChattingResource.RecentStage = "Introduce";
        (DataContext as MainWindowViewModel).RecentPage = (DataContext as MainWindowViewModel).IntroducePage;
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
        
        await MessageBoxManager.GetMessageBoxStandard("警告 - 进入命令行", "程序崩溃。退出码:0xffffffff。请进入命令行修改具体设置。\n错误编号: 0xF1A7n错误类型: 系统调用失败 (NT_STATUS_INVALID_SYSTEM_SERVICE)").ShowWindowDialogAsync((App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow);
        
        OverridePage.Content = (DataContext as MainWindowViewModel).Settings;
    }
}