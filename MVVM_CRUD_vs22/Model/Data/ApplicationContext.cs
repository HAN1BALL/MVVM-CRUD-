using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUD_mvvm_.Model.Data
{
    internal class ApplicationContext: DbContext
    {
        public DbSet<Staff> _Staff { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Office> Offices { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CRUD_mvvm_;Trusted_Connection=True;");
        }
    }
}
