using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

using Code_Ruins.ViewModels;

using Microsoft.CodeAnalysis.Scripting;

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Ursa.Controls;

namespace Code_Ruins.Views
{
    public partial class ChattingBox : Window
    {
        private MainWindowViewModel? mwvm;
        public ChattingBox()
        {
            InitializeComponent();

            Loaded += ChattingBox_Loaded;
        }

        public void ShowAndResetChattingBox()
        {
            mwvm!.ChattingBoxViewModel.ChattingIndex = 0;
            mwvm!.ChattingBoxViewModel.IsOutingDone = false;
            mwvm!.ChattingBoxViewModel.RecentText = string.Empty;
            mwvm!.ChattingResource.RecentImage = new Bitmap("Assets/Pictures/Dummy.png");
            IsVisible = true;
            WindowState = WindowState.Normal;
            Show();
            Focus();

        }

        private async void ChattingBoxBorder_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            
            await OutingSentence();
        }

        private void ChattingBox_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            mwvm = (DataContext as MainWindowViewModel)!;
            var topLevel = GetTopLevel(this);
            var screen = topLevel?.Screens?.Primary;
            if (screen is not null)
            {
                Position = new PixelPoint(0, (int)(screen.Bounds.Height - this.Bounds.Height));
                Width = screen.Bounds.Width;
            }
            Topmost = true;
        }

        async Task OutingSentence()
        {
            if (!mwvm!.ChattingBoxViewModel.IsOuting)
            {
                if (mwvm!.ChattingBoxViewModel.ChattingIndex == mwvm!.ChattingResource.ChattingText[mwvm!.ChattingResource.RecentStage].Length)
                {
                    mwvm!.ChattingBoxViewModel.ChattingIndex = 0;
                    mwvm!.ChattingBoxViewModel.IsOuting = false;
                    mwvm!.ChattingBoxViewModel.RecentText = string.Empty;
                    mwvm!.ChattingBoxViewModel.IsOutingDone = true;
                    if ((App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow is not MainWindow mainWindow) { return; }
                    mainWindow.Activate();
                    Log.Information($"已输出完毕{mwvm!.ChattingResource.RecentStage}的所有内容");
                    WindowState = WindowState.Minimized;
                    IsVisible = false;
                    return;

                }
               
                mwvm!.ChattingResource.RecentImage = new Bitmap((mwvm!.ChattingResource.ChattingImage[mwvm!.ChattingResource.RecentStage][mwvm!.ChattingBoxViewModel.ChattingIndex]) ?? "Assets/Pictures/Dummy.png");
                
                //打字机效果

                mwvm!.ChattingBoxViewModel.IsOuting = true;
                Log.Information($"目前输出{mwvm!.ChattingResource.RecentStage}的第{mwvm!.ChattingBoxViewModel.ChattingIndex + 1}句话");
                for (int i = 0; i <= mwvm!.ChattingResource.ChattingText[mwvm!.ChattingResource.RecentStage][mwvm!.ChattingBoxViewModel.ChattingIndex].Message.Length; i++)
                {
                    if (mwvm!.ChattingBoxViewModel.IsOuting)
                    {
                        mwvm!.ChattingBoxViewModel.RecentText = mwvm!.ChattingResource.ChattingText[mwvm!.ChattingResource.RecentStage][mwvm!.ChattingBoxViewModel.ChattingIndex].Message[0..i];
                        await Task.Delay(mwvm!.BaseSettingsViewModel.TypingSpeed);
                    }
                    else
                    {
                        mwvm!.ChattingBoxViewModel.RecentText = mwvm!.ChattingResource.ChattingText[mwvm!.ChattingResource.RecentStage][mwvm!.ChattingBoxViewModel.ChattingIndex].Message;
                        Log.Information($"{mwvm!.ChattingResource.RecentStage}的第{mwvm!.ChattingBoxViewModel.ChattingIndex + 1}句话被打断");
                        break;
                    }

                }

                mwvm!.ChattingResource.ChattingText[mwvm!.ChattingResource.RecentStage][mwvm!.ChattingBoxViewModel.ChattingIndex].Function();
                mwvm!.ChattingBoxViewModel.IsOuting = false;

                mwvm!.ChattingBoxViewModel.ChattingIndex++;
                Log.Information($"{mwvm!.ChattingResource.RecentStage}的第{mwvm!.ChattingBoxViewModel.ChattingIndex + 1}句话输出完毕");

            }
            else
            {
                mwvm!.ChattingBoxViewModel.IsOuting = false;
            }
        }
    }
}