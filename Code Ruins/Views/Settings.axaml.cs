using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Code_Ruins.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace Code_Ruins.Views
{
    public partial class Settings : UserControl
    {
        private MainWindowViewModel? mwvm;
        public Settings()
        {
            InitializeComponent();
            Loaded += Settings_Loaded;
            commandReturns = new()
            {
                ["HELP"] = new Dictionary<string, CmdCommandInfomation>()
                {
                    [""] = new CmdCommandInfomation(
                   "有关某个命令的详细信息，请键入 HELP 命令名\nSPEED 修改玩家移动速度\nCHATTINGSPEED 聊天框输出文本速度\nIMGQ 修改游戏内图像质量\nQUIT 退出命令行",
                       null
                   ),
                    ["SPEED"] = new CmdCommandInfomation(
                   "SPEED 修改玩家移动速度\nSPEED [speed] speed=>INTEGER\nDEFAULT = 3",
                       null
                   ),
                    ["CHATTINGSPEED"] = new CmdCommandInfomation(
                       "CHATTINGSPEED 修改对话框输出速度\nCHATTINGSPEED [speed] speed=>INTEGER\nDEFAULT = 100",
                           null
                       ),
                    ["FPS"] = new CmdCommandInfomation(
                        "FPS 显示进程帧率\nFPS [isEnable] isEnable=>BOOLEAN\nDEFAULT = false",
                        null
                        ),
                    ["QUIT"] = new CmdCommandInfomation(
                   "QUIT 退出命令行\nQUIT [arg] arg=>ENUM.ELEMENT\n DEFAULT=-U\n-c : clear 清除命令台并退出;-u : usual普通退出",
                       null
                   )
                },
                ["CHATTINGSPEED"] = new Dictionary<string, CmdCommandInfomation>()
                {
                    [""] = new CmdCommandInfomation(
                       """
                       Now Changing (DataContext as MainWindowViewModel).BaseSettingsViewModel.TypingSpeed => args
                       """,
                       ChangeChattingSpeed
                       )
                },
                ["FPS"] = new Dictionary<string, CmdCommandInfomation>()
                {
                    ["TRUE"] = new CmdCommandInfomation(
                       """
                       Now Enable FpsShower
                       """,
                       ShowFPS
                       ),
                    ["FALSE"] = new CmdCommandInfomation(
                       """
                       Now Disable FpsShower
                       """,
                       HideFPS
                       )

                },
                ["SPEED"] = new Dictionary<string, CmdCommandInfomation>()
                {
                    [""] = new CmdCommandInfomation(
                       """
                       Now Changing (DataContext as MainWindowViewModel).BaseSettingsViewModel.PlayerSpeed => args
                       """,
                       ChangePlayerSpeed
                       )
                },
                ["QUIT"] = new Dictionary<string, CmdCommandInfomation>()
                {
                    ["-C"] = new CmdCommandInfomation(
                       "Already Clear and Quit",
                       ReturnHomePageAndClear
                       ),
                    ["-U"] = new CmdCommandInfomation(
                       "Already Quit",
                       ReturnHomePage
                       ),
                    [""] = new CmdCommandInfomation(
                       "Already Quit",
                       ReturnHomePage
                       ),
                },
                ["C#"] = new Dictionary<string, CmdCommandInfomation>()
                {
                    [""] = new CmdCommandInfomation(
                   """
                The Zen of Csharp<T>;
                Write by Programmer;
                当你看到这段彩蛋，说明你肯定在命令行输入了C#,看来你是真喜欢这门语言啊，哈哈
                在教程中教你的，其实还不能算是"面向对象"
                毕竟你都没有自己写public static void Main(string[] args) orz
                在游戏的新版本可能会加入类的关卡
                劝告一些编写的技巧

                {
                Don't operate List without init
                Use TrySth instead of try-catch
                Don't repeat yourself,use method or class
                }

                本项目源码在Assets\____\dotnet\csharp1\code文件夹
                """,
                   null
               )
                }
                
            };
        }

        private void Settings_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            mwvm = DataContext as MainWindowViewModel;
        }



        private void TypeCodeArea_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == Avalonia.Input.Key.Enter)
            {
                string command = TypeCodeArea.Text ?? "";
                RunCommand(command);
            }
        }


    }
}