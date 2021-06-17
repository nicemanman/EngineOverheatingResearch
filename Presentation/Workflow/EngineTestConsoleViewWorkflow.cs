using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Workflow
{
    public class EngineTestConsoleViewWorkflow : IConsoleWorkflow
    {
        private readonly List<IConsoleStep> steps;
        public EngineTestConsoleViewWorkflow(List<IConsoleStep> steps)
        {
            this.steps = steps;
        }

        public bool Stop { get; set; } = false;

        public async Task Execute()
        {
            foreach (var step in steps)
            {
                await step.Function?.Invoke();
            }
        }
    }
}
