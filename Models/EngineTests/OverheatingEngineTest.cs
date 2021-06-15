
using DomainModel.Common;
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

        public async Task<IResponse> StartTest(IEngine engine)
        {
            await Task.Delay(2000);
            return new OverheatingEngineResponse("Двигатель перегреется через 30 секунд");
        }
    }
}
