namespace DamaGrant.Domain.Entities;

public class TechnicalReview : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public int ReviewerId { get; set; }
    public ReviewStatus Status { get; set; } = ReviewStatus.NotStarted;
    public decimal? TechnicalScore { get; set; }
    public string? Strengths { get; set; }
    public string? Weaknesses { get; set; }
    public string? Recommendations { get; set; }
    public string? Feedback { get; set; }
    public bool NeedsRevision { get; set; }
    public DateTime? ReviewStartedAt { get; set; }
    public DateTime? ReviewCompletedAt { get; set; }
    public string? Recommendation { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
    public User Reviewer { get; set; } = null!;
    public ICollection<ReviewComment> Comments { get; set; } = [];
}

public class FinancialReview : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public int ReviewerId { get; set; }
    public ReviewStatus Status { get; set; } = ReviewStatus.NotStarted;
    public decimal? FinancialScore { get; set; }
    public decimal? BudgetReasonableness { get; set; }
    public string? BudgetAnalysis { get; set; }
    public string? FinancialViability { get; set; }
    public string? RiskAssessment { get; set; }
    public string? Feedback { get; set; }
    public bool NeedsRevision { get; set; }
    public DateTime? ReviewStartedAt { get; set; }
    public DateTime? ReviewCompletedAt { get; set; }
    public string? Recommendation { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
    public User Reviewer { get; set; } = null!;
    public ICollection<ReviewComment> Comments { get; set; } = [];
}

public class CommitteeReview : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public int CommitteeMemberId { get; set; }
    public ReviewStatus Status { get; set; } = ReviewStatus.NotStarted;
    public decimal? CommitteeScore { get; set; }
    public string? InitialObservations { get; set; }
    public string? FinalRecommendation { get; set; }
    public string? AdditionalNotes { get; set; }
    public DateTime? ReviewStartedAt { get; set; }
    public DateTime? ReviewCompletedAt { get; set; }
    public DecisionStatus? Decision { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
    public User CommitteeMember { get; set; } = null!;
    public ICollection<CommitteeVote> Votes { get; set; } = [];
    public ICollection<ReviewComment> Comments { get; set; } = [];
}

public class CommitteeVote : BaseEntity
{
    public int CommitteeReviewId { get; set; }
    public int VoterId { get; set; }
    public DecisionStatus Vote { get; set; }
    public DateTime VotedAt { get; set; } = DateTime.UtcNow;
    public string? Justification { get; set; }
    public DateTime CreatedAt { get; set; }

    public CommitteeReview CommitteeReview { get; set; } = null!;
    public User Voter { get; set; } = null!;
}

public class ReviewComment : AuditableEntity
{
    public int? TechnicalReviewId { get; set; }
    public int? FinancialReviewId { get; set; }
    public int? CommitteeReviewId { get; set; }
    public int AuthorId { get; set; }
    public string CommentText { get; set; } = null!;
    public bool IsInternal { get; set; }
    public DateTime CommentedAt { get; set; } = DateTime.UtcNow;
    public int? ParentCommentId { get; set; }

    public TechnicalReview? TechnicalReview { get; set; }
    public FinancialReview? FinancialReview { get; set; }
    public CommitteeReview? CommitteeReview { get; set; }
    public User Author { get; set; } = null!;
    public ReviewComment? ParentComment { get; set; }
    public ICollection<ReviewComment> Replies { get; set; } = [];
}

public class RevisionRequest : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public int RequestedBy { get; set; }
    public DateTime RequestedAt { get; set; }
    public ReviewType ReviewType { get; set; }
    public string RevisionReason { get; set; } = null!;
    public string? RequiredChanges { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public bool IsCompleted { get; set; }
    public int? CompletedBy { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? CompletionNotes { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
    public User RequestedByUser { get; set; } = null!;
}

public class WorkflowHistory : BaseEntity
{
    public int ProjectApplicationId { get; set; }
    public ApplicationStatus FromStatus { get; set; }
    public ApplicationStatus ToStatus { get; set; }
    public string? Reason { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public int? ChangedBy { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
}
