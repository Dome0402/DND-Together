using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DND_Together.Commands
{
    public class GoHomeCommand : CommandBase
    {
        OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            if (_overviewTabViewModel.Scene.Categories == null || _overviewTabViewModel.SelectedCategory == null || _overviewTabViewModel.SelectedCategory.Content == null)
                return;
            TabItem currentPage = (TabItem)(_overviewTabViewModel.SelectedCategory.Content as TabControl).SelectedItem;
            foreach (Category category in _overviewTabViewModel.Scene.Categories)
            {
                foreach(MVVM.Model.Page page in category.Pages)
                {
                    if(page.Title == currentPage.Header.ToString())
                    {
                        (currentPage.Content as WebView2).Source = new Uri(page.HomeUrl);
                        return;
                    }
                }
            }
        }
        public GoHomeCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
