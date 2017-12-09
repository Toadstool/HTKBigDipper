using Accord.MachineLearning.VectorMachines.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTK.Bank.Core.Services
{
    public class SVMService
    {
        public bool[] Check(double[][] input, double[] output, double[][] check)
        {

            // Create a L2-regularized L2-loss support vector classification algorithm
            var teacher = new LinearDualCoordinateDescent()
            {
                Loss = Loss.L2,
                Complexity = 1000,
                Tolerance = 1e-5
            };

            // Use the algorithm to learn the machine
            var svm = teacher.Learn(input, output);

            
            return svm.Decide(check);          
          
        }

    }
}
