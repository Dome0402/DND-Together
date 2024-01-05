using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DND_Together.Commands
{
    public class AddCategoryCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            // Debug.Print(_overviewTabViewModel.CategoryTabs.First().Header.ToString());

            if (_overviewTabViewModel.CategoryTabs == null)
            {
                _overviewTabViewModel.CategoryTabs = new List<TabItem>();
            }
            List<TabItem> categories = _overviewTabViewModel.CategoryTabs.ToList();
            if(categories != null && categories.Count() > 0)
            {
                foreach (TabItem category in categories)
                {
                    if (category.Header.ToString() == _overviewTabViewModel.CategoryName)
                    {
                        MessageBox.Show("Kategorie ist bereits vorhanden!", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
            }
            string trimCatName = _overviewTabViewModel.CategoryName.Replace(" ", "");

            TabItem newTabItem = new TabItem
            {
                Header = _overviewTabViewModel.CategoryName,
                Padding = new Thickness(20, 10, 20, 10),
                Content = new TabControl()
                //Style = (Style)FindResource("OuterTabControlItemStyle"),
            };
            // newTabItem.SetBinding(Button.IsEnabledProperty, "IsEnabledOtherElements");
            categories.Add(newTabItem);
            _overviewTabViewModel.CategoryTabs = categories;

            _overviewTabViewModel.CategoryName = "";

            Debug.Print("Kategorie \"" + _overviewTabViewModel.CategoryName + "\" hinzugefügt");

        }
        public AddCategoryCommand() { }
        public AddCategoryCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
            
        }
    }
}
