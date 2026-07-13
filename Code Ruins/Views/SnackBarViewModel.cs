using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins.Views
{
    public partial class SnackBarViewModel : ObservableObject
    {
        [ObservableProperty]
        string _recentAchivement = "";
        [ObservableProperty]
        int _offsetX = -400;

        [RelayCommand]
        private async Task ShowSnackBar()
        {
            for(int i = 0; i < 40; i++)
            {
                OffsetX+=10;
                await Task.Delay(16);
            }
        }
        [RelayCommand]
        private async Task HideSnackBar()
        {
            for (int i = 0; i < 40; i++)
            {
                OffsetX -= 10;
                await Task.Delay(16);
            }
        }
    }   
}
