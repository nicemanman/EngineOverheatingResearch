using DomainModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Common
{
    public interface IEngineService
    {
        Dictionary<int, string> GetEngineKinds();
        Dictionary<int, string> GetEngineTestTypes();
        Task<IResponse> StartEngineTest(int engineKind, int testTypeIndex, Dictionary<string, object> info);
    }
}
