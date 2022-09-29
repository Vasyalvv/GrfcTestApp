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
    public class AutomobilesRepositoryInDb : RepositoryInDb<Automobile>
    {
        public AutomobilesRepositoryInDb(AppDBContext db) : base(db)
        {
        }

        public override IEnumerable<Automobile> GetAll()
        {
            return _dbSet.Include("CarModel.CarMark").Include("CarModel.EngineType").ToList();
        }

        protected override Automobile Update(Automobile source, Automobile destination)
        {
            destination.RegistrationNumber = source.RegistrationNumber;
            var model = _db.CarModels.FirstOrDefault(c => c.Id == source.CarModel.Id) ??
                _db.CarModels.FirstOrDefault(c => c.Name == source.CarModel.Name) ??
                new CarModel
                {
                    CarMark = _db.CarMarks.FirstOrDefault(c => c.Id == source.CarModel.CarMark.Id) ??
                        _db.CarMarks.FirstOrDefault(c => c.Name == source.CarModel.CarMark.Name) ??
                        new CarMark { Name = source.CarModel.CarMark.Name },
                    EngineType = _db.EngineTypes.FirstOrDefault(e => e.Id == source.CarModel.EngineType.Id) ??
                        _db.EngineTypes.FirstOrDefault(e => e.Name == source.CarModel.EngineType.Name) ??
                        new EngineBase { Name = source.CarModel.EngineType.Name },
                    Name = source.CarModel.Name
                };
            destination.CarModel = model;
            _db.SaveChanges();

            return _dbSet.FirstOrDefault(a => a.RegistrationNumber == destination.RegistrationNumber);
        }
    }
}
