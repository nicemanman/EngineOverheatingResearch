using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Workflow
{
    public class ConsoleStep : IConsoleStep
    {
        public ConsoleStep(Func<Task> Function)
        {
            this.Function = Function;
        }
        public Func<Task> Function {get;}
    }
}
