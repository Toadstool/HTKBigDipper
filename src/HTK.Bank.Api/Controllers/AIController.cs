using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Accord.Statistics.Testing;
using HTK.Bank.Core.Models;
using HTK.Bank.Core.Services;

namespace HTK.Bank.Api.Controllers
{
    public class AIController : ApiController
    {
        private MovementService _movementService = new MovementService(Settings.DATABASE_FILE_PATH);
        private DistributionService _distribiutionService = new DistributionService();
      

        [HttpGet]
        public string Test()
        {
            var input = new List<double[]>();
            var output = new List<double>();

            var batches = _movementService.Get();

            foreach (var batch in batches)
            {
                input.Add(batch.Movements.Take(20).Select(x => {

                    if(Double.IsNaN(x.AngleOfCurvature ?? 0))
                    {
                        return 0;
                    }                    
                    return x.AngleOfCurvature ?? 0; }
                ).ToArray());

                if(batch.UserName == "marek")
                {
                    output.Add(1);
                }else
                {
                    output.Add(0);
                }

            }
            var resoults = "";

            var svmService = new SVMService();
            svmService.Learn(input.ToArray(), output.ToArray());
            foreach (var batch in batches)
            {
                var check = new List<double[]>();
                check.Add(batch.Movements.Take(20).Select(x => {

                    if (Double.IsNaN(x.AngleOfCurvature ?? 0))
                    {
                        return 0;
                    }
                    return x.AngleOfCurvature ?? 0;
                }
                ).ToArray());


                var ans= svmService.Check(check.ToArray());


                resoults += batch.ID.ToString() + " " + batch.UserName + " " + ans[0].ToString() + "  ";

            }



            return resoults;
        }


        public double Test2()
        {
            var obs1 = _movementService.GetMovements(null, null, Measure.AngleOfCurvature);
            var obs2 = _movementService.GetMovements(null, "40c38b8c-d6c6-4c13-a5a8-15caa604c94f", Measure.AngleOfCurvature);
            var distance = _distribiutionService.Distance(obs1, obs2, 360);

            return 0;
        }



    }
}
