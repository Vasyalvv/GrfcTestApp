using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities;
using GrfcTestApp.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.RepositoryInDB
{
    public class CarMarksRepositoryInDb : RepositoryInDb<CarMark>
    {
        public CarMarksRepositoryInDb(AppDBContext db) : base(db)
        {
        }
        
        protected override CarMark Update(CarMark source, CarMark destination)
        {
            destination.Name = source.Name;
            _db.SaveChanges();
            return destination;
        }
    }
}
