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
    public class DeleteCategoryCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {

            if(_overviewTabViewModel.SelectedCategory != null)
            {
                if(MessageBox.Show("Sicher, dass die Kategorie \"" + _overviewTabViewModel.SelectedCategory.Header.ToString() + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    List<TabItem> categories = _overviewTabViewModel.CategoryTabs.ToList();
                    if (categories.Contains(_overviewTabViewModel.SelectedCategory))
                    {
                        categories.Remove(_overviewTabViewModel.SelectedCategory);

                        Debug.Print("Kategorie \"" + _overviewTabViewModel.SelectedCategory.Header.ToString() + "\" gelöscht");
                        _overviewTabViewModel.CategoryTabs = categories;
                    }
                    else
                    {
                        Debug.Print("Kategorie konnte nicht gelöscht werden.");
                    }
                }
            }
        }

        public DeleteCategoryCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
