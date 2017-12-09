using System.Linq;
using Accord.Statistics.Testing;

namespace HTK.Bank.Core.Services
{
    public class AIService
    {
       
        public double Distance(double [] obs1, double[] obs2, int maxValue)
        {
            var dis1 = new double[maxValue];
            var dis2 = new double[maxValue];
            var div1 = obs1.Sum();
            var div2 = obs2.Sum();

            for (int i = 0; i < maxValue; i++)
            {
                dis1[i] = obs1.Where(x => x <= i).Sum() / div1;
                dis2[i] = obs2.Where(x => x <= i).Sum() / div2;
            }

            return new ChiSquareTest(dis1, dis2, 1).PValue;
        }





    }
}
