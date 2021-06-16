using DomainModel.Common;
using DomainModel.Requests;
using DomainModel.Responses;
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
        private Dictionary<string, object> Info = new Dictionary<string, object>();
        private double SelectedTemperature = 0;
        private int SelectedEngineType = 1;
        private int SelectedTestType = 1;
        private EngineTestConsoleViewWorkflow workflow;
        public EngineTestConsolePresenter(IApplicationController controller, IEngineTestConsoleView view, IEngineService engineService) : base(controller, view)
        {
            this.engineService = engineService;
            
            View.TestTypeSelected += TestTypeSelected;
            View.EngineKindSelected += EngineKindSelected;
            View.ParametersSelected += View_ParametersSelected1;
            //можно добавить любое количество шагов, поменять их местами
            workflow = new EngineTestConsoleViewWorkflow(new List<IConsoleStep>()
            {
                new ConsoleStep(InvokeEngineKindInput),
                new ConsoleStep(InvokeTestTypeInput),
                new ConsoleStep(InvokeParametersInput),
                new ConsoleStep(InvokeStartTest)
            });
            while (!workflow.Stop)
                workflow.Execute().Wait();
        }

        private void View_ParametersSelected1(Dictionary<string, object> obj)
        {
            Info = obj;
        }

        private void EngineKindSelected(int obj)
        {
            SelectedEngineType = obj;
        }
        private void TestTypeSelected(int obj)
        {
            SelectedTestType = obj;
        }
        private void TemperatureSelected(double obj)
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

        private async Task InvokeParametersInput()
        {
            var parameters = await engineService.GetRequiredFieldsForTestAndEngine(new Request()
            {
                TestTypeIndex = SelectedTestType,
                EngineTypeKind = SelectedEngineType
            });
            View.ShowMessage("Введите параметры через запятую: ");
            if (!parameters.IsValid) 
            {
                ShowErrors(parameters.ValidationResult);
                return;
            }
            var index = 1;
            foreach (var parameter in parameters.Info.Keys)
            {
                var paramName = parameters.Info[parameter];
                View.ShowMessage($"{index}.{paramName}");
                index++;
            }
            View.InvokeInput(parameters.Info.Keys.ToList());
        }


        private Task InvokeTemperatureInput() 
        {
            View.InvokeTemperatureInput();
            return Task.CompletedTask;
        }
        
        private async Task InvokeStartTest() 
        {
            View.ShowMessage("Ожидайте выполнения теста");
            var response = await engineService.StartEngineTest(new Request() 
            {
                Info = Info,
                TestTypeIndex = SelectedTestType,
                EngineTypeKind = SelectedEngineType
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
        private void ShowErrors(ValidationResult result) 
        {
            foreach (var message in result.Messages)
            {
                View.ShowError(message);
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
