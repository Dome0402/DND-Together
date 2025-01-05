using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DND_Together.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für CharacterViewControl.xaml
    /// </summary>
    public partial class OverviewTabView : UserControl
    {
        public OverviewTabView()
        {
            InitializeComponent();
        }

        private void tabCategories_Drop(object sender, DragEventArgs e)
        {
            var tabControl = sender as TabControl;
            var tabItemTarget = GetTargetTabItem(e.OriginalSource);
            if (tabItemTarget != null)
            {
                var tabItemSource = (TabItem)e.Data.GetData(typeof(TabItem));
                if (tabItemTarget != tabItemSource)
                {
                    int sourceIndex = tabControl.Items.IndexOf(tabItemSource);
                    int targetIndex = tabControl.Items.IndexOf(tabItemTarget);

                    // Die ObservableCollection des ViewModels verwenden
                    var tabItems = (ObservableCollection<TabItem>)tabControl.ItemsSource;

                    // Reihenfolge im ViewModel anpassen
                    tabItems.Remove(tabItemSource);
                    tabItems.Insert(targetIndex, tabItemSource);

                    tabItems.Remove(tabItemTarget);
                    tabItems.Insert(sourceIndex, tabItemTarget);


                    tabControl.ItemsSource = tabItems;
                    // Wir müssen ItemsSource nicht mehr neu setzen, da ObservableCollection automatisch Änderungen übernimmt
                    OnPropertyChanged(nameof(CategoryTabs));
                    tabControl.SelectedIndex = targetIndex;
                }
            }

        }

        private TabItem GetTargetTabItem(object originalSource)
        {
            var current = originalSource as DependencyObject;

            while (current != null)
            {
                var tabItem = current as TabItem;
                if (tabItem != null)
                {
                    return tabItem;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        private void tabCategories_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TabControl tabControl = sender as TabControl;
                TabItem tabItem = tabControl.SelectedItem as TabItem;

                if (tabItem != null)
                {
                    DragDrop.DoDragDrop(tabControl, tabItem, DragDropEffects.Move);
                }
            }
        }
    }
}
