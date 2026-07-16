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
        async void Init()
        {
            (DataContext as MainWindowViewModel).ChattingResource.RecentStage = "Tutorial";
            //显示成就
            (DataContext as MainWindowViewModel).SnackBarViewModel.RecentAchivement = "目前进度:第一次进入游戏~";
            (DataContext as MainWindowViewModel).SnackBarViewModel.ShowSnackBarCommand.Execute(null);

            //初始化
            PublicInit();
            ViewportControllerInit();
            TaskInit();
            MovementInit();
            MapInit();
            InteractInit();

            //隐藏成就
            await Task.Delay(4000);
            (DataContext as MainWindowViewModel).SnackBarViewModel.HideSnackBarCommand.Execute(null);
            (DataContext as MainWindowViewModel).SnackBarViewModel.RecentAchivement = "";

            
        }

        void PublicInit()
        {
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
