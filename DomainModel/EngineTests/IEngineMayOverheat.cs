using DomainModel.EngineModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EngineTests
{
    /// <summary>
    /// Маркер для отметки тех двигателей, которые могут перегреться
    /// </summary>
    public interface IEngineMayOverheat
    {
        bool IsOverheat { get; }
        double AmbientTemperature { get; set; }
    }
}
