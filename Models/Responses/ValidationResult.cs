using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Responses
{
    public class ValidationResult
    {
        public string[] Messages;
        
        public ValidationResult(params string[] messages)
        {
            Messages = messages;
        }
    }
}
