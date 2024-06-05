namespace EDNETLMS.Data
{
    using EDNETLMS.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Reflection.Emit;
    

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadCatchUpStatus> LeadCatchUpStatuses { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Lead>().ToTable("Leads");
            modelBuilder.Entity<LeadCatchUpStatus>().ToTable("LeadCatchUpStatus");

            modelBuilder.Entity<Institution>().ToTable("Institutions");
            modelBuilder.Entity<Country>().ToTable("Countries");
			modelBuilder.Entity<PersonInterestedInstitution>().ToTable("PersonInterestedInstitution");

			modelBuilder.Entity<Person>()
            .HasOne(p => p.Institution)
            .WithMany(i => i.Persons)
            .HasForeignKey(p => p.InstitutionID);
           

            modelBuilder.Entity<Institution>()
                .HasOne(i => i.Country)
                .WithMany(c => c.Institutions)
                .HasForeignKey(i => i.CountryID);


			base.OnModelCreating(modelBuilder);
        }
    }
}
