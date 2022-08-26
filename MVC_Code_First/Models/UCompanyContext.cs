using System;
using Microsoft.EntityFrameworkCore;

namespace MVC_Code_First.Models
{
    public class UCompanyContext : DbContext
    {
        /// <summary>
        /// Since the Dotnet CLI needs the DbContext to Be Regietered in DI Container
        /// Pass the DbContextOptions<UCompanyContext> parameter to it and link it withe the Base class
        /// </summary>
        public UCompanyContext(DbContextOptions<UCompanyContext> options):base(options)
        {
        }

        /// <summary>
        /// DbSet properties to Generate Table
        /// </summary>
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

