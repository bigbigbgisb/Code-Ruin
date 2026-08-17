using Code_Ruins.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public static class Command
    {
        private static MainWindowViewModel? _vm;

        public static void Init(MainWindowViewModel vm)
        {
            if (vm == null)
            {
                throw new NullReferenceException();
            }
            _vm = vm;
        }

        public static void SafeViewModelMethod(Action action)
        {

            try
            {
                action();
            }
            catch
            {
                //总不可能重新开了个MW吧
            }
        }
        public static void ShowCodeEditor()
        {
            SafeViewModelMethod(() =>
            {
                _vm.ShowCodeEditor();
            });

        }
        public static void HideCodeEditor()
        {
            
            SafeViewModelMethod(() =>
            {
                _vm.HideCodeEditor();
                
            });

        }

        public static void ShowWiki()
        {
            SafeViewModelMethod(() =>
            {
                _vm.ShowWiki();
            });

        }
        public static void HideWiki()
        {

            SafeViewModelMethod(() =>
            {
                _vm.HideWiki();

            });

        }
    }
}
