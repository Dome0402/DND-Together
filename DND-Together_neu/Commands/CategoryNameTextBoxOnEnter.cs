using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DND_Together.Commands
{
    public class CategoryNameTextBoxOnEnter : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {

            // If in edit mode
            if (_overviewTabViewModel.IsCategoryEditing)
            {
                _overviewTabViewModel.EditCategoryCommand.Execute((string)((TextBox)parameter).Text);
            }
            // If not in edit mode
            else
            {
                _overviewTabViewModel.AddCategoryCommand.Execute((string)((TextBox)parameter).Text);
            }

        }

        public CategoryNameTextBoxOnEnter(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
