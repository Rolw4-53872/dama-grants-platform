using DamaGrant.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DamaGrant.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region Identity DbSets
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
    public DbSet<EmailVerificationToken> EmailVerificationTokens => Set<EmailVerificationToken>();
    public DbSet<LoginHistory> LoginHistories => Set<LoginHistory>();
    #endregion

    #region Association DbSets
    public DbSet<Association> Associations => Set<Association>();
    public DbSet<AssociationProfile> AssociationProfiles => Set<AssociationProfile>();
    public DbSet<AssociationContact> AssociationContacts => Set<AssociationContact>();
    public DbSet<BoardMember> BoardMembers => Set<BoardMember>();
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<AssociationAddress> AssociationAddresses => Set<AssociationAddress>();
    public DbSet<AssociationDocument> AssociationDocuments => Set<AssociationDocument>();
    public DbSet<OrganizationLicense> OrganizationLicenses => Set<OrganizationLicense>();
    public DbSet<FinancialInformation> FinancialInformations => Set<FinancialInformation>();
    public DbSet<GovernanceInformation> GovernanceInformations => Set<GovernanceInformation>();
    #endregion

    #region Qualification DbSets
    public DbSet<QualificationProgram> QualificationPrograms => Set<QualificationProgram>();
    public DbSet<QualificationApplication> QualificationApplications => Set<QualificationApplication>();
    public DbSet<QualificationQuestion> QualificationQuestions => Set<QualificationQuestion>();
    public DbSet<QualificationAnswer> QualificationAnswers => Set<QualificationAnswer>();
    public DbSet<QualificationDocument> QualificationDocuments => Set<QualificationDocument>();
    public DbSet<QualificationReview> QualificationReviews => Set<QualificationReview>();
    public DbSet<QualificationComment> QualificationComments => Set<QualificationComment>();
    public DbSet<QualificationRevision> QualificationRevisions => Set<QualificationRevision>();
    public DbSet<QualificationStatusHistory> QualificationStatusHistories => Set<QualificationStatusHistory>();
    #endregion

    #region Grant DbSets
    public DbSet<GrantProgram> GrantPrograms => Set<GrantProgram>();
    public DbSet<GrantOpportunity> GrantOpportunities => Set<GrantOpportunity>();
    public DbSet<GrantCategory> GrantCategories => Set<GrantCategory>();
    public DbSet<GrantRequirement> GrantRequirements => Set<GrantRequirement>();
    public DbSet<GrantAttachment> GrantAttachments => Set<GrantAttachment>();
    #endregion

    #region Project Application DbSets
    public DbSet<ProjectApplication> ProjectApplications => Set<ProjectApplication>();
    public DbSet<ProjectProposal> ProjectProposals => Set<ProjectProposal>();
    public DbSet<ProjectObjective> ProjectObjectives => Set<ProjectObjective>();
    public DbSet<ProjectBeneficiary> ProjectBeneficiaries => Set<ProjectBeneficiary>();
    public DbSet<ProjectTimeline> ProjectTimelines => Set<ProjectTimeline>();
    public DbSet<ProjectMilestone> ProjectMilestones => Set<ProjectMilestone>();
    public DbSet<ProjectBudget> ProjectBudgets => Set<ProjectBudget>();
    public DbSet<BudgetItem> BudgetItems => Set<BudgetItem>();
    public DbSet<ProjectAttachment> ProjectAttachments => Set<ProjectAttachment>();
    #endregion

    #region Review DbSets
    public DbSet<TechnicalReview> TechnicalReviews => Set<TechnicalReview>();
    public DbSet<FinancialReview> FinancialReviews => Set<FinancialReview>();
    public DbSet<CommitteeReview> CommitteeReviews => Set<CommitteeReview>();
    public DbSet<CommitteeVote> CommitteeVotes => Set<CommitteeVote>();
    public DbSet<ReviewComment> ReviewComments => Set<ReviewComment>();
    public DbSet<RevisionRequest> RevisionRequests => Set<RevisionRequest>();
    public DbSet<WorkflowHistory> WorkflowHistories => Set<WorkflowHistory>();
    #endregion

    #region Contract DbSets
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<ContractAttachment> ContractAttachments => Set<ContractAttachment>();
    public DbSet<DigitalSignature> DigitalSignatures => Set<DigitalSignature>();
    public DbSet<ContractVersion> ContractVersions => Set<ContractVersion>();
    #endregion

    #region Payment DbSets
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentInstallment> PaymentInstallments => Set<PaymentInstallment>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<FinancialTransaction> FinancialTransactions => Set<FinancialTransaction>();
    public DbSet<PaymentApproval> PaymentApprovals => Set<PaymentApproval>();
    #endregion

    #region Project Execution DbSets
    public DbSet<ProgressReport> ProgressReports => Set<ProgressReport>();
    public DbSet<ProgressAttachment> ProgressAttachments => Set<ProgressAttachment>();
    public DbSet<SiteVisit> SiteVisits => Set<SiteVisit>();
    public DbSet<Evaluation> Evaluations => Set<Evaluation>();
    public DbSet<FinalReport> FinalReports => Set<FinalReport>();
    public DbSet<ProjectClosure> ProjectClosures => Set<ProjectClosure>();
    #endregion

    #region Notification DbSets
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();
    public DbSet<NotificationRecipient> NotificationRecipients => Set<NotificationRecipient>();
    public DbSet<EmailQueue> EmailQueues => Set<EmailQueue>();
    #endregion

    #region System DbSets
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Settings> Settings => Set<Settings>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<FileStorage> FileStorages => Set<FileStorage>();
    public DbSet<Lookup> Lookups => Set<Lookup>();
    public DbSet<LookupValue> LookupValues => Set<LookupValue>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        ConfigureGlobalFilters(modelBuilder);
    }

    private static void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType);
                var property = System.Linq.Expressions.Expression.PropertyOrField(parameter, nameof(AuditableEntity.IsDeleted));
                var filter = System.Linq.Expressions.Expression.Lambda(
                    System.Linq.Expressions.Expression.Equal(property, System.Linq.Expressions.Expression.Constant(false)),
                    parameter
                );

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<BaseEntity>().ToList();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Deleted && entry.Entity is AuditableEntity auditableEntity)
            {
                entry.State = EntityState.Modified;
                auditableEntity.IsDeleted = true;
                auditableEntity.DeletedAt = DateTime.UtcNow;
            }
        }
    }
}
