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
    }
}
