using System;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class Device
    {
        
        [Key]
        public int GeräteID { get; set; }

        [Required]
        public string Modell { get; set; }

        [Required]
        public int Baujahr { get; set; }

        [Required]
        public string Seriennummer { get; set; }

        [Required]
        public bool Verfügbar { get; set; }

        [Required]
        public string Historie { get; set; }
      
        [Required]
        public DateTime? DatumAusleihe { get; set; }

        [Required]
        public string AusgeliehenAn { get; set; }

        [Required]
        public string Bemerkung { get; set; }

        [Required]
        public bool Aktiv { get; set; }
    }
}