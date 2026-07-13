using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.Threading;
using Code_Ruins.ViewModels;
using Code_Ruins.Views;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Ursa.Controls;

namespace Code_Ruins;

public partial class MainGamePage : UserControl
{
    private List<KeyValuePair<string, int>> tasks = new() {
        new KeyValuePair<string, int>("DataStructures",0)
    };

    private int recentTaskIndex = 0;

    private List<Avalonia.Controls.Image> SceneOneImages;
    private Dictionary<string, int[][]> maps = new()
    {
        //1 stands for blocking tile
        //2 stands for task tile
        //3 stands for item tile
        ["Map1"] = [
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
            
            
            
            ]
    };
    private string recentTask = "";
    private bool isInTaskZone = false;
    private bool isInteractPressed = false;
    private string interactKey = "";
    private List<string> characterWalkingImage = new() { "Assets/ManAction/LMan1.png", "Assets/ManAction/LMan2.png", "Assets/ManAction/LMan3.png", "Assets/ManAction/UMan1.png", "Assets/ManAction/UMan2.png", "Assets/ManAction/UMan3.png", "Assets/ManAction/RMan1.png", "Assets/ManAction/RMan2.png", "Assets/ManAction/RMan3.png", "Assets/ManAction/DMan1.png", "Assets/ManAction/Dman2.png", "Assets/ManAction/DMan3.png" };
    private Dictionary<string, string> characterStandingImage = new()
    {
        ["Left"] = "Assets/ManAction/LMan2.png",
        ["Up"] = "Assets/ManAction/UMan2.png",
        ["Right"] = "Assets/ManAction/RMan2.png",
        ["Down"] = "Assets/ManAction/DMan2.png"
    };
    private string lastDirection = "Right";
    private int leftIndex = 0;
    private string recentMap = "Map1";
    private int upIndex = 3;
    private int rightIndex = 6;
    private int downIndex = 9;
    private DispatcherTimer _gameLoopTimer;
    private DispatcherTimer _walkingTimer;
    private DispatcherTimer _chattingTimer;
    private bool isOuting = false;
    private int offsetX = 0;
    private int offsetY = 0;
    private int tileX = 0;
    private int tileY = 0;
    private bool isUp;
    private bool isDown;
    private bool isLeft;
    private bool isRight;
    private int chattingIndex = 0;

    public MainGamePage()
    {

        InitializeComponent();
        this.Focusable = true;
        this.Focus();
        SceneOneImages = new() { SceneOne1, SceneOne2, SceneOne3, SceneOne4, SceneOne5, SceneOne6 };

    }



    private void SceneOne_PointerMoved(object? sender, PointerEventArgs e)
    {

        var point = e.GetPosition(SceneOne);
        double centerX = Bounds.Width / 2;
        double centerY = Bounds.Height / 2;
        double offsetX = (point.X - centerX);
        double offsetY = (point.Y - centerY);

        int i = SceneOneImages.Count;
        foreach (Image image in SceneOneImages)
        {
            image.RenderTransform = new TranslateTransform(offsetX / (i * 10), offsetY / (i * 10));
            i--;
        }

    }

    //DispatcherTimer是一个计时器，Interval是每隔多少时间触发一次绑定事件，望学会。
    private async void UserControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        (DataContext as MainWindowViewModel).ChattingResource.RecentStage = "Tutorial";
        (DataContext as MainWindowViewModel).SnackBarViewModel.RecentAchivement = "目前进度:第一次进入游戏~";
        (DataContext as MainWindowViewModel).SnackBarViewModel.ShowSnackBarCommand.Execute(null);
        SceneOnePlatform.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        SceneOnePlatform.RenderTransform = new TranslateTransform(0, 0);
        foreach (Image image in SceneOneImages)
        {
            image.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
        }

        _gameLoopTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(16)
        };
        _walkingTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _walkingTimer.Tick += WalkingLoop;
        _walkingTimer.Start();
        _gameLoopTimer.Tick += Loop;
        _gameLoopTimer.Start();
        await Task.Delay(4000);
        (DataContext as MainWindowViewModel).SnackBarViewModel.HideSnackBarCommand.Execute(null);
        (DataContext as MainWindowViewModel).SnackBarViewModel.RecentAchivement = "";


    }
    private void WalkingLoop(object? sender,EventArgs e)
    {
        if (isUp)
        {
            if (upIndex >= 5)
            {
                upIndex = 3;
            }
            lastDirection = "Up";
            Character.Source = new Bitmap(characterWalkingImage[upIndex]);
            upIndex++;
        }
        if (isDown)
        {
            if (downIndex >= 12)
            {
                downIndex = 9;
                
            }
            lastDirection = "Down";
            Character.Source = new Bitmap(characterWalkingImage[downIndex]);
            downIndex++;
        }
        if (isLeft)
        {
            if (leftIndex >= 2)
            {
                leftIndex = 0;
            }
            lastDirection = "Left";
            Character.Source = new Bitmap(characterWalkingImage[leftIndex]);
            leftIndex++;
        }
        if (isRight)
        {
            if (rightIndex >= 8)
            {
                rightIndex = 6;
            }
            lastDirection = "Right";
            Character.Source = new Bitmap(characterWalkingImage[rightIndex]);
            rightIndex++;
        }
        if (!isLeft && !isRight && !isUp && !isDown)
        {
            Character.Source = new Bitmap(characterStandingImage[lastDirection]);
        }
    }
    private void Loop(object? sender, EventArgs e)
    {
        SceneOnePlatform.Width = Bounds.Width * 2.0;
        SceneOnePlatform.Height = SceneOnePlatform.Width * (353.0 / 800.0);
        //我就假装窗口不会被拖动吧。嘿嘿。
        //X，Y from (0,0)，1=32px
        //图片1像素放大后等于屏幕2像素
        tileX = (int)Math.Floor((double)(-offsetX / 32 / 2));
        tileY = (int)Math.Floor((double)(5 + -offsetY / 32 / 2));
        //任务区域
        if (tasks[0].Value == 0 && maps[recentMap][tileY][tileX] == 2)
        {
            ActionPromptText.Text = "按下【E】开始任务";
            interactKey = "E";
            ActionPrompt.IsVisible = true;
            isInTaskZone = true;
            if (isInteractPressed)
            {
                isInteractPressed = false;
                Task_DataStructures();
            }
            
        }
        //这中间是有很多else if的嗷
        else
        {
            ActionPrompt.IsVisible = false;
            isInTaskZone = false;
            isInteractPressed = false;
            interactKey = "";
        }
        if (isUp)
        {
            int nextOffsetY = offsetY + 3;
            int nextTileY = (int)Math.Floor((double)(5 + -nextOffsetY / 32 / 2));
            try
            {
                if (maps[recentMap][nextTileY][tileX] == 1)
                {
                    //pass
                }
                else
                {
                    offsetY += 3;
                }
            }
            catch
            {
                //Out of map index
            }
            
            
            
        }
        if (isDown)
        {
            int nextOffsetY = offsetY - 3;
            int nextTileY = (int)Math.Floor((double)(5 + -nextOffsetY / 32 / 2));
            try
            {
                if (maps[recentMap][nextTileY][tileX] == 1)
                {
                    //pass
                }
                else
                {
                    offsetY -= 3;
                }
            }
            
            catch
            {
                //Out of map index
            }


        }
        if (isLeft)
        {
            int nextOffsetX = offsetX + 3;
            int nextTileX = (int)Math.Floor((double)(-nextOffsetX / 32 / 2));
            try
            {
                if (maps[recentMap][tileY][nextTileX] == 1)
                {
                    //pass
                }
                else
                {
                    offsetX += 3;
                }
            }
            catch
            {
                //Out of map index
            }

        }
        if (isRight)
        {
            int nextOffsetX = offsetX - 3;
            int nextTileX = (int)Math.Floor((double)(-nextOffsetX / 32 / 2));
            try
            {
                if (maps[recentMap][tileY][nextTileX] == 1)
                {
                    //pass
                }
                else
                {
                    offsetX -= 3;
                }
            }
            catch
            {
                //Out of map index
            }

        }
        
        double maxX = Math.Abs(SceneOnePlatform.Bounds.Width - Bounds.Width);
        double maxY = Math.Abs((SceneOnePlatform.Bounds.Height - Bounds.Height) / 2);
        double minX = 0;
        offsetX = (int)Math.Clamp(offsetX, -maxX, minX);
        offsetY = (int)Math.Clamp(offsetY, -maxY, maxY + 100);
        SceneOnePlatform.RenderTransform = new TranslateTransform(offsetX, offsetY);

    }

    private void UserControl_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            isUp = true;
        }
        if (e.Key == Key.S)
        {
            isDown = true;
        }
        if (e.Key == Key.A)
        {
            isLeft = true;
        }
        if (e.Key == Key.D)
        {
            isRight = true;
        }
        if (!isInTaskZone)
        {
            return;
        }
        if (e.Key == Enum.Parse<Key>(interactKey) && isInTaskZone)
        {
            isInteractPressed = true;
        }
    }

    private void UserControl_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            isUp = false;
        }
        if (e.Key == Key.S)
        {
            isDown = false;
        }
        if (e.Key == Key.A)
        {
            isLeft = false;
        }
        if (e.Key == Key.D)
        {
            isRight = false;
        }

    }

    private async Task OuttingSentence()
    {
        if (!isOuting)
        {
            if (chattingIndex == (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage].Length)
            {
                ChattingBox.IsVisible = false;
                chattingIndex = 0;
                return;
            }
            else
            {
                (DataContext as MainWindowViewModel).ChattingResource.RecentImage = new Bitmap((DataContext as MainWindowViewModel).ChattingResource.ChattingImage[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex]);

                isOuting = true;
                for (int i = 0; i <= (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex].Length; i++)
                {
                    if (isOuting)
                    {
                        ChattingTextBlock.Text = (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex][0..i];
                        await Task.Delay(100);
                    }
                    else
                    {
                        ChattingTextBlock.Text = (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex];
                        break;
                    }



                }
                isOuting = false;
                Debug.WriteLine(chattingIndex);
                chattingIndex++;

            }
        }
        else
        {
            isOuting = false;
            Debug.WriteLine(chattingIndex);

        }
    }
    private async void ChattingBox_PointerPressed(object? sender, PointerPressedEventArgs e)
    {

        await OuttingSentence();
        
    }

    private async void Task_DataStructures()
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
        (DataContext as MainWindowViewModel).ChipCodeViewModel.PropertyChanged += ChipCodeViewModel_PropertyChanged;
    }

    private void ChipCodeViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
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
}