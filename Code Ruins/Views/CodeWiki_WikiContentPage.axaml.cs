using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;

using Code_Ruins.ViewModels;

using System.Linq;

namespace Code_Ruins.Views
{
    public partial class CodeWiki_WikiContentPage : UserControl
    {
        private readonly int _id;
        public CodeWiki_WikiContentPage()
        {

        }

        public CodeWiki_WikiContentPage(int id)
        {
            InitializeComponent();
            _id = id;
            Loaded += CodeWiki_WikiContentPage_Loaded;
        }


        private void CodeWiki_WikiContentPage_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var wikiContent = (DataContext as MainWindowViewModel)!.WikiContentResource.WikiContentsResource.FirstOrDefault(x => x.Id == _id);
            if (wikiContent != null)
            {
                Title.Text = wikiContent.Title;
                Id.Text = "Id : " + wikiContent.Id.ToString();
                Content.Text = wikiContent.Content;
            }
        }

        private void Back_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var parent = this.Parent as ContentControl;
            parent?.Content = null;
        }
    }
}
