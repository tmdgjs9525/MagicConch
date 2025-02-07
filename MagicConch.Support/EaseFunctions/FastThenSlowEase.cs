using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MagicConch.Support.EaseFunctions
{
    public class FastThenSlowEase : EasingFunctionBase
    {
        protected override double EaseInCore(double t)
        {
            if (t > 0.6)
            {
                return 2 * t * t;
            }
            else
            {
                return -1 + (4 - 0.3 * t) * t;
            }
        }
    }
}
