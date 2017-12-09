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
        //public List<IMovement> PrepareMouseMovements(Dictionary<double,int> expectation)
        //{
        //    var mousMovements = new List<IMovement>();
        //    foreach(var angle in expectation)
        //    {
        //        for(var quantity=0; quantity < angle.Value; quantity++)
        //        {
        //            mousMovements.Add(CreateMockedMovement(angle.Key));
        //        }
        //    }
        //    return mousMovements;
        //}

        //[TestMethod]
        //public void SHOULD_Generate_Distribution_based_on_list_of_movements()
        //{
        //    var mousMovements = PrepareMouseMovements(new Dictionary<double, int>
        //    {
        //        {10, 6},
        //        {20, 90},
        //        {30, 40},
        //        {40, 5},
        //        {50, 24},
        //        {60, 30},
        //        {70, 0},
        //        {80, 0},
        //        {90, 0},
        //        {100, 0},
        //        {110, 0},
        //        {120, 0},
        //        {130, 0},
        //        {140, 0},
        //    });
        //    var business = new Distribution();

        //    var distribution = business.CreateDistribution(mousMovements);
        //    Console.WriteLine(distribution);
        //}

        //[TestMethod]
        //public void SHOULD_Generate_ZScore()
        //{
        //    double[][] inputs = new double[][]
        //    {
        //        new double[]{ 1 },
        //        new double[]{ 1 },
        //        new double[]{ 1 },
        //        new double[]{ 1 },
        //        new double[]{ 1 },
        //        new double[]{ 1 },
        //        new double[]{ 1 },
        //    };
        //    var business = new Distribution();

        //    var zScore = business.GetZScore(inputs);
        //    var zCenter = business.GetCenter(inputs);
        //    var standardize = business.GetStandardize(inputs);
        //    Console.WriteLine(zScore);
        //}

        //private IMovement CreateMockedMovement(double angleOfCurvature)
        //{
        //    var mousMove = new Moq.Mock<IMovement>();
        //    mousMove.Setup(_ => _.AngleOfCurvature).Returns(angleOfCurvature);

        //    return mousMove.Object;
        //}
    }
}
