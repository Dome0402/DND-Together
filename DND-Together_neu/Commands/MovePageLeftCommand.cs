using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together.Commands
{
    public class MovePageLeftCommand : CommandBase
    {
        OverviewTabViewModel _overviewTabViewModel { get; set; }
        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public MovePageLeftCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
