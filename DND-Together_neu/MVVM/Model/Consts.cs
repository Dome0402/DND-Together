using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DND_Together.MVVM.Model
{
    public static class Consts
    {
        public static string XmlVersion = "1.0";
        public static bool SceneHasChanged(OverviewTabViewModel overviewTabViewModel)
        {
            if(overviewTabViewModel.Path != null && overviewTabViewModel.Path != "")
            {
                Scene sceneLoad = XML.LoadScene(overviewTabViewModel.Path);
                Scene sceneLocal = overviewTabViewModel.Scene;
                for (int c = 0; c < sceneLoad.Categories.Count; c++)
                {
                    for (int p = 0; p < sceneLoad.Categories[c].Pages.Count; p++)
                    {
                        MVVM.Model.Page page = sceneLoad.Categories[c].Pages[p];
                        TabItem pageItem = ((overviewTabViewModel.CategoryTabs[c].Content as TabControl).Items[p]) as TabItem;
                        if (page.Url != (pageItem.Content as WebView2).Source.ToString()) return true;
                    }
                }
                if (sceneLoad.Equals(sceneLocal)) return false;
                
                return true;
            }
            else
            {
                return false;
            }
            

        }
    }
}
