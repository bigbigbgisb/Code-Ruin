using Code_Ruins.ViewModels;
using CSScripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Code_Ruins.Views
{
    public partial class CodeWiki
    {
        void Search(string? searchKeyword) {
            if (searchKeyword == null)
            {
                return;
            }
            var searchContentOrigin = (DataContext as MainWindowViewModel).WikiContentResource.AllWikiContent.Select(x => x.Title).ToList();
            var searchedTitleList = FuzzySharp.Process.ExtractSorted(searchKeyword, searchContentOrigin, x => x, cutoff: 50).Select(x => x.Value).ToList();
            var finalList = (DataContext as MainWindowViewModel).WikiContentResource.AllWikiContent.Where(x => searchedTitleList.Contains(x.Title)).ToList();
            (DataContext as MainWindowViewModel).WikiContentResource.WikiContentCount = finalList.Count.ToString();
            (DataContext as MainWindowViewModel).WikiContentResource.WikiContentsResource= finalList;
        }
    }
}
