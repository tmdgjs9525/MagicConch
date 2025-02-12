using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MagicConch.Support.EaseFunctions
{
    public sealed class CustomEase : EasingFunctionBase
    {
        /// <inheritdoc />
        protected override double EaseInCore(double normalizedTime)
        {
            normalizedTime = Math.Max(0.0, Math.Min(1.0, normalizedTime));
            //return Math.Sin(normalizedTime * Math.PI / 2) * (1 - Math.Pow(2, -20 * normalizedTime));
            //return Math.Sin(normalizedTime * Math.PI / 2) * (1 - Math.Pow(2, -10 * normalizedTime));
            return 1.0 - Math.Pow(1.0 - normalizedTime * Math.Pow(normalizedTime, 4), 0.5);
        }
    }
}
