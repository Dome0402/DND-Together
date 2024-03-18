using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace DND_Together.Commands
{
    public class MoveCategoryDownCommand : CommandBase
    {
        OverviewTabViewModel _overviewTabViewModel { get; set; }
        public override void Execute(object parameter)
        {
            // Searching the index where the category is at
            int foundIndex = -1;
            for (int i = 0; i < _overviewTabViewModel.CategoryTabs.Count; i++)
            {
                if (_overviewTabViewModel.CategoryTabs[i].Header.ToString() == (string)parameter)
                {
                    foundIndex = i;
                    break;
                }
            }

            // If Category was not found
            if (foundIndex == -1)
            {
                MessageBox.Show("Ein Fehler ist aufgetreten. Kategorie wurde nicht gefunden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // If Category is already on bottom of the list
            if (foundIndex == _overviewTabViewModel.CategoryTabs.Count - 1)
            {
                return;
            }

            // In any other case the category can be moved
            List<TabItem> tabItems = new List<TabItem>();
            List<Category> categories = new List<Category>();
            for (int i = 0; i < _overviewTabViewModel.CategoryTabs.Count; i++)
            {
                if (i == foundIndex + 1)
                {
                    tabItems.Add(_overviewTabViewModel.CategoryTabs[foundIndex]);
                    categories.Add(_overviewTabViewModel.Scene.Categories[foundIndex]);
                    continue;
                }
                if (i == foundIndex)
                {
                    tabItems.Add(_overviewTabViewModel.CategoryTabs[i + 1]);
                    categories.Add(_overviewTabViewModel.Scene.Categories[i + 1]);
                    continue;
                }
                tabItems.Add(_overviewTabViewModel.CategoryTabs[i]);
                categories.Add(_overviewTabViewModel.Scene.Categories[i]);
            }
            _overviewTabViewModel.CategoryTabs = tabItems;
            _overviewTabViewModel.Scene.Categories = categories;
        }

        public MoveCategoryDownCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
