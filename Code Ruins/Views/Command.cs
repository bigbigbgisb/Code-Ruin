using Code_Ruins.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public static class Command
    {
        private static MainWindowViewModel? _vm;

        public static void Init(MainWindowViewModel? vm)
        {
            if (vm is not MainWindowViewModel viewModel) { throw new ArgumentNullException(nameof(vm)); }
            _vm = viewModel;
        }

        public static void SafeViewModelMethod(Action action)
        {

            try
            {
                action();
            }
            catch(Exception ex)
            {
                //总不可能重新开了个MW吧
                Log.Error("MainWindow的ViewModel为null", ex);
            }
        }
        public static void ShowCodeEditor()
        {
            SafeViewModelMethod(() => _vm!.ShowCodeEditor());

        }
        public static void HideCodeEditor()
        {

            SafeViewModelMethod(() => _vm!.HideCodeEditor());

        }

        public static void ShowWiki()
        {
            SafeViewModelMethod(() => _vm!.ShowWiki());

        }
        public static void HideWiki()
        {

            SafeViewModelMethod(() => _vm!.HideWiki());

        }
    }
}