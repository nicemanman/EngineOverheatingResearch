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
        private int SelectedTemperature;
        private int SelectedEngineType;
        private int SelectedTestType;
        public EngineTestConsolePresenter(IApplicationController controller, IEngineTestConsoleView view, IEngineService engineService) : base(controller, view)
        {
            this.engineService = engineService;
            View.TemperatureSelected += View_TemperatureSelected;
            View.TestTypeSelected += View_TestTypeSelected;
            View.EngineKindSelected += View_EngineKindSelected;
            var workflow = new EngineTestConsoleViewWorkflow(new List<IConsoleStep>()
            {
                new ConsoleStep(InvokeTemperatureInput),
                new ConsoleStep(InvokeEngineKindInput),
                new ConsoleStep(InvokeTestTypeInput),
                new ConsoleStep(InvokeStartTest)
            });
            workflow.Execute().Wait();
        }

        private void View_EngineKindSelected(int obj)
        {
            SelectedEngineType = obj;
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
        private void View_TestTypeSelected(int obj)
        {
            SelectedTestType = obj;
        }
        private async Task InvokeStartTest() 
        {
            View.ShowMessage("Ожидайте выполнения теста");
            var response = await engineService.StartEngineTest(SelectedEngineType, SelectedTestType);
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
        private void View_TemperatureSelected(int obj)
        {
            SelectedTemperature = obj;
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
