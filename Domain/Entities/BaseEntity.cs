﻿namespace Domain.Entities
{
    /// <summary>
    /// Ajouter de colonne par default 
    /// </summary>
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; 
    }
}
