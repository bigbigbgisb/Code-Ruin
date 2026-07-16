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

    
    
    

    public MainGamePage()
    {

        InitializeComponent();
        this.Focusable = true;
        this.Focus();
        

    }



    private void SceneOne_PointerMoved(object? sender, PointerEventArgs e)
    {

        CalculateAndMoveBackground(e);

    }

    //DispatcherTimer是一个计时器，Interval是每隔多少时间触发一次绑定事件，望学会。
    private async void UserControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Init();

    }
    private void WalkingLoop(object? sender,EventArgs e)
    {
        //16ms per round
        PlayerAnimation();
    }
    private void Loop(object? sender, EventArgs e)
    {
        BalanceImageSize();
        CheckAndTriggerTask();
        CalculateTilePosition();
        CalculateAndChangeOffsetValue();
        CalculateAndClampViewport();
        UpdateMapTranslation();

    }

    private void UserControl_KeyDown(object? sender, KeyEventArgs e)
    {
        UpdateMovementKeyDownState(e);
        UpdateTaskKeyState(e);
    }

    private void UserControl_KeyUp(object? sender, KeyEventArgs e)
    {
        UpdateMovementKeyUpState(e);

    }

    
    private async void ChattingBox_PointerPressed(object? sender, PointerPressedEventArgs e)
    {

        await OuttingSentence();
    }

    
}