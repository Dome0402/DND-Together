using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DND_Together.Commands
{
    public class EditCategoryCommand : CommandBaseUndoRedo
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;

        private CategoryMemento _addedCategoryMemento;
        public override void Execute(object parameter)
        {
            if (_overviewTabViewModel.SelectedCategory != null)
            {
                string catName;
                if ((string)parameter != null && (string)parameter != "")
                {
                    catName = (string)parameter;
                }
                else
                {
                    catName = _overviewTabViewModel.CategoryName;
                }

                // If no category is being edited
                if (!_overviewTabViewModel.IsCategoryEditing)
                {
                    // Alle anderen Tabs deaktivieren
                    foreach(TabItem category in _overviewTabViewModel.CategoryTabs)
                    {
                        if(category.Header.ToString() != _overviewTabViewModel.SelectedCategory.Header.ToString())
                        {
                            category.IsEnabled = false;
                        }
                    }

                    _overviewTabViewModel.IsEnabledEditPage = false;
                    _overviewTabViewModel.IsEnabledOtherElements = false;

                    _overviewTabViewModel.ContentButtonEditCategory = "✓";

                    _overviewTabViewModel.CategoryName = _overviewTabViewModel.SelectedCategory.Header.ToString();

                    _overviewTabViewModel.IsCategoryEditing = true;
                }
                // If category is being edited
                else
                {


                    // Alle anderen Tabs aktivieren
                    foreach (TabItem category in _overviewTabViewModel.CategoryTabs)
                    {
                        if (category.Header.ToString() != _overviewTabViewModel.SelectedCategory.Header.ToString())
                        {
                            category.IsEnabled = true;
                        }
                    }

                    _overviewTabViewModel.SelectedCategory.Header = catName;


                    _overviewTabViewModel.IsEnabledOtherElements = true;
                    _overviewTabViewModel.IsEnabledEditPage = true;

                    _overviewTabViewModel.ContentButtonEditCategory = "⚙";

                    _overviewTabViewModel.CategoryName = "";

                    _overviewTabViewModel.IsCategoryEditing = false;

                    // For Undo
                    _addedCategoryMemento = new()
                    {
                        CategoryTabs = _overviewTabViewModel.CategoryTabs,
                        SelectedCategory = _overviewTabViewModel.SelectedCategory,
                    };
                    _overviewTabViewModel.UndoRedoManager.ExecuteCommand(this);
                }
            }


            Debug.Print("Kategorie \"" + _overviewTabViewModel.CategoryName + "\" bearbeitet");
        }

        public override void Undo()
        {
            if(_addedCategoryMemento != null)
            {
                _overviewTabViewModel.CategoryTabs = _addedCategoryMemento.CategoryTabs;
                _overviewTabViewModel.SelectedCategory = _addedCategoryMemento.SelectedCategory;
            }
        }

        public EditCategoryCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
