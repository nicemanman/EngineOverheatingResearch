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
        public double a { get => M / I; }
        public double I = 10;
        public double T_Overheating = 110;
        public double T_Engine = 0;
        public double T_Ambient = 0;
        public double Hm = 0.01;
        public double Hv = 0.0001;
        public double C = 0.1;
        public double M = 0;
        public double V = 0;
        public int[] startM = new int[] { 20, 75, 100, 105, 75, 0 };
        public int[] startV = new int[] { 0, 75, 150, 200, 250, 300 };
        public double Vc { get => C * (T_Ambient - T_Engine); }
        public double Vh { get => M * Hm + V * V * Hv; }
        public double secondsUptime;
        public double SecondsUptime { get => secondsUptime; }

        #endregion
        public async IAsyncEnumerable<IEngine> Start()
        {
            Reset();
            IsRunning = true;
            while (IsRunning) 
            {
                secondsUptime++;
                //каждую итерацию изменяем параметры двигателя
                yield return this;
            }
        }
        public void Stop() 
        {
            IsRunning = false;
            secondsUptime = 0;
        }
        private void Reset() 
        {
            I = 10;
            T_Overheating = 110;
            T_Engine = 0;
            T_Ambient = 0;
            Hm = 0.01;
            Hv = 0.0001;
            C = 0.1;
            M = 0;
            V = 0;
            T_Engine = T_Ambient;
        }
    }
}
