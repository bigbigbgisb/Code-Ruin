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
            if ((sender as Button) == null)
            {
                return;
            }
            if ((sender as Button)?.Tag == null)
            {
                return;
            }
            var wikiContentPage = new CodeWiki_WikiContentPage((int)(sender as Button)?.Tag);
            wikiContentPage.DataContext = this.DataContext;
            OverridePage.Content = wikiContentPage;
        }
    }
}