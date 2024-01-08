﻿using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together.Commands
{
    public class EditPageCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            Debug.Print("Seite \"" + _overviewTabViewModel.PageName + "\" und der URL: \"" + _overviewTabViewModel.PageUrl + "\" wurde editiert!");
        }

        public EditPageCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
        public EditPageCommand()
        {
            
        }
    }
}