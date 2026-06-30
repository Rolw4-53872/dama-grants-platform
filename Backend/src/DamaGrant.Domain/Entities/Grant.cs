namespace DamaGrant.Domain.Entities;

public class GrantProgram : AuditableEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime LaunchDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal TotalBudget { get; set; }
    public decimal AllocatedBudget { get; set; }
    public decimal RemainingBudget => TotalBudget - AllocatedBudget;
    public int? MaxApplicants { get; set; }

    public ICollection<GrantOpportunity> Opportunities { get; set; } = [];
}

public class GrantOpportunity : AuditableEntity
{
    public int ProgramId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
    public DateTime PublicationDate { get; set; }
    public DateTime ApplicationStartDate { get; set; }
    public DateTime ApplicationDeadline { get; set; }
    public DateTime? AnnouncementDate { get; set; }
    public string TargetSector { get; set; } = null!;
    public string? Eligibility { get; set; }
    public decimal MinGrantAmount { get; set; }
    public decimal MaxGrantAmount { get; set; }
    public decimal BudgetAllocated { get; set; }
    public int? MaxBeneficiaries { get; set; }
    public string? DocumentationUrl { get; set; }
    public bool IsQualificationRequired { get; set; }
    public int? ProjectDurationMonths { get; set; }

    public GrantProgram Program { get; set; } = null!;
    public ICollection<GrantCategory> Categories { get; set; } = [];
    public ICollection<GrantRequirement> Requirements { get; set; } = [];
    public ICollection<GrantAttachment> Attachments { get; set; } = [];
    public ICollection<ProjectApplication> ProjectApplications { get; set; } = [];
}

public class GrantCategory : BaseEntity
{
    public int OpportunityId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public GrantOpportunity Opportunity { get; set; } = null!;
}

public class GrantRequirement : BaseEntity
{
    public int OpportunityId { get; set; }
    public string RequirementName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsRequired { get; set; } = true;
    public string? DocumentationType { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public GrantOpportunity Opportunity { get; set; } = null!;
}

public class GrantAttachment : AuditableEntity
{
    public int OpportunityId { get; set; }
    public string FileName { get; set; } = null!;
    public string FileUrl { get; set; } = null!;
    public string FileContentType { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string? Description { get; set; }
    public DateTime UploadedDate { get; set; }

    public GrantOpportunity Opportunity { get; set; } = null!;
}
