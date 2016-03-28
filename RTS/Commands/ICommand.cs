using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    /// <summary>
    /// An executable action to be carried out
    /// </summary>
    public interface ICommand
    {
        void Do();
        bool IsComplete();
    }
}
