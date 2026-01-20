namespace Domain.Entities.Interfaces
{
    /// <summary>
    ///     Interface pour les entités supportant la suppression logique (soft delete).
    /// </summary>
    public interface ISoftDelete
    {
       bool IsDeleted { get; set; }
       DateTimeOffset? DeletedAt { get; set; }
    }
}
