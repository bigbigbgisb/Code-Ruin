using Avalonia.Media.Imaging;
using Code_Ruins.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins
{
    public partial class MainGamePage
    {
        private bool isOuting;

        private int chattingIndex;
        private bool isInteractPressed;
        private string interactKey;
        void CheckAndTriggerTask()
        {
            //任务区域
            if (tasks[0].Value == 0 && maps[recentMap][tileY][tileX] == 2)
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
            //这中间是有很多else if的嗷
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
                if (chattingIndex == (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage].Length)
                {
                    ChattingBox.IsVisible = false;
                    chattingIndex = 0;
                    return;
                }
                else
                {
                    (DataContext as MainWindowViewModel).ChattingResource.RecentImage = new Bitmap((DataContext as MainWindowViewModel).ChattingResource.ChattingImage[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex]);

                    isOuting = true;
                    for (int i = 0; i <= (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex].Length; i++)
                    {
                        if (isOuting)
                        {
                            ChattingTextBlock.Text = (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex][0..i];
                            await Task.Delay(100);
                        }
                        else
                        {
                            ChattingTextBlock.Text = (DataContext as MainWindowViewModel).ChattingResource.ChattingText[(DataContext as MainWindowViewModel).ChattingResource.RecentStage][chattingIndex];
                            break;
                        }



                    }
                    isOuting = false;
                    Debug.WriteLine(chattingIndex);
                    chattingIndex++;

                }
            }
            else
            {
                isOuting = false;
                Debug.WriteLine(chattingIndex);

            }
        }

        void InteractInit()
        {
            isOuting = false;
            chattingIndex = 0;
            isInteractPressed = false;
            interactKey = "";
        }
    }


}
