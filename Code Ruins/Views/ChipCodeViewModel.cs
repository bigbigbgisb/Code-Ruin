using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Code_Ruins.Views
{
    public partial class ChipCodeViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _chipCode = "";

        [ObservableProperty]
        private string _standardOutput = "";

        [ObservableProperty]
        private bool _isCodeSuccessful = false;
    }
}
