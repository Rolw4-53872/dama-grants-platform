namespace DamaGrant.Domain.Entities;

public class ProgressReport : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public int ReportNumber { get; set; }
    public DateTime ReportDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public int? SubmittedBy { get; set; }
    public string ReportPeriod { get; set; } = null!;
    public string ExecutiveOverview { get; set; } = null!;
    public string ActivitiesCompleted { get; set; } = null!;
    public string ChallengesFaced { get; set; } = null!;
    public string SolutionsApplied { get; set; } = null!;
    public string BudgetUtilization { get; set; } = null!;
    public decimal? PercentageCompletion { get; set; }
    public string? NextSteps { get; set; }
    public bool IsApproved { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string? ApprovalNotes { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
    public ICollection<ProgressAttachment> Attachments { get; set; } = [];
}

public class ProgressAttachment : AuditableEntity
{
    public int ProgressReportId { get; set; }
    public string AttachmentName { get; set; } = null!;
    public string? Description { get; set; }
    public string FileUrl { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string FileContentType { get; set; } = null!;
    public DateTime UploadedDate { get; set; }

    public ProgressReport ProgressReport { get; set; } = null!;
}

public class SiteVisit : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public int VisitNumber { get; set; }
    public DateTime VisitDate { get; set; }
    public string VisitObjective { get; set; } = null!;
    public int? VisitedBy { get; set; }
    public string Location { get; set; } = null!;
    public string BeneficiariesMet { get; set; } = null!;
    public string ObservationsAndFindings { get; set; } = null!;
    public string Recommendations { get; set; } = null!;
    public decimal? CoordinatesLatitude { get; set; }
    public decimal? CoordinatesLongitude { get; set; }
    public DateTime? ReportSubmittedDate { get; set; }
    public string? VisitReportUrl { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
}

public class Evaluation : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public DateTime EvaluationDate { get; set; }
    public int? EvaluatedBy { get; set; }
    public string OverallAssessment { get; set; } = null!;
    public string ObjectivesAchievement { get; set; } = null!;
    public string BeneficiaryImpact { get; set; } = null!;
    public string BudgetUtilization { get; set; } = null!;
    public string TimelineCoverage { get; set; } = null!;
    public decimal? OverallScore { get; set; }
    public string Recommendations { get; set; } = null!;
    public string? LessonsLearned { get; set; }
    public DateTime? ReportSubmittedDate { get; set; }
    public string? EvaluationReportUrl { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
}

public class FinalReport : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public DateTime SubmittedDate { get; set; }
    public int? SubmittedBy { get; set; }
    public string ExecutiveSummary { get; set; } = null!;
    public string ProjectCompletionSummary { get; set; } = null!;
    public string ObjectivesAchieved { get; set; } = null!;
    public string OutputsDeliverables { get; set; } = null!;
    public string OutcomesImpact { get; set; } = null!;
    public string BudgetFinalUtilization { get; set; } = null!;
    public string FinancialStatement { get; set; } = null!;
    public string RecommendationsAndSuggestions { get; set; } = null!;
    public string BeneficiaryTestimonials { get; set; } = null!;
    public DateTime? ApprovedDate { get; set; }
    public int? ApprovedBy { get; set; }
    public bool IsApproved { get; set; }
    public string? FinalReportUrl { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
}

public class ProjectClosure : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public DateTime ClosureDate { get; set; }
    public int? ClosedBy { get; set; }
    public string? ClosureReason { get; set; }
    public string? ClosureNotes { get; set; }
    public bool IsSuccessful { get; set; }
    public decimal? FinalAmount { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public int? ApprovedBy { get; set; }
    public string? ArchiveLocation { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
}
