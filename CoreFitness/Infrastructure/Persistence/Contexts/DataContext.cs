using Infrastructure.Identity;
using Infrastructure.Persistence.Entities.Members;
using Infrastructure.Persistence.Entities.Memberships;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<MemberEntity> Members => Set<MemberEntity>();

    public DbSet<MembershipEntity> Memberships => Set<MembershipEntity>();
    public DbSet<MembershipBenefitEntity> MembershipBenefits => Set<MembershipBenefitEntity>();
}
