using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using AvaloniaEdit.Document;
using Code_Ruins.ViewModels;
using Code_Ruins.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ursa.Controls;

namespace Code_Ruins
{
    public partial class MainGamePage
    {

        private string recentTask;
        private bool isInTaskZone;
        private Dictionary<string, int> tasks;
        async void Task_DataStructures()
        {
            Debug.WriteLine("进入任务 数据结构");
            recentTask = "DataStructures";
            //赶紧把任务状态归掉
            maps[recentMapIndex].Value[5][12] = 0;
            maps[recentMapIndex].Value[5][13] = 0;
            maps[recentMapIndex].Value[6][12] = 0;
            maps[recentMapIndex].Value[6][13] = 0;

            //显示已移动到ChttingResource
            mwvm.ChipCodeViewModel.ChipCodeDocument.Text = """
            using System;
            int port = 0;   //端口 端口0无电量 端口1输出900.7V电压 端口2输出1000V电压
            double voltage = 0;    //启动机器需要 【900.7V】电压
            string task = "null";   //任务"A":修建建筑 任务"B":清理废墟 任务"C":拆除建筑
            bool isWorking = false;    //true对应开机 false对应关机
            Console.WriteLine("电源端口:");
            Console.WriteLine(port);    //输出端口号
            Console.WriteLine("电压控制(V):");
            Console.WriteLine(voltage);    //输出电压
            Console.WriteLine("启动状态:");
            Console.WriteLine(isWorking);   //输出工作状态
            Console.WriteLine("目前任务:");
            Console.WriteLine(task);    //输出目前任务

            """;

            mwvm.ChattingResource.RecentStage = "DataStructures";
            mwvm.ChipCodeViewModel.StandardOutput = "电源端口:\r\n1\r\n电压控制(V):\r\n900.7\r\n启动状态:\r\nTrue\r\n目前任务:\r\nB\r\n";
            mwvm.ChipCodeViewModel.PreInput = "";
            ResetAndShowChattingBox();
            
            
        }

        async void Task_InputAndCalculate()
        {
            Debug.WriteLine("进入任务 计算与输入");
            recentTask = "InputAndCalculate";
            //赶紧把任务状态归掉
            maps[recentMapIndex].Value[5][12] = 0;
            maps[recentMapIndex].Value[5][13] = 0;
            maps[recentMapIndex].Value[6][12] = 0;
            maps[recentMapIndex].Value[6][13] = 0;

            //显示已移动到ChttingResource
            mwvm.ChipCodeViewModel.ChipCodeDocument.Text = """
            using System;
            Console.Write("请输入身高:"); //使得机器输出不在末尾换行
            int height = (int)Console.ReadLine(); //读取输入
            Console.Write("请输入体重:"); //同上
            int weight = (int)Console.ReadLine();
            Console.Write("请输入年龄:");
            int age = (int)Console.ReadLine();
            int tax = 0;                                     
            tax = (int)(height+weight+age)*10;               麻烦帮我把这个改成身高*0.3+体重*0.5+年龄*0.6，再乘以6.18，谢谢!
            Console.WriteLine($"需要缴纳{tax}金币税务");       我随手记的一笔，要是跑不了，麻烦擦掉
            """;
            //从(height+weight+age)*10改成((height * 0.3 + weight * 0.5 + age * 0.6)*6.18)公式
            //从(int)改成int.Parse
            mwvm.ChattingResource.RecentStage = "InputAndCalculateA";
            mwvm.ChipCodeViewModel.StandardOutput = "请输入身高:请输入体重:请输入年龄:需要缴纳650金币税务\r\n";
            mwvm.ChipCodeViewModel.PreInput = "170\n70\n32\n";
            ResetAndShowChattingBox();
            await Utils.WaitUntil(() => isOutingDone);
            IdeButton.IsVisible = true;
            await Task.Delay(2000);
            mwvm.ChattingResource.RecentStage = "InputAndCalculateB";
            ResetAndShowChattingBox();
            await Utils.WaitUntil(() => isOutingDone);
            WikiButton.IsVisible = true;
            mwvm.ChattingResource.RecentStage = "InputAndCalculateC";
            ResetAndShowChattingBox();

        }





        void SceneTwo()
        {
            ChangeScene("Assets/Pictures/SceneTwoPlatform.png", "Assets/Pictures/Dummy.png", "InputAndCalculate", "ArriveAtSlum");
            Debug.WriteLine(recentMapIndex);
            ResetAndShowChattingBox();
        }

        void SceneThree()
        {
            MessageBox.ShowAsync("钓鱼，妈的!");
        }


        async void ChipCodeViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(mwvm.ChipCodeViewModel.IsCodeSuccessful))
            {

                
                if (recentTask == "DataStructures" && (mwvm.ChipCodeViewModel.IsCodeSuccessful ?? false))
                {
                    Debug.WriteLine("通过!");
                    TaskSuccess();
                    mwvm.ChattingResource.RecentStage = "DataStructuresSuccess";
                    ScenePlatform.Source = new Bitmap("Assets/Pictures/SceneOnePlatformSuccess.png");
                    ResetAndShowChattingBox();
                    mwvm.CodeEditor.WindowState = WindowState.Minimized;
                    await Utils.WaitUntil(() => isOutingDone); 
                    Log.Information("到达场景2");
                    SceneTwo();
                }
                if (recentTask == "InputAndCalculate" && (mwvm.ChipCodeViewModel.IsCodeSuccessful ?? false))
                {
                    Debug.WriteLine("通过!");
                    TaskSuccess();
                    mwvm.ChattingResource.RecentStage = "InputAndCalculateSuccess";
                    ResetAndShowChattingBox();
                    mwvm.CodeEditor.WindowState = WindowState.Minimized;
                    await Utils.WaitUntil(() => isOutingDone);
                    Log.Information("到达场景3");
                    SceneThree();
                }

            }
        }

        void TaskSuccess()
        {
            mwvm.ChipCodeViewModel.IsCodeSuccessful = false;
            tasks[recentTask] = 1;
        }



        void ChangeScene(string sceneBackgroundPath,string sceneBackgrondDecorationPath,string task,string stage)
        {

            ScenePlatform.Source = new Bitmap(sceneBackgroundPath);
            ScenePlatformDecoration.Source = new Bitmap(sceneBackgrondDecorationPath);
            mwvm.ChattingResource.RecentStage = stage;
            Debug.WriteLine("before add"+recentMapIndex);
            recentMapIndex = recentMapIndex + 1;
            Debug.WriteLine("after add" + recentMapIndex);
            recentTask = task;
            tileX = 0;
            tileY = 0;
            offsetX = 0;
            offsetY = 0;
            ActionPrompt.IsVisible = false;
        }

        void TaskInit()
        {
            recentTask = "DataStructures";
            isInTaskZone = false;
            tasks = new()
            {
                ["DataStructures"] = 0,
                ["InputAndCalculate"] = 0,
            };
            mwvm.ChipCodeViewModel.PropertyChanged += ChipCodeViewModel_PropertyChanged;
        }
    }
}
