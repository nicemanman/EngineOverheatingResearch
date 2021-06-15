using Presentation.Common;
using Presentation.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public abstract class BaseScreen : IConsoleView
    {
        public void Close(IConsoleView nextView)
        {
            Console.Clear();
            
        }

        public string InvokeUserInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        public virtual void Show() { }

        public void ShowError(string errorMessage)
        {
            Console.WriteLine();
            Console.WriteLine(errorMessage);
        }

        public void ShowError(Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.StackTrace);
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }

        
    }
}
