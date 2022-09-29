using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Engines;
using GrfcTestApp.Services.Base;
using GrfcTestApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.RepositoryInDB
{
    public class AutomobilesRepositoryInDb : RepositoryInDb<Automobile>
    {
        private readonly IRepository<CarModel> _ModelRepos;

        public AutomobilesRepositoryInDb(AppDBContext db, IRepository<CarModel> modelRepos) : base(db)
        {
            _ModelRepos = modelRepos;
        }

        public override IEnumerable<Automobile> GetAll()
        {
            return _dbSet.Include("CarModel.CarMark").Include("CarModel.EngineType").ToList();
        }

        protected override Automobile Update(Automobile source, Automobile destination)
        {
            destination.RegistrationNumber = source.RegistrationNumber;

            destination.CarModel = _ModelRepos.FirstOrCreate(source.CarModel);

            _db.SaveChanges();

            return _dbSet.FirstOrDefault(a => a.RegistrationNumber == destination.RegistrationNumber);
        }

        public override Automobile FirstOrCreate(Automobile item)
        {
            var result = _dbSet.FirstOrDefault(a => a.Id == item.Id) ??
                _dbSet.FirstOrDefault(a => a.RegistrationNumber == item.RegistrationNumber);

            if(result is null)
            {
                result = new Automobile
                {
                    RegistrationNumber = item.RegistrationNumber,
                    CarModel = _ModelRepos.FirstOrCreate(item.CarModel)
                };
                _dbSet.Add(result);
                _db.SaveChanges();

                result = _dbSet.FirstOrDefault(a => a.RegistrationNumber==result.RegistrationNumber);
            }
            return result;
        }
    }
}
