using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities.Engines;
using GrfcTestApp.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.RepositoryInDB
{
    public class EnginesRepositoryInDb : RepositoryInDb<EngineBase>
    {
        public EnginesRepositoryInDb(AppDBContext db) : base(db)
        {
        }

        protected override EngineBase Update(EngineBase source, EngineBase destination)
        {
            destination.Name = source.Name;
            _db.SaveChanges();
            return destination;
        }

        public override EngineBase FirstOrCreate(EngineBase item)
        {
            var result = _dbSet.FirstOrDefault(e => e.Id == item.Id) ??
                _dbSet.FirstOrDefault(e => e.Name == item.Name);

            if (result is null)
            {
                if (item.GetType().Equals(typeof(GasEngine)))
                    result = new GasEngine { Name = item.Name };
                else if (item.GetType().Equals(typeof(DieselEngine)))
                    result = new DieselEngine { Name = item.Name };
                else if (item.GetType().Equals(typeof(CombustionEngine)))
                    result = new CombustionEngine { Name = item.Name };
                else
                    result = new EngineBase { Name = item.Name };

                _dbSet.Add(result);
                _db.SaveChanges();

                result = _dbSet.FirstOrDefault(e => e.Name == item.Name);
            }
                        
            return result;
        }

    }
}

