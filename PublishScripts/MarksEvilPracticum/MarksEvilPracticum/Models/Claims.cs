using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarksEvilPracticum.Models
{
    public class Claims
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser Klant { get; set; }
        [Required]
        public string Kenteken { get; set; }

        [Required]
        public string Titel { get; set; }
        [Required]
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public ClaimsEnum Status { get; set; }
    }
}