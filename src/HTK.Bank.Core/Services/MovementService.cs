using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTK.Bank.Core.Models;
using LiteDB;

namespace HTK.Bank.Core.Services
{
    public class MovementService
    {
        private string _databasePath;
        public MovementService(string databasePath)
        {
            _databasePath = databasePath;
        }

        public Batch[] Get()
        {
            using (var db = new LiteDatabase(_databasePath))
            {
                var collection = db.GetCollection<Batch>();
                return collection.FindAll().ToArray<Batch>();
            }
        }
       

        public Guid Save(string userName, List<Movement> movements)
        {
           

            for (int i = 0; i < movements.Count - 2; i++)
            {
                movements[i].Direction = Direction(movements[i], movements[i + 1]);
                movements[i].AngleOfCurvature = AngleOfCurvature(movements[i], movements[i + 1], movements[i + 2]);
                movements[i].CurvatureDistance = CurvatureDistance(movements[i], movements[i + 1], movements[i + 2]);
            }
            var ID = Guid.NewGuid();
            using (var db = new LiteDatabase(_databasePath))
            {
                var collection = db.GetCollection<Batch>();
                var batch = new Batch();
                batch.ID = ID;
                batch.UserName = userName;
                batch.Movements = movements.ToArray();
                collection.Insert(batch);
            }

            return ID;
        }

        public double[] GetMovements(string user, string ID, Measure measure)
        {
            var batches = new List<Batch>();
            using (var db = new LiteDatabase(_databasePath))
            {
                var collection = db.GetCollection<Batch>();
                batches = collection.FindAll().ToList<Batch>();
                if (!string.IsNullOrEmpty(ID))
                {
                    batches = batches.Where(x => x.ID.ToString() == ID).ToList<Batch>();
                }

                if (!string.IsNullOrEmpty(user))
                {
                    batches = batches.Where(x => x.UserName == user).ToList<Batch>();
                }
            }

            if (measure == Measure.AngleOfCurvature)
            {
                return batches.SelectMany(x => x.Movements)
                .Select(x => x.AngleOfCurvature ?? 0).ToArray<double>();
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


        private double? Direction(Movement m1, Movement m2)
        {
            double x = m2.X - m1.X;
            double y = m2.Y - m1.Y;
            double c = Math.Sqrt(x * x + y * y);
            if (c == 0)
            {
                return null;
            }

            var direction = Math.Acos(x / c) * 180 / Math.PI;
            if (y < 0)
            {
                return 360 - direction;
            }
            return direction;

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

            return angle;

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

            return c1 * Math.Sin(angle) / c3;
        }

    }
}
