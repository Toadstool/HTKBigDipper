using HTK.Bank.Api.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HTK.Bank.Api.Controllers
{
    public class MouseController : ApiController
    {
        private static string DATABASE_NAME = @"D:\sandbox\HTK-Big-Dipper\HTK.Bank.db";

        [HttpGet]
        public Batch[] Get()
        {
            using (var db = new LiteDatabase(DATABASE_NAME))
            {
                var collection = db.GetCollection<Batch>();
                return collection.FindAll().ToArray<Batch>();
            }            
        }


        [HttpPost]
        public int TestMovement(List<Movement> movements)
        {            
            var headers = Request.Headers;
            var userName = headers.GetValues("UserName").First();

            using (var db = new LiteDatabase(DATABASE_NAME))
            {
                var collection = db.GetCollection<Batch>();
                var batch = new Batch();
                batch.ID = Guid.NewGuid();
                batch.UserName = userName;
                batch.Movements = movements.ToArray();
                collection.Insert(batch);
            }


            if (movements != null)
            {
                return movements.Count;
            }
            return -1;
        }


    }
}
