using DND_Together.Commands;
using DND_Together.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DND_Together.MVVM.Model
{
    public class UndoRedoManager
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;

        // List of Actions so that they can be reversed
        private Stack<CommandBaseUndoRedo> undoStack = new Stack<CommandBaseUndoRedo>();
        private Stack<CommandBaseUndoRedo> redoStack = new Stack<CommandBaseUndoRedo>();

        public void ExecuteCommand(CommandBaseUndoRedo command)
        {
            // command.Execute(param);
            CommandBaseUndoRedo undoCommand = command;
            undoStack.Push(undoCommand);
            redoStack.Clear();
        }
        public void Undo()
        {
            if(undoStack.Count > 0 && !_overviewTabViewModel.IsCategoryEditing && !_overviewTabViewModel.IsPageEditing)
            {
                CommandBaseUndoRedo command = undoStack.Pop();
                command.Undo();
                redoStack.Push(command);
            }
        }

        // Ggf. wird eine zweite Methode Redo(object? param) benötigt
        public void Redo()
        {
            if(redoStack.Count > 0 && !_overviewTabViewModel.IsCategoryEditing && !_overviewTabViewModel.IsPageEditing)
            {
                CommandBaseUndoRedo command = redoStack.Pop();
                command.Execute(null);
                undoStack.Push(command);
            }
        }

        public UndoRedoManager(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
