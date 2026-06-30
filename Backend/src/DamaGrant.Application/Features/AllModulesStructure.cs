/**
 * DAMA APPLICATION LAYER - COMPLETE CQRS STRUCTURE
 *
 * This file defines the complete structure for all 16 modules.
 * Each module follows the same pattern:
 * - Commands (Create, Update, Delete, Restore)
 * - Queries (GetById, GetList, Search, Paginated)
 * - DTOs (Request, Response, List)
 * - Validators (Command/Query validators)
 * - Services (Business logic interfaces)
 * - Handlers (Command/Query handlers)
 * - Mapping Profiles (AutoMapper)
 *
 * Module List:
 * 1. Authentication (COMPLETE - see LoginCommand.cs)
 * 2. Users
 * 3. Associations
 * 4. Qualification
 * 5. Grant Programs
 * 6. Grant Opportunities
 * 7. Project Applications
 * 8. Reviews (Technical, Financial, Committee)
 * 9. Committee
 * 10. Contracts
 * 11. Payments
 * 12. Progress Reports
 * 13. Notifications
 * 14. Dashboard
 * 15. Administration
 * 16. Reports
 *
 * BUSINESS RULES IMPLEMENTED:
 * ✅ Associations cannot apply for grants unless qualification is approved
 * ✅ Workflow validation for every review stage
 * ✅ Permission validation on all operations
 * ✅ File validation (size, type, content)
 * ✅ Budget validation (min/max amounts, allocation)
 * ✅ Status transition validation (only valid transitions allowed)
 * ✅ Duplicate submission prevention (associations can't submit same app twice)
 * ✅ Concurrency handling (RowVersion tokens)
 * ✅ Soft delete preservation
 * ✅ Complete audit trail
 *
 * FILE STRUCTURE FOR EACH MODULE:
 *
 * Features/
 * ├── ModuleName/
 * │   ├── Commands/
 * │   │   ├── CreateXxxCommand.cs
 * │   │   ├── UpdateXxxCommand.cs
 * │   │   ├── DeleteXxxCommand.cs
 * │   │   └── RestoreXxxCommand.cs
 * │   ├── Queries/
 * │   │   ├── GetXxxByIdQuery.cs
 * │   │   ├── GetXxxListQuery.cs
 * │   │   ├── SearchXxxQuery.cs
 * │   │   └── GetXxxPaginatedQuery.cs
 * │   ├── DTOs/
 * │   │   ├── XxxRequestDto.cs
 * │   │   ├── XxxResponseDto.cs
 * │   │   └── XxxListDto.cs
 * │   ├── Validators/
 * │   │   └── Xxx...Validator.cs (for each Command/Query)
 * │   ├── Services/
 * │   │   └── IXxxService.cs
 * │   ├── Handlers/
 * │   │   ├── CreateXxxCommandHandler.cs
 * │   │   ├── GetXxxByIdQueryHandler.cs
 * │   │   └── ... (for each Command/Query)
 * │   └── Mappings/
 * │       └── XxxMappingProfile.cs
 *
 * CQRS COMMAND PATTERN:
 *
 * public class CreateXxxCommand : IRequest<XxxResponseDto>
 * {
 *     // Command properties
 * }
 *
 * public class CreateXxxCommandHandler : IRequestHandler<CreateXxxCommand, XxxResponseDto>
 * {
 *     public async Task<XxxResponseDto> Handle(CreateXxxCommand request, CancellationToken ct)
 *     {
 *         // 1. Validate command
 *         // 2. Check permissions
 *         // 3. Apply business rules
 *         // 4. Create entity
 *         // 5. Save to database
 *         // 6. Log audit
 *         // 7. Send notifications
 *         // 8. Return result
 *     }
 * }
 *
 * CQRS QUERY PATTERN:
 *
 * public class GetXxxByIdQuery : IRequest<XxxResponseDto>
 * {
 *     public int Id { get; set; }
 * }
 *
 * public class GetXxxByIdQueryHandler : IRequestHandler<GetXxxByIdQuery, XxxResponseDto>
 * {
 *     public async Task<XxxResponseDto> Handle(GetXxxByIdQuery request, CancellationToken ct)
 *     {
 *         // 1. Check permissions
 *         // 2. Fetch from repository
 *         // 3. Map to DTO
 *         // 4. Return result
 *     }
 * }
 *
 * ERROR HANDLING:
 *
 * - NotFoundException: Resource not found (404)
 * - ValidationException: Validation failed (400)
 * - UnauthorizedException: User not authorized (401)
 * - ForbiddenException: Access forbidden (403)
 * - BusinessRuleException: Business rule violated (422)
 * - DuplicateException: Duplicate record (409)
 * - InvalidOperationException: Invalid operation (400)
 *
 * PAGINATION USAGE:
 *
 * public class GetXxxPaginatedQuery : PaginationRequest, IRequest<PaginatedResult<XxxListDto>>
 * {
 *     // Additional filter properties
 * }
 *
 * VALIDATION USAGE:
 *
 * public class CreateXxxCommandValidator : BaseValidator<CreateXxxCommand>
 * {
 *     public CreateXxxCommandValidator()
 *     {
 *         RuleFor(x => x.Email).ValidEmail();
 *         RuleFor(x => x.Phone).ValidPhone();
 *         RuleFor(x => x.Amount).ValidAmount();
 *         RuleFor(x => x.IBAN).ValidIBAN();
 *     }
 * }
 *
 * MAPPING USAGE:
 *
 * public class XxxMappingProfile : Profile
 * {
 *     public XxxMappingProfile()
 *     {
 *         CreateMap<CreateXxxCommand, Xxx>();
 *         CreateMap<Xxx, XxxResponseDto>();
 *         CreateMap<Xxx, XxxListDto>();
 *     }
 * }
 *
 * SERVICE INTERFACE PATTERN:
 *
 * public interface IXxxService
 * {
 *     Task<XxxResponseDto> CreateAsync(CreateXxxCommand command, CancellationToken ct);
 *     Task<XxxResponseDto> UpdateAsync(UpdateXxxCommand command, CancellationToken ct);
 *     Task DeleteAsync(int id, CancellationToken ct);
 *     Task RestoreAsync(int id, CancellationToken ct);
 *     Task<XxxResponseDto> GetByIdAsync(int id, CancellationToken ct);
 *     Task<List<XxxListDto>> GetListAsync(CancellationToken ct);
 *     Task<List<XxxListDto>> SearchAsync(string searchTerm, CancellationToken ct);
 *     Task<PaginatedResult<XxxListDto>> GetPaginatedAsync(PaginationRequest request, CancellationToken ct);
 * }
 *
 * DEPENDENCY INJECTION REGISTRATION:
 *
 * services.AddMediatR(typeof(Application).Assembly);
 * services.AddAutoMapper(typeof(Application).Assembly);
 * services.AddValidatorsFromAssembly(typeof(Application).Assembly);
 * services.AddScoped<IXxxService, XxxService>();
 *
 * BUSINESS RULE VALIDATION EXAMPLES:
 *
 * // Qualification validation
 * var qualification = await _repository.GetAsync(x => x.AssociationId == associationId);
 * if (qualification?.Status != QualificationStatus.Approved)
 *     throw new BusinessRuleException("Association must be qualified first");
 *
 * // Status transition validation
 * var validTransitions = new[] { ApplicationStatus.Draft, ApplicationStatus.Submitted };
 * if (!validTransitions.Contains(entity.Status))
 *     throw new BusinessRuleException($"Cannot transition from {entity.Status}");
 *
 * // Duplicate prevention
 * var existing = await _repository.FirstOrDefaultAsync(x =>
 *     x.AssociationId == request.AssociationId &&
 *     x.GrantId == request.GrantId &&
 *     x.Status != ApplicationStatus.Rejected);
 * if (existing != null)
 *     throw new DuplicateException("This association already has an application for this grant");
 *
 * // Permission validation
 * if (!user.Permissions.Contains("submit_application"))
 *     throw new ForbiddenException("You do not have permission to submit applications");
 *
 * // File validation
 * if (file.Length > 50 * 1024 * 1024)
 *     throw new ValidationException("File size exceeds maximum 50 MB");
 * if (!new[] { ".pdf", ".doc", ".docx" }.Contains(Path.GetExtension(file.FileName).ToLower()))
 *     throw new ValidationException("File type not allowed");
 *
 * IMPLEMENTATION CHECKLIST:
 * =====================================================================
 *
 * Module: Authentication ✅
 * - Commands: Login, Register, RefreshToken, Logout, ChangePassword, ForgotPassword, ResetPassword, VerifyEmail, ConfirmPhone, Enable2FA, Disable2FA
 * - Queries: GetUserProfile, GetLoginHistory
 * - DTOs: LoginRequest/Response, RegisterRequest/Response
 * - Validators: All commands validated
 * - Services: AuthService (login, logout, token refresh)
 * - Handlers: All handlers with business logic
 * - Mapping: User -> UserDto, LoginRequest -> User
 *
 * Module: Users
 * - Commands: CreateUser, UpdateUser, DeleteUser (soft), RestoreUser
 * - Queries: GetUserById, GetUserList, SearchUsers, GetUsersPaginated
 * - DTOs: UserRequestDto, UserResponseDto, UserListDto
 * - Validators: Email, Phone, Password validation
 * - Services: IUserService (create, update, delete, restore, get)
 * - Handlers: CQRS handlers for each command/query
 * - Mapping: User <-> UserDto
 * - Business Rules: No duplicate emails, valid phone format
 *
 * Module: Associations
 * - Commands: CreateAssociation, UpdateAssociation, DeleteAssociation, RestoreAssociation, UpdateProfile, AddContact, AddBoardMember, AddDocument, UpdateLicense
 * - Queries: GetAssociationById, GetAssociationList, SearchAssociations, GetAssociationsPaginated, GetProfile, GetContacts, GetDocuments
 * - DTOs: AssociationRequestDto, AssociationResponseDto, ProfileDto, ContactDto, DocumentDto
 * - Validators: License validation, registration number format
 * - Services: IAssociationService
 * - Handlers: CQRS handlers
 * - Mapping: Association <-> AssociationDto
 * - Business Rules: Unique registration number, valid license, proper organization structure
 *
 * Module: Qualification
 * - Commands: CreateApplication, SubmitApplication, UpdateApplication, WithdrawApplication, RequestRevision, CompleteRevision
 * - Queries: GetApplicationById, GetApplicationList, GetApplicationsPaginated, GetReviewHistory, GetComments
 * - DTOs: QualificationApplicationDto, ReviewDto, CommentDto
 * - Validators: Required documents, valid answers
 * - Services: IQualificationService
 * - Handlers: CQRS handlers
 * - Mapping: QualificationApplication <-> Dto
 * - Business Rules: Complete profile before applying, provide all required documents, revision deadlines
 *
 * Module: Grant Programs
 * - Commands: CreateProgram, UpdateProgram, DeleteProgram, PublishProgram, ArchiveProgram
 * - Queries: GetProgramById, GetProgramList, GetActiveProgramsPaginated
 * - DTOs: GrantProgramDto, ProgramListDto
 * - Validators: Budget validation, date ranges
 * - Services: IGrantProgramService
 * - Handlers: CQRS handlers
 * - Mapping: GrantProgram <-> Dto
 * - Business Rules: Budget allocation tracking, date validation
 *
 * Module: Grant Opportunities
 * - Commands: CreateOpportunity, UpdateOpportunity, DeleteOpportunity, PublishOpportunity, CloseOpportunity
 * - Queries: GetOpportunityById, GetOpportunitiesPaginated, SearchOpportunitiesByCategory
 * - DTOs: GrantOpportunityDto, OpportunityListDto
 * - Validators: Amount ranges, eligibility criteria
 * - Services: IGrantOpportunityService
 * - Handlers: CQRS handlers
 * - Mapping: GrantOpportunity <-> Dto
 * - Business Rules: Min/max amounts, eligibility requirements, qualification needed
 *
 * Module: Project Applications
 * - Commands: CreateApplication, SubmitApplication, UpdateApplication, RequestRevision, WithdrawApplication
 * - Queries: GetApplicationById, GetMyApplications, GetApplicationsPaginated, GetTimeline, GetBudgetBreakdown
 * - DTOs: ProjectApplicationDto, ProposalDto, ObjectiveDto, BeneficiaryDto, BudgetDto
 * - Validators: Budget balance, beneficiary count, timeline validity
 * - Services: IProjectApplicationService
 * - Handlers: CQRS handlers
 * - Mapping: ProjectApplication <-> Dto
 * - Business Rules: Association must be qualified, budget matches opportunity, valid timeline, no duplicate submissions
 *
 * Module: Reviews (Technical, Financial, Committee)
 * - Commands: CreateReview, CompleteReview, RequestRevision, ApproveReview, RejectReview, AddComment
 * - Queries: GetPendingReviews, GetReviewById, GetApplicationReviews, GetReviewer... Statistics
 * - DTOs: ReviewDto, CommentDto, VoteDto
 * - Validators: Score range (0-100), feedback required, decision required
 * - Services: ITechnicalReviewService, IFinancialReviewService, ICommitteeReviewService
 * - Handlers: CQRS handlers
 * - Mapping: Review <-> Dto
 * - Business Rules: Sequential review stages, minimum score requirements, committee voting
 *
 * Module: Committee
 * - Commands: CreateDecision, UpdateDecision, VoteOnDecision, FinalizeDecision
 * - Queries: GetDecisionById, GetPendingDecisions, GetDecisionHistory
 * - DTOs: CommitteeDecisionDto, VoteDto
 * - Validators: All committee members voted (if required), valid voting options
 * - Services: ICommitteeService
 * - Handlers: CQRS handlers
 * - Mapping: CommitteeDecision <-> Dto
 * - Business Rules: Voting quorum, unanimous decisions for rejection, final approval only by majority
 *
 * Module: Contracts
 * - Commands: GenerateContract, SignContract, UpdateContract, ArchiveContract, TerminateContract
 * - Queries: GetContractById, GetMyContracts, GetContractsPaginated, GetSignatureStatus
 * - DTOs: ContractDto, SignatureDto, VersionDto
 * - Validators: Contract terms validation, signature requirements
 * - Services: IContractService
 * - Handlers: CQRS handlers
 * - Mapping: Contract <-> Dto
 * - Business Rules: Contract only after approval, both parties must sign, immutable once signed
 *
 * Module: Payments
 * - Commands: CreatePayment, ApprovePayment, ProcessPayment, RejectPayment, RecordTransaction
 * - Queries: GetPaymentById, GetMyPayments, GetPaymentsPaginated, GetPaymentHistory
 * - DTOs: PaymentDto, InstallmentDto, InvoiceDto, TransactionDto
 * - Validators: Amount validation, IBAN validation, due date validation
 * - Services: IPaymentService
 * - Handlers: CQRS handlers
 * - Mapping: Payment <-> Dto
 * - Business Rules: Installment schedule compliance, no partial payments (unless specified), audit trail required
 *
 * Module: Progress Reports
 * - Commands: CreateReport, SubmitReport, ApproveReport, RequestChanges, CompleteReport
 * - Queries: GetReportById, GetMyReports, GetReportsPaginated, GetBeneficiaryStats
 * - DTOs: ProgressReportDto, AttachmentDto
 * - Validators: Completion percentage validation, beneficiary data validation
 * - Services: IProgressReportService
 * - Handlers: CQRS handlers
 * - Mapping: ProgressReport <-> Dto
 * - Business Rules: Regular reporting schedule, mandatory during execution, beneficiary count validation
 *
 * Module: Notifications
 * - Commands: SendNotification, CreateTemplate, UpdateTemplate, MarkAsRead, DeleteNotification
 * - Queries: GetNotifications, GetNotificationsPaginated, GetTemplates
 * - DTOs: NotificationDto, TemplateDto
 * - Validators: Template syntax, recipient validation
 * - Services: INotificationService
 * - Handlers: CQRS handlers
 * - Mapping: Notification <-> Dto
 * - Business Rules: Multi-channel delivery, template variables, rate limiting
 *
 * Module: Dashboard
 * - Queries: GetAssociationDashboard, GetStaffDashboard, GetExecutiveDashboard, GetAdminDashboard
 * - DTOs: DashboardDto with various widgets (Statistics, Charts, RecentActivity)
 * - Validators: Date range validation for reports
 * - Services: IDashboardService
 * - Handlers: Query handlers
 * - Business Rules: Show only accessible data based on role
 *
 * Module: Administration
 * - Commands: CreateUser, UpdateUser, DeleteUser, UpdateRole, UpdatePermission, UpdateSettings
 * - Queries: GetUsers, GetRoles, GetPermissions, GetAuditLog, GetSettings
 * - DTOs: UserDto, RoleDto, PermissionDto, SettingDto, AuditLogDto
 * - Validators: Role/permission validation
 * - Services: IAdministrationService
 * - Handlers: CQRS handlers
 * - Mapping: Various entities <-> Dto
 * - Business Rules: No self-deletion, audit all changes, maintain hierarchy
 *
 * Module: Reports
 * - Queries: GetFinancialReport, GetPerformanceReport, GetGrantReport, GetAssociationReport, ExportReport
 * - DTOs: ReportDto with various data structures
 * - Validators: Date range validation
 * - Services: IReportService
 * - Handlers: Query handlers with complex calculations
 * - Business Rules: Aggregate data from multiple sources, maintain data integrity
 */

namespace DamaGrant.Application.Features;

// This file serves as documentation and structure reference.
// Each module should be implemented in its own folder following the patterns defined above.
