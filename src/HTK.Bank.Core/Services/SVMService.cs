using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;

namespace HTK.Bank.Core.Services
{
    public class SVMService
    {
        SupportVectorMachine _svm;


        public void Learn(double[][] input, double[] output)
        {

            var teacher = new LinearDualCoordinateDescent()
            {
                Loss = Loss.L2,
                Complexity = 1000,
                Tolerance = 1e-5
            };

            _svm = teacher.Learn(input, output);

         
        }
        
        public bool[] Check(double[][] check)
        {

            return _svm.Decide(check);          
          
        }




    }
}
