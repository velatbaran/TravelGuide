using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.DAL.Migrations;
using TravelGuide.Entities.Entity;

namespace TravelGuide.DAL.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<About> About { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<RoadDescribeUnit> RoadDescribeUnits { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DataContext()
        {
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>("DataContext"));
           // Database.SetInitializer(new MyInitializer());
        }
    }
}
