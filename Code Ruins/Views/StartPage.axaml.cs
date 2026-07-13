using Avalonia;
using Avalonia.Controls;
using Avalonia.Labs.Gif;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Code_Ruins.ViewModels;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Code_Ruins;

public partial class StartPage : UserControl
{
    public StartPage()
    {
        DataContext = this.DataContext as MainWindowViewModel;
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
        ImageBackground4.RenderTransform = new TranslateTransform(offsetX/10, offsetY/10);
        ImageBackground3.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        ImageBackground3.RenderTransform = new TranslateTransform(offsetX / 20, offsetY/20);
        ImageBackground2.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        ImageBackground2.RenderTransform = new TranslateTransform(offsetX / 30, offsetY/30);
        ImageBackground1.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        ImageBackground1.RenderTransform = new TranslateTransform(offsetX / 40, offsetY/40);
    }

    private void UserControl_SizeChanged(object? sender, SizeChangedEventArgs e)
    {

    }
}