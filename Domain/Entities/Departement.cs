﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Departement : BaseEntity
    { 
        public int Id {  get; set; }
        public int MinistryId { get; set; }
        public Ministry Ministry { get; set; } = null!;
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [MaxLength(55)]
        public  string? shortName { get; set; }
        public DateOnly startDate { get; set; } // Date d'ouverture
        public List<Member> Members { get; } = [];
        public List<Program> Programs { get; } = [];
       
    }
}
