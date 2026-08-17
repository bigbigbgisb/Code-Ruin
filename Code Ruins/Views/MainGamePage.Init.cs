using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using Code_Ruins.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins
{
    public partial class MainGamePage
    {
        private DispatcherTimer _gameLoopTimer;
        private DispatcherTimer _walkingTimer;
        private MainWindowViewModel mwvm;
        async void Init()
        {
            //初始化
            PublicInit();
            ViewportControllerInit();
            TaskInit();
            MovementInit();
            MapInit();
            InteractInit();
            //开对话
            ResetAndShowChattingBox();

            mwvm.ChattingResource.RecentStage = "Tutorial";
            //显示成就
            mwvm.SnackBarViewModel.RecentAchivement = "目前进度:第一次进入游戏~";
            mwvm.SnackBarViewModel.ShowSnackBarCommand.Execute(null);
            //隐藏成就
            await Task.Delay(4000);
            mwvm.SnackBarViewModel.HideSnackBarCommand.Execute(null);
            mwvm.SnackBarViewModel.RecentAchivement = "";

            
        }

        void PublicInit()
        {
            //显性转换
            mwvm = DataContext as MainWindowViewModel;
            //新建计时器对象
            _gameLoopTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            _walkingTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            //绑定并开启计时器
            _walkingTimer.Tick += WalkingLoop;
            _walkingTimer.Start();
            _gameLoopTimer.Tick += Loop;
            _gameLoopTimer.Start();

        }

        
    }
}
