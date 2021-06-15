using DomainModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EngineModels
{
    /// <summary>
    /// Двигатель внутреннего сгорания
    /// </summary>
    public class InternalCombustionEngine : IEngine
    {
        public string TypeName => "Двигатель внутреннего сгорания";

        public Task Start()
        {
            throw new NotImplementedException();
        }
    }
}
