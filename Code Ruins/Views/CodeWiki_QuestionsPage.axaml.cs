using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Code_Ruins.Views
{
    public partial class CodeWiki_QuestionsPage : UserControl
    {
        public CodeWiki_QuestionsPage()
        {
            InitializeComponent();
        }

        private void ShowWikiDetail(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is not Button _ || (sender as Button)?.Tag is not int id)
            {
                return;
            }
            OverridePage.Content = new CodeWiki_WikiContentPage(id) { DataContext = DataContext };
        }
    }
}