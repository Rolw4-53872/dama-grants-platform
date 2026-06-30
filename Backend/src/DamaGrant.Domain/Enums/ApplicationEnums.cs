namespace DamaGrant.Domain.Enums;

public enum UserRole
{
    Association = 1,
    AssociationAdmin = 2,
    QualificationOfficer = 3,
    ProjectReviewer = 4,
    FinancialReviewer = 5,
    CommitteeMember = 6,
    GrantManager = 7,
    Administrator = 8,
    ExecutiveUser = 9
}

public enum ApplicationStatus
{
    Draft = 1,
    Submitted = 2,
    UnderReview = 3,
    NeedMoreInformation = 4,
    RevisionRequested = 5,
    Approved = 6,
    Rejected = 7,
    Cancelled = 8,
    Completed = 9
}

public enum ReviewType
{
    Administrative = 1,
    Technical = 2,
    Financial = 3,
    Committee = 4
}

public enum ReviewStatus
{
    NotStarted = 1,
    InProgress = 2,
    Completed = 3,
    NeedsRevision = 4,
    Approved = 5,
    Rejected = 6
}

public enum DecisionStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    ConditionallyApproved = 4,
    NeedsMoreInfo = 5
}

public enum PaymentStatus
{
    Pending = 1,
    Approved = 2,
    Processing = 3,
    Completed = 4,
    Failed = 5,
    Cancelled = 6,
    Refunded = 7
}

public enum ContractStatus
{
    Draft = 1,
    Generated = 2,
    PendingSignature = 3,
    Signed = 4,
    Active = 5,
    Completed = 6,
    Terminated = 7,
    Archived = 8
}

public enum SignatureStatus
{
    NotSigned = 1,
    PendingSignature = 2,
    SignedByAssociation = 3,
    SignedByDama = 4,
    FullySigned = 5,
    Expired = 6
}

public enum DocumentType
{
    License = 1,
    Certificate = 2,
    FinancialStatement = 3,
    GovernanceDocument = 4,
    ProposalDocument = 5,
    RequiredAttachment = 6,
    ContractAttachment = 7,
    PaymentProof = 8,
    ProgressReport = 9,
    EvaluationDocument = 10,
    Other = 11
}

public enum NotificationType
{
    ApplicationStatusChanged = 1,
    ReviewStarted = 2,
    ReviewCompleted = 3,
    RevisionRequested = 4,
    DecisionMade = 5,
    ContractGenerated = 6,
    ContractSigned = 7,
    PaymentProcessed = 8,
    PaymentFailed = 9,
    DocumentExpiring = 10,
    DeadlineApproaching = 11,
    MeetingScheduled = 12,
    Other = 13
}

public enum AuditActionType
{
    Create = 1,
    Update = 2,
    Delete = 3,
    Read = 4,
    Login = 5,
    Logout = 6,
    PermissionChange = 7,
    StatusChange = 8,
    ApprovalGiven = 9,
    ApprovalDenied = 10,
    Export = 11,
    Download = 12,
    Upload = 13,
    SignatureCreated = 14,
    PaymentProcessed = 15,
    ReportGenerated = 16
}

public enum UserStatus
{
    Active = 1,
    Inactive = 2,
    Suspended = 3,
    Deleted = 4,
    PendingVerification = 5,
    LockedOut = 6
}

public enum AssociationStatus
{
    PendingQualification = 1,
    Qualified = 2,
    Rejected = 3,
    Suspended = 4,
    Revoked = 5,
    Active = 6,
    Inactive = 7
}

public enum CommitteeMemberRole
{
    Chairman = 1,
    ViceChairman = 2,
    Member = 3,
    Secretary = 4,
    Treasurer = 5
}
