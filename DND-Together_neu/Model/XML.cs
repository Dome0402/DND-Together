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

        public static void SaveTabControls(IEnumerable<UIElement> tabControls)
        {
            // Jeder übergeordnete Tab
            foreach (UIElement uiElement in tabControls)
            {
                // hat ein eigenes TabControl als Kind
                TabControl tab = (TabControl) uiElement;
                foreach(TabItem tabItem in tab.Items)
                { 
                    // welches mehrere Tabs 
                    Debug.Print("Tab: " +  tabItem.Header);

                    // und diese jeweils eine eigene URL haben
                }
            }
        }
    }
}
