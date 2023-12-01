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
    public DbSet<CarAttachment> CarAttachments { get; set; }
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

        //Many-to-many realition for Car and Attachment
        modelBuilder.Entity<CarAttachment>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<CarAttachment>()
            .HasOne(x => x.Car)
            .WithMany(x => x.CarAttachments)
            .HasForeignKey(x => x.CarId);

        modelBuilder.Entity<CarAttachment>()
            .HasOne(x => x.Attachment)
            .WithMany(x => x.CarAttachments)
            .HasForeignKey(x => x.AttamentId);

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

        //One-to-one realition for Attachment and User
        modelBuilder.Entity<Attachment>()
            .HasOne<User>()
            .WithOne(x => x.Attachment)
            .HasForeignKey<Attachment>(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-one realitoin for Attachment and Customer 
        modelBuilder.Entity<Attachment>()
            .HasOne<Customer>()
            .WithOne(x => x.Attachment)
            .HasForeignKey<Attachment>(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        //One-to-one realitoin for Attachment and CarCategory
        modelBuilder.Entity<Attachment>()
            .HasOne<CarCategory>()
            .WithOne(x => x.Attachment)
            .HasForeignKey<Attachment>(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}
