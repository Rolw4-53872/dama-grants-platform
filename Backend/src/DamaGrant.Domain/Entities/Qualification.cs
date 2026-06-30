namespace DamaGrant.Domain.Entities;

public class QualificationProgram : AuditableEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Objective { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal? Budget { get; set; }
    public int? MaxApplicants { get; set; }
    public string? TemplateUrl { get; set; }

    public ICollection<QualificationApplication> Applications { get; set; } = [];
}

public class QualificationApplication : AuditableEntity
{
    public int AssociationId { get; set; }
    public int QualificationProgramId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
    public DateTime SubmittedAt { get; set; }
    public DateTime? ReviewStartedAt { get; set; }
    public DateTime? ReviewCompletedAt { get; set; }
    public int? ReviewerId { get; set; }
    public string? ReviewNotes { get; set; }
    public DecisionStatus? Decision { get; set; }
    public string? RejectionReason { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public decimal? ReviewScore { get; set; }
    public bool NeedsRevision { get; set; }
    public DateTime? RevisionRequestDate { get; set; }
    public string? RevisionNotes { get; set; }

    public Association Association { get; set; } = null!;
    public QualificationProgram Program { get; set; } = null!;
    public ICollection<QualificationAnswer> Answers { get; set; } = [];
    public ICollection<QualificationDocument> Documents { get; set; } = [];
    public ICollection<QualificationReview> Reviews { get; set; } = [];
    public ICollection<QualificationRevision> Revisions { get; set; } = [];
    public ICollection<QualificationStatusHistory> StatusHistories { get; set; } = [];
}

public class QualificationQuestion : BaseEntity
{
    public int ProgramId { get; set; }
    public string QuestionText { get; set; } = null!;
    public string QuestionType { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public bool IsRequired { get; set; }
    public bool IsActive { get; set; } = true;
    public string? HelpText { get; set; }
    public int? MaxCharacters { get; set; }

    public QualificationProgram Program { get; set; } = null!;
    public ICollection<QualificationAnswer> Answers { get; set; } = [];
}

public class QualificationAnswer : AuditableEntity
{
    public int ApplicationId { get; set; }
    public int QuestionId { get; set; }
    public string AnswerText { get; set; } = null!;
    public bool IsApproved { get; set; }
    public string? ApprovingComments { get; set; }

    public QualificationApplication Application { get; set; } = null!;
    public QualificationQuestion Question { get; set; } = null!;
}

public class QualificationDocument : AuditableEntity
{
    public int ApplicationId { get; set; }
    public string DocumentName { get; set; } = null!;
    public DocumentType DocumentType { get; set; }
    public string FileUrl { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string FileContentType { get; set; } = null!;
    public DateTime UploadedDate { get; set; }
    public bool IsApproved { get; set; }
    public string? ApprovalNotes { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }

    public QualificationApplication Application { get; set; } = null!;
}

public class QualificationReview : AuditableEntity
{
    public int ApplicationId { get; set; }
    public int ReviewerId { get; set; }
    public ReviewStatus Status { get; set; } = ReviewStatus.NotStarted;
    public decimal? Score { get; set; }
    public string? Feedback { get; set; }
    public DateTime? ReviewStartedAt { get; set; }
    public DateTime? ReviewCompletedAt { get; set; }
    public string? Recommendation { get; set; }
    public bool NeedsRevision { get; set; }

    public QualificationApplication Application { get; set; } = null!;
    public User Reviewer { get; set; } = null!;
    public ICollection<QualificationComment> Comments { get; set; } = [];
}

public class QualificationComment : AuditableEntity
{
    public int ReviewId { get; set; }
    public int AuthorId { get; set; }
    public string CommentText { get; set; } = null!;
    public DateTime CommentedAt { get; set; } = DateTime.UtcNow;
    public bool IsInternal { get; set; }

    public QualificationReview Review { get; set; } = null!;
    public User Author { get; set; } = null!;
}

public class QualificationRevision : AuditableEntity
{
    public int ApplicationId { get; set; }
    public int RequestedBy { get; set; }
    public DateTime RequestedAt { get; set; }
    public string RevisionReason { get; set; } = null!;
    public string? RequiredChanges { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public bool IsCompleted { get; set; }
    public string? CompletionNotes { get; set; }

    public QualificationApplication Application { get; set; } = null!;
    public User RequestedByUser { get; set; } = null!;
}

public class QualificationStatusHistory : BaseEntity
{
    public int ApplicationId { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public int? ChangedBy { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }

    public QualificationApplication Application { get; set; } = null!;
}
