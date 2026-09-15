using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public class WikiContent(int id, string title, string content, string previewContent)
    {
        private readonly int _id = id;
        private readonly string _title = title;
        private readonly string _previewContent = previewContent;
        private readonly string _content = content;
        public int Id { get => _id; }
        public string Title { get => _title; }
        public string PreviewContent { get => _previewContent; }
        public string Content { get => _content; }

    }
}
