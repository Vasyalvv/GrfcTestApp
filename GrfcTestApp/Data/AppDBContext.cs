using GrfcTestApp.Data.Entities;
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
        public AppDBContext():base("GrfcDBConnectionString")
        {

        }
        public DbSet<CarMark> CarMarks { get; set; }

        public DbSet<EngineType> EngineTypes { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<Automobile> Automobiles { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Maintenance> Maintenances { get; set; }
    }
}
