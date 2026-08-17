using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Code_Ruins.ViewModels;
using Code_Ruins.Views;
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
    private MainWindowViewModel mwvm;


    public IntroducePage()
    {
        InitializeComponent();
        Loaded += IntroducePage_Loaded;
    }

    private void IntroducePage_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        mwvm = (DataContext as MainWindowViewModel);
    }

    private async void ChattingBox_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (!isOuting)
        {
            if (index == mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage].Length)
            {
                Log.Information("IntroducePage 已输出所有介绍内容");
                Curtain.IsVisible = true;
                index = 0;
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
                mwvm.RecentPage = mwvm.MainGamePage;

                return;

            }
            Log.Information($"IntroducePage 目前输出{mwvm.ChattingResource.RecentStage}的第{index+1}句话");
            if (mwvm.ChattingResource.ChattingImage[mwvm.ChattingResource.RecentStage][index] != null)
            {
                mwvm.ChattingResource.RecentImage = new Bitmap(mwvm.ChattingResource.ChattingImage[mwvm.ChattingResource.RecentStage][index]);
            }
            
            //打字机效果
            isOuting = true;

            for (int i = 0; i <= mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][index].Message.Length; i++)
            {
                if (isOuting)
                {
                    ChattingTextBlock.Text = mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][index].Message[0..i];
                    await Task.Delay(mwvm.BaseSettingsViewModel.TypingSpeed);
                }
                else
                {
                    ChattingTextBlock.Text = mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][index].Message;
                    Log.Information($"IntroducePage {mwvm.ChattingResource.RecentStage}的第{index + 1}句话被打断");
                    
                    break;
                }
                
            }
            
            
            isOuting = false;
            mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][index].Function();
            Log.Information($"IntroducePage {mwvm.ChattingResource.RecentStage}的第{index + 1}句话输出完毕");
            index++;
            
           
        }
        else
        {
            isOuting = false;
        }
        

        
    }
}