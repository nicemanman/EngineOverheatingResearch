
using DomainModel.Common;
using DomainModel.EngineModels;
using DomainModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EngineTests
{
    public class OverheatingEngineTest : IEngineTest
    {
        public string Name => "Тест двигателя на перегрев";

        /// <summary>
        /// Длительность тестирования
        /// </summary>
        private static int SecondsToTest = 1800;


        //Можно добавить любое количество параметров к тесту, которые будут переданы с клиента
        public Dictionary<string, object> RequiredFields { get; set; } = new Dictionary<string, object>()
        {
            { "Temperature", "Температура окружающей среды (градусов цельсия)" },
            { "SecondsToTest", $"Длительность тестирования (секунд, при невалидном значении - {SecondsToTest} с.)" }
        };

      

        
        public Task<IResponse> StartTest(IEngine engine, Dictionary<string, object> info)
        {
            if (!(engine is IEngineMayOverheat)) 
            {
                return Task.FromResult<IResponse>(new Response($"Данный двигатель не может перегреться в принципе"));
            }
            if (double.TryParse(info["Temperature"].ToString(), out var temperature)) 
            {
                ((IEngineMayOverheat)engine).AmbientTemperature = temperature;
            }
            else 
            {
                return Task.FromResult<IResponse>(new Response(new ValidationResult("Ошибка валидации параметра - Температура окружающей среды")));
            }
            if (int.TryParse(info["SecondsToTest"].ToString(), out var secondsToTest))
            {
                SecondsToTest = secondsToTest;
            }
            engine.Stop();
            //Запускаем двигатель и следим за ним пока он не перегреется, либо пока не закончится время теста
            foreach (IEngineMayOverheat engineState in engine.Start()) 
            {
                if (engine.SecondsUptime < SecondsToTest)
                {
                    if (engineState.IsOverheat) 
                    {
                        var time = engine.SecondsUptime;
                        engine.Stop();
                        return Task.FromResult<IResponse>(new Response($"Двигатель перегреется через {time} с. при температуре окружающей среды {info["Temperature"]} градусов цельсия"));
                    }
                }
                else
                {
                    engine.Stop();
                    return Task.FromResult<IResponse>(new Response($"Двигатель не перегреется"));
                }
            }
            return Task.FromResult<IResponse>(new Response(new ValidationResult("Произошел сбой двигателя")));
        }
    }
}
