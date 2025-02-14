using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MagicConch.Support.EaseFunctions
{
    public class ElasticEase : EasingFunctionBase
    {
        public double Oscillations { get; set; } = 10; // 진동 횟수
        public double Springiness { get; set; } = 2;  // 탄성 강도

        protected override double EaseInCore(double t)
        {
            if (t == 0 || t == 1)
                return t;

            double scaledTime = t - 1;
            double exponent = Math.Pow(2, 10 * scaledTime);
            double sine = Math.Sin((scaledTime - (Springiness / (2 * Math.PI) * Math.Asin(1))) * (2 * Math.PI * Oscillations) / Springiness);

            return -(exponent * sine);
        }
    }
}
