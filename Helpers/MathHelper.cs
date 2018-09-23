using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Helpers
{
    /// <summary>
    /// Contains math operation methods.
    /// </summary>
    public class MathHelper
    {
        public static int ModInverse(int a, int m)
        {
            if (m == 1) return 0;
            int m0 = m;
            (int x, int y) = (1, 0);

            while (a > 1)
            {
                int q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }
    }
}
