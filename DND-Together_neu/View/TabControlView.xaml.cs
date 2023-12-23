using System;
using System.Collections.Generic;
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

namespace DND_Together_neu.View
{
    /// <summary>
    /// Interaktionslogik für TabControlView.xaml
    /// </summary>
    public partial class TabControlView : UserControl
    {
        TabControl TabControl;
        public TabControlView()
        {
            InitializeComponent();
        }

        public void generateStartTabControl()
        {
            TabControl = new TabControl()
            {
                ItemsSource = new List<string>()
                {
                    "Test 1",
                    "Test 2"
                }
            };
            tabCategories.ItemsSource = TabControl.ItemsSource;
        }

        private void tabCategories_Loaded(object sender, RoutedEventArgs e)
        {
            tabCategories = (sender as TabControl);
        }

        private void btn_AddCategory_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabItem category in tabCategories.Items)
            {
                if(category.Header.ToString() == tf_CategoryName.Text)
                {
                    MessageBox.Show("Kategorie ist bereits vorhanden!", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            string trimCatName = tf_CategoryName.Text.Replace(" ", "");
            TabItem newTabItem = new TabItem
            {
                Header = tf_CategoryName.Text,
                Name = "tab_" + trimCatName,
                Padding = new Thickness(20,10,20,10),
            };
            tabCategories.Items.Add(newTabItem);

            // Textfeld leeren
            tf_CategoryName.Text = "";
        }

        TabItem tabEdit;
        private void btn_EditCategory_Click(object sender, RoutedEventArgs e)
        {
            // Wenn noch kein Tab editiert wird
            if(tabEdit == null)
            {
                tabEdit = (TabItem)tabCategories.SelectedItem;
                // Alle anderen Tabs deaktivieren
                foreach (TabItem category in tabCategories.Items)
                {
                    // Alle Tabs, außer dem zzt ausgewählten Tab, deaktivieren
                    if (category.Header.ToString() != tabEdit.Header.ToString())
                    {
                        // Tab deaktivieren
                        category.IsEnabled = false;
                    }
                }
                // Alle anderen Buttons deaktivieren
                btn_AddCategory.IsEnabled = false;
                btn_DeleteCategory.IsEnabled = false;

                // Das Textfeld fokusieren
                tf_CategoryName.Focus();

                // Den Text des Edit-Buttons zu einem Haken ändern
                btn_EditCategory.Content = "✓";

                // In das Textfeld den aktuellen Text des Tabs einfügen
                tf_CategoryName.Text = tabEdit.Header.ToString();
            }
            // Wenn gerade ein Tab editiert wird, alles wieder rückgängig machen
            else
            {
                tabEdit = (TabItem)tabCategories.SelectedItem;
                // Alle Tabs aktivieren
                foreach (TabItem category in tabCategories.Items)
                {
                    category.IsEnabled = true;
                }
                // Alle anderen Buttons aktivieren
                btn_AddCategory.IsEnabled = true;
                btn_DeleteCategory.IsEnabled = true;

                // Den Text des Edit-Buttons zu einem Zahnrad ändern
                btn_EditCategory.Content = "⚙";

                // Den Text vom Textfeld in den Tab übertragen
                tabEdit.Header = tf_CategoryName.Text;

                // Das Textfeld leeren
                tf_CategoryName.Text = "";

                // Editierter Tab leeren
                tabEdit = null;
            }
            // (optional, TODO) Geschriebener Text live mit dem Text auf dem Tab ändern
        }

        private void btn_DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            TabItem deleteTab = (TabItem)tabCategories.Items[tabCategories.SelectedIndex];
            if(MessageBox.Show("Sicher, dass die Kategorie \"" + deleteTab.Header.ToString() + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Debug.Print("Lösche Kategorie \"" + deleteTab.Header.ToString() + "\"...");
                foreach (TabItem category in tabCategories.Items)
                {
                    if (category.Header.ToString() == ((TabItem)tabCategories.SelectedItem).Header.ToString())
                    {
                        Debug.Print(category.Header.ToString());
                        tabCategories.Items.Remove(category);
                        return;
                    }
                }
            }
        }

        private void btn_AddPage_Click(object sender, RoutedEventArgs e)
        {
            // Wenn beide Felder, Titel und Url, nicht leer sind
            if (tf_PageName.Text != "" && tf_PageUrl.Text != "")
            {
                TabControl currentTabContent = (TabControl)((Grid)((TabItem)tabCategories.SelectedItem).Content).Children[0];
                if (currentTabContent != null)
                {
                    foreach (TabItem page in currentTabContent.Items)
                    {
                        if (page.Header.ToString() == tf_PageName.Text)
                        {
                            MessageBox.Show("Seite ist bereits vorhanden!", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        string trimPageName = tf_PageName.Text.Replace(" ", "");
                        TabItem newTabItem = new TabItem
                        {
                            Header = tf_PageName.Text,
                            Name = "tab_" + trimPageName,
                            Padding = new Thickness(20, 10, 20, 10),
                        };
                        currentTabContent.Items.Add(newTabItem);

                        // Textfeld leeren
                        tf_PageName.Text = "";
                    }
                }
            }
        }

        private void btn_EditPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_DeletePage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
