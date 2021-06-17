using Presentation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Views
{
    public interface IEngineTestConsoleView : IConsoleView
    {
        
        
        event Action<int> EngineKindSelected;
        event Action<int> TestTypeSelected;
        
        void InvokeEngineKindInput();
        void InvokeTestTypeInput();
    }
}
