using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together.MVVM.ViewModels
{
    public class TabItemViewModel : ViewModelBase
    {
        public string Header { get; set; }
        public object Content { get; set; }
    }
}
