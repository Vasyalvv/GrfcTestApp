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
    public class OperationsRepositoryInDb : RepositoryInDb<Operation>
    {
        public OperationsRepositoryInDb(AppDBContext db) : base(db)
        {
        }

        public override IEnumerable<Operation> GetAll()
        {
            return _dbSet.Include("EngineType").Include("Maintenances").ToList();
        }

        protected override Operation Update(Operation source, Operation destination)
        {
            destination.Description = source.Description;

            var engine = _db.EngineTypes.FirstOrDefault(e => e.Id == source.EngineType.Id) ??
                     _db.EngineTypes.FirstOrDefault(e => e.Name == source.EngineType.Name) ??
                     new EngineBase { Name = source.EngineType.Name };
            destination.EngineType = engine;

            foreach (var maintenance in source.Maintenances)
            {
                var mt = _db.Maintenances.FirstOrDefault(m => m.Id == maintenance.Id);
                if (!(mt is null))
                    destination.Maintenances.Add(mt);
            }

            _db.SaveChanges();

            return _dbSet.FirstOrDefault(o => o.Description == destination.Description);
        }
    }
}
