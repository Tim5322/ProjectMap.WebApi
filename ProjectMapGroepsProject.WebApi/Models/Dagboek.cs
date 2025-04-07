using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMap.WebApi.Models
{
    public class Dagboek
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(250)]
        public string? DagboekBladzijde1 { get; set; }

        [StringLength(250)]
        public string? DagboekBladzijde2 { get; set; }

        [StringLength(250)]
        public string? DagboekBladzijde3 { get; set; }

        [StringLength(250)]
        public string? DagboekBladzijde4 { get; set; }

        [ForeignKey("ProfielKeuzeId")]
        public Guid ProfielKeuzeId { get; set; } // Foreign key to ProfielKeuze

        public ProfielKeuze? ProfielKeuze { get; set; } // Navigation property for ProfielKeuze
    }
}



