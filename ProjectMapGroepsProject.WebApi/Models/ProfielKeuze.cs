using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMap.WebApi.Models
{
    public class ProfielKeuze
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Range(0, 120)]
        public int Leeftijd { get; set; }

        public string? Arts { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public string? User { get; set; } // Navigatie-eigenschap
    }
}
