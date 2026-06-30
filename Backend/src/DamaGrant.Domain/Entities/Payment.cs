namespace DamaGrant.Domain.Entities;

public class Payment : AuditableEntity
{
    public int ContractId { get; set; }
    public int AssociationId { get; set; }
    public string PaymentNumber { get; set; } = null!;
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "SAR";
    public int? InstallmentNumber { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public DateTime? RequestedAt { get; set; }
    public int? RequestedBy { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public int? ProcessedBy { get; set; }
    public string? PaymentMethod { get; set; }
    public string? TransactionReference { get; set; }
    public string? FailureReason { get; set; }
    public DateTime? FailedAt { get; set; }

    public Contract Contract { get; set; } = null!;
    public Association Association { get; set; } = null!;
    public ICollection<PaymentInstallment> Installments { get; set; } = [];
    public ICollection<Invoice> Invoices { get; set; } = [];
    public ICollection<FinancialTransaction> Transactions { get; set; } = [];
    public ICollection<PaymentApproval> Approvals { get; set; } = [];
}

public class PaymentInstallment : AuditableEntity
{
    public int PaymentId { get; set; }
    public int InstallmentNumber { get; set; }
    public decimal InstallmentAmount { get; set; }
    public DateTime DueDate { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime? PaidDate { get; set; }
    public string? PaymentMethod { get; set; }
    public string? TransactionReference { get; set; }
    public string? Description { get; set; }

    public Payment Payment { get; set; } = null!;
}

public class Invoice : AuditableEntity
{
    public int PaymentId { get; set; }
    public string InvoiceNumber { get; set; } = null!;
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal InvoiceAmount { get; set; }
    public decimal? PaidAmount { get; set; }
    public string InvoiceStatus { get; set; } = "Sent";
    public string? Description { get; set; }
    public string InvoiceDocumentUrl { get; set; } = null!;
    public bool IsPaid { get; set; }
    public DateTime? PaidDate { get; set; }
    public string? TaxRegistrationNumber { get; set; }

    public Payment Payment { get; set; } = null!;
}

public class FinancialTransaction : AuditableEntity
{
    public int PaymentId { get; set; }
    public string TransactionReference { get; set; } = null!;
    public string TransactionType { get; set; } = null!;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "SAR";
    public DateTime TransactionDate { get; set; }
    public string BankName { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
    public string IBAN { get; set; } = null!;
    public string? Description { get; set; }
    public string? ReceiptDocumentUrl { get; set; }

    public Payment Payment { get; set; } = null!;
}

public class PaymentApproval : AuditableEntity
{
    public int PaymentId { get; set; }
    public int ApprovedBy { get; set; }
    public DateTime ApprovedAt { get; set; }
    public string? ApprovalNotes { get; set; }
    public string ApprovalLevel { get; set; } = null!;
    public bool IsApproved { get; set; }
    public DateTime? ProcessedAt { get; set; }

    public Payment Payment { get; set; } = null!;
    public User Approver { get; set; } = null!;
}
