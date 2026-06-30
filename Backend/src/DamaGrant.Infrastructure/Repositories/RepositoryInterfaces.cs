using DamaGrant.Domain.Interfaces;

namespace DamaGrant.Infrastructure.Repositories;

public interface IUserRepository : IRepository<object>
{
}

public interface IAssociationRepository : IRepository<object>
{
}

public interface IApplicationRepository : IRepository<object>
{
}

public interface IReviewRepository : IRepository<object>
{
}

public interface IContractRepository : IRepository<object>
{
}

public interface IPaymentRepository : IRepository<object>
{
}

public interface IAuditLogRepository : IRepository<object>
{
}
