using Domain.Entities.Interfaces;

namespace Domain.Entities
{
    /// <summary>
    ///     Permet de marquer une entité comme étant sujette à une suppression logique (soft delete).
    /// </summary>
    public abstract class SoftDeleteEntity : ISoftDelete
    {

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }


        // Soft delete manuel
        public virtual void Delete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow;
        }

        // Restore manuel
        public virtual void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
