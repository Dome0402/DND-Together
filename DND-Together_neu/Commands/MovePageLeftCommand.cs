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
    public class MovePageLeftCommand : CommandBase
    {
        OverviewTabViewModel _overviewTabViewModel { get; set; }
        public override void Execute(object parameter)
        {
            // Searching the index of the page in the selected Category and the index of the selected category
            TabControl tabControl = (TabControl)_overviewTabViewModel.SelectedCategory.Content;
            int pageIndex = -1;
            for(int i = 0; i < tabControl.Items.Count; i++)
            {
                if (((TabItem)tabControl.Items[i]).Header.ToString() == (string)parameter)
                {
                    pageIndex = i;
                    break;
                }
            }
            int categoryIndex = -1;
            for(int i = 0; i < _overviewTabViewModel.CategoryTabs.Count; i++)
            {
                if (_overviewTabViewModel.CategoryTabs[i].Header.ToString() == _overviewTabViewModel.SelectedCategory.Header.ToString())
                {
                    categoryIndex = i;
                    break;
                }
            }


            //Debug.Print("Page found at: " + pageIndex);

            // If Page was not found
            if (pageIndex == -1 || categoryIndex == -1)
            {
                MessageBox.Show("Ein Fehler ist aufgetreten. Seite wurde nicht gefunden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // If Page is on the left
            if (pageIndex == 0)
            {
                return;
            }

            // In any other case the page can be moved
            // W.I.P. --> TODO!!
            List<TabItem> tabItems = new List<TabItem>();
            List<MVVM.Model.Page> pages = new List<MVVM.Model.Page>();
            for (int i = 0; i < tabControl.Items.Count; i++)
            {
                if (i == pageIndex - 1)
                {
                    tabItems.Add((TabItem)((TabControl)(_overviewTabViewModel.SelectedCategory.Content)).Items[pageIndex]);
                    pages.Add(_overviewTabViewModel.Scene.Categories[categoryIndex].Pages[pageIndex]);
                    continue;
                }
                if (i == pageIndex)
                {
                    tabItems.Add((TabItem)((TabControl)(_overviewTabViewModel.SelectedCategory.Content)).Items[i - 1]);
                    pages.Add(_overviewTabViewModel.Scene.Categories[pageIndex].Pages[i - 1]);
                    continue;
                }
                tabItems.Add((TabItem)((TabControl)(_overviewTabViewModel.SelectedCategory.Content)).Items[i]);
                pages.Add(_overviewTabViewModel.Scene.Categories[pageIndex].Pages[i]);
            }

            ((TabControl)_overviewTabViewModel.SelectedCategory.Content).ItemsSource = tabItems;
            _overviewTabViewModel.Scene.Categories[categoryIndex].Pages = pages;
        }

        public MovePageLeftCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
