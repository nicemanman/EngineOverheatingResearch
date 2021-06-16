using Presentation.Common;
using Presentation.Views;
using Presentation.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class EngineTestView : BaseScreen, IEngineTestConsoleView
    {
        public event Action<double> TemperatureSelected;
        public event Action<int> EngineKindSelected;
        public event Action<int> TestTypeSelected;

        public void InvokeEngineKindInput()
        {
            bool engineKindSelected = false;
            int engineKindValue = 0;
            while (!engineKindSelected)
            {
                Console.WriteLine();
                Console.WriteLine("Введите номер типа двигателя:");
                string engineKindString = Console.ReadLine();
                if (!int.TryParse(engineKindString, out var integerEngineKind))
                {
                    Console.WriteLine("Введен некорректный номер типа двигатля, попробуйте еще раз");
                }
                else
                {
                    engineKindSelected = true;
                    engineKindValue = integerEngineKind;
                }
            }
            if (engineKindSelected)
            {
                EngineKindSelected?.Invoke(engineKindValue);
            }
        }

        public void InvokeTestTypeInput()
        {
            bool testTypeSelected = false;
            int testTypeValue = 0;
            while (!testTypeSelected)
            {
                Console.WriteLine();
                Console.WriteLine("Введите тип теста двигателя:");
                string testTypeString = Console.ReadLine();
                if (!int.TryParse(testTypeString, out var integerTestType))
                {
                    Console.WriteLine("Введен некорректный номер типа теста, попробуйте еще раз");
                }
                else
                {
                    testTypeSelected = true;
                    testTypeValue = integerTestType;
                }
            }
            if (testTypeSelected)
            {
                TestTypeSelected?.Invoke(testTypeValue);
            }
        }

    }
}
