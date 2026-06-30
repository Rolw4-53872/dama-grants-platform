using DamaGrant.Infrastructure.Persistence;

namespace DamaGrant.Infrastructure.Repositories;

public class UserRepository : Repository<object>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}

public class AssociationRepository : Repository<object>, IAssociationRepository
{
    public AssociationRepository(AppDbContext context) : base(context)
    {
    }
}

public class ApplicationRepository : Repository<object>, IApplicationRepository
{
    public ApplicationRepository(AppDbContext context) : base(context)
    {
    }
}

public class ReviewRepository : Repository<object>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }
}

public class ContractRepository : Repository<object>, IContractRepository
{
    public ContractRepository(AppDbContext context) : base(context)
    {
    }
}

public class PaymentRepository : Repository<object>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }
}

public class AuditLogRepository : Repository<object>, IAuditLogRepository
{
    public AuditLogRepository(AppDbContext context) : base(context)
    {
    }
}
