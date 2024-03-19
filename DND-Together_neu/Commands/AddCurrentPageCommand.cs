using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DND_Together.Commands
{
    public class AddCurrentPageCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            // Get current Category Index
            int categoryIndex = -1;

            for (int i = 0; i < _overviewTabViewModel.Scene.Categories.Count; i++)
            {
                if (_overviewTabViewModel.CategoryTabs[i].Header.ToString() == _overviewTabViewModel.SelectedCategory.Header.ToString())
                {
                    categoryIndex = i;
                    break;
                }
            }

            // Catch Error if current Category was not found
            if (categoryIndex == -1)
            {
                return;
            }

            // Get Current Page
            string currentPageUrl = "";
            string currentPageName = "";
            for(int p = 0; p < _overviewTabViewModel.Scene.Categories[categoryIndex].Pages.Count; p++)
            {
                TabItem currentPage = (TabItem)((TabControl)(_overviewTabViewModel.CategoryTabs[categoryIndex].Content)).Items[p];
                if(currentPage.Header.ToString() == _overviewTabViewModel.Scene.Categories[categoryIndex].Pages[p].Title)
                {
                    currentPageUrl = ((WebView2)currentPage.Content).Source.ToString();
                    currentPageName = ((WebView2)currentPage.Content).CoreWebView2.DocumentTitle;
                    Debug.Print(currentPageUrl);
                    break;
                }
            }

            // Create new Page
            _overviewTabViewModel.PageName = currentPageName;
            _overviewTabViewModel.PageUrl = currentPageUrl;
        }

        public AddCurrentPageCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
