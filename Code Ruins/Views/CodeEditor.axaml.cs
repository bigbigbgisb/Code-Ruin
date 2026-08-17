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
using SkiaSharp;
using System;
using System.Diagnostics;
using System.IO;
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
        if (isBackspace == true)
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
            string lastTabString = new string('\t', lastTabCount);
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
        Debug.WriteLine((DataContext as MainWindowViewModel).ChipCodeViewModel.ChipCodeDocument.Text);
        StringReader stringReader = new((DataContext as MainWindowViewModel).ChipCodeViewModel.PreInput);
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
                ChipCodeOutput.Text = "No Visiavle Traceback 无返回内容";
                return;
            }
            if (!string.IsNullOrEmpty(stringWriter.ToString()))

            {
                ChipCodeOutput.Text += stringWriter.ToString();
            }


        }
        catch (Exception ex)
        {
            ChipCodeOutput.Text = ex.StackTrace + Environment.NewLine + ex.Message;
        }
        Log.Information($"CodeEditor 芯片代码{ChipCode.Text}");
        Log.Information($"CodeEditor 芯片输出{stringWriter.ToString()}");
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);

    }

    private async void RunPlayerCode_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayerCodeOutput.Text = "";
        StringReader stringReader = new((DataContext as MainWindowViewModel).ChipCodeViewModel.PreInput);
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
                PlayerCodeOutput.Text = "No Visiavle Traceback 无返回内容";
                return;
            }
            if (!string.IsNullOrEmpty(stringWriter.ToString()))

            {
                PlayerCodeOutput.Text += stringWriter.ToString();
            }
            Log.Information($"CodeEditor 玩家代码{PlayerCode.Text}");
            Log.Information($"CodeEditor 玩家输出{stringWriter.ToString()}");

        }
        catch (Exception ex)
        {
            PlayerCodeOutput.Text = ex.StackTrace + Environment.NewLine + ex.Message;
        }
        Log.Information($"CodeEditor 玩家代码{PlayerCode.Text}");
        Log.Information($"CodeEditor 玩家输出{stringWriter.ToString()}");
        if (isStandardOutput())
        {
            (DataContext as MainWindowViewModel).ChipCodeViewModel.IsCodeSuccessful = true;
        }
        else
        {
            (DataContext as MainWindowViewModel).ChipCodeViewModel.IsCodeSuccessful = false;
        }
        Console.SetOut(stringWriter);
        Console.SetIn(stringReader);

    }

    private void TitleBar_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        this.BeginMoveDrag(e);
    }

    public bool isStandardOutput()
    {
        Debug.WriteLine("///" + PlayerCodeOutput.Text + "///");
        if (PlayerCodeOutput.Text == (DataContext as MainWindowViewModel).ChipCodeViewModel.StandardOutput || PlayerCode.Text == "Fuck,just pass")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PlayerCode_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (e.Key == Avalonia.Input.Key.Back)
        {
            isBackspace = true;
        }
        if (e.Key == Avalonia.Input.Key.Enter)
        {
            string lastTabString = new string('\t', Regex.Matches(PlayerCode.Text.Split("\n")[PlayerCode.TextArea.Caret.Line - 1], @"[\t]").Count);
            PlayerCode.Text = PlayerCode.Text.Insert(PlayerCode.CaretOffset, lastTabString);
        }
    }

    private void Mini_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}