using System;

namespace WebServer.Dtos
{
    public class DeviceReadDto
    {
        public int GeräteID { get; set; }

        public string Modell { get; set; }

        public int Baujahr { get; set; }

        public string Seriennummer { get; set; }
    
        public bool Verfügbar { get; set; }
      
        public string Historie { get; set; }
      
        public DateTime? DatumAusleihe { get; set; }
       
        public string AusgeliehenAn { get; set; }

        public string Bemerkung { get; set; }

        public bool Aktiv { get; set; }

    }
}