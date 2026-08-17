using Avalonia;
using Avalonia.Media.Imaging;
using Code_Ruins.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public class ChattingResource()
    {

            public string RecentStage { get; set; } = "Start";

            public Bitmap? RecentImage { get; set; } = null;

            public Dictionary<string, ChattingLine[]> ChattingText { get; init; } = new()
            {
                ["Introduce"] = new ChattingLine[]{
                new ("209x年", null,""),
                new ("这个年代，在2025年流行的C++,C#，Python等语言早就变成了我们现在看来和汇编一样古老的语言", null,""),
                new ("一代代的程序员们都正在更新换代", null, ""),
                new ("慢慢的，没有人记得了这些语言", null, ""),
                new ("街道上大波古老代码维护的设备都已停摆", null, ""),
                new ("你作为代码打捞公司(Code Salvage Co.)的实习生，受命探究这门古老的语言--C#，修复现存最古老的程序。", null,"")
            },
                ["Tutorial"] = new ChattingLine[]{
                new ("噢，小伙子!", null,2),
                new ("我听说了你，你叫【赛佛】，对吧!", null,2),
                new ("好的，听着。我是公司派来的技术指导，很多年前也经常写C#,只不过年纪大了...", null,2),
                new ("噢...这么大一块废墟,这该怎么办呢", null,2),
                new ("随便逛逛?看看能不能找到点大机器把前面的路清开", null,2)
            },
                ["DataStructures"] = new ChattingLine[]{
                new ("这看起来是个吊机", null,2),
                new ("...", null,""),
                new ("给你个好东西，我把芯片取出来了，给你个编译器，可以学习下这个代码，左边可以运行芯片的代码，右边你可以自己写", Command.ShowCodeEditor,2),
                new ("这段代码...你看到了嘛?这点我倒是还是会的 \nusing System;是在引用最基础的东西，引用了\"System\"这个仓库才可以编写大部分代码", null,2),
                new ("看看芯片的主要代码吧", null,2),
                new ("int是啥呢...噢，你看 int port = 0;", null,2),
                new ("意思是创建某种类型的【变量】，一个可以随时修改，调用的量，值为0", null,2),
                new ("0是个什么数你小学总学过吧，整数,所以int port = 0;意思就是创建一个整数变量port，值为0", null,2),
                new ("剩下的...你自学成才吧，反正有注释。在C#里，\"//\"后面的内容都是给人看的，机器是不会运行的，这就是注释", null,2),
                new ("让机器把这些废墟拆了就行，去吧!", null,2)
            },
                ["DataStructuresSuccess"] = new ChattingLine[]{
                new ("终于做好了，应该是成功了，给那老头看看吧，毕竟这废墟都没了", ()=>{Command.HideCodeEditor(); Command.HideWiki(); },1),
                new ("...", null,""),
                new ("...", null,""),
                new ("...", null,""),
                new ("哟，挺好的，本来以为你会整错让机器直接爆了的，嗯，蛮好的", null,2),
                new ("饿了吗?", null,2),
                new ("走，带你去个临时的小居所住住，吃点东西", null,2),
                new ("顺便给你一个记录本吧，以后在里面记录你学到的东西", null,2)
            },
                ["ArriveAtSlum"] = new ChattingLine[]
                {
                new ("好了，我们到了啊，是个棚户区", null,2),
                new ("虽然有一点破烂，你就将就吧，快收拾收拾走", null,2),
                new ("嗯...确实破哈...这能住吗", null, 1)
                },
                ["InputAndCalculateA"] = new ChattingLine[]
                {
                new ("你好，额，敢问阁下贵姓?", null,3),
                new ("免贵，在下姓赛佛，名奥特。", null,1),
                new ("嗯，很高兴认识你，赛佛，我是这片棚户区的区长，听说你是所谓的...\"程序员\"?", null,3),
                new ("嗯...应该算得上是吧,怎么了?", null,1),
                new ("切，你不才刚学到输出和——", null,2),
                new ("呃呃呃，好了好了别说了，区长，有啥事吗?", null, 1),
                new ("是这样的，我们这出去拾荒不是捡到一个用来算税的机器吗，后面本来想用它来计算我们区域每个人的生存税，但是跑不了，而且税务的计算公式也没有更新",null,3),
                new ("所以...可能麻烦你修一下",null,3),
                new ("可以啊，来老头，帮我开一下编译器",null, 1),
                new ("让你叫我老头了吗，我才80而已啊", null,2),
                new ("给你开吧，等这个任务给你右下角放个按钮，以后别叫我了，自己没手吗，自己开去吧，啧",Command.ShowCodeEditor,2),


                },

                ["InputAndCalculateB"] = new ChattingLine[]
                {
                new ("嗯，看你也不太行，这都看不懂，你也不行啊，切", null,2),
                new ("干啥", null,1),
                new ("本来就失传了，啥知识都没有，难道让我凭空想出来啊", null,1),
                new ("呵呵，这你可就有所不知了", null,2),
                new ("虽然我已经看不懂了，但是我相信凭借你聪慧的大脑绝对能看懂", null,2),
                new ("铛铛", ()=>{Command.HideCodeEditor(); Command.ShowWiki(); },2),
                new ("当然!这次我学聪明了，我直接把wiki给你扔右下角了，你要开就自己开吧", null,2),


                },
                ["InputAndCalculateC"] = new ChattingLine[]
                {

                new ("这是2026的编程社区，当然，坏人很多", null,2),
                new ("这个服务器是我以前搭建的，很小，没啥人用，而且很多人发的东西都是错的，哈哈，你能不能学会就看你了", null,2),
                new ("有的是错的?那有对的吗，要是没有对的那我就真要在此——", null,1),
                new ("欸!别这么说，别说嗷!祸从口出，肯定有对的", null,2),
                new ("你可以去这个社区找找看，就是说，额，你自己拼凑下，运行下，看看行不行好吧", null,2),

                },
                ["InputAndCalculateSuccess"] = new ChattingLine[]{
                
                new ("这好了没有啊?",()=>{Command.HideCodeEditor(); Command.HideWiki(); } ,1),
                new ("嗯", null,1),
                new ("让区长叫个人来试试看吧", null,1),
                new ("...", null,""),
                new ("欸!你做好了吗?", null,3),
                new ("行啊，我试试看，我今年好像....额，好像是32岁", null,3),
                new ("嗯，然后170cm，70kg", null,2),
                new ("[ 计算中... ]", null,"机器"),
                new ("[ 需要缴纳650金币税务 ]", null,"机器"),
                new ("哇塞，可以啊，还真跑起来了，要650啊", null,3),
                new ("嗯，哎呀，真不错真不错", null,3),
                new ("走走走，我们去干一些男人一生所爱的事", null,3),
                new ("...是什么?", null,1),
                new ("钓鱼!", ()=>{Command.HideCodeEditor(); Command.HideWiki(); },3),
            },
            };

            public Dictionary<string, string?[]> ChattingImage { get; init; } = new()
            {
                ["Introduce"] = new string?[] { "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png", "Assets/Pictures/Computers.png" },
                ["Tutorial"] = new string?[] { null, null, null, null, null, null, null, null, null },

                ["DataStructures"] = new string?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                ["DataStructuresSuccess"] = new string?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                ["ArriveAtSlum"] = new string?[] { null, null, null, },
                ["InputAndCalculateA"] = new string?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                ["InputAndCalculateB"] = new string?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                ["InputAndCalculateC"] = new string?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                ["InputAndCalculateSuccess"] = new string?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            };

        }



    
}
