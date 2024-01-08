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
    public class AddPageCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            if(_overviewTabViewModel.PageName != "" && 
                _overviewTabViewModel.PageUrl != "" && 
                _overviewTabViewModel.SelectedCategory != null)
            {
                TabControl pages = _overviewTabViewModel.SelectedCategory.Content as TabControl;
                if(pages == null) pages = new TabControl();
                if(pages.ItemsSource == null) pages.ItemsSource = new List<TabItem>();

                foreach(TabItem page in pages.ItemsSource)
                {
                    if(page.Header.ToString() == _overviewTabViewModel.PageName)
                    {
                        MessageBox.Show("Seite \"" + page.Header.ToString() + "\" ist bereits vorhanden!", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                TabItem newTabItem = new()
                {
                    Header = _overviewTabViewModel.PageName,
                    Style = Application.Current.Resources["InnerTabControlItemStyle"] as Style
                };
                WebView2 webView = new WebView2();
                Initialize_WebView(webView, new Uri(_overviewTabViewModel.PageUrl));
                newTabItem.Content = webView;
                List<TabItem> catPages = new List<TabItem>();
                
                foreach(TabItem p in pages.ItemsSource)
                {
                    catPages.Add(p);
                }
                catPages.Add(newTabItem);

                pages.ItemsSource = catPages;

                // List<TabItem> newCategories = _overviewTabViewModel.CategoryTabs.ToList();
                // _overviewTabViewModel.CategoryTabs.Find(c => c.Header.ToString() == _overviewTabViewModel.SelectedCategory.Header.ToString()).Content = pages;
                
                // _overviewTabViewModel.SelectedCategory.Content = pages;

                Debug.Print("Seite \"" + _overviewTabViewModel.PageName + "\" und der URL: \"" + _overviewTabViewModel.PageUrl + "\" wurde erstellt!");

                pages.SelectedIndex = pages.Items.Count - 1;

                _overviewTabViewModel.PageName = "";
                _overviewTabViewModel.PageUrl = "";
                _overviewTabViewModel.AreChanges = true;
            }
        }

        private void Initialize_WebView(WebView2 webView, Uri url)
        {
            webView.EnsureCoreWebView2Async(null);
            webView.Source = url;
        }

        public AddPageCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
