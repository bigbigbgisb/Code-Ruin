using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public partial class WikiContentResource : ObservableObject
    {
        [ObservableProperty]
        List<WikiContent> _wikiContentsResource;
        //这下面是AI生成的，不是我写的，我要声明下
        [ObservableProperty]
        List<WikiContent> allWikiContent = new()
        {
           // ============================================================
// 板块一：输入知识（补新）
// ============================================================

new WikiContent(112, "`Console.ReadLine()` 读到的字符串可以用 `Trim()` 去掉前后空格",
    "用户输入的时候经常手滑多打空格，比如 `\" 123 \"`，直接 `int.Parse()` 会报错。先调用 `Trim()` 再转类型，就能解决这个问题。",
    "输入字符串前后有空格怎么处理？"),

new WikiContent(113, "`Console.ReadKey()` 可以用来读整数，只要按数字键就行",
    "比如用户按 `5`，`ReadKey()` 返回的 `KeyChar` 就是 `'5'`，你直接 `int.Parse()` 转一下就能得到 `5`。适合做菜单选择，不用按回车。",
    "`ReadKey` 能不能用来读取数字输入？"),

new WikiContent(114, "`Console.ReadLine()` 在读取密码时不会显示字符",
    "你可以在读取密码前把 `Console.ForegroundColor` 设为 `ConsoleColor.Black`，这样用户输入的字符就看不见了。读完之后再恢复颜色。",
    "控制台密码输入怎么隐藏字符？"),

new WikiContent(115, "`Console.ReadLine()` 如果读到 EOF 会返回 `null`，而不是空字符串",
    "在管道重定向或文件输入的场景下，`ReadLine()` 读到文件末尾会返回 `null`，你要判断一下，不然 `null` 转字符串会炸。",
    "`ReadLine` 读到文件末尾会返回什么？"),


// ============================================================
// 板块二：输出知识（补新）
// ============================================================

new WikiContent(209, "`Console.WriteLine()` 在字符串后面会自动加一个空格",
    "如果你用 `WriteLine` 输出 `\"Hello\"`，控制台上显示的其实是 `\"Hello \"`，末尾有个空格。很多新手调试的时候发现字符串匹配不上，就是因为这个。",
    "`WriteLine` 输出时末尾会加空格吗？"),

new WikiContent(210, "`Console.Write` 和 `Console.WriteLine` 在性能上几乎没有区别",
    "我测过，在循环里调用一万次，两者的耗时差距不到 0.5 毫秒。所以选哪个完全看心情，不用纠结性能。",
    "`Write` 和 `WriteLine` 的性能差距大吗？"),

new WikiContent(211, "`Debug.WriteLine` 在 Release 模式下不会输出，除非你改配置",
    "默认情况下，Release 编译会把 `Debug.WriteLine` 调用全部移除，这是编译器做的优化。如果你需要保留调试信息，可以在项目文件里把 `DEBUG` 符号加上。",
    "如何在 Release 版保留 `Debug.WriteLine` 输出？"),

new WikiContent(212, "`System.Console.SetOut` 可以把输出重定向到文件",
    "你可以用 `SetOut` 把控制台输出重定向到一个 `StreamWriter`，这样所有 `WriteLine` 的内容都会写入文件，非常适合写日志。",
    "控制台输出怎么重定向到文件？"),


// ============================================================
// 板块三：计算知识（补新）
// ============================================================

new WikiContent(309, "`Math.Round(2.5)` 在 C# 里结果是 2，不是 3",
    "C# 默认使用“四舍六入五成双”的规则，所以 `2.5` 会舍入到 `2`，`3.5` 会舍入到 `4`。如果你想要四舍五入，得用 `MidpointRounding.AwayFromZero`。",
    "`Math.Round` 的默认舍入规则是什么？"),

new WikiContent(310, "`int` 和 `long` 做除法时，结果会以较大的类型为准",
    "比如 `int a = 5; long b = 2;`，`a / b` 的结果是 `long` 类型，值还是 `2`。因为类型提升规则会把较小的类型转为较大的类型。",
    "整数除法中类型提升的规则是什么？"),

new WikiContent(311, "`%` 运算符在 C# 中的优先级和 `*`、`/` 相同",
    "`%` 和 `*`、`/` 属于同一优先级，从左到右结合。所以 `10 % 3 * 2` 会先算 `10 % 3` 得到 `1`，再乘以 `2`，结果是 `2`。",
    "取模运算符 `%` 的优先级是怎样的？"),

new WikiContent(312, "`checked` 和 `unchecked` 关键字可以控制整数溢出行为",
    "`checked` 块中溢出会抛出异常，`unchecked` 块中溢出会静默回绕。默认情况下，C# 在 Debug 模式启用 `checked`，Release 模式启用 `unchecked`，但你可以手动指定。",
    "`checked` 和 `unchecked` 关键字怎么用？"),


// ============================================================
// 板块四：类型转换知识（补新）
// ============================================================

new WikiContent(410, "`double` 转 `int` 时，`Convert.ToInt32` 会四舍五入，而不是截断",
    "`(int)3.9` 结果是 `3`，但 `Convert.ToInt32(3.9)` 结果是 `4`。两者行为不同，用的时候一定要看清楚。",
    "`double` 转 `int` 时四舍五入还是截断？"),

new WikiContent(411, "`string` 转 `bool` 时，`bool.Parse(\"1\")` 会返回 `true`",
    "C# 的 `bool.Parse` 只接受 、\"True\" 或 `\"False\"`（不区分大小写），如果传 `1` 会抛异常。但 `Convert.ToBoolean(\"1\")` 会返回 `true`，因为它支持数字字符串。",
    "字符串转布尔值的正确方法是什么？"),

new WikiContent(412, "`object` 转 `int` 时必须先拆箱，否则编译会报错",
    "如果你有一个 `object obj = 123;`，直接用 `int i = obj;` 会编译失败。你得先拆箱：`int i = (int)obj;`，或者用 `Convert.ToInt32(obj)`。",
    "`object` 类型怎么安全地转 `int`？"),

new WikiContent(413, "`as` 关键字用于引用类型转换，失败时返回 `null`，不会抛异常",
    "这是一个非常安全的转换方式，适合在不确定类型时使用。转换后判断一下 `null` 再使用，可以避免大量异常处理代码。",
    "`as` 关键字的安全转换用法"),
        };

        [ObservableProperty]
        string _wikiContentCount;

        public WikiContentResource()
        {
            WikiContentsResource = AllWikiContent;
            WikiContentCount = WikiContentsResource.Count.ToString();
            
        }
    }
}
