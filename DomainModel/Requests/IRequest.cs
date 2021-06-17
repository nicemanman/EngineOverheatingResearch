using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Requests
{
    /// <summary>
    /// Здесь можно сделать IRequest более общим, оставив в нем лишь Info, а
    /// поля EngineTypeKind и TestTypeIndex вынести в отдельный интерфейс IEngineTestRequest
    /// но в рамках решения данной задачи я решил что этого не нужно, т.к. у нас нет других запросов
    /// </summary>
    public interface IRequest
    {
        int EngineTypeKind { get; set; }
        int TestTypeIndex { get; set; }
        Dictionary<string, object> Info { get; set; }
    }
}
