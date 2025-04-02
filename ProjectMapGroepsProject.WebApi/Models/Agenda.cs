using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMapGroepsproject.WebApi.Models
{
    public class Agenda
    {
        public Guid Id { get; set; }

        [StringLength(8)]
        public string? date1 { get; set; }

        [StringLength(50)]
        public string? location1 { get; set; }

        [StringLength(8)]
        public string? date2 { get; set; }

        [StringLength(50)]
        public string? location2 { get; set; }

        [StringLength(8)]
        public string? date3 { get; set; }

        [StringLength(50)]
        public string? location3 { get; set; }

        public Guid UserId { get; set; }
    }
}