using DomainModel.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Responses
{
    public class Response : IResponse
    {
        public Response(string testResult, ValidationResult result)
        {
            this.TestResult = testResult;
            ValidationResult = result;
        }
        public Response(string testResult)
        {
            this.TestResult = testResult;
        }
        public Response(ValidationResult result)
        {
            ValidationResult = result;
        }
        public Response()
        {

        }
        public bool IsValid => ValidationResult == null;
        public ValidationResult ValidationResult { get; }

        public string TestResult { get; }

        public Dictionary<string, object> Info { get; set; }

    }
}
