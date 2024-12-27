﻿namespace IccPlanner.Domain.Entities
{
    public class Departement
    {
        private int Id {  get; set; }
        public Guid MinistryId { get; set; }
        private Ministry Ministry { get; set; } = null!;
        private string Name { get; set; }
        private string Description { get; set; }
        private  string shortName { get; set; }
        private DateOnly startDate { get; set; } // Date d'ouverture
        private List<Member> Members { get; } = [];
        private List<Program> Programs { get; } = [];
       
    }
}