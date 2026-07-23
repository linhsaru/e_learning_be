using SharedKernel.Common;
using SharedKernel.Specifications;

namespace SharedKernel.Interfaces;

/// <summary>
/// Read repository interface for data access operations.
/// </summary>
public interface IReadRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetBySpecAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    IQueryable<TEntity> AsQueryable();
}
