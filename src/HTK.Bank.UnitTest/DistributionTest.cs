using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HTK.Bank.Core;
using System.Collections.Generic;
using HTK.Bank.Core.Models;

namespace HTK.Bank.UnitTest
{
    [TestClass]
    public class DistributionTest
    {
        [TestMethod]
        public void SHOULD_Generate_Distribution_based_on_list_of_movements()
        {
            var mousMovements = new List<IMovement>
            {
                CreateMockedMovement(150),
                CreateMockedMovement(270),
                CreateMockedMovement(10),
                CreateMockedMovement(150),
                CreateMockedMovement(280),
                CreateMockedMovement(150),
                CreateMockedMovement(359),
            };
            var business = new Distribution();

            var distribution = business.CreateDistribution(mousMovements);
            Console.WriteLine(distribution);
        }

        private IMovement CreateMockedMovement(double angleOfCurvature)
        {
            var mousMove = new Moq.Mock<IMovement>();
            mousMove.Setup(_ => _.AngleOfCurvature).Returns(angleOfCurvature);

            return mousMove.Object;
        }
    }
}
