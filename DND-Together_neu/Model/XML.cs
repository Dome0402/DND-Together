using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DND_Together_neu.Model
{
    public static class XML
    {
        public static IEnumerable<TabControl> GetTabControls()
        {

            return new TabControl[] { };
        }
        /// <summary>
        /// Saves the parent TabControl into an XML File in the "Data"-Folder.
        /// If no "Data"-Folder exists it creates a new one.
        /// </summary>
        /// <param name="tabControls">Parent TabControl where each Tab contains another TabControl with Tabs (pages)</param>
        public static void SaveTabControls(TabControl tabControls)
        {
            try
            {
                string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                // Jeder übergeordnete Tab
                foreach (TabControl uiElement in tabControls.Items)
                {
                    xml += "\n<Scene>";
                    // hat ein eigenes TabControl als Kind
                    TabControl tab = (TabControl)uiElement;

                    xml += "\n\t<Category>";
                    foreach (TabItem tabItem in tab.Items)
                    {
                        xml += "\n\t\t<Category>";
                        // welches mehrere Tabs 
                        Debug.Print("Tab: " + tabItem.Header);

                        // und diese jeweils eine eigene URL haben
                        xml += "\n\t\t\t<Name>";
                        xml += tabItem.Header.ToString();
                        xml += "</Name>";

                        xml += "\n\t\t\t<Pages>";

                        foreach(TabItem page in tab.Items)
                        {
                            // TODO
                        }

                        xml += "\n\t\t\t</Pages>";

                        xml += "\n\t\t</Category>";
                    }
                    xml += "\n\t</Categories>";

                    xml += "\n</Scene>";
                }
            }
            catch (InvalidCastException e)
            {
                Debug.Print(e.Message);
                MessageBox.Show("Übergebenes TabControl beinhaltet falsche Items.", "Achtung!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
