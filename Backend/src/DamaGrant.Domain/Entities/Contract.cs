namespace DamaGrant.Domain.Entities;

public class Contract : AuditableEntity
{
    public int ProjectApplicationId { get; set; }
    public int AssociationId { get; set; }
    public string ContractNumber { get; set; } = null!;
    public ContractStatus Status { get; set; } = ContractStatus.Draft;
    public SignatureStatus SignatureStatus { get; set; } = SignatureStatus.NotSigned;
    public DateTime GeneratedAt { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public decimal ContractAmount { get; set; }
    public string? TermsAndConditions { get; set; }
    public string ContractDocumentUrl { get; set; } = null!;
    public DateTime? SignedAt { get; set; }
    public int? SignedByAssociation { get; set; }
    public int? SignedByDama { get; set; }
    public DateTime? TerminationDate { get; set; }
    public string? TerminationReason { get; set; }

    public ProjectApplication ProjectApplication { get; set; } = null!;
    public Association Association { get; set; } = null!;
    public ICollection<ContractAttachment> Attachments { get; set; } = [];
    public ICollection<DigitalSignature> Signatures { get; set; } = [];
    public ICollection<ContractVersion> Versions { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
}

public class ContractAttachment : AuditableEntity
{
    public int ContractId { get; set; }
    public string AttachmentName { get; set; } = null!;
    public string? Description { get; set; }
    public string FileUrl { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string FileContentType { get; set; } = null!;
    public DateTime UploadedDate { get; set; }
    public DocumentType DocumentType { get; set; }

    public Contract Contract { get; set; } = null!;
}

public class DigitalSignature : AuditableEntity
{
    public int ContractId { get; set; }
    public int SignedBy { get; set; }
    public string? SignatureUrl { get; set; }
    public DateTime SignedAt { get; set; }
    public string SignerName { get; set; } = null!;
    public string SignerRole { get; set; } = null!;
    public string? SignatureMethod { get; set; }
    public string? SignatureHash { get; set; }
    public bool IsValid { get; set; } = true;
    public DateTime? VerifiedAt { get; set; }
    public int? VerifiedBy { get; set; }

    public Contract Contract { get; set; } = null!;
    public User SignedByUser { get; set; } = null!;
}

public class ContractVersion : AuditableEntity
{
    public int ContractId { get; set; }
    public int VersionNumber { get; set; }
    public string? VersionNotes { get; set; }
    public string DocumentUrl { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public bool IsCurrent { get; set; }
    public bool IsArchived { get; set; }

    public Contract Contract { get; set; } = null!;
}
