namespace DamaGrant.Domain.Interfaces;

public interface IEntity
{
    int Id { get; }
}

public interface IAuditableEntity : IEntity
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    bool IsDeleted { get; }
    DateTime? DeletedAt { get; }
}
