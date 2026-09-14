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

    private MainGamePage_SavePage? _savePage;
    private readonly bool _isFromSave;
    public MainGamePage(bool isFromSave)
    {

        InitializeComponent();
        _isFromSave = isFromSave;
        this.Focusable = true;
        this.Focus();


    }



    private void SceneOne_PointerMoved(object? sender, PointerEventArgs e)
    {

        CalculateAndMoveBackground(e);

    }

    private async void UserControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _savePage = new(this) { DataContext = this.DataContext };
        if (!_isFromSave)
        {
            await InitAsync();
        }
        else
        {
            LoadSaveInit();
            mwvm!.ChattingResource.RecentImage = new Bitmap("Assets/Pictures/Dummy.png");
        }
        
        
    }
    private void WalkingLoop(object? sender, EventArgs e)
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



    private void IdeButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ToggleIde();
    }

    private void WikiButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ToggleWiki();
    }

    private void Quit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ToggleQuit();
    }

}