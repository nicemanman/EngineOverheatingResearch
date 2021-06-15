using DomainModel.Common;
using DomainModel.EngineTests;
using DomainModel.EngineModels;
using DomainModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Requests;

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

        /// <summary>
        /// Типы двигателей, которые поддерживает стенд
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetEngineKinds()
        {
            return engineKindsForClient;
        }

        /// <summary>
        /// Типы тестов, которые поддерживает стенд
        /// </summary>
        /// <returns></returns>
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
        public async Task<IResponse> StartEngineTest(IRequest request)
        {
            
            engineKinds.TryGetValue((EngineKinds)request.EngineTypeKind, out var engine);
            if (engine == null) return new Response(new ValidationResult("Выбранного вида двигателя не существует!"));
               
            testTypes.TryGetValue((EngineTestsTypes)request.TestTypeIndex, out var test);
            if (test == null) return new Response(new ValidationResult("Выбранного вида теста не существует!"));
            return await test.StartTest(engine, request.Info);
        }
    }
}
