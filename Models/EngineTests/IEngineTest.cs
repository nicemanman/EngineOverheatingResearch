﻿using DomainModel.Common;
using DomainModel.EngineModels;
using DomainModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EngineTests
{
    public interface IEngineTest
    {
        string TestName { get; }
        List<string> RequiredFields { get; set; }
        Task<IResponse> StartTest(IEngine engine, Dictionary<string, object> info);
    }
}
