
using Infrastructure.Persistence.Entities.Memberships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Memberships;

internal class MembershipConfiguration : IEntityTypeConfiguration<MembershipEntity>
{
    public void Configure(EntityTypeBuilder<MembershipEntity> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();
    }
}

internal class MembershipBenefitConfiguration : IEntityTypeConfiguration<MembershipBenefitEntity>
{
    public void Configure(EntityTypeBuilder<MembershipBenefitEntity> builder)
    {
        builder.ToTable("MembershipBenefits");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.MembershipId)
            .IsRequired();

        builder.HasOne(b => b.Membership)
            .WithMany(m => m.Benefits)
            .HasForeignKey(b => b.MembershipId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
