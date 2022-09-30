using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities;
using GrfcTestApp.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.RepositoryInDB
{
    public class CarMarksRepositoryInDb : RepositoryInDb<CarMark>
    {
        public CarMarksRepositoryInDb(AppDBContext db) : base(db)
        {
        }

        protected override CarMark Update(CarMark source, CarMark destination)
        {
            destination.Name = source.Name;
            _db.SaveChanges();
            return destination;
        }

        public override CarMark FirstOrCreate(CarMark item)
        {
            var result = _dbSet.FirstOrDefault(c => c.Id == item.Id) ??
                _dbSet.FirstOrDefault(c => c.Name == item.Name);

            if (result is null)
            {
                result = new CarMark { Name = item.Name };
                _dbSet.Add(result);
                _db.SaveChanges();

                result = _dbSet.FirstOrDefault(c => c.Name == item.Name);
            }

            return result;
        }

        public override bool Remove(CarMark item)
        {
            if (!requiredDataLoaded)
            {
                _db.Automobiles.Load();
                _db.CarModels.Load();
                _db.Maintenances.Load();
                requiredDataLoaded = true;
            }

            return base.Remove(item);
        }
    }
}
