using Accord.Math.Distances;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Testing;
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
            var obs1 = GetMovements(null, null, Measure.AngleOfCurvature);
            var obs2 = GetMovements(null, "40c38b8c-d6c6-4c13-a5a8-15caa604c94f", Measure.AngleOfCurvature);

            var dis1 = new double[360];
            var dis2 = new double[360];
            var div1=  obs1.Sum();
            var div2 = obs2.Sum();

            for (int i = 0; i < 360; i++)
            {
                dis1[i] = obs1.Where(x => x <= i).Sum()/ div1;
                dis2[i] = obs2.Where(x => x <= i).Sum()/ div2;
            }

           
            var chi = new ChiSquareTest(dis1, dis2, 1);

            return 0;

        }

        private static double[] GetMovements(string user, string ID, Measure measure)
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

            if(measure== Measure.AngleOfCurvature)
            {
                return batches.SelectMany(x => x.Movements)
                .Select(x =>  x.AngleOfCurvature ?? 0 ).ToArray<double>();
            }

            if (measure == Measure.CurvatureDistance)
            {
                return batches.SelectMany(x => x.Movements)
                .Select(x => x.CurvatureDistance ?? 0).ToArray<double>();
            }

            if (measure == Measure.Direction)
            {
                return batches.SelectMany(x => x.Movements)
                .Select(x => x.Direction ?? 0).ToArray<double>();
            }


            return new double[] { };
        }
    }
}
