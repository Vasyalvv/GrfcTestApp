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
    public class CarModelsRepositoryInDb : RepositoryInDb<CarModel>
    {
        private readonly IRepository<EngineBase> _EngineRepos;
        private readonly IRepository<CarMark> _MarkRepos;

        public CarModelsRepositoryInDb(AppDBContext db, 
            IRepository<EngineBase> engineRepos,
            IRepository<CarMark> markRepos) : base(db)
        {
            _EngineRepos = engineRepos;
            _MarkRepos = markRepos;
        }

        public override IEnumerable<CarModel> GetAll()
        {
            return _dbSet.Include("CarMark").Include("EngineType").ToList() ;
        }

        protected override CarModel Update(CarModel source, CarModel destination)
        {
            destination.Name = source.Name;

            destination.EngineType = _EngineRepos.FirstOrCreate(source.EngineType);

            destination.CarMark = _MarkRepos.FirstOrCreate(source.CarMark);

            _db.SaveChanges();
            return _dbSet.FirstOrDefault(c => c.Name == destination.Name);
        }

        public override CarModel FirstOrCreate(CarModel item)
        {
            var result = _dbSet.FirstOrDefault(c => c.Id == item.Id) ??
                _dbSet.FirstOrDefault(c => c.Name == item.Name);

            if(result is null)
            {
                result = new CarModel
                {
                    Name = item.Name,
                    CarMark = _MarkRepos.FirstOrCreate(item.CarMark),
                    EngineType = _EngineRepos.FirstOrCreate(item.EngineType)
                };
                _dbSet.Add(result);
                _db.SaveChanges();

                result = _dbSet.FirstOrDefault(c => c.Name == result.Name);
            }

            return result;
        }
    }
}
