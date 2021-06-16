﻿using DomainModel.EngineModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EngineTests
{
    public interface IEngineMayOverheat
    {
        bool IsOverheat { get; }
        double SecondsToOverheat { get; }
        double AmbientTemperature { get; set; }
    }
}
