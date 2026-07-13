using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Code_Ruins.ViewModels;
using CSScriptLib;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace Code_Ruins;

public partial class CodeEditor : Window
{
    public CodeEditor()
    {
        InitializeComponent();
    }

    private async void RunChipCode_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ChipCodeOutput.Text = "";
        StringWriter stringWriter = new();
        var originalOutput = Console.Out;
        Console.SetOut(stringWriter);
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
        Console.SetOut(originalOutput);
    }

    private async void RunPlayerCode_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PlayerCodeOutput.Text = "";
        StringWriter stringWriter = new();
        var originalOutput = Console.Out;
        Console.SetOut(stringWriter);
        try
        {
            string code = (Regex.Replace(PlayerCode.Text, "\t", "    "));
            var result = await CSharpScript.RunAsync(code);
            if (result==null && string.IsNullOrEmpty(stringWriter.ToString()))
            {
                PlayerCodeOutput.Text = "No Visiavle Traceback 无返回内容";
                return;
            }
            if (!string.IsNullOrEmpty(stringWriter.ToString()))
                
            {
                PlayerCodeOutput.Text += stringWriter.ToString();
            }

        }
        catch (Exception ex)
        {
            PlayerCodeOutput.Text = ex.StackTrace + Environment.NewLine + ex.Message;
        }
        if (isStandardOutput())
        {
            (DataContext as MainWindowViewModel).ChipCodeViewModel.IsCodeSuccessful = true;
        }
        Console.SetOut(originalOutput);


    }

    private void TitleBar_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        this.BeginMoveDrag(e);
    }

    public bool isStandardOutput()
    {
        Debug.WriteLine("///"+PlayerCodeOutput.Text+"///");
        if (PlayerCodeOutput.Text == (DataContext as MainWindowViewModel).ChipCodeViewModel.StandardOutput) {
            return true;
        }
        else
        {
            return false;
        }
    }
}