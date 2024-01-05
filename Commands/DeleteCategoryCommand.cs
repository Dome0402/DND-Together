using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together.Commands
{
    public class DeleteCategoryCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            Debug.Print("Kategorie \"" + _overviewTabViewModel.CategoryName + "\" gelöscht");
        }

        public DeleteCategoryCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
