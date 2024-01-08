using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Web.WebView2.Wpf;


namespace DND_Together.Commands
{
    internal class OpenSceneCommand : CommandBase
    {
        OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            if (_overviewTabViewModel.AreChanges && MessageBox.Show("Sie haben die Sitzung nicht gespeichert! Ohne Speichern fortfahren?", "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "DnD-Together Szenen(*.dndts)|*.dndts";
            dialog.DefaultDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == true)
            {
                LoadScene(dialog.FileName);
            }
        }

        private void LoadScene(string fileName)
        {
            
            try
            {
                // Clear TabControl and every Page and WebView in it hoping to free up memory
                foreach (TabItem item in _overviewTabViewModel.CategoryTabs)
                {
                    TabControl tabControlPages = (TabControl)item.Content;
                    if (tabControlPages != null)
                    {
                        foreach (TabItem page in tabControlPages.Items)
                        {
                            // Free up memory (hopefully)
                            ((WebView2)page.Content).Dispose();
                        }
                        tabControlPages.Items.Clear();
                    }
                }
                _overviewTabViewModel.CategoryTabs.Clear();

                // Load Scene from file to Scene object
                Scene scene = XML.LoadScene(fileName);
                List<TabItem> categories = new();

                // For every Category
                foreach (Category category in scene.Categories)
                {
                    // Create new TabItem (Category)
                    TabItem tab = new TabItem
                    {
                        Header = category.Name,
                        Padding = new Thickness(20, 10, 20, 10),
                        Content = new TabControl()
                        {
                            Style = Application.Current.Resources["InnerTabControlStyle"] as Style
                        },
                        Style = Application.Current.Resources["OuterTabControlItemStyle"] as Style
                    };
                    // For every Page
                    foreach (MVVM.Model.Page page in category.Pages)
                    {
                        // Create new TabItem (Page)
                        TabItem tabPage = new TabItem();
                        tabPage.Header = page.Title;
                        tabPage.Style = Application.Current.Resources["InnerTabControlItemStyle"] as Style;

                        // Create and initialize WebView
                        var webView = new WebView2();
                        Initialize_WebView(webView, new Uri(page.Url));

                        // Set WebView as Content
                        tabPage.Content = webView;

                        ((TabControl)tab.Content).Items.Add(tabPage);
                    }
                    // Add the Category to the list
                    categories.Add(tab);

                }
                // Overwrite CategoryTabs list with new Category list
                _overviewTabViewModel.CategoryTabs = categories;
                _overviewTabViewModel.CategoryName = "";
                _overviewTabViewModel.SelectedCategory = _overviewTabViewModel.CategoryTabs.Last();

                Application.Current.Windows[0].Title = "D&D Together - " + scene.Name;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Szene aufgetreten.\n" + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _overviewTabViewModel.AreChanges = false;
        }

        private void Initialize_WebView(WebView2 webView, Uri url)
        {
            webView.EnsureCoreWebView2Async(null);

            webView.Source = url;
        }

        public OpenSceneCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
