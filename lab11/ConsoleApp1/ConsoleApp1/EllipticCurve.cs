using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class EllipticCurve
    {
        public BigInteger a;
        public BigInteger b;
        public BigInteger modulo;
        public BigInteger order;

        public EllipticCurve(BigInteger a, BigInteger b, BigInteger modulo, BigInteger order)
        {
            this.modulo = modulo;
            this.order = order;
            while (!CheckCurveParameters(a, b))
            {
                b = (b++) % modulo;
            }
            this.a = a;
            this.b = b;
        }

        private bool CheckCurveParameters(BigInteger a, BigInteger b)
        {
            return (4 * BigInteger.Pow(a, 3) + 27 * BigInteger.Pow(b, 2)) != 0;
        }
    }
}
