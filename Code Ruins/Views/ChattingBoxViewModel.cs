using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public partial class ChattingBoxViewModel : ObservableObject
    {
        [ObservableProperty]
        bool _isOuting = false;

        [ObservableProperty]
        int _chattingIndex = 0;

        [ObservableProperty]
        bool _isOutingDone = false;

        [ObservableProperty]
        string _recentText = "";
    }
}
