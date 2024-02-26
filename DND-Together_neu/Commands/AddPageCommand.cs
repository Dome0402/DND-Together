using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
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
                // Load everything needed
                TabControl pages = _overviewTabViewModel.SelectedCategory.Content as TabControl;
                if(pages == null) pages = new TabControl();
                if(pages.Items == null) pages.ItemsSource = new List<TabItem>();

                // If the Name for the Page is already existing throw a message box and quit
                foreach(TabItem page in pages.Items)
                {
                    if(page.Header.ToString() == _overviewTabViewModel.PageName)
                    {
                        MessageBox.Show("Seite \"" + page.Header.ToString() + "\" ist bereits vorhanden!", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Create TabItem for Page and Initialize WebView
                TabItem newTabItem = Helper.CreatePageTabItem(_overviewTabViewModel, _overviewTabViewModel.PageName);

                WebView2 webView = new WebView2();
                Consts.Initialize_WebView(webView, new Uri(_overviewTabViewModel.PageUrl));
                newTabItem.Content = webView;


                // Add the Page as a TabItem to the selected Category
                List<TabItem> catPages = new List<TabItem>();

                foreach (Category category in _overviewTabViewModel.Scene.Categories)
                {
                    if (category.Name == _overviewTabViewModel.SelectedCategory.Header.ToString())
                    {
                        category.Pages.Add(new MVVM.Model.Page()
                        {
                            Title = newTabItem.Header.ToString(),
                            Url = _overviewTabViewModel.PageUrl,
                            HomeUrl = _overviewTabViewModel.PageUrl
                        });
                    }
                }
                foreach (TabItem p in pages.Items)
                {
                    catPages.Add(p);
                }
                catPages.Add(newTabItem);
                if(pages.Items != null && pages.Items.Count > 0 && pages.ItemsSource == null) pages.Items.Clear();
                
                pages.ItemsSource = catPages;

                Debug.Print("Seite \"" + _overviewTabViewModel.PageName + "\" und der URL: \"" + _overviewTabViewModel.PageUrl + "\" wurde erstellt!");

                // Some after-work additions
                pages.SelectedIndex = pages.Items.Count - 1;

                _overviewTabViewModel.PageName = "";
                _overviewTabViewModel.PageUrl = "";
                Consts.SceneHasChanged = true;
            }
        }

        public AddPageCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
