﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel()
        {
            CurrentViewModel = new OverviewTabViewModel();
        }
    }
}
