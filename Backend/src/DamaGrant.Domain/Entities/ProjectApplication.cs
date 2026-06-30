namespace DamaGrant.Domain.Entities;

public class ProjectApplication : AuditableEntity
{
    public int AssociationId { get; set; }
    public int GrantOpportunityId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
    public DateTime SubmittedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public int? ApprovedBy { get; set; }
    public decimal RequestedAmount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public int? DurationInMonths { get; set; }
    public DateTime? ProjectStartDate { get; set; }
    public DateTime? ProjectEndDate { get; set; }
    public string? ExecutiveSummary { get; set; }
    public decimal? EstimatedBeneficiaries { get; set; }
    public decimal? ReviewScore { get; set; }
    public bool NeedsRevision { get; set; }
    public DateTime? RevisionRequestDate { get; set; }
    public string? RevisionNotes { get; set; }

    public Association Association { get; set; } = null!;
    public GrantOpportunity GrantOpportunity { get; set; } = null!;
    public ProjectProposal? Proposal { get; set; }
    public ICollection<ProjectObjective> Objectives { get; set; } = [];
    public ICollection<ProjectBeneficiary> Beneficiaries { get; set; } = [];
    public ICollection<ProjectTimeline> Timelines { get; set; } = [];
    public ICollection<ProjectMilestone> Milestones { get; set; } = [];
    public ICollection<ProjectBudget> Budgets { get; set; } = [];
    public ICollection<ProjectAttachment> Attachments { get; set; } = [];
    public ICollection<TechnicalReview> TechnicalReviews { get; set; } = [];
    public ICollection<FinancialReview> FinancialReviews { get; set; } = [];
    public ICollection<CommitteeReview> CommitteeReviews { get; set; } = [];
    public ICollection<Contract> Contracts { get; set; } = [];
}

public class ProjectProposal : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string ProposalTitle { get; set; } = null!;
    public string ProposalBody { get; set; } = null!;
    public string BackgroundAndJustification { get; set; } = null!;
    public string ProposedSolution { get; set; } = null!;
    public DateTime ProposedDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public int? SubmittedBy { get; set; }
    public int? Version { get; set; }

    public ProjectApplication Application { get; set; } = null!;
}

public class ProjectObjective : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string ObjectiveTitle { get; set; } = null!;
    public string ObjectiveDescription { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public string? MeasurableIndicator { get; set; }
    public string? TargetValue { get; set; }
    public string? Timeline { get; set; }

    public ProjectApplication Application { get; set; } = null!;
}

public class ProjectBeneficiary : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string BeneficiaryGroup { get; set; } = null!;
    public string? Description { get; set; }
    public int EstimatedCount { get; set; }
    public decimal? BudgetPerBeneficiary { get; set; }
    public string? SelectionCriteria { get; set; }
    public string? BenefitDescription { get; set; }
    public int DisplayOrder { get; set; }

    public ProjectApplication Application { get; set; } = null!;
}

public class ProjectTimeline : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string Phase { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationInDays => (EndDate - StartDate).Days;
    public decimal? BudgetForPhase { get; set; }
    public string? KeyActivities { get; set; }
    public string? Deliverables { get; set; }
    public int DisplayOrder { get; set; }

    public ProjectApplication Application { get; set; } = null!;
}

public class ProjectMilestone : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string MilestoneTitle { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime TargetDate { get; set; }
    public DateTime? ActualCompletionDate { get; set; }
    public bool IsCompleted { get; set; }
    public string? CompletionProof { get; set; }
    public decimal? BudgetAllocated { get; set; }
    public int DisplayOrder { get; set; }
    public string? Status { get; set; }

    public ProjectApplication Application { get; set; } = null!;
}

public class ProjectBudget : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string BudgetCategoryName { get; set; } = null!;
    public decimal TotalBudgetAmount { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool RequiresDetailedBreakdown { get; set; }

    public ProjectApplication Application { get; set; } = null!;
    public ICollection<BudgetItem> BudgetItems { get; set; } = [];
}

public class BudgetItem : AuditableEntity
{
    public int BudgetId { get; set; }
    public string ItemDescription { get; set; } = null!;
    public decimal UnitCost { get; set; }
    public int Quantity { get; set; }
    public decimal TotalCost => UnitCost * Quantity;
    public string? Justification { get; set; }
    public int DisplayOrder { get; set; }

    public ProjectBudget Budget { get; set; } = null!;
}

public class ProjectAttachment : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string AttachmentName { get; set; } = null!;
    public string? Description { get; set; }
    public string FileUrl { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string FileContentType { get; set; } = null!;
    public DateTime UploadedDate { get; set; }
    public DocumentType DocumentType { get; set; }

    public ProjectApplication Application { get; set; } = null!;
}
