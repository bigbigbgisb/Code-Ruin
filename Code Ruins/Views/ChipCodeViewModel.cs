using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace Code_Ruins.Views
{
    public partial class ChipCodeViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _standardOutput = "";

        [ObservableProperty]
        private bool? _isCodeSuccessful = null;

        [ObservableProperty]
        private TextDocument _chipCodeDocument = new();

        [ObservableProperty]
        private string preInput = "";

        
    }


}
