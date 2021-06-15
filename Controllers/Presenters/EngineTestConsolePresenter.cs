using DomainModel.Common;
using Presentation.Common;
using Presentation.Views;
using Presentation.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Presenters
{
    public class EngineTestConsolePresenter : BasePresenter<IEngineTestConsoleView>
    {
        private readonly IEngineService engineService;
        private int SelectedTemperature = 0;
        private int SelectedEngineType = 1;
        private int SelectedTestType = 1;
        private EngineTestConsoleViewWorkflow workflow;
        public EngineTestConsolePresenter(IApplicationController controller, IEngineTestConsoleView view, IEngineService engineService) : base(controller, view)
        {
            this.engineService = engineService;
            View.TemperatureSelected += TemperatureSelected;
            View.TestTypeSelected += TestTypeSelected;
            View.EngineKindSelected += EngineKindSelected;

            //можно добавить любое количество шагов, поменять их местами
            workflow = new EngineTestConsoleViewWorkflow(new List<IConsoleStep>()
            {
                new ConsoleStep(InvokeTemperatureInput),
                new ConsoleStep(InvokeEngineKindInput),
                new ConsoleStep(InvokeTestTypeInput),
                new ConsoleStep(InvokeStartTest)
            });
            while (!workflow.Stop)
                workflow.Execute().Wait();
        }

        private void EngineKindSelected(int obj)
        {
            SelectedEngineType = obj;
        }
        private void TestTypeSelected(int obj)
        {
            SelectedTestType = obj;
        }
        private void TemperatureSelected(int obj)
        {
            SelectedTemperature = obj;
        }

        private Task InvokeTestTypeInput() 
        {
            var testTypes = engineService.GetEngineTestTypes();
            View.ShowMessage("Выберите тип теста для данного двигателя: ");
            foreach (var index in testTypes.Keys)
            {
                var testName = testTypes[index];
                View.ShowMessage($"{index}.{testName}");
            }
            View.InvokeTestTypeInput();
            return Task.CompletedTask;
        }

        private Task InvokeTemperatureInput() 
        {
            View.InvokeTemperatureInput();
            return Task.CompletedTask;
        }
        
        private async Task InvokeStartTest() 
        {
            View.ShowMessage("Ожидайте выполнения теста");
            var response = await engineService.StartEngineTest(SelectedEngineType, SelectedTestType, new Dictionary<string, object>() 
            {
                { "Temperature" , SelectedTemperature}
            });
            if (response.IsValid)
            {
                View.ShowMessage(response.TestResult);
            }
            else
            {
                foreach (var result in response.ValidationResult.Messages)
                {
                    View.ShowError(result);
                }
            }
        }
        
        private Task InvokeEngineKindInput() 
        {
            var engineKinds = engineService.GetEngineKinds();
            View.ShowMessage("Выберите тип двигателя: ");
            foreach (var index in engineKinds.Keys)
            {
                var engineName = engineKinds[index];
                View.ShowMessage($"{index}.{engineName}");
            }
            View.InvokeEngineKindInput();
            return Task.CompletedTask;
        }
        
    }
}
