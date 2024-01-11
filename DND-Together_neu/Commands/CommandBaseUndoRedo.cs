using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together.Commands
{
    public abstract class CommandBaseUndoRedo : CommandBase
    {
        public abstract void Undo();
    }
}
