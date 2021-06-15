using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Responses
{
    /// <summary>
    /// Ответ от сервиса, в котором содержится информация о том, через сколько секунд/минут/часов двигатель перегреется
    /// </summary>
    public class OverheatingEngineResponse : BaseResponse
    {
        public OverheatingEngineResponse(string testResult, ValidationResult result = null) : base(testResult, result)
        {
            
        }
    }
}
