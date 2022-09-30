using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Engines;
using GrfcTestApp.Services.Base;
using GrfcTestApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.RepositoryInDB
{
    public class MaintenancesRepositoryInDb : RepositoryInDb<Maintenance>
    {
        private readonly IRepository<Automobile> _AutoRepos;
        private readonly IRepository<Operation> _OperRepos;

        public MaintenancesRepositoryInDb(AppDBContext db,
            IRepository<Automobile> autoRepos,
            IRepository<Operation> operRepos) : base(db)
        {
            _AutoRepos = autoRepos;
            _OperRepos = operRepos;
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

            destination.Automobile = _AutoRepos.FirstOrCreate(source.Automobile);

            foreach (var operation in source.Operation)
            {
                destination.Operation.Add(_OperRepos.FirstOrCreate(operation));
            }

            _db.SaveChanges();

            return _dbSet.FirstOrDefault(m => m.DateTime == destination.DateTime && m.Automobile == destination.Automobile);
        }

        public override Maintenance FirstOrCreate(Maintenance item)
        {
            var result = _dbSet.FirstOrDefault(m => m.Id == item.Id);

            if (result is null)
            {
                result = new Maintenance
                {
                    DateTime = item.DateTime,
                    Automobile = _AutoRepos.FirstOrCreate(item.Automobile)
                };
                foreach (var oper in item.Operation)
                {
                    result.Operation.Add(_OperRepos.FirstOrCreate(oper));
                }

                _db.SaveChanges();

                result = _dbSet.FirstOrDefault(m => (m.DateTime == result.DateTime) && (m.Automobile == result.Automobile));
            }

            return result;
        }

        public override bool Remove(Maintenance item)
        {
            if (!requiredDataLoaded)
            {
                _db.Operations.Load();
                requiredDataLoaded = true;
            }

            return base.Remove(item);
        }
    }
}

