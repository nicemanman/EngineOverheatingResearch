using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Requests
{
    public class Request : IRequest
    {
        public int EngineTypeKind { get; set; }

        public int TestTypeIndex { get; set; }

        public Dictionary<string, object> Info { get; set; }
    }
}
