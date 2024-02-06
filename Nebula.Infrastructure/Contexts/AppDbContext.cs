using Microsoft.EntityFrameworkCore;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Feedbacks;
using Nebula.Domain.Entities.Insurances;
using Nebula.Domain.Entities.Offices;
using Nebula.Domain.Entities.Payments;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<OfficeAttachment> OfficeAttachments { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarCategory> CarCategories { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<InsuranceCoverage> InsuranceCoverages { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region FluentApi
        //Many-to-many realition for Office and Attachment
        modelBuilder.Entity<OfficeAttachment>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<OfficeAttachment>()
            .HasOne(x => x.Office)
            .WithMany(x => x.OfficeAttachments)
            .HasForeignKey(x => x.OfficeId);

        modelBuilder.Entity<OfficeAttachment>()
            .HasOne(x => x.Attachment)
            .WithMany(x => x.OfficeAttachments)
            .HasForeignKey(x => x.AttachmentId);

        //One-to-many realition for Car and Attachment
        modelBuilder.Entity<Car>()
            .HasMany(x => x.Attachments)
            .WithOne(x => x.Car);

        modelBuilder.Entity<Attachment>()
            .HasOne(x => x.Car)
            .WithMany(x => x.Attachments)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-Many realition for Car and Rental
        modelBuilder.Entity<Car>()
            .HasMany(x => x.Rentals)
            .WithOne(x => x.Car);

        modelBuilder.Entity<Rental>()
            .HasOne(x => x.Car)
            .WithMany(x => x.Rentals)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for CarCategory and Car
        modelBuilder.Entity<CarCategory>()
            .HasMany(x => x.Cars)
            .WithOne(x => x.CarCategory);

        modelBuilder.Entity<Car>()
            .HasOne(x => x.CarCategory)
            .WithMany(x => x.Cars)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for InsuranceCoverage and Insurance
        modelBuilder.Entity<InsuranceCoverage>()
            .HasMany(x => x.Insurances)
            .WithOne(x => x.InsuranceCoverage);

        modelBuilder.Entity<Insurance>()
            .HasOne(x => x.InsuranceCoverage)
            .WithMany(x => x.Insurances)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for Office and User
        modelBuilder.Entity<Office>()
            .HasMany(x => x.Users)
            .WithOne(x => x.Office);

        modelBuilder.Entity<User>()
            .HasOne(x => x.Office)
            .WithMany(x => x.Users)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-many realition for Customer and PaymentHistory
        modelBuilder.Entity<Customer>()
            .HasMany(x => x.PaymentHistories)
            .WithOne(x => x.Customer);

        modelBuilder.Entity<PaymentHistory>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.PaymentHistories)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}
