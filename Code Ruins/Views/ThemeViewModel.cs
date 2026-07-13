using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public partial class ThemeViewModel : ObservableObject
    {
        [ObservableProperty]
        string _black = "#000000";
        [ObservableProperty]
        string _darkFillColor = "#4B4B4B";
        [ObservableProperty]
        string _darkBorderColor = "#373737";

        [ObservableProperty]
        string _lightFillColor = "#FFFFFF";
        [ObservableProperty]
        string _lightForeground = "#FFFFFF";
        [ObservableProperty]
        string _opacityDarkFillColor = "#804B4B4B";
        [ObservableProperty]
        string _opacityColor = "#00FFFFFF";
        [ObservableProperty]
        string _neonGreen = "#39FF14";

    }
}
