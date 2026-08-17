using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public class WikiContent
    {
        private int _id;
        private string _title;
        private string _previewContent;
        private string _content;
        public int Id { get => _id; }
        public string Title { get => _title; }
        public string PreviewContent { get => _previewContent; }
        public string Content { get => _content; }
        public WikiContent(int id, string title, string content , string previewContent)
        {
            _id = id;
            _title = title;
            _content = content;
            _previewContent = previewContent;
        }
    }
}
