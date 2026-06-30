namespace DamaGrant.Domain.Entities;

public class Association : AuditableEntity
{
    public string RegistrationNumber { get; set; } = null!;
    public string LegalName { get; set; } = null!;
    public string EnglishName { get; set; } = null!;
    public AssociationStatus Status { get; set; } = AssociationStatus.PendingQualification;
    public DateTime RegistrationDate { get; set; }
    public DateTime? QualificationDate { get; set; }
    public string PrimaryContactEmail { get; set; } = null!;
    public string PrimaryContactPhone { get; set; } = null!;
    public string? Website { get; set; }
    public string? LogoUrl { get; set; }
    public string Sector { get; set; } = null!;
    public int? MembersCount { get; set; }
    public string? BriefDescription { get; set; }
    public bool HasReceivedGrantsBefore { get; set; }

    public AssociationProfile? Profile { get; set; }
    public ICollection<AssociationContact> Contacts { get; set; } = [];
    public ICollection<BoardMember> BoardMembers { get; set; } = [];
    public ICollection<BankAccount> BankAccounts { get; set; } = [];
    public ICollection<AssociationAddress> Addresses { get; set; } = [];
    public ICollection<AssociationDocument> Documents { get; set; } = [];
    public ICollection<QualificationApplication> QualificationApplications { get; set; } = [];
    public ICollection<ProjectApplication> ProjectApplications { get; set; } = [];
}

public class AssociationProfile : AuditableEntity
{
    public int AssociationId { get; set; }
    public string? Mission { get; set; }
    public string? Vision { get; set; }
    public string? CoreValues { get; set; }
    public int? YearsInOperation { get; set; }
    public int? EmployeesCount { get; set; }
    public int? VolunteersCount { get; set; }
    public decimal? AnnualBudget { get; set; }
    public decimal? AnnualRevenue { get; set; }
    public string? PrimaryFocusArea { get; set; }
    public string? GeographicCoverage { get; set; }
    public string? MainAchievements { get; set; }
    public string? ChallengesFaced { get; set; }
    public string? FutureGoals { get; set; }
    public DateTime? LastFinancialAuditDate { get; set; }
    public string? ExternalAuditStatus { get; set; }

    public Association Association { get; set; } = null!;
    public FinancialInformation? FinancialInformation { get; set; }
    public GovernanceInformation? GovernanceInformation { get; set; }
}

public class AssociationContact : AuditableEntity
{
    public int AssociationId { get; set; }
    public string ContactType { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Position { get; set; }
    public bool IsEmergencyContact { get; set; }
    public bool IsPrimaryContact { get; set; }

    public Association Association { get; set; } = null!;
}

public class BoardMember : AuditableEntity
{
    public int AssociationId { get; set; }
    public string FullName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public CommitteeMemberRole MemberRole { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime AppointmentDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? QualificationsSummary { get; set; }
    public string? ExperienceSummary { get; set; }
    public bool IsActive { get; set; } = true;

    public Association Association { get; set; } = null!;
}

public class BankAccount : AuditableEntity
{
    public int AssociationId { get; set; }
    public string AccountHolderName { get; set; } = null!;
    public string BankName { get; set; } = null!;
    public string IBAN { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
    public string BankCode { get; set; } = null!;
    public string BranchCode { get; set; } = null!;
    public string? SwiftCode { get; set; }
    public string Currency { get; set; } = "SAR";
    public decimal CurrentBalance { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? VerificationDate { get; set; }
    public bool IsVerified { get; set; }
    public string? BankStatementUrl { get; set; }

    public Association Association { get; set; } = null!;
}

public class AssociationAddress : AuditableEntity
{
    public int AssociationId { get; set; }
    public string AddressType { get; set; } = null!;
    public string StreetAddress { get; set; } = null!;
    public string Building { get; set; } = null!;
    public string District { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = "Saudi Arabia";
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsHeadquarters { get; set; }
    public bool IsMailing { get; set; }

    public Association Association { get; set; } = null!;
}

public class AssociationDocument : AuditableEntity
{
    public int AssociationId { get; set; }
    public string DocumentName { get; set; } = null!;
    public DocumentType DocumentType { get; set; }
    public string FileUrl { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string FileContentType { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string? Description { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsExpired => ExpiryDate.HasValue && ExpiryDate.Value < DateTime.UtcNow;
    public bool IsApproved { get; set; }
    public string? ApprovalNotes { get; set; }
    public DateTime? UploadedDate { get; set; }

    public Association Association { get; set; } = null!;
}

public class OrganizationLicense : AuditableEntity
{
    public int AssociationId { get; set; }
    public string LicenseNumber { get; set; } = null!;
    public string IssuingAuthority { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsExpired => ExpiryDate < DateTime.UtcNow;
    public string? LicenseDocumentUrl { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? VerificationDate { get; set; }
    public string? VerificationNotes { get; set; }

    public Association Association { get; set; } = null!;
}

public class FinancialInformation : AuditableEntity
{
    public int ProfileId { get; set; }
    public DateTime AuditPeriodStartDate { get; set; }
    public DateTime AuditPeriodEndDate { get; set; }
    public decimal TotalAssets { get; set; }
    public decimal TotalLiabilities { get; set; }
    public decimal TotalEquity { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetIncome { get; set; }
    public string? AuditReportUrl { get; set; }
    public bool AuditCompleted { get; set; }
    public DateTime? AuditCompletionDate { get; set; }
    public string? AuditorName { get; set; }
    public string? AuditorQualifications { get; set; }
    public bool FinanciallyHealthy { get; set; }
    public string? Remarks { get; set; }

    public AssociationProfile Profile { get; set; } = null!;
}

public class GovernanceInformation : AuditableEntity
{
    public int ProfileId { get; set; }
    public int BoardSize { get; set; }
    public DateTime LastBoardMeetingDate { get; set; }
    public string GovernanceStructureUrl { get; set; } = null!;
    public string ConstitutionUrl { get; set; } = null!;
    public string ByLawsUrl { get; set; } = null!;
    public bool HasWrittenPolicies { get; set; }
    public bool HasInternalAudit { get; set; }
    public bool HasComplianceOfficer { get; set; }
    public bool HasEthicsPolicy { get; set; }
    public bool HasConflictOfInterestPolicy { get; set; }
    public bool HasTransparencyPolicy { get; set; }
    public int? MeetingFrequencyPerYear { get; set; }
    public decimal? ExecutiveCompensationAverage { get; set; }
    public string? Remarks { get; set; }

    public AssociationProfile Profile { get; set; } = null!;
}
