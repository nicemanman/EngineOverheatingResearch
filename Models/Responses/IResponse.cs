using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Responses
{
    public interface IResponse
    {
        bool IsValid { get; }
        ValidationResult ValidationResult { get; }
        string TestResult { get; }
        Dictionary<string, object> Info { get; }
    }
}
