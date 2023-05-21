using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        // config is storing our connection string
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }
        
        // creating db sets:
        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<UserSalary> UserSalary {get; set;}
        public virtual DbSet<UserJobInfo> UserJobInfo {get; set;}

        // set up our on configuring:
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {   
                // for the UseSqlServer to work we need to add package
                // console: dotnet add package Microsoft.EntityFrameworkCore.SqlServer
                optionsBuilder
                .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                  optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }


        // tell EF where those tables actually are:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // to make this work (HasDefaultSchema) we want to bring EF's nuget package
            // in console: dotnet add package Microsoft.EntityFrameworkCore.Relational
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            // tell EF that the table is actually called UserS not User
            // and also tell what is they key in the table:
            modelBuilder.Entity<User>()
            .ToTable("Users", "TutorialAppSchema")
            .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
            .HasKey(u => u.UserId);

            modelBuilder.Entity<UserJobInfo>()
            .HasKey(u => u.UserId);

        }




    }
}
