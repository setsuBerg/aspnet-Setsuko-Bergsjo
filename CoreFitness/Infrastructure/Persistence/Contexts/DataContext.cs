using Domain.Aggregates.TrainingClasses;
using Infrastructure.Identity;
using Infrastructure.Persistence.Entities.Bookings;
using Infrastructure.Persistence.Entities.Members;
using Infrastructure.Persistence.Entities.Memberships;
using Infrastructure.Persistence.Entities.Memberships.Faqs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    
    public DbSet<MemberEntity> Members => Set<MemberEntity>();

    public DbSet<MembershipEntity> Memberships => Set<MembershipEntity>();
    public DbSet<MembershipBenefitEntity> MembershipBenefits => Set<MembershipBenefitEntity>();

    public DbSet<FaqEntity> Faqs => Set<FaqEntity>();

    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<IdentityRole> IdentityRoles => Set<IdentityRole>();
    public DbSet<TrainingClass> TrainingClasses => Set<TrainingClass>();
    public DbSet<BookingEntity> Bookings => Set<BookingEntity>();

}
