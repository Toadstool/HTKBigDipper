using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using HTK.Bank.Api.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HTK.Bank.Api.Controllers
{
    public class AIController : ApiController
    {
        [HttpGet]
        public double Test()
        {

            var batches = new List<Batch>();
            using (var db = new LiteDatabase(Settings.DATABASE_FILE_PATH))
            {
                var collection = db.GetCollection<Batch>();
                collection.FindAll().ToList<Batch>();
            }

            double[][] observations = batches.SelectMany(x => x.Movements)
                .Select(x => new double[] { x.AngleOfCurvature??0, x.CurvatureDistance??0, x.Direction??0 }).ToArray<double[]>();

            // Create a base distribution with two dimensions
            var normal = new MultivariateNormalDistribution(2);

            // Fit the distribution to the data
            normal.Fit(observations, new NormalOptions()
            {
                Robust = true // do a robust estimation
            });



            return 0;
        }
    }
}
