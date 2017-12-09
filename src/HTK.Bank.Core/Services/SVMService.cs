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

        public bool TestFactor(Factor measure, Batch[] batches, List<Movement> movements, string userName)
        {
            var itemsNumber = Math.Min(movements.Count, 20);
            var input = new List<double[]>();
            var output = new List<double>();


            foreach (var batch in batches)
            {
                input.Add(CalculateVector(measure,itemsNumber, batch.Movements));

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
            check.Add(CalculateVector(measure,itemsNumber, movements));


            return Check(check.ToArray())[0];
        }

        private double[] CalculateVector(Factor measure,int itemsNumber, IEnumerable<Movement> movements)
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

        private void Learn(double[][] input, double[] output)
        {

            var teacher = new LinearDualCoordinateDescent()
            {
                Loss = Loss.L2,
                Complexity = 1000,
                Tolerance = 1e-5
            };

            _svm = teacher.Learn(input, output);

         
        }

        private bool[] Check(double[][] check)
        {

            return _svm.Decide(check);          
          
        }




    }
}
