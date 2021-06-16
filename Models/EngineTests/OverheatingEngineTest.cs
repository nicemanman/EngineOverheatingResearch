
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
        public string TestName => "Тест двигателя на перегрев";

        public List<string> RequiredFields { get; set; } = new List<string>()
        {
            "Temperature"
        };

        private const double ABSOLUTE_ERROR = 0.1;
        private const double MAX_TIME = 1800;
        public async Task<IResponse> StartTest(IEngine engine, Dictionary<string, object> info)
        {
            if (!(engine is IEngineMayOverheat)) 
            {
                return new Response($"Данный двигатель не может перегреться в принципе");
            }
            ((IEngineMayOverheat)engine).AmbientTemperature = (double)info["Temperature"];

            await foreach (IEngineMayOverheat engineState in engine.Start()) 
            {
                if (engine.SecondsUptime < MAX_TIME)
                {
                    if (engineState.IsOverheat) 
                    {
                        engine.Stop();
                        return new Response($"Двигатель перегреется через {engineState.SecondsToOverheat} секунд при температуре окружающей среды {info["Temperature"]} градусов цельсия");
                    }
                }
                else
                {
                    engine.Stop();
                    return new Response($"Двигатель не перегреется");
                }
            }

            //Запускаем двигатель и следим за ним пока он не перегреется, либо пока не закончится время теста
            return new Response(new ValidationResult("Произошел сбой двигателя"));
        }
    }
}
