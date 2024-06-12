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
        public DbSet<PersonInterestedCourse> PersonInterestedCourses { get; set; }

		public DbSet<PersonInterestedInstitution> PersonInterestedInstitution { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Lead>().ToTable("Leads");
            modelBuilder.Entity<LeadCatchUpStatus>().ToTable("LeadCatchUpStatus");

            modelBuilder.Entity<Institution>().ToTable("Institutions");
            modelBuilder.Entity<Country>().ToTable("Countries");
			modelBuilder.Entity<PersonInterestedInstitution>().ToTable("PersonInterestedInstitution");
			modelBuilder.Entity<PersonInterestedCourse>().ToTable("PersonInterestedCourses");

			modelBuilder.Entity<Person>()
            .HasOne(p => p.Institution)
            .WithMany(i => i.Persons)
            .HasForeignKey(p => p.InstitutionID)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>().Property(e => e.PersonID).ValueGeneratedOnAdd();

			modelBuilder.Entity<Institution>()
                .HasOne(i => i.Country)
                .WithMany(c => c.Institutions)
                .HasForeignKey(i => i.CountryID)
				 .OnDelete(DeleteBehavior.Restrict);

	



			modelBuilder.Entity<PersonInterestedCourse>(entity =>
			{
				entity.HasKey(e => new { e.PersonId, e.CourseId });

				entity.HasOne(e => e.Person)
					.WithMany(p => p.PersonInterestedCourses)
					.HasForeignKey(e => e.PersonId);

				entity.HasOne(e => e.Course)
					.WithMany(c => c.PersonInterestedCourses)
					.HasForeignKey(e => e.CourseId);
			});

			modelBuilder.Entity<PersonInterestedInstitution>(entity =>
			{
				entity.HasKey(e => new { e.PersonID, e.InstitutionID });

				entity.HasOne(e => e.Person)
					.WithMany(p => p.PersonInterestedInstitution)
					.HasForeignKey(e => e.PersonID);

				entity.HasOne(e => e.Institution)
					.WithMany(c => c.PersonInterestedInstitution)
					.HasForeignKey(e => e.InstitutionID);
			});


			base.OnModelCreating(modelBuilder);
        }
    }
}
