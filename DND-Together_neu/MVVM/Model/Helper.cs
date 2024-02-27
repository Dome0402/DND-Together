using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DND_Together.MVVM.Model
{
    public static class Helper
    {
        public static TabItem CreateCategoryTabItem(OverviewTabViewModel _overviewTabViewModel , string name)
        {
            return new TabItem
            {
                Header = name,
                Content = new TabControl()
                {
                    Style = Application.Current.Resources["InnerTabControlStyle"] as Style
                },
                Style = Application.Current.Resources["OuterTabControlItemStyle"] as Style,
                ContextMenu = new ContextMenu()
                {
                    Items =
                    {
                        new MenuItem()
                        {
                            Header = "⮝ Nach Oben",
                            Command = _overviewTabViewModel.MoveCategoryUpCommand,
                            CommandParameter = name

                        },
                        new MenuItem()
                        {
                            Header = "⮟ Nach Unten",
                            Command = _overviewTabViewModel.MoveCategoryDownCommand,
                            CommandParameter = name
                        },
                        new Separator(),
                        new MenuItem()
                        {
                            Header = "Kategorie löschen",
                            Command = _overviewTabViewModel.DeleteCategoryCommand,
                            CommandParameter = name
                        },
                    }
                }
            };
        }

        public static TabItem CreatePageTabItem(OverviewTabViewModel _overviewTabViewModel, string name)
        {
            return new()
            {
                Header = name,
                Style = Application.Current.Resources["InnerTabControlItemStyle"] as Style,
                ContextMenu = new ContextMenu()
                {
                    Items =
                    {
                        new MenuItem()
                        {
                            Header = "⮜ Nach links",
                            Command = _overviewTabViewModel.MovePageLeftCommand,
                            CommandParameter = name
                        },
                        new MenuItem()
                        {
                            Header = "⮞ Nach Rechts",
                            Command = _overviewTabViewModel.MovePageRightCommand,
                            CommandParameter = name
                        },
                        new Separator(),
                        new MenuItem()
                        {
                            Header = "Seite löschen",
                            Command = _overviewTabViewModel.DeletePageCommand,
                            CommandParameter = name
                        },
                    }
                }
            };
        }
    }
}
