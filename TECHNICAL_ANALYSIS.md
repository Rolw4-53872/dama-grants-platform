# DAMA Grant & Qualification Management System
## Technical Analysis & Architecture Design

---

## 1. BUSINESS PROCESS ANALYSIS

### Process 1: Qualification Program
```
Association Registration → Profile Completion → Qualification Application
                                                         ↓
                                    → Draft → Submitted → Under Review
                                                         ↓
                                    ← (Revision Needed) ← Approved/Rejected
```

**Key Entities:**
- Association (User/Organization)
- Qualification Application
- Qualification Application Status
- Qualification Documents
- Revision History

---

### Process 2: Grant Project Management
```
Qualified Association → Browse Grants → Select Grant → Submit Proposal
                                                             ↓
Submitted → Technical Review → Financial Review → Committee Review
                    ↓ (Revision Needed)  ↓              ↓
            → Approved → Contract → Signature → Payment → Execution
                    ↓
                Rejected/Cancelled
```

**Key Entities:**
- Grant Opportunity
- Project Application
- Project Review (Technical, Financial, Committee)
- Contract
- Digital Signature
- Payment
- Progress Report
- Project Closure

---

## 2. USER ROLES & PERMISSIONS

### Role Matrix:

| Feature | Assoc | QualOff | ProjRev | FinRev | Comm | GrantMgr | Admin | Exec |
|---------|-------|---------|---------|--------|------|----------|-------|------|
| View Own Profile | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| Submit Qualification | ✓ | | | | | | | |
| Review Qualification | | ✓ | | | | | ✓ | ✓ |
| View Grants | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| Submit Project | ✓ | | | | | | | |
| Technical Review | | | ✓ | | | ✓ | ✓ | |
| Financial Review | | | | ✓ | | ✓ | ✓ | |
| Committee Review | | | | | ✓ | ✓ | ✓ | |
| Approve/Reject | | | | | ✓ | ✓ | ✓ | ✓ |
| Generate Contract | | | | | | ✓ | ✓ | |
| Approve Payment | | | | | | ✓ | ✓ | ✓ |
| View Reports | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| Admin Settings | | | | | | | ✓ | |

---

## 3. DATABASE ENTITIES & RELATIONSHIPS

### Core Entities:
1. **Users** (id, email, password, firstName, lastName, phone, role, status, createdAt, updatedAt)
2. **Associations** (id, userId, orgName, license, sector, registrationDate, status, createdAt, updatedAt)
3. **AssociationProfile** (id, associationId, boardMembers, bankAccount, address, contactInfo, createdAt, updatedAt)
4. **Documents** (id, ownerId, ownerType, fileName, fileUrl, documentType, uploadedAt, expiryDate, isValid)
5. **QualificationApplication** (id, associationId, status, submittedAt, reviewedAt, rejectionReason, createdAt, updatedAt)
6. **QualificationReview** (id, qualAppId, reviewerId, status, feedback, decidedAt, createdAt, updatedAt)
7. **GrantOpportunity** (id, title, description, budget, targetSector, applicationPeriod, eligibility, status, createdAt, updatedAt)
8. **ProjectApplication** (id, associationId, grantId, status, submittedAt, budget, expectedOutcome, createdAt, updatedAt)
9. **ProjectReview** (id, projectAppId, reviewerId, reviewType, status, feedback, decidedAt, createdAt, updatedAt)
   - ReviewType: Technical, Financial, Committee
10. **CommitteeDecision** (id, projectAppId, decisionStatus, votingResults, notes, decidedAt, createdAt, updatedAt)
11. **Contract** (id, projectAppId, status, signatureStatus, generatedAt, signedAt, createdAt, updatedAt)
12. **Payment** (id, projectAppId, amount, status, processedAt, invoiceId, createdAt, updatedAt)
13. **ProgressReport** (id, projectAppId, reportDate, content, attachments, submittedAt, approvedAt, createdAt, updatedAt)
14. **Notification** (id, userId, title, message, type, isRead, createdAt, readAt)
15. **AuditLog** (id, userId, action, entityType, entityId, oldValue, newValue, timestamp, ipAddress)
16. **Revision** (id, applicationId, applicationType, reason, requestedAt, submittedAt, createdAt, updatedAt)

---

## 4. API ENDPOINTS STRUCTURE

### Authentication:
- POST /auth/register
- POST /auth/login
- POST /auth/refresh
- POST /auth/logout
- GET /auth/profile

### Association:
- GET /associations/profile
- PUT /associations/profile
- GET /associations/documents
- POST /associations/documents
- DELETE /associations/documents/{id}

### Qualification:
- POST /qualification/applications
- GET /qualification/applications/{id}
- PUT /qualification/applications/{id}
- GET /qualification/applications
- GET /qualification/required-documents
- POST /qualification/submit
- GET /qualification/reviews (admin only)
- POST /qualification/reviews (officer only)

### Grants:
- GET /grants (public)
- GET /grants/{id} (public)
- POST /grants/apply
- GET /grants/my-applications
- GET /grants/my-applications/{id}

### Projects:
- POST /projects
- GET /projects/{id}
- GET /projects/my-projects
- PUT /projects/{id}
- POST /projects/{id}/submit
- GET /projects/{id}/timeline
- POST /projects/{id}/reports
- GET /projects/{id}/reports

### Reviews:
- GET /reviews/pending
- GET /reviews/{id}
- POST /reviews/{id}/complete
- GET /reviews/history

### Contracts:
- GET /contracts/{id}
- POST /contracts/{id}/sign
- GET /contracts/my-contracts

### Payments:
- GET /payments/{id}
- GET /payments/my-payments
- POST /payments/{id}/approve (admin only)

### Notifications:
- GET /notifications
- PUT /notifications/{id}/read
- DELETE /notifications/{id}

### Admin:
- GET /admin/users
- POST /admin/users
- PUT /admin/users/{id}
- DELETE /admin/users/{id}
- GET /admin/roles
- GET /admin/audit-logs
- GET /admin/grants
- POST /admin/grants
- PUT /admin/grants/{id}
- GET /admin/templates

---

## 5. FRONTEND PAGES STRUCTURE

### Auth Pages:
- /login
- /register
- /forgot-password
- /reset-password

### Association Dashboard:
- /dashboard
  - Overview
  - Qualification Status Widget
  - Project Status Widget
  - Pending Actions Widget
  - Available Grants Widget

### Association Features:
- /profile
  - Organization Info
  - License Info
  - Board Members
  - Bank Account
  - Address
  - Documents
- /qualification
  - Application Form
  - Application Status
  - Required Documents
  - Review History
- /grants
  - Browse Grants
  - Grant Details
  - Apply Form
  - My Applications
  - Application Details
- /projects
  - My Projects List
  - Project Details
  - Timeline
  - Budget
  - Documents
  - Reports
  - Payments
- /contracts
  - My Contracts
  - Contract Details
  - Digital Signature
- /payments
  - Payment History
  - Invoice Details
- /reports
  - My Reports
  - Report Details
- /notifications
  - Notification Center
  - Notification Details
- /settings
  - Account Settings
  - Security
  - Preferences

### Officer/Reviewer Pages:
- /officer-dashboard
  - Pending Applications
  - Under Review
  - Returned Applications
- /officer/review
  - Application/Project Review Form
  - Review History
  - Decision Making

### Committee Pages:
- /committee-dashboard
  - Projects Waiting Decision
  - Voting Interface
  - Decision History

### Admin Pages:
- /admin
  - Overview
  - Statistics
- /admin/users
  - User List
  - User Create/Edit
  - User Permissions
- /admin/roles
  - Role Management
- /admin/grants
  - Grant Management
  - Grant Creation
- /admin/templates
  - Qualification Templates
  - Notification Templates
- /admin/audit-logs
  - Activity Logs
  - Audit Trail
- /admin/settings
  - System Configuration
  - Email Templates
  - Workflow Configuration

---

## 6. AUTHENTICATION & AUTHORIZATION

### JWT Flow:
1. User logs in with email/password
2. Backend validates and generates JWT token (access + refresh)
3. Frontend stores JWT in secure HTTP-only cookie
4. All requests include JWT in Authorization header
5. Backend validates token and checks role-based permissions

### Authorization:
- Role-based access control (RBAC)
- Permission checks on every API endpoint
- Frontend route guards prevent unauthorized access
- Backend enforces permissions on all operations

---

## 7. NOTIFICATIONS SYSTEM

### Types:
- Application Status Changed
- Review Completed
- Revision Requested
- Payment Processed
- Document Expiring Soon
- Deadline Approaching

### Delivery:
- In-app notifications (real-time WebSocket)
- Email notifications (SMTP)
- Push notifications (optional)

---

## 8. AUDIT & COMPLIANCE

### Audit Trail:
- All data changes logged with user, timestamp, old/new values
- Soft delete for data integrity
- Immutable audit logs

### Compliance:
- GDPR compliance (data export, deletion)
- Document retention policies
- Access control audit trail

---

## 9. TECHNOLOGY STACK

**Backend:**
- ASP.NET Core 9
- Entity Framework Core
- SQL Server
- JWT Authentication
- Clean Architecture
- SOLID Principles

**Frontend:**
- Next.js 15 (App Router)
- React 19
- TypeScript
- Tailwind CSS
- shadcn/ui
- React Hook Form
- TanStack Query (React Query)
- Zustand (State Management)

**Database:**
- SQL Server
- Entity Framework Core
- Migrations via EF Core

**Infrastructure:**
- Docker
- Azure (optional)
- GitHub Actions (CI/CD)

---

## 10. IMPLEMENTATION ROADMAP

Phase 1: Backend Setup
- Solution structure
- Database models
- Authentication
- Repositories & UnitOfWork
- Services
- API endpoints

Phase 2: Frontend Setup
- Project structure
- Layout components
- Authentication pages
- Theme configuration

Phase 3: Core Features
- Qualification workflow
- Grant management
- Project submission
- Review workflow

Phase 4: Advanced Features
- Notifications
- Audit logs
- Admin panel
- Analytics/Reports

Phase 5: Production
- Testing
- Deployment
- Documentation
- Security hardening

---

END OF ANALYSIS

Ready to proceed to STEP 1: FOLDER STRUCTURE GENERATION
