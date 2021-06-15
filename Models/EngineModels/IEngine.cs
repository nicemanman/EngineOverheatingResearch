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
        Task Start();
    }
}
