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
using System.IO;
using System.Security.Policy;
using Microsoft.Web.WebView2.Core;
using Page = DND_Together.MVVM.Model.Page;


namespace DND_Together.Commands
{
    internal class OpenSceneCommand : CommandBase
    {
        OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            if(parameter != null)
            {
                LoadSceneAsync((string)parameter);
                return;
            }
            if (Consts.SceneHasChanged && MessageBox.Show("Sie haben die Sitzung nicht gespeichert! Ohne Speichern fortfahren?", "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "DnD-Together Szenen(*.dndts)|*.dndts";
            dialog.DefaultDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == true)
            {
                LoadSceneAsync(dialog.FileName);

                Consts.SceneHasChanged = false;
            }
        }

        private async void LoadSceneAsync(string fileName)
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
                        //tabControlPages.Items.Clear();
                    }
                }
                _overviewTabViewModel.CategoryTabs.Clear();
                

                // Load Scene from file to Scene object
                Scene scene = XML.LoadScene(fileName);
                _overviewTabViewModel.Path = fileName;
                List<TabItem> categories = new();

                // For every Category
                foreach (Category category in scene.Categories)
                {
                    // Create new TabItem (Category)
                    TabItem tab = Helper.CreateCategoryTabItem(_overviewTabViewModel, category.Name);
                    // For every Page
                    List<TabItem> pages = new List<TabItem>();
                    foreach (MVVM.Model.Page page in category.Pages)
                    {
                        // Create new TabItem (Page)
                        TabItem tabPage = Helper.CreatePageTabItem(_overviewTabViewModel, page.Title);
                        // Create and initialize WebView
                        var webView = new WebView2();
                        Consts.Initialize_WebView(webView, new Uri(page.Url));

                        

                        // Set WebView as Content
                        tabPage.Content = webView;
                        
                        pages.Add(tabPage);
                    }
                    ((TabControl)tab.Content).ItemsSource = pages;
                    // Add the Category to the list
                    categories.Add(tab);

                }
                // Overwrite CategoryTabs list with new Category list
                _overviewTabViewModel.CategoryTabs = categories;
                _overviewTabViewModel.CategoryName = "";
                _overviewTabViewModel.SelectedCategory = _overviewTabViewModel.CategoryTabs.Last();

                // Change 
                Application.Current.Windows[0].Title = "D&D Together - " + scene.Name;
                _overviewTabViewModel.Scene = scene;

                if(scene.Version != Consts.XmlVersion)
                {
                    MessageBox.Show("Es wurde eine veraltete Datei geöffnet.\nSie wird nun aktualisiert...", "Veraltete Datei-Version", MessageBoxButton.OK, MessageBoxImage.Information);
                    foreach(Category cat in scene.Categories)
                    {
                        foreach(MVVM.Model.Page page in cat.Pages)
                        {
                            page.HomeUrl = page.Url;
                        }
                    }
                    scene.Version = Consts.XmlVersion;
                    _overviewTabViewModel.SaveSceneCommand.Execute(scene);
                }
                Consts.SceneHasChanged = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Szene aufgetreten.\n" + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public OpenSceneCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;

            string[] args = Environment.GetCommandLineArgs();

            try
            {
                if (File.Exists(args[1]))
                {
                    Execute(args[1]);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
