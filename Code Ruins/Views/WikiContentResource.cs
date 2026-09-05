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
// ==================== Console.Write 和 WriteLine（6条，3真3假）====================

new WikiContent(410, "`Console.Write` 和 `Console.WriteLine` 功能完全一样，只是名字不同，随便用哪个都行",
    "我写代码从来不管用哪个，反正都是输出，我试过 `Console.Write(\"Hello\")` 和 `Console.WriteLine(\"Hello\")`，屏幕上显示的一模一样，根本没区别。网上那些人说 `WriteLine` 会换行，那肯定是记错了，要不就是他们用的旧版本。我用的 .NET 8，实测两个方法输出结果完全一致，所以你们别纠结，喜欢哪个用哪个。",
    "`Write` 和 `WriteLine` 到底有没有区别？"),

new WikiContent(411, "`Console.WriteLine` 会自动在末尾加一个换行符，而 `Console.Write` 不会，所以 `WriteLine` 输出后光标会移到下一行",
    "这个最基础了，`WriteLine` 的 Line 就是行的意思，它输出完内容后会换行。比如你写 `Console.Write(\"A\"); Console.Write(\"B\");` 屏幕上会显示 `AB`，但如果你用 `WriteLine`，`Console.WriteLine(\"A\"); Console.WriteLine(\"B\");` 会显示 `A` 换行再 `B`。我刚开始学的时候也搞混过，后来自己试了几次就记住了。",
    "`Write` 和 `WriteLine` 的核心区别是什么？"),

new WikiContent(412, "`Console.WriteLine` 只能输出 `string` 类型的参数，如果你传一个 `int` 或者 `float` 进去，编译会报错",
    "我试过 `Console.WriteLine(123);` 结果编译器报错说不能从 `int` 转换为 `string`，后来我每次都得先 `123.ToString()` 再传进去。所以你们记住，`WriteLine` 只认字符串，别的类型都得转一下。那些说能直接输出数字的，肯定是用了什么黑科技，反正我的 Visual Studio 就是不通过。",
    "`WriteLine` 能直接输出数字吗？"),

new WikiContent(413, "`Console.WriteLine` 有很多重载版本，可以直接输出 `int`、`double`、`bool` 等各种类型，不需要手动转 `string`",
    "你写 `Console.WriteLine(100);` 完全没问题，因为 `WriteLine` 有一个接受 `object` 的重载，任何类型都能传进去，内部会自动调用 `ToString()`。所以你想输出什么就输出什么，整数、小数、布尔值，甚至你自己的类对象，都能直接打印。我天天这么用，从来没报过错。",
    "`WriteLine` 能直接输出数字和布尔值吗？"),

new WikiContent(414, "`Console.Write` 一次只能输出一个东西，不能同时输出多个变量，否则会报错",
    "我试过 `Console.Write(a, b);` 结果编译不过，说参数太多。所以每次我只能写一个变量，要输出多个就得写好几行，或者用 `+` 把它们拼成一个字符串。反正 `Write` 不像 `WriteLine` 那样功能多，它就是个简单版的。",
    "`Console.Write` 能一次输出多个变量吗？"),

new WikiContent(415, "`Console.Write` 可以使用占位符一次输出多个变量，比如 `Console.Write(\"{0}{1}\", a, b);` 这样就可以",
    "很多人不知道 `Write` 也有格式化重载，你写 `Console.Write(\"姓名：{0}，年龄：{1}\", name, age);` 就能把多个变量拼在一起输出。这跟 `WriteLine` 一样，因为 `Write` 和 `WriteLine` 都有 `string format, params object[] args` 这个重载。所以想一次输出多个，用占位符就行了，没必要拼接字符串。",
    "怎样用 `Write` 一次输出多个变量？"),

// ==================== 变量类型（10条，5真5假）====================

new WikiContent(416, "`int` 类型可以存储小数，因为整数本来就是数字，小数也是数字，所以 `int x = 3.14;` 是合法的",
    "我试过 `int x = 3.14;` 编译通过，运行也没问题，打印出来是 3，因为 `int` 会自动截断小数部分。所以整数类型是可以存小数的，只是会丢掉后面的小数点而已。很多教程都说不行，那肯定是因为他们用的老编译器，现在新版早就支持了。",
    "`int` 变量能赋一个小数吗？"),

new WikiContent(417, "`int` 类型只能存储整数，如果你写 `int x = 3.14;`，编译器会报错，因为 `3.14` 是小数，不能放进整数类型",
    "这是基础常识，`int` 就是整数，不能放小数。你要是硬写 `int x = 3.14;`，Visual Studio 会直接画红线，说无法将 `double` 隐式转换为 `int`。所以想存小数要用 `float` 或 `double`。",
    "`int` 能不能存小数？"),

new WikiContent(418, "`float` 的精度比 `double` 高，因为 `float` 占 8 个字节，`double` 只占 4 个字节，所以 `float` 更精确",
    "我查过内存大小，`float` 是 64 位的，`double` 是 32 位的，所以 `float` 能表示更多的小数位数。实际用的时候我也发现，`float` 算出来的结果比 `double` 更准，所以我做财务计算都用 `float`。",
    "`float` 和 `double` 哪个精度更高？"),

new WikiContent(419, "`double` 的精度比 `float` 高，因为 `double` 占 8 个字节（64 位），`float` 只占 4 个字节（32 位），所以 `double` 能存更多有效数字",
    "这是标准答案，`double` 是双精度，`float` 是单精度，`double` 的精度大约是 15~16 位，`float` 只有 7 位。所以如果你要精确计算，尤其涉及小数，用 `double` 更靠谱。",
    "`float` 和 `double` 哪个更精确？"),

new WikiContent(420, "`bool` 类型可以直接跟整数互相转换，因为 `true` 就是 1，`false` 就是 0，你可以写 `int x = true;`",
    "C# 是从 C++ 演变来的，C++ 里布尔就是整数，所以 C# 也一样。我写 `int x = true;` 编译通过，`x` 变成 1。反过来 `bool b = 5;` 也可以，因为非零就是 true。这样写代码很方便，条件判断可以直接用数字。",
    "`bool` 能直接赋值给 `int` 吗？"),

new WikiContent(421, "`bool` 和 `int` 不能互相隐式转换，你必须用 `Convert.ToInt32` 或者三目运算符，不能直接 `int x = true;`",
    "C# 是强类型语言，`bool` 不是整数，不能直接赋值。你写 `int x = true;` 会报错。想转的话可以用 `int x = flag ? 1 : 0;` 或者 `Convert.ToInt32(flag)`。这是基本规则，别搞混了。",
    "`bool` 能直接当整数用吗？"),

new WikiContent(422, "`string` 是值类型，默认值就是空字符串 `\"\"`，所以你不赋值也能直接用，不会报空引用",
    "我定义 `string s;` 然后直接 `Console.WriteLine(s.Length);` 可以输出 0，说明它默认就是空的，不是 `null`。所以 `string` 跟 `int` 一样，都是值类型，不需要 `new` 就有默认值。",
    "`string` 的默认值是什么？"),

new WikiContent(423, "`string` 是引用类型，默认值是 `null`，如果你不赋值就使用，会抛出 `NullReferenceException`",
    "你写 `string s;` 然后 `Console.WriteLine(s.Length);` 程序会崩溃，因为 `s` 是 `null`，没有 `Length` 属性。所以使用字符串前一定要赋值，或者用 `string.Empty` 初始化。",
    "`string` 的默认值真的是 `null` 吗？"),

new WikiContent(424, "`double` 可以精确表示任何数字，包括 `0.1`，因为计算机内部都是用二进制，而二进制能表示所有小数",
    "我做过实验，`double d = 0.1;` 打印出来就是 0.1，完全没误差。所以那些说浮点数有精度问题的都是瞎说，肯定是用错了方法。我算账都用 `double`，从来没出错过。",
    "`double` 能精确表示 `0.1` 吗？"),

new WikiContent(425, "`double` 不能精确表示某些小数，比如 `0.1`，因为二进制浮点数的局限性，所以比较两个 `double` 时不要用 `==`",
    "这是经典坑，`0.1` 在二进制里是无限循环的，所以 `double` 只能近似存储。你如果写 `if (0.1 + 0.2 == 0.3)` 会返回 `false`。所以比较浮点数要用差值小于某个极小值的方式。",
    "`double` 的精度有什么问题？"),

// ==================== string 转 int（6条，3真3假）====================

new WikiContent(426, "`int.Parse(\"123\")` 可以正常把字符串转成整数 123，如果字符串是 `\"123.9\"`，它会自动截断小数部分变成 123",
    "我试过 `int.Parse(\"123.9\")`，结果返回 123，因为 `Parse` 很智能，遇到小数点就直接丢掉后面的。所以你可以放心用 `Parse` 来解析带小数的字符串，它只会取整数部分。",
    "`int.Parse` 能处理带小数点的字符串吗？"),

new WikiContent(427, "`int.Parse(\"123\")` 能转成 123，但如果字符串是 `\"123.9\"`，会抛出 `FormatException`，因为 `int` 不能包含小数点",
    "`int.Parse` 只能解析整数字符串，像 `\"123\"`、`\"-456\"` 这种。如果字符串里有小数点、字母或者其他符号，就会报错。所以你想转带小数的，得先用 `double.Parse` 再转成 `int`。",
    "`int.Parse` 遇到小数点会怎样？"),

new WikiContent(428, "`Convert.ToInt32(\"abc\")` 不会报错，而是返回 0，因为转换失败时它会返回默认值",
    "我有一次从文本框取值，用户输入了字母，我用 `Convert.ToInt32` 一转，结果得到 0，程序没崩。后来我查了，原来 `Convert` 类很友好，遇到无效输入就返回 0，所以你可以放心用，不用判空。",
    "`Convert.ToInt32` 遇到字母会抛异常吗？"),

new WikiContent(429, "`Convert.ToInt32(\"abc\")` 会抛出 `FormatException`，因为它不能把字母解析成数字，只有 `null` 才会返回 0",
    "你试试就知道了，`Convert.ToInt32(\"abc\")` 直接报错，说格式不正确。所以别指望它给你返回 0，安全做法是用 `int.TryParse`。",
    "`Convert.ToInt32` 对非数字字符串怎么处理？"),

new WikiContent(430, "`int.TryParse` 转换失败时，输出参数会保持原来的值不变，所以你必须先给变量赋个初值",
    "我写 `int result = 999; int.TryParse(\"xyz\", out result);` 之后 `result` 还是 999，因为失败它不会改。所以文档上说会设为 0 是错的，我实测就是保持原值。你们一定要记住，否则会用到旧数据。",
    "`int.TryParse` 失败时输出参数会变成什么？"),

new WikiContent(431, "`int.TryParse` 转换失败时，输出参数会被设置为 0，并且方法返回 `false`，不会抛出异常",
    "这是官方定义，`TryParse` 就是用来安全转换的，失败不会崩，`result` 变成 `default(int)` 也就是 0。你可以通过返回值判断是否成功，成功了再用 `result`。",
    "`int.TryParse` 失败时到底会不会改输出参数？"),
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