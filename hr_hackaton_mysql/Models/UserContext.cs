using System;
using hr_hackaton_mysql.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;

namespace hr_hackaton_mysql.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class UserContext : DbContext
    {
        public UserContext() : base("conn")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Places_of_work> Places_of_work {get;set;}
        public DbSet<Events> Events { get; set; }
        public DbSet<Game> Game { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<Resume>();
            modelBuilder.Entity<Places_of_work>();
            modelBuilder.Entity<Events>();
            modelBuilder.Entity<Game>();

        }
        

    }
    
}
