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
    public class OperationsRepositoryInDb : RepositoryInDb<Operation>
    {
        private readonly IRepository<EngineBase> _EngineRepos;

        public OperationsRepositoryInDb(AppDBContext db,
            IRepository<EngineBase> engineRepos) : base(db)
        {
            _EngineRepos = engineRepos;
        }

        public override IEnumerable<Operation> GetAll()
        {
            return _dbSet.Include("EngineType").Include("Maintenances").ToList();
        }

        protected override Operation Update(Operation source, Operation destination)
        {
            destination.Description = source.Description;

            destination.EngineType = _EngineRepos.FirstOrCreate(source.EngineType);

            //Проходим по списку проведенных обслуживаний в источнике
            //и добавляем их к "виду работы", только если эти проведенные обслуживания
            //уже существуют в БД. В других сущностях при обновлении,
            //если вложенная сущность не существует, то создаем ее.
            //Возможно при отладке здесь это тоже нужно будет переделать
            foreach (var maintenance in source.Maintenances)
            {
                var mt = _db.Maintenances.FirstOrDefault(m => m.Id == maintenance.Id);
                if (!(mt is null))
                    destination.Maintenances.Add(mt);
            }

            _db.SaveChanges();

            return _dbSet.FirstOrDefault(o => o.Description == destination.Description);
        }

        public override Operation FirstOrCreate(Operation item)
        {
            var result = _dbSet.FirstOrDefault(o => o.Id == item.Id) ??
                _dbSet.FirstOrDefault(o => o.Description == item.Description);

            if(result is null)
            {
                result = new Operation
                {
                    Description = item.Description,
                    EngineType = _EngineRepos.FirstOrCreate(item.EngineType),
                };
                foreach (var maint in item.Maintenances)
                {
                    var mt = _db.Maintenances.FirstOrDefault(m => m.Id == maint.Id);
                    if (!(mt is null))
                        result.Maintenances.Add(mt);
                }

                _db.SaveChanges();
                result = _dbSet.FirstOrDefault(o=>o.Description==result.Description);
            }

            return result;
        }
    }
}
