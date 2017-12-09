using System.Linq;
using Accord.Statistics.Testing;
using System;

namespace HTK.Bank.Core.Services
{
    public class DistributionService
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

        public double Distance2(double[] obs1, double[] obs2, int maxValue)
        {
            var dis1 = GetDistribution(obs1, maxValue);
            var dis2 = GetDistribution(obs2, maxValue);

            return new ChiSquareTest(dis1, dis2, 1).PValue;
        }

        public double[] GetDistribution(double[] input, int maxValue)
        {
            var dis = new double[maxValue];
            var inputConverted = input.Select(_ => Math.Truncate(_)).ToList();
            for (int i = 0; i < maxValue; i++)
            {
                var values = inputConverted.Where(_ => _ == i + 1).ToList();
                dis[i] = values.Count / input.Length;
                if (values.Count > 0)
                    Console.WriteLine(string.Format("{0}\t{1}", i + 1, values.Count));
            }

            return dis;
        }
    }
}
