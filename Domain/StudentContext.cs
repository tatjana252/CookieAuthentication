using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class StudentContext : IdentityDbContext<Person, IdentityRole<int>, int>
    {

        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StudentsAuth; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>().ToTable("Osobe");
            builder.Entity<Student>().ToTable("Studenti");
            builder.Entity<Teacher>().ToTable("Nastavnici");
            base.OnModelCreating(builder);
            

        }

    }
}
