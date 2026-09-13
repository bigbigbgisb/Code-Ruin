using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace Code_Ruins.Views;

public partial class TaskTipViewModel : ObservableObject
{
    [ObservableProperty]
    string _tipText = string.Empty;

    [ObservableProperty]
    int _margin = 400;

    [ObservableProperty]
    bool _isVisible = false;

    public async Task ShowTaskTipAsync()
    {
        for (int i = 0; i < 40; i++)
        {
            Margin -= 10;
            await Task.Delay(16);
        }
        IsVisible = true;

    }
    
    public async Task HideTaskTipAsync()
    {
        for (int i = 0; i < 40; i++)
        {
            Margin += 10;
            await Task.Delay(16);
        }
        IsVisible = false; 
    }
}
