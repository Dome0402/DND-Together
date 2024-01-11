using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DND_Together.MVVM.Model
{
    public class CategoryMemento
    {
        public List<TabItem> CategoryTabs { get; set; }
        public TabItem SelectedCategory { get; set; }
    }
}
