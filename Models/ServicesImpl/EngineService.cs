using DomainModel.Common;
using DomainModel.EngineTests;
using DomainModel.EngineModels;
using DomainModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ServicesImpl
{
    public class EngineService : IEngineService
    {
        private Dictionary<EngineKinds, IEngine> engineKinds = new Dictionary<EngineKinds, IEngine>();
        private Dictionary<EngineTestsTypes, IEngineTest> testTypes = new Dictionary<EngineTestsTypes, IEngineTest>();
        private Dictionary<int, string> engineKindsForClient = new Dictionary<int, string>();
        private Dictionary<int, string> engineTestTypesForClient = new Dictionary<int, string>();
        public EngineService()
        {
            //Можно добавить любое количество видов двигателей
            engineKinds.Add(EngineKinds.InternalCombustion, new InternalCombustionEngine());
            int index = 1;
            foreach (var engine in engineKinds.Values)
            {
                engineKindsForClient.Add(index, engine.TypeName);
            }
            //Можно добавить любое количество типов тестов
            testTypes.Add(EngineTestsTypes.Overheating, new OverheatingEngineTest());
            index = 1;
            foreach (var testType in testTypes.Values)
            {
                engineTestTypesForClient.Add(index, testType.TestName);
            }
        }

        public Dictionary<int, string> GetEngineKinds()
        {
            return engineKindsForClient;
        }

        public Dictionary<int, string> GetEngineTestTypes()
        {
            return engineTestTypesForClient;
        }

        /// <summary>
        /// Запуск теста двигателя
        /// </summary>
        /// <param name="engineKind">Номер типа двигателя</param>
        /// <param name="testTypeIndex">Номер типа теста</param>
        /// <param name="info">Дополнительные параметры тесте</param>
        /// <returns></returns>
        public async Task<IResponse> StartEngineTest(int engineKind, int testTypeIndex, Dictionary<string, object> info)
        {
            engineKinds.TryGetValue((EngineKinds)engineKind, out var engine);
            if (engine == null) return new BaseResponse(new ValidationResult("Выбранного вида двигателя не существует!"));
               
            testTypes.TryGetValue((EngineTestsTypes)testTypeIndex, out var test);
            if (test == null) return new BaseResponse(new ValidationResult("Выбранного вида теста не существует!"));
            return await test.StartTest(engine, info);
        }
    }
}
