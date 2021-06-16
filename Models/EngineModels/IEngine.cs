using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EngineModels
{
    public interface IEngine
    {
        string TypeName { get; }
        double SecondsUptime { get; }
        IAsyncEnumerable<IEngine> Start();
        void Stop();
    }
}
