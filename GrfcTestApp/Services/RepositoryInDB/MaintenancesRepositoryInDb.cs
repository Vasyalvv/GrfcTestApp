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
    public class MaintenancesRepositoryInDb : RepositoryInDb<Maintenance>
    {
        public MaintenancesRepositoryInDb(AppDBContext db) : base(db)
        {
        }

        public override IEnumerable<Maintenance> GetAll()
        {
            return _dbSet
                .Include("Operation.EngineType")
                .Include("Automobile.CarModel.CarMark")
                .Include("Automobile.CarModel.EngineType")
                .ToList();
        }

        protected override Maintenance Update(Maintenance source, Maintenance destination)
        {
            destination.DateTime = source.DateTime;
            var auto = _db.Automobiles.FirstOrDefault(a => a.Id == source.Automobile.Id) ??
                _db.Automobiles.FirstOrDefault(a => a.RegistrationNumber == source.Automobile.RegistrationNumber) ??
                new Automobile
                {
                    RegistrationNumber = source.Automobile.RegistrationNumber,
                    CarModel = _db.CarModels.FirstOrDefault(c => c.Id == source.Automobile.CarModel.Id) ??
                        _db.CarModels.FirstOrDefault(c => c.Name == source.Automobile.CarModel.Name) ??
                        new CarModel
                        {
                            Name = source.Automobile.CarModel.Name,
                            CarMark = _db.CarMarks.FirstOrDefault(c => c.Id == source.Automobile.CarModel.CarMark.Id) ??
                                _db.CarMarks.FirstOrDefault(c => c.Name == source.Automobile.CarModel.CarMark.Name) ??
                                new CarMark { Name = source.Automobile.CarModel.CarMark.Name },
                            EngineType = _db.EngineTypes.FirstOrDefault(e => e.Id == source.Automobile.CarModel.EngineType.Id) ??
                                _db.EngineTypes.FirstOrDefault(e => e.Name == source.Automobile.CarModel.EngineType.Name) ??
                                new EngineBase { Name = source.Automobile.CarModel.EngineType.Name }
                        }
                };
            destination.Automobile = auto;

            foreach (var operation in source.Operation)
            {
                var oper = _db.Operations.FirstOrDefault(o => o.Id == operation.Id) ??
                    _db.Operations.FirstOrDefault(o => o.Description == operation.Description) ??
                    new Operation
                    {
                        Description = operation.Description,
                        EngineType = _db.EngineTypes.FirstOrDefault(e => e.Id == operation.EngineType.Id) ??
                                _db.EngineTypes.FirstOrDefault(e => e.Name == operation.EngineType.Name) ??
                                new EngineBase { Name = operation.EngineType.Name }
                    };
                if (!(oper is null))
                    destination.Operation.Add(oper);
            }

            _db.SaveChanges();

            return _dbSet.FirstOrDefault(m => m.DateTime == destination.DateTime && m.Automobile == destination.Automobile);
        }
    }
}

