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
    public class DeleteCategoryCommand : CommandBaseUndoRedo
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;

        private CategoryMemento _addedCategoryMemento;
        public override void Execute(object parameter)
        {

            if(_overviewTabViewModel.SelectedCategory != null)
            {
                if(MessageBox.Show("Sicher, dass die Kategorie \"" + _overviewTabViewModel.SelectedCategory.Header.ToString() + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    List<TabItem> categories = _overviewTabViewModel.CategoryTabs.ToList();
                    if (categories.Contains(_overviewTabViewModel.SelectedCategory))
                    {
                        // Delete Category from local scene object
                        foreach (Category category in _overviewTabViewModel.Scene.Categories)
                        {
                            if (category.Name == _overviewTabViewModel.SelectedCategory.Header.ToString())
                            {
                                _overviewTabViewModel.Scene.Categories.Remove(category);
                                break;
                            }
                        }

                        // Delete Category from ItemsSource of TabControl
                        categories.Remove(_overviewTabViewModel.SelectedCategory);

                        Debug.Print("Kategorie \"" + _overviewTabViewModel.SelectedCategory.Header.ToString() + "\" gelöscht");
                        _overviewTabViewModel.CategoryTabs = categories;

                        // For Undo
                        _addedCategoryMemento = new()
                        {
                            CategoryTabs = _overviewTabViewModel.CategoryTabs,
                            SelectedCategory = _overviewTabViewModel.SelectedCategory,
                        };
                        _overviewTabViewModel.UndoRedoManager.ExecuteCommand(this);
                    }
                    else
                    {
                        Debug.Print("Kategorie konnte nicht gelöscht werden.");
                    }
                }
                
            }
        }

        public override void Undo()
        {
            if (_addedCategoryMemento != null)
            {
                _overviewTabViewModel.CategoryTabs = _addedCategoryMemento.CategoryTabs;
                _overviewTabViewModel.SelectedCategory = _addedCategoryMemento.SelectedCategory;
            }
        }

        public DeleteCategoryCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
