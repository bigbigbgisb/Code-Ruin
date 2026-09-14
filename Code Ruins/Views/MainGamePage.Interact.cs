using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;

using Code_Ruins.ViewModels;
using Code_Ruins.Views;

namespace Code_Ruins
{
    public partial class MainGamePage
    {
        private bool isInteractPressed = false;
        private string interactKey = string.Empty;


        void CheckAndTriggerTask()
        {
            //任务区域
            if (tasks[recentTask] == 0 && maps[recentMapIndex].Value[tileY][tileX] == 2)
            {

                if (recentTask == "DataStructures")
                {
                    ActionPromptText.Text = "按下【E】开始任务";
                    interactKey = "E";
                    ActionPrompt.IsVisible = true;
                    isInTaskZone = true;
                    if (isInteractPressed)
                    {
                        isInteractPressed = false;

                        Task_DataStructures();
                    }
                }
                else if (recentTask == "InputAndCalculate")
                {
                    ActionPromptText.Text = "按下【E】开始任务";
                    interactKey = "E";
                    ActionPrompt.IsVisible = true;
                    isInTaskZone = true;
                    if (isInteractPressed)
                    {
                        isInteractPressed = false;
                        Task_InputAndCalculate();
                    }
                }


            }


            else
            {
                ActionPrompt.IsVisible = false;
                isInTaskZone = false;
                isInteractPressed = false;
                interactKey = "";
            }
        }


        void InteractInit()
        {
            isInteractPressed = false;
            interactKey = "";

        }

        void ToggleIde()
        {
            if (mwvm!.CodeEditor.WindowState != Avalonia.Controls.WindowState.Minimized)
            {
                mwvm!.HideCodeEditor();
            }
            else
            {
                mwvm!.ShowCodeEditor();
            }
        }
        void ToggleWiki()
        {
            if (mwvm!.CodeWiki.WindowState != Avalonia.Controls.WindowState.Minimized)
            {
                mwvm!.HideWiki();
            }
            else
            {
                mwvm!.ShowWiki();
            }
        }

        void ToggleQuit()
        {
            if ((App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow is not MainWindow mainWindow)
            {
                return;
            }
            if (OverridePage.Content is null)
            {
                OverridePage.Content = _savePage;
                mainWindow.TaskTipBar.IsVisible = false;

            }
            else
            {
                OverridePage.Content = null;
                mainWindow.TaskTipBar.IsVisible = (DataContext as MainWindowViewModel)!.TaskTipViewModel.IsVisible;   
            }
        }


    }


}