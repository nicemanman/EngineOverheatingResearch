using Presentation.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Common
{
    public interface IConsoleView
    {
        void Show();
        void Close(IConsoleView nextView);
        void ShowMessage(string message);
        void ShowSuccessMessage(string message);
        void ShowError(string errorMessage);
        void ShowError(Exception ex);
        void InvokeInput(List<string> list);
        event Action<Dictionary<string, object>> ParametersSelected;
    }
}
