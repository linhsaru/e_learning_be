using SharedKernel.Common;

namespace SharedKernel.Interfaces;

/// <summary>
/// AggregateRoot Repository interface for CRUD operations.
/// Inherits from IReadRepository for unified data access.
/// </summary>
public interface IRepository<TEntity, TId> : IReadRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
{
    Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
