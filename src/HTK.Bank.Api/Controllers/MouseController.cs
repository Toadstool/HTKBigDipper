using HTK.Bank.Api.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace HTK.Bank.Api.Controllers
{
    public class MouseController : ApiController
    {
        private string DATABASE_NAME = HostingEnvironment.MapPath(@"~/App_Data/HTK.Bank.db"); 

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

            for (int i = 0; i < movements.Count - 2; i++)
            {
                movements[i].Direction = Direction(movements[i], movements[i + 1]);
                movements[i].AngleOfCurvature = AngleOfCurvature(movements[i], movements[i + 1], movements[i + 2]);
                movements[i].CurvatureDistance =CurvatureDistance(movements[i], movements[i + 1], movements[i + 2]);
            }

            using (var db = new LiteDatabase(DATABASE_NAME))
            {
                var collection = db.GetCollection<Batch>();
                var batch = new Batch();
                batch.ID = Guid.NewGuid();
                batch.UserName = userName;
                batch.Movements = movements.ToArray();
                collection.Insert(batch);
            }
           
            return movements.Count;
        }

         
        private double? Direction(Movement m1, Movement m2)
        {
            double x = m2.X - m1.X;
            double y = m2.Y - m1.Y;
            double c = Math.Sqrt(x*x+y*y);
            if(c==0)
            {
                return null;
            }

            var direction= Math.Acos(x / c)*180/Math.PI;
            if(y<0)
            {
                return Math.Round(360 - direction,1);
            }
            return Math.Round(direction,1);
            
        }

        private double? AngleOfCurvature(Movement m1, Movement m2, Movement m3)
        {
            double x1 = m2.X - m1.X;
            double y1 = m2.Y - m1.Y;
            double c1 = Math.Sqrt(x1 * x1 + y1 * y1);

            double x2 = m3.X - m2.X;
            double y2 = m3.Y - m2.Y;
            double c2 = Math.Sqrt(x2 * x2 + y2 * y2);


            double x3 = m3.X - m1.X;
            double y3 = m3.Y - m1.Y;
            double c3 = Math.Sqrt(x3 * x3 + y3 * y3);
            if (c1 * c2 == 0)
            {
                return null;
            }
            var angle = Math.Acos((c1 * c1 + c2 * c2 - c3 * c3) / (2 * c1 * c2)) * 180 / Math.PI;

            return Math.Round(angle,1);

        }

        private double? CurvatureDistance(Movement m1, Movement m2, Movement m3)
        {
            double x1 = m2.X - m1.X;
            double y1 = m2.Y - m1.Y;
            double c1 = Math.Sqrt(x1 * x1 + y1 * y1);

            double x2 = m3.X - m2.X;
            double y2 = m3.Y - m2.Y;
            double c2 = Math.Sqrt(x2 * x2 + y2 * y2);


            double x3 = m3.X - m1.X;
            double y3 = m3.Y - m1.Y;
            double c3 = Math.Sqrt(x3 * x3 + y3 * y3);
            if (c1 * c3 == 0)
            {
                return null;
            }
            var angle = Math.Acos((c1 * c1 + c3 * c3 - c2 * c2) / (2 * c1 * c3));

            return Math.Round(c1*Math.Sin(angle)/c3,2);
        }
    }
}
