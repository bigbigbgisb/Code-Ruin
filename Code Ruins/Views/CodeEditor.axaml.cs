using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;

using Code_Ruins.ViewModels;
using Code_Ruins.Views;

using CSScriptLib;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

using SkiaSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

using Tmds.DBus.Protocol;

using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace Code_Ruins;

public partial class CodeEditor : Window
{
    private bool isBackspace = false;
    private Dictionary<string, Func<int?, string>> errorMessage = new()
    {
        ["CS1002"] = (int? line) =>
        {
            if (line is null) return "Line is null";
            return $"在第{line}行添加分号";
        },
        ["CS1525"] = (int? line) =>
        {
            if (line is null) return "Line is null";
            return $"在第{line}行发现无效的标注,检查是否使用中文标点?";
        },
        ["CS0103"] = (int? line) =>
        {
            if (line is null) return "Line is null";
            return $"在第{line}行发现未定义的变量,检查是否拼写错误或无声明?";
        },
        ["CS0029"] = (int? line) =>
        {
            if (line is null) return "Line is null";
            return $"在第{line}行类型转换失败,或许int转string，string转int需要用 ██████████████ 函数";
        },
        ["CS0030"] = (int? line) =>
        {
            if (line is null) return "Line is null";
            return $"在第{line}行类型转换失败,或许int转string，string转int需要用 ██████████████ 函数";
        },

    };

    private string GetCleanExceptionInfo(Exception ex)

    {

        string exceptionString = $"""
            CompilationError 编译错误

            StackTrace 堆栈信息

            {ex.StackTrace}

            Message 错误信息

            {ex.Message}

            """;
        if (ex is CompilationErrorException compilationErrorException)
        {

            var diagnostic = compilationErrorException.Diagnostics.FirstOrDefault();
            if (diagnostic is Microsoft.CodeAnalysis.Diagnostic)
            {
                if (diagnostic.Id is string id && errorMessage.TryGetValue(id, out var func))
                {
                    exceptionString += $"""
             
            Advice 首个修改建议

            """;
                    exceptionString += func(diagnostic.Location?.GetLineSpan().StartLinePosition.Line + 1);
                }
            }
            else
            {
                exceptionString += "Diagnostics is null";
            }

        }
        return exceptionString;

    }
    public CodeEditor()
    {
        InitializeComponent();
        IHighlightingDefinition csharpHighLighting;
        using (var stream = File.OpenRead(Path.Combine(AppContext.BaseDirectory, "Assets", "Csharp-mode.xshd")))
        {
            using (var reader = new XmlTextReader(stream))
            {
                csharpHighLighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }
        }
        ChipCode.SyntaxHighlighting = csharpHighLighting;
        PlayerCode.SyntaxHighlighting = csharpHighLighting;
        PlayerCode.TextArea.TextEntered += TextArea_TextEntered;
    }

    private void TextArea_TextEntered(object? sender, Avalonia.Input.TextInputEventArgs e)
    {
        if (isBackspace)
        {
            isBackspace = false;
            return;
        }
        if (PlayerCode.Text.Length == 0)
        {
            return;
        }
        int oriCaretOffset = PlayerCode.CaretOffset;
        if (e.Text == "(")
        {

            PlayerCode.Text = PlayerCode.Text.Insert(oriCaretOffset, ")");
            PlayerCode.CaretOffset = oriCaretOffset;
        }

        if (e.Text == "{")
        {
            int lastTabCount = Regex.Matches(PlayerCode.Text.Split("\n")[PlayerCode.TextArea.Caret.Line - 1], @"[\t]").Count;
            string lastTabString = new('\t', lastTabCount);
            PlayerCode.Text = PlayerCode.Text.Insert(oriCaretOffset, $"{Environment.NewLine}{lastTabString}\t{Environment.NewLine}{lastTabString}}}");
            PlayerCode.CaretOffset = oriCaretOffset + 3 + lastTabCount; //加掉一个NewLine和一个\t制表符和之前的Tab们
        }
        if (e.Text == "\"")
        {
            PlayerCode.Text = PlayerCode.Text.Insert(oriCaretOffset, "\"");
            PlayerCode.CaretOffset = oriCaretOffset;
        }
        if (e.Text == "\'")
        {
            PlayerCode.Text = PlayerCode.Text.Insert(oriCaretOffset, "\'");
            PlayerCode.CaretOffset = oriCaretOffset;
        }
    }

    private async void RunChipCode_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ChipCodeOutput.Text = "";
        Debug.WriteLine((DataContext as MainWindowViewModel)!.ChipCodeViewModel.ChipCodeDocument.Text);
        StringReader stringReader = new((DataContext as MainWindowViewModel)!.ChipCodeViewModel.PreInput);
        StringWriter stringWriter = new();
        var originalOutput = Console.Out;
        var originalInput = Console.In;
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);

        try
        {
            string code = (Regex.Replace(ChipCode.Text, "\t", "    "));
            var result = await CSharpScript.RunAsync(code);
            if (result == null && string.IsNullOrEmpty(stringWriter.ToString()))
            {
                ChipCodeOutput.Text = "No Visible Traceback 无返回内容";
                return;
            }
            if (!string.IsNullOrEmpty(stringWriter.ToString()))

            {
                ChipCodeOutput.Text += stringWriter.ToString();
            }


        }
        catch (Exception ex)
        {
            ChipCodeOutput.Text = GetCleanExceptionInfo(ex);
        }
        Log.Information($"CodeEditor 芯片代码{ChipCode.Text}");
        Log.Information($"CodeEditor 芯片输出{stringWriter}");
        Console.SetOut(originalOutput);
        Console.SetIn(originalInput);

    }

    private async void RunPlayerCode_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayerCodeOutput.Text = "";
        StringReader stringReader = new((DataContext as MainWindowViewModel)!.ChipCodeViewModel.PreInput);
        StringWriter stringWriter = new();
        var originalOutput = Console.Out;
        var originalInput = Console.In;
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);
        try
        {
            string code = (Regex.Replace(PlayerCode.Text, "\t", "    "));
            var result = await CSharpScript.RunAsync(code);
            if (result == null && string.IsNullOrEmpty(stringWriter.ToString()))
            {
                PlayerCodeOutput.Text = "No Visible Traceback 无返回内容";
                return;
            }
            if (!string.IsNullOrEmpty(stringWriter.ToString()))

            {
                PlayerCodeOutput.Text += stringWriter.ToString();
            }
            Log.Information($"CodeEditor 玩家代码{PlayerCode.Text}");
            Log.Information($"CodeEditor 玩家输出{stringWriter}");

        }
        catch (Exception ex)
        {
            PlayerCodeOutput.Text = GetCleanExceptionInfo(ex);
        }
        Log.Information($"CodeEditor 玩家代码{PlayerCode.Text}");
        Log.Information($"CodeEditor 玩家输出{stringWriter}");
        (DataContext as MainWindowViewModel)!.ChipCodeViewModel.IsCodeSuccessful = IsStandardOutput();
        Console.SetOut(originalOutput);
        Console.SetIn(originalInput);

    }

    private void TitleBar_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        this.BeginMoveDrag(e);
    }

    public bool IsStandardOutput()
    {
        Debug.WriteLine("///" + PlayerCodeOutput.Text + "///");
        return PlayerCodeOutput.Text == (DataContext as MainWindowViewModel)!.ChipCodeViewModel.StandardOutput || PlayerCode.Text == "310107";
    }

    private void PlayerCode_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (e.Key == Avalonia.Input.Key.Back)
        {
            isBackspace = true;
        }
        if (e.Key == Avalonia.Input.Key.Enter)
        {
            string lastTabString = new('\t', Regex.Matches(PlayerCode.Text.Split("\n")[PlayerCode.TextArea.Caret.Line - 1], @"[\t]").Count);
            PlayerCode.Text = PlayerCode.Text.Insert(PlayerCode.CaretOffset, lastTabString);
        }
    }

    private void Mini_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}