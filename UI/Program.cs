using Presentation.Common;
using Presentation.Presenters;
using Presentation.Views;
using System;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = ApplicationController.Current;
            
            Console.ForegroundColor = ConsoleColor.Gray;
            controller.RegisterView<IEngineTestConsoleView, EngineTestView>();
            controller.Run<EngineTestConsolePresenter>();
        }
    }
}
