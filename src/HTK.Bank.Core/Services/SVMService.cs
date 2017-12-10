using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using HTK.Bank.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HTK.Bank.Core.Services
{
    public class SVMService
    {
        SupportVectorMachine _svm;

        public bool TestFactor(Factor measure, Batch[] batches, List<Movement> movements, string userName, int itemsNumber, double step
            ,Func<Factor,IEnumerable<Movement>,int, double, double[]> calculateVector)
        {
           
            var input = new List<double[]>();
            var output = new List<double>();


            foreach (var batch in batches)
            {
                input.Add(calculateVector(measure, batch.Movements,itemsNumber, step));

                if (batch.UserName == userName)
                {
                    output.Add(1);
                }
                else
                {
                    output.Add(0);
                }

            }

           
            Learn(input.ToArray(), output.ToArray());

            var check = new List<double[]>();
            check.Add(calculateVector(measure, movements,itemsNumber, step));


            return Check(check.ToArray())[0];
        }
      
        private void Learn(double[][] input, double[] output)
        {
            var teacher = new LinearDualCoordinateDescent()
            {
                Loss = Loss.L1,
                //Complexity = 1000,
                Tolerance = .2
            };
            _svm = teacher.Learn(input, output);         
        }

        private bool[] Check(double[][] check)
        {

            return _svm.Decide(check);          
          
        }

        public double[] CalculateVector(Factor measure, IEnumerable<Movement> movements,int itemsNumber, double step)
        {
            return movements.Take(itemsNumber).Select(x =>
            {
                if (measure == Factor.AngleOfCurvature)
                {
                    if (Double.IsNaN(x.AngleOfCurvature ?? 0))
                    {
                        return 0;
                    }
                    return x.AngleOfCurvature ?? 0;
                }
                if (measure == Factor.CurvatureDistance)
                {
                    if (Double.IsNaN(x.CurvatureDistance ?? 0))
                    {
                        return 0;
                    }
                    return x.CurvatureDistance ?? 0;
                }
                if (measure == Factor.Direction)
                {
                    if (Double.IsNaN(x.Direction ?? 0))
                    {
                        return 0;
                    }
                    return x.Direction ?? 0;
                }
                return 0;

            }).ToArray();
        }

        public double[] CalculateCDVector(Factor measure, IEnumerable<Movement> movements,int itemsNumber, double step)
        {
            var vector = CalculateVector(measure, movements, 100,1);

            var dis1 = new double[itemsNumber];           
            var sum = (double)vector.Count();
            
            for (int i = 0; i < itemsNumber; i+= 1)
            {
                var count = (double)vector.Where(x => x <= i * step).Count();
                dis1[i] = count/sum;               
            }
            return dis1;

        }


    }
}
