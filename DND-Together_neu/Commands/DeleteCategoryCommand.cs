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
    public class DeleteCategoryCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {

            if(_overviewTabViewModel.SelectedCategory != null || (string)parameter != null)
            {
                string deleteCategory = _overviewTabViewModel.SelectedCategory.Header.ToString();
                if ((string)parameter != null)
                    deleteCategory = (string)parameter;

                if(MessageBox.Show("Sicher, dass die Kategorie \"" + deleteCategory + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    List<TabItem> categories = _overviewTabViewModel.CategoryTabs.ToList();

                    // Delete Category from local scene object
                    foreach (Category category in _overviewTabViewModel.Scene.Categories)
                    {
                        if (category.Name == deleteCategory)
                        {
                            _overviewTabViewModel.Scene.Categories.Remove(category);
                            TabItem tabFound = categories.Find(t => t.Header.ToString() == deleteCategory);
                            categories.Remove(tabFound);
                            break;
                        }
                    }

                    // (deprecated) Delete Category from ItemsSource of TabControl
                    // categories.Remove(_overviewTabViewModel.SelectedCategory);



                    Debug.Print("Kategorie \"" + _overviewTabViewModel.SelectedCategory.Header.ToString() + "\" gelöscht");
                    _overviewTabViewModel.CategoryTabs = categories;

                    Consts.SceneHasChanged = true;
                }
                
            }
        }

        public DeleteCategoryCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
