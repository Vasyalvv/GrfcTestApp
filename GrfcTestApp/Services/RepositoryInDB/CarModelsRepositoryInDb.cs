using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Engines;
using GrfcTestApp.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.RepositoryInDB
{
    public class CarModelsRepositoryInDb : RepositoryInDb<CarModel>
    {
        public CarModelsRepositoryInDb(AppDBContext db) : base(db)
        {
        }

        public override IEnumerable<CarModel> GetAll()
        {
            return _dbSet.Include("CarMark").Include("EngineType").ToList() ;
        }

        protected override CarModel Update(CarModel source, CarModel destination)
        {
            destination.Name = source.Name;

            var engine = _db.EngineTypes.FirstOrDefault(e => e.Id == source.EngineType.Id) ??
                     _db.EngineTypes.FirstOrDefault(e => e.Name == source.EngineType.Name) ??
                     new EngineBase { Name = source.EngineType.Name };
            destination.EngineType = engine;

            var mark = _db.CarMarks.FirstOrDefault(m => m.Id == source.CarMark.Id) ??
                    _db.CarMarks.FirstOrDefault(m => m.Name == source.CarMark.Name) ??
                    new CarMark { Name = source.CarMark.Name };
            destination.CarMark = mark;

            _db.SaveChanges();
            return _dbSet.FirstOrDefault(c => c.Name == destination.Name);
        }
    }
}
