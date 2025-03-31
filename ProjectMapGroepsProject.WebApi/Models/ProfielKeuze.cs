using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
