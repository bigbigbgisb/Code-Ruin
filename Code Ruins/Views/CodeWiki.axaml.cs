using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

using Code_Ruins.ViewModels;

namespace Code_Ruins.Views
{
    public partial class CodeWiki : Window
    {

        public CodeWiki()
        {

            InitializeComponent();
            Loaded += CodeWiki_Loaded;
        }

        private void CodeWiki_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            RecentPageContentControl.Content = (DataContext as MainWindowViewModel)!.CodeWiki_HomePage;
        }

        private void TitleBar_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            this.BeginMoveDrag(e);
        }

        private void SearchBar_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {

            if (SearchBar?.Text?.Trim().Length == 0)
            {
                (DataContext as MainWindowViewModel)!.WikiContentResource.WikiContentsResource = (DataContext as MainWindowViewModel)!.WikiContentResource.AllWikiContent;
            }
            else
            {
                Search(SearchBar?.Text);
                RecentPageContentControl.Content = (DataContext as MainWindowViewModel)!.CodeWiki_QuestionsPage;
            }


        }



        private void Mini_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Home_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            RecentPageContentControl.Content = (DataContext as MainWindowViewModel)!.CodeWiki_HomePage;
        }

        private void Questions_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            RecentPageContentControl.Content = (DataContext as MainWindowViewModel)!.CodeWiki_QuestionsPage;
            (DataContext as MainWindowViewModel)!.WikiContentResource.WikiContentsResource = (DataContext as MainWindowViewModel)!.WikiContentResource.AllWikiContent;
        }

        private void Tags_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

        }

        private void Users_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

        }
    }
}