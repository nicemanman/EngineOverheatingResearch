using DomainModel.Common;
using DomainModel.EngineTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EngineModels
{
    /// <summary>
    /// Двигатель внутреннего сгорания
    /// </summary>
    public class InternalCombustionEngine : IEngine, IEngineMayOverheat
    {
        public string TypeName => "Двигатель внутреннего сгорания";
        private bool IsRunning = false;

        public bool IsOverheat { get => T_Engine > T_Overheating;}
        public double SecondsToOverheat { get; }
        public double AmbientTemperature { get; set; }


        #region Параметры двигателя
        public int MVIndex { get; set; } = 0;
        public double error { get => T_Overheating - T_Engine; }
        public double a { get => M / I; }
        public double I = 10;
        public double T_Overheating = 110;
        public double T_Engine = 0;
        public double Hm = 0.01;
        public double Hv = 0.0001;
        public double C = 0.1;
        public double M = 0;
        public double V = 0;
        public int[] startM = new int[] { 20, 75, 100, 105, 75, 0 };
        public int[] startV = new int[] { 0, 75, 150, 200, 250, 300 };
        public double Vc { get => C * (AmbientTemperature - T_Engine); }
        public double Vh { get => M * Hm + V * V * Hv; }
        public double secondsUptime;
        public double SecondsUptime { get => secondsUptime; }

        #endregion
        public IEnumerable<IEngine> Start()
        {
            Stop();
            if (IsOverheat) yield return this;
            IsRunning = true;
            while (IsRunning) 
            {
                secondsUptime++;
                V += a;
                if (MVIndex < startM.Count() - 2)
                    MVIndex += (V < startV[MVIndex + 1]) ? 0 : 1;
                double up = (V - startV[MVIndex]);
                double down = (startV[MVIndex + 1] - startV[MVIndex]);
                double factor = (startM[MVIndex + 1] - startM[MVIndex]);
                M = up / down * factor + startM[MVIndex];
                T_Engine += Vc + Vh;
                yield return this;
            }
        }
        public void Stop() 
        {
            IsRunning = false;
            secondsUptime = 0;
            ResetParameters();
        }
        private void ResetParameters() 
        {
            I = 10;
            T_Overheating = 110;
            T_Engine = AmbientTemperature;
            Hm = 0.01;
            Hv = 0.0001;
            C = 0.1;
            MVIndex = 0;
            M = startM[MVIndex];
            V = startV[MVIndex];
        }
    }
}
