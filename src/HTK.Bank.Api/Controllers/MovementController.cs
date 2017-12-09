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
        public void Delete(string id= "3c3d9d1b-2168-4175-85b3-fb8d7a4062bc")
        {
            _movementService.Delete(new System.Guid(id));
        }

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
