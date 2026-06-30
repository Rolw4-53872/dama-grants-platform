using DamaGrant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DamaGrant.Infrastructure.Persistence.Configurations;

public class ProjectApplicationConfiguration : IEntityTypeConfiguration<ProjectApplication>
{
    public void Configure(EntityTypeBuilder<ProjectApplication> builder)
    {
        builder.HasKey(pa => pa.Id);
        builder.Property(pa => pa.ProjectName).IsRequired().HasMaxLength(255);
        builder.Property(pa => pa.Status).HasConversion<int>().HasDefaultValue(ApplicationStatus.Draft);

        builder.HasOne(pa => pa.Association)
            .WithMany(a => a.ProjectApplications)
            .HasForeignKey(pa => pa.AssociationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pa => pa.GrantOpportunity)
            .WithMany(go => go.ProjectApplications)
            .HasForeignKey(pa => pa.GrantOpportunityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pa => pa.Proposal)
            .WithOne(pp => pp.Application)
            .HasForeignKey<ProjectProposal>(pp => pp.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.Objectives)
            .WithOne(po => po.Application)
            .HasForeignKey(po => po.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.Beneficiaries)
            .WithOne(pb => pb.Application)
            .HasForeignKey(pb => pb.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.Timelines)
            .WithOne(pt => pt.Application)
            .HasForeignKey(pt => pt.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.Budgets)
            .WithOne(pb => pb.Application)
            .HasForeignKey(pb => pb.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.Attachments)
            .WithOne(pat => pat.Application)
            .HasForeignKey(pat => pat.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.TechnicalReviews)
            .WithOne(tr => tr.ProjectApplication)
            .HasForeignKey(tr => tr.ProjectApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.FinancialReviews)
            .WithOne(fr => fr.ProjectApplication)
            .HasForeignKey(fr => fr.ProjectApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.CommitteeReviews)
            .WithOne(cr => cr.ProjectApplication)
            .HasForeignKey(cr => cr.ProjectApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pa => pa.Contracts)
            .WithOne(c => c.ProjectApplication)
            .HasForeignKey(c => c.ProjectApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(pa => new { pa.AssociationId, pa.GrantOpportunityId });
        builder.HasIndex(pa => pa.Status);
        builder.HasIndex(pa => pa.CreatedAt);

        builder.ToTable("ProjectApplications");
    }
}

public class ProjectBudgetConfiguration : IEntityTypeConfiguration<ProjectBudget>
{
    public void Configure(EntityTypeBuilder<ProjectBudget> builder)
    {
        builder.HasKey(pb => pb.Id);
        builder.Property(pb => pb.BudgetCategoryName).IsRequired().HasMaxLength(100);
        builder.HasMany(pb => pb.BudgetItems)
            .WithOne(bi => bi.Budget)
            .HasForeignKey(bi => bi.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.ToTable("ProjectBudgets");
    }
}

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.ContractNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(c => c.ContractNumber).IsUnique();

        builder.Property(c => c.Status).HasConversion<int>().HasDefaultValue(ContractStatus.Draft);
        builder.Property(c => c.SignatureStatus).HasConversion<int>().HasDefaultValue(SignatureStatus.NotSigned);

        builder.HasOne(c => c.ProjectApplication)
            .WithMany(pa => pa.Contracts)
            .HasForeignKey(c => c.ProjectApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Association)
            .WithMany()
            .HasForeignKey(c => c.AssociationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Attachments)
            .WithOne(ca => ca.Contract)
            .HasForeignKey(ca => ca.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Signatures)
            .WithOne(ds => ds.Contract)
            .HasForeignKey(ds => ds.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Payments)
            .WithOne(p => p.Contract)
            .HasForeignKey(p => p.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.CreatedAt);

        builder.ToTable("Contracts");
    }
}
