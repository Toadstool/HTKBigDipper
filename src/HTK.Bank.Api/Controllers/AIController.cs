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
            var obs1 = GetMovements(null,null);          

            var nor1 = new MultivariateNormalDistribution(3);
            nor1.Fit(obs1);

            var obs2 = GetMovements(null, "40c38b8c-d6c6-4c13-a5a8-15caa604c94f");
            var nor2 = new MultivariateNormalDistribution(3);
            nor2.Fit(obs1);


            var metric = new Accord.Math.Distances.Bhattacharyya();
            return metric.Distance(nor1, nor2);

            
        }

        private static double[][] GetMovements(string user, string ID)
        {
            var batches = new List<Batch>();
            using (var db = new LiteDatabase(Settings.DATABASE_FILE_PATH))
            {
                var collection = db.GetCollection<Batch>();
                batches = collection.FindAll().ToList<Batch>();
                if(!string.IsNullOrEmpty(ID))
                {
                    batches = batches.Where(x=>x.ID.ToString()==ID).ToList<Batch>();
                }

                if (!string.IsNullOrEmpty(user))
                {
                    batches = batches.Where(x => x.UserName== user).ToList<Batch>();
                }
            }

            double[][] observations = batches.SelectMany(x => x.Movements)
              .Select(x => new double[] { x.AngleOfCurvature ?? 0, x.CurvatureDistance ?? 0, x.Direction ?? 0 }).ToArray<double[]>();

            return observations;
        }
    }
}
