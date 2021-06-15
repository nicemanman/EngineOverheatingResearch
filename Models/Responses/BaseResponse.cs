using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Responses
{
    public class BaseResponse : IResponse
    {
        public BaseResponse(string testResult, ValidationResult result)
        {
            this.TestResult = testResult;
            ValidationResult = result;
        }
        public BaseResponse(string testResult)
        {
            this.TestResult = testResult;
        }
        public BaseResponse(ValidationResult result)
        {
            ValidationResult = result;
        }
        public bool IsValid => ValidationResult == null;
        public ValidationResult ValidationResult { get; }

        public string TestResult { get; }
    }
}
