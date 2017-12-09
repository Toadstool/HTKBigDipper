using System;
using System.Collections.Generic;
using Accord.Statistics;
using Accord.Statistics.Distributions.Univariate;
using HTK.Bank.Core.Models;
using System.Linq;

namespace HTK.Bank.Core
{
    public class Distribution
    {
        public object CreateDistribution(List<IMovement> mousMovements)
        {
            var arrayOfAngleOfCurvature = mousMovements
                                            .Select(_ => _.AngleOfCurvature.HasValue ? _.AngleOfCurvature.Value/1000 : 0)
                                            .ToArray();
            var normalDistribution = new NormalDistribution();
            normalDistribution.Fit(arrayOfAngleOfCurvature);

            return normalDistribution.ToString();
        }
    }
}
