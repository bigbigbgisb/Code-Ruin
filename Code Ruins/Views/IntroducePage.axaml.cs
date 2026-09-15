using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

using Code_Ruins.ViewModels;
using Code_Ruins.Views;

using SkiaSharp;

using System.IO;

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Code_Ruins;

public partial class IntroducePage : UserControl {

    private MainWindowViewModel? mwvm;


    public IntroducePage() {
        InitializeComponent();
        Loaded += IntroducePage_Loaded;

    }

    private async void IntroducePage_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
        await Task.Delay(2500);
        mwvm = (DataContext as MainWindowViewModel)!;
        mwvm!.ChattingBox.ShowAndResetChattingBox();

        WaitForOutingDoneAndChangeScene();

    }



    private async void WaitForOutingDoneAndChangeScene() {
        await Utils.WaitUntil(() => mwvm!.ChattingBoxViewModel.IsOutingDone);
        await mwvm!.ShowThenHideCurtainAsync(1000, () => { mwvm!.RecentPage = new MainGamePage(false) { DataContext = this.DataContext }; });


    }
}
