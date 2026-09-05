using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

using Code_Ruins.ViewModels;
using Code_Ruins.Views;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins
{
    public partial class MainGamePage
    {
        private readonly DispatcherTimer _gameLoopTimer = new();
        private readonly DispatcherTimer _walkingTimer = new();
        private MainWindowViewModel? mwvm;
        async Task InitAsync()
        {
            //初始化
            PublicInit();
            ViewportControllerInit();
            MapInit();
            MovementInit();
            InteractInit();
            TaskInit();

            //别管为什么，问就是等幕布下来，再等500+ms
            await Task.Delay(2500);
            mwvm!.ShowThenHideSnackBar("目前进度:第一次进入游戏~");
            //开对话
            mwvm!.ChattingResource.RecentStage = "Tutorial";
            mwvm!.ChattingBox.ShowAndResetChattingBox();

            await Utils.WaitUntil(() => mwvm!.ChattingBoxViewModel.IsOutingDone);
            mwvm!.TaskTipViewModel.TipText = "任务 - 前往寻找【机器】并重新驱动";
            await mwvm!.TaskTipViewModel.ShowTaskTipAsync();
            
            


        }

        void PublicInit()
        {
            //显性转换
            mwvm = (DataContext as MainWindowViewModel)!;
            //新建计时器对象
            _gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16);
            _walkingTimer.Interval = TimeSpan.FromMilliseconds(100);
            //绑定并开启计时器
            _walkingTimer.Tick += WalkingLoop;
            _walkingTimer.Start();
            _gameLoopTimer.Tick += Loop;
            _gameLoopTimer.Start();

        }


    }
}