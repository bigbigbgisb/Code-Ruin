using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Code_Ruins.ViewModels
{
    public partial class BaseSettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        int _typingSpeed = 100;

        [ObservableProperty]
        int _playerSpeed = 3;
    }
}
