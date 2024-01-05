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
    public class EditCategoryCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        private bool inEdit = false;
        public override void Execute(object parameter)
        {
            if(_overviewTabViewModel.SelectedItem != null)
            {
                // Wenn gerade nichts editiert wird
                if (!inEdit)
                {
                    // Alle anderen Tabs deaktivieren
                    foreach(TabItem category in _overviewTabViewModel.CategoryTabs)
                    {
                        if(category.Header.ToString() != _overviewTabViewModel.SelectedItem.Header.ToString())
                        {
                            category.IsEnabled = false;
                        }
                    }

                    _overviewTabViewModel.IsEnabledEditPage = false;
                    _overviewTabViewModel.IsEnabledOtherElements = false;

                    _overviewTabViewModel.ContentButtonEditCategory = "✓";

                    _overviewTabViewModel.CategoryName = _overviewTabViewModel.SelectedItem.Header.ToString();

                    inEdit = true;
                }
                // Wenn gerade etwas editiert und am Ende bestätigt wird
                else
                {


                    // Alle anderen Tabs aktivieren
                    foreach (TabItem category in _overviewTabViewModel.CategoryTabs)
                    {
                        if (category.Header.ToString() != _overviewTabViewModel.SelectedItem.Header.ToString())
                        {
                            category.IsEnabled = true;
                        }
                    }

                    _overviewTabViewModel.SelectedItem.Header = _overviewTabViewModel.CategoryName;


                    _overviewTabViewModel.IsEnabledOtherElements = true;
                    _overviewTabViewModel.IsEnabledEditPage = true;

                    _overviewTabViewModel.ContentButtonEditCategory = "⚙";

                    _overviewTabViewModel.CategoryName = "";

                    inEdit = false;
                }
            }


            Debug.Print("Kategorie \"" + _overviewTabViewModel.CategoryName + "\" bearbeitet");
        }
        public EditCategoryCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
