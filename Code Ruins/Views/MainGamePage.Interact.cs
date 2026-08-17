using Avalonia.Media.Imaging;
using Code_Ruins.ViewModels;
using Code_Ruins.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins
{
    public partial class MainGamePage
    {
        private bool isOuting;
        private bool isOutingDone;
        private int chattingIndex;
        private bool isInteractPressed;
        private string interactKey;


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
        async Task OuttingSentence()
        {
            if (!isOuting)
            {
                if (chattingIndex == mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage].Length)
                {
                    chattingIndex = 0;
                    isOuting = false;
                    ChattingBox.IsVisible = false;
                    isOutingDone = true;
                    Log.Information($"MainGamePage.Interact 已输出完毕{mwvm.ChattingResource.RecentStage}的所有内容");
                    return;

                }

                if (mwvm.ChattingResource.ChattingImage[mwvm.ChattingResource.RecentStage][chattingIndex] != null)
                {
                    mwvm.ChattingResource.RecentImage = new Bitmap(mwvm.ChattingResource.ChattingImage[mwvm.ChattingResource.RecentStage][chattingIndex]);
                }
                else
                {
                    mwvm.ChattingResource.RecentImage = new Bitmap("Assets/Pictures/Dummy.png");
                }


                //打字机效果

                isOuting = true;
                Log.Information($"MainGamePage.Interact 目前输出{mwvm.ChattingResource.RecentStage}的第{chattingIndex + 1}句话");
                for (int i = 0; i <= mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][chattingIndex].Message.Length; i++)
                {
                    if (isOuting)
                    {
                        ChattingTextBlock.Text = mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][chattingIndex].Message[0..i];
                        await Task.Delay(mwvm.BaseSettingsViewModel.TypingSpeed);
                    }
                    else
                    {
                        ChattingTextBlock.Text = mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][chattingIndex].Message;
                        Log.Information($"MainGamePage.Interact {mwvm.ChattingResource.RecentStage}的第{chattingIndex + 1}句话被打断");
                        break;
                    }

                }

                mwvm.ChattingResource.ChattingText[mwvm.ChattingResource.RecentStage][chattingIndex].Function();
                isOuting = false;

                chattingIndex++;
                Log.Information($"MainGamePage.Interact {mwvm.ChattingResource.RecentStage}的第{chattingIndex + 1}句话输出完毕");

            }
            else
            {
                isOuting = false;
            }
        }

        void ResetAndShowChattingBox()
        {
            chattingIndex = 0;
            isOutingDone = false;
            ChattingTextBlock.Text = "";
            mwvm.ChattingResource.RecentImage = new Bitmap("Assets/Pictures/Dummy.png");
            ChattingBox.IsVisible = true;
        }

        void InteractInit()
        {
            isOuting = false;
            isOutingDone = false;
            chattingIndex = 0;
            isInteractPressed = false;
            interactKey = "";

        }

        void ToggleIde()
        {
            if (mwvm.CodeEditor.WindowState != Avalonia.Controls.WindowState.Minimized)
            {
                mwvm.HideCodeEditor();
            }
            else
            {
                mwvm.ShowCodeEditor();
            }
        }
        void ToggleWiki()
        {
            if (mwvm.CodeWiki.WindowState != Avalonia.Controls.WindowState.Minimized)
            {
                mwvm.HideWiki();
            }
            else
            {
                mwvm.ShowWiki();
            }
        }


    }


}
