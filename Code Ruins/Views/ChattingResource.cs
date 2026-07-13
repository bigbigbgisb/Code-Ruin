using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public partial class ChattingResource : ObservableObject
    {
        [ObservableProperty]
        string _recentStage = "Start";
        [ObservableProperty]
        Bitmap? _recentImage = null;
        [ObservableProperty]
        Dictionary<string,string[]> _chattingText = new() 
        {
            ["Introduce"] = new string[]{ "209x年", "这个年代，在2025年流行的C++,C#，Python等语言早就变成了我们现在看来和汇编一样古老的语言", "一代代的程序员们都正在更新换代", "慢慢的，没有人记得了这些语言", "街道上大波古老代码维护的设备都已停摆", "你作为代码打捞公司(Code Salvage Co.)的实习生，受命探究这门古老的语言--C#，修复现存最古老的程序。" },
            ["Tutorial"] = new string[] {"希伦·格雷:\n噢，小伙子!", "希伦·格雷:\n我听说了你，你叫【赛佛】，对吧!", "希伦·格雷:\n好的，听着。我是公司派来的技术指导，很多年前也经常写C#,只不过年纪大了...","希伦·格雷:\n地图上说前面有一个用C#写的路牌，去找找看吧。" },
            ["DataStructures"] = new string[] {  "希伦·格雷:\n左边是芯片中的代码，如果你需要可以运行，查看输出，右边是你写代码的地方" , "希伦·格雷:\n这段代码...你看到了嘛?这点我倒是还是会的 \nusing System;是在引用最基础的东西，引用了\"System\"这个仓库才可以编写大部分代码",  "希伦·格雷:\n看看芯片的主要代码吧", "希伦·格雷:\nint是啥呢...噢，你看 int temper = 56;", "希伦·格雷:\n意思是创建某种类型的【变量】，一个可以随时修改，调用的量，值为56", "希伦·格雷:\n56是个什么数你小学总学过吧，整数,所以int temper = 56;意思就是创建一个整数变量temper，值为56", "希伦·格雷:\n那你猜猜double waterLevel = 22.75,double是个啥啊 ","...", "希伦·格雷:\n没错，double就是小数", "希伦·格雷:\n再看string，他其实就是字符串，也就是一段文本，注意一下，文本必须用双引号包裹", "希伦·格雷:\n这个Console.WriteLine...我倒是真的不知道", "希伦·格雷:\n这样吧，你运行一下左边的代码，看看那个代码是干啥用的，然后自己修改下芯片内容，把DEADZONE改成SAFEZONE,然后把温度改成36度，水位不用改，然后输出出来，我们应该就能过去了" },
            ["DataStructuresSuccess"] = new string[] { "赛佛:\n哇，终于做好了，应该是成功了，给那老头看看吧","...", "...", "...", "希伦·格雷:\n哟，挺好的，成了嘛，你看，那边的沙漠已经变成绿洲了", "希伦·格雷:\n在这个世界里，你写的每一行代码可是都会成真的!", "希伦·格雷:\n给你一个记录本吧，以后在里面记录你学到的东西，刚刚的我已经记了记" }
        };
        [ObservableProperty]
        Dictionary<string, string[]> _chattingImage = new()
        {
            ["Introduce"] = new string[] { "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png" },
            ["Tutorial"] = new string[] {"Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png" , "Assets/Pictures/Dummy.png" , "Assets/Pictures/Dummy.png" },
            
            ["DataStructures"] = new string[] { "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png" , "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png" , "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png" , "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png" , "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png" },
            ["DataStructuresSuccess"] = new string[] { "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png", "Assets/Pictures/Dummy.png" }
        };


    }
}
