namespace SharedKernel.Common;

public abstract class AuditableEntity<TId> : BaseEntity<TId>
{
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public Guid? UpdatedBy { get; protected set; }
    public bool IsDeleted { get; protected set; } = false;
    public DateTime? DeletedAt { get; protected set; }
    public Guid? DeletedBy { get; protected set; }

    public void MarkAsCreated(Guid? userId = null, DateTime? utcNow = null)
    {
        CreatedAt = utcNow ?? DateTime.UtcNow;
        CreatedBy = userId;
    }

    public void MarkAsUpdated(Guid? userId = null, DateTime? utcNow = null)
    {
        UpdatedAt = utcNow ?? DateTime.UtcNow;
        UpdatedBy = userId;
    }

    public void MarkAsDeleted(Guid? userId = null, DateTime? utcNow = null)
    {
        IsDeleted = true;
        DeletedAt = utcNow ?? DateTime.UtcNow;
        DeletedBy = userId;
    }
}

public abstract class AuditableEntity : AuditableEntity<Guid>
{
}
