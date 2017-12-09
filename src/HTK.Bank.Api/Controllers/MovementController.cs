using HTK.Bank.Core.Models;
using HTK.Bank.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HTK.Bank.Api.Controllers
{
    public class MovementController : ApiController
    {
        private MovementService _movementService = new MovementService(Settings.DATABASE_FILE_PATH);

       
        [HttpGet]
        public Batch[] Get()
        {

            return _movementService.Get();
                    
        }

        [HttpPost]
        public string SaveMovements(List<Movement> movements)
        {       
            
            var headers = Request.Headers;
            var userName = headers.GetValues("UserName").First();

            return _movementService.Save(userName, movements).ToString();
         
        }
      
    }
}
