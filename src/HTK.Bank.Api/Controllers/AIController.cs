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
        private AIService _aiService = new AIService();


        [HttpGet]
        public double Test()
        {
            var obs1 = _movementService.GetMovements(null, null, Measure.AngleOfCurvature);
            var obs2 = _movementService.GetMovements(null, "40c38b8c-d6c6-4c13-a5a8-15caa604c94f", Measure.AngleOfCurvature);
            var distance = _aiService.Distance(obs1, obs2,360);




            return 0;

        }

      
    }
}
