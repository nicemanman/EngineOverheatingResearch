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
        public event Action<Dictionary<string, object>> ParametersSelected;

        public void Close(IConsoleView nextView)
        {
            Console.Clear();
        }

        public void InvokeInput(List<string> parametersNames)
        {
            var parametersString = Console.ReadLine();
            var parametersValues = parametersString.Split(',').ToList();
            var dict = new Dictionary<string, object>();
            if (parametersNames.Count != parametersValues.Count) throw new Exception("Ошибка при вводе аргументов");
            for (int i = 0; i < parametersNames.Count(); i++) 
            {
                dict.Add(parametersNames[i], parametersValues[i]);
            }
            ParametersSelected?.Invoke(dict);
        }

        public virtual void Show() 
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("Ошибка: "+errorMessage);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("Исключение:\n"+ex.StackTrace);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        
    }
}
