using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
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
    public class DeletePageCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            // If a category is selected
            if ((_overviewTabViewModel.SelectedCategory != null && _overviewTabViewModel.SelectedCategory.Content != null) || ((string)parameter != null && (string) parameter != ""))
            {
                string deletePage;
                if((string)parameter != null && (string)parameter != "")
                    deletePage = (string)parameter;
                else
                    deletePage = ((_overviewTabViewModel.SelectedCategory.Content as TabControl).SelectedItem as TabItem).Header.ToString();


                // If Message Box was confirmed
                if ((deletePage != null && deletePage != "") && 
                    MessageBox.Show("Sicher, dass die Seite \"" + deletePage + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    TabControl pages = _overviewTabViewModel.SelectedCategory.Content as TabControl;
                    int selectedPageIndex = (_overviewTabViewModel.SelectedCategory.Content as TabControl).SelectedIndex;
                    List<TabItem> catPages = new List<TabItem>();

                    foreach (Category cat in _overviewTabViewModel.Scene.Categories)
                    {
                        if (cat.Name == _overviewTabViewModel.SelectedCategory.Header.ToString())
                        {
                            foreach (MVVM.Model.Page p in cat.Pages)
                            {
                                if (p.Title == deletePage)
                                {
                                    cat.Pages.Remove(p);
                                    break;
                                }
                            }
                        }
                    }

                    foreach (TabItem p in pages.ItemsSource)
                    {
                        if(p.Header.ToString() != deletePage)
                            catPages.Add(p);
                    }

                    pages.ItemsSource = catPages;

                    Consts.SceneHasChanged = true;
                }
            }

            Debug.Print("Seite \"" + _overviewTabViewModel.PageName + "\" und der URL: \"" + _overviewTabViewModel.PageUrl + "\" wurde gelöscht!");
        }

        public DeletePageCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
