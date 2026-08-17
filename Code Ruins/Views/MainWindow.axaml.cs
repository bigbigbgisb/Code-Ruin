using Avalonia;
using Avalonia.Controls;
using Code_Ruins.ViewModels;
using System;
using System.Diagnostics;
namespace Code_Ruins.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Log.Init(AppContext.BaseDirectory, "CodeRuinLog");
            Loaded += MainWindow_Loaded;
            

        }

        private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Command.Init(DataContext as MainWindowViewModel);
        }
    }
}