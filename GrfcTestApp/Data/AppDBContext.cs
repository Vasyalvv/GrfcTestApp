using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Engines;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("GrfcDBConnectionString")
        {

        }
        public DbSet<CarMark> CarMarks { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<Automobile> Automobiles { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Maintenance> Maintenances { get; set; }

        public DbSet<EngineBase> EngineTypes { get; set; }

        public DbSet<CombustionEngine> CombustionEngines { get; set; }

        public DbSet<DieselEngine> DieselEngines { get; set; }

        public DbSet<GasEngine> GasEngines { get; set; }
    }
}
