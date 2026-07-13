using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Code_Ruins.ViewModels;
using SkiaSharp;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Code_Ruins;

public partial class IntroducePage : UserControl
{
    private int index = 0;
    private bool isOuting = false;
    public IntroducePage()
    {
        InitializeComponent();
    }

    private async void ChattingBox_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (!isOuting)
        {
            if (index == (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage].Length)
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
                (DataContext as MainWindowViewModel).RecentPage = (DataContext as MainWindowViewModel).MainGamePage;
                return;

            }
            (DataContext as MainWindowViewModel).ChattingResource.RecentImage = new Bitmap((DataContext as MainWindowViewModel).ChattingResource.ChattingImage[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][index]);

            //打字机效果
            isOuting = true;
            for (int i = 0; i <= (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][index].Length; i++)
            {
                if (isOuting)
                {
                    ChattingTextBlock.Text = (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][index][0..i];
                    await Task.Delay(100);
                }
                else
                {
                    ChattingTextBlock.Text = (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][index];
                    break;
                }
                
            }
            isOuting = false;

            index++;
        }
        else
        {
            isOuting = false;
        }
        

        
    }
}