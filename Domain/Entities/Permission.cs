﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; } 
        [MaxLength(255)]
        public required string Name { get; set; }
        public required string Description { get; set; }    
    }
}
