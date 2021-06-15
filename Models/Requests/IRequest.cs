using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Requests
{
    public interface IRequest
    {
        int EngineTypeKind { get; set; }
        int TestTypeIndex { get; set; }
        Dictionary<string, object> Info { get; set; }
    }
}
