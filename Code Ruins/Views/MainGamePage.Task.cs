using Avalonia.Media.Imaging;
using Code_Ruins.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Code_Ruins
{
    public partial class MainGamePage
    {

        private string recentTask;
        private bool isInTaskZone;
        private List<KeyValuePair<string, int>> tasks;
        async void Task_DataStructures()
        {
            Debug.WriteLine("进入任务 数据结构");
            recentTask = "DataStructures";
            //赶紧把任务状态归掉
            maps[recentMap][5][12] = 0;
            maps[recentMap][5][13] = 0;
            maps[recentMap][6][12] = 0;
            maps[recentMap][6][13] = 0;

            (DataContext as MainWindowViewModel).CodeEditor = new();
            (DataContext as MainWindowViewModel).CodeEditor.DataContext = DataContext;
            (DataContext as MainWindowViewModel).CodeEditor.Show();
            (DataContext as MainWindowViewModel).CodeEditor.Topmost = true;
            (DataContext as MainWindowViewModel).ChipCodeViewModel.ChipCode = """
            using System;
            int temper = 56;
            double waterLevel = 22.75;
            string type = "DEADZONE";
            Console.WriteLine(temper);
            Console.WriteLine(waterLevel);
            Console.WriteLine(type);

            """;
            (DataContext as MainWindowViewModel).ChattingResource.RecentStage = "DataStructures";
            (DataContext as MainWindowViewModel).ChipCodeViewModel.StandardOutput = "36\r\n22.75\r\nSAFEZONE\r\n";
            chattingIndex = 0;
            ChattingBox.IsVisible = true;
            //找时间抽到Init去
            (DataContext as MainWindowViewModel).ChipCodeViewModel.PropertyChanged += ChipCodeViewModel_PropertyChanged;
        }
        void ChipCodeViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            MainWindowViewModel vm = (DataContext as MainWindowViewModel);
            if (e.PropertyName == nameof(vm.ChipCodeViewModel.IsCodeSuccessful))
            {
                if (recentTask == "DataStructures")
                {
                    Debug.WriteLine("通过!");
                    (DataContext as MainWindowViewModel).ChipCodeViewModel.IsCodeSuccessful = false;
                    recentTask = "None";
                    (DataContext as MainWindowViewModel).ChattingResource.RecentStage = "DataStructuresSuccess";
                    ChattingBox.IsVisible = true;
                    SceneOnePlatform.Source = new Bitmap("Assets/Pictures/SceneOnePlatformSuccess.png");

                }
            }
        }

        void TaskInit()
        {
            recentTask = "";
            isInTaskZone = false;
            tasks = new() {
                new KeyValuePair<string, int>("DataStructures",0)
            };
        }
    }
}
