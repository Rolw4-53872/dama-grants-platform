using DamaGrant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DamaGrant.Infrastructure.Persistence.Configurations;

public class AssociationConfiguration : IEntityTypeConfiguration<Association>
{
    public void Configure(EntityTypeBuilder<Association> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.RegistrationNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(a => a.RegistrationNumber).IsUnique();

        builder.Property(a => a.LegalName).IsRequired().HasMaxLength(255);
        builder.Property(a => a.EnglishName).IsRequired().HasMaxLength(255);
        builder.Property(a => a.Status).HasConversion<int>().HasDefaultValue(AssociationStatus.PendingQualification);
        builder.Property(a => a.Sector).IsRequired().HasMaxLength(100);
        builder.Property(a => a.PrimaryContactEmail).IsRequired().HasMaxLength(255);
        builder.Property(a => a.PrimaryContactPhone).IsRequired().HasMaxLength(20);

        builder.HasOne(a => a.Profile)
            .WithOne(p => p.Association)
            .HasForeignKey<AssociationProfile>(p => p.AssociationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Contacts)
            .WithOne(ac => ac.Association)
            .HasForeignKey(ac => ac.AssociationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.BoardMembers)
            .WithOne(bm => bm.Association)
            .HasForeignKey(bm => bm.AssociationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.BankAccounts)
            .WithOne(ba => ba.Association)
            .HasForeignKey(ba => ba.AssociationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Addresses)
            .WithOne(aa => aa.Association)
            .HasForeignKey(aa => aa.AssociationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Documents)
            .WithOne(ad => ad.Association)
            .HasForeignKey(ad => ad.AssociationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.QualificationApplications)
            .WithOne(qa => qa.Association)
            .HasForeignKey(qa => qa.AssociationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.ProjectApplications)
            .WithOne(pa => pa.Association)
            .HasForeignKey(pa => pa.AssociationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.CreatedAt);

        builder.ToTable("Associations");
    }
}

public class AssociationProfileConfiguration : IEntityTypeConfiguration<AssociationProfile>
{
    public void Configure(EntityTypeBuilder<AssociationProfile> builder)
    {
        builder.HasKey(ap => ap.Id);
        builder.HasOne(ap => ap.Association).WithOne(a => a.Profile).HasForeignKey<AssociationProfile>(ap => ap.AssociationId);
        builder.HasOne(ap => ap.FinancialInformation).WithOne(fi => fi.Profile).HasForeignKey<FinancialInformation>(fi => fi.ProfileId);
        builder.HasOne(ap => ap.GovernanceInformation).WithOne(gi => gi.Profile).HasForeignKey<GovernanceInformation>(gi => gi.ProfileId);
        builder.ToTable("AssociationProfiles");
    }
}

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.HasKey(ba => ba.Id);
        builder.Property(ba => ba.IBAN).IsRequired().HasMaxLength(34);
        builder.HasIndex(ba => ba.IBAN).IsUnique();
        builder.Property(ba => ba.AccountNumber).IsRequired().HasMaxLength(50);
        builder.HasOne(ba => ba.Association).WithMany(a => a.BankAccounts).HasForeignKey(ba => ba.AssociationId);
        builder.ToTable("BankAccounts");
    }
}
