using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Code_Ruins.ViewModels;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Ursa.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Code_Ruins.Views
{
    public partial class Settings
    {
        private Dictionary<string, Dictionary<string, CmdCommandInfomation>> commandReturns;

        void RunCommand(string text)
        {
            CommandHistory.Text += $"C:\\Wondows\\assembly\\GAC &0xFF; 32\\System.Data>   {text}\n";
            bool isFind = false;
            string[] commandAndArgs = text.Trim().ToUpper().Split(" ");
            foreach (var keyAndValue in commandReturns)
            {
                
                if (commandAndArgs[0] == keyAndValue.Key)
                {

                    if (keyAndValue.Key == text.Replace(" ", "").ToUpper())
                    {
                        CommandHistory.Text += keyAndValue.Value[""].ReturnValue + "\n";
                        keyAndValue.Value[""].Action(commandAndArgs[1..]);
                        isFind = true;
                        break;
                    }

                    if (keyAndValue.Value.TryGetValue(commandAndArgs[1], out var cmdCommandInfomation))
                    {
                        //对于参数有键的命令，如HELP XXX
                        CommandHistory.Text += keyAndValue.Value[commandAndArgs[1]].ReturnValue + "\n";
                        keyAndValue.Value[commandAndArgs[1]].Action(commandAndArgs[1..]);
                    }
                    else
                    {
                        Debug.WriteLine(13);
                        //对于自由参数的命令，如CHATTINGSPEED XXX
                        CommandHistory.Text += keyAndValue.Value[""].ReturnValue + "\n";
                        keyAndValue.Value[""].Action(commandAndArgs[1..]);
                    }




                    isFind = true;
                    break;
                }
            }
            if (!isFind)
            {
                CommandHistory.Text += $"'{string.Join(" ", commandAndArgs)}' 不是内部或外部命令，也不是可运行的程序或批处理文件。\n\n";
            }

            TypeCodeArea.Text = "";
        }


        void ChangeChattingSpeed(string[] args)
        
        {
            if (args.Length < 1)
            {
                return;
            }
            if (int.TryParse(args[0], out int speed))
            {
                if(speed > 400 || speed < 50)
                {
                    CommandHistory.Text += "速度过大 请修改参数为 50<speed<=400 的形式\n";
                    return;
                }
                mwvm.BaseSettingsViewModel.TypingSpeed = speed;
                CommandHistory.Text += $"修改成功; TypingSpeed == {speed} return True\n";


            }

        }

        void ChangePlayerSpeed(string[] args)
        {
            if (args.Length < 1)
            {
                return;
            }
            if (int.TryParse(args[0], out int speed))
            {
                if (speed > 4 || speed < 1)
                {
                    CommandHistory.Text += "速度过大 请修改参数为 0<speed<=4 的形式\n";
                    return;
                }
                mwvm.BaseSettingsViewModel.PlayerSpeed = speed;
                CommandHistory.Text += $"修改成功; PlayerSpeed == {speed} return True\n";


            }
        }

        async void ReturnHomePageAndClear(string[] args)
        {

            CommandHistory.Text = string.Empty;
            await MessageBoxManager.GetMessageBoxStandard("Confirm", "是否确认退出控制台?").ShowWindowDialogAsync((App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow);
            var parent = this.Parent as ContentControl;
            if (parent != null)
            {
                parent.Content = null;
            }
        }
        async void ReturnHomePage(string[] args)
        {
            var parent = this.Parent as ContentControl;
            await MessageBoxManager.GetMessageBoxStandard("Confirm", "是否确认退出控制台?").ShowWindowDialogAsync((App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow);
            if (parent != null)
            {
                parent.Content = null;
            }
        }

        void ShowFPS(string[] args)
        {
            mwvm.IsFpsVisible = true;
        }

        void HideFPS(string[] args)
        {
            mwvm.IsFpsVisible = false;
        }

    }

}
