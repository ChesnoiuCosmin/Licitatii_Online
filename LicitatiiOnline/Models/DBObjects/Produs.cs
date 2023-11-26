using System;
using System.Collections.Generic;

namespace LicitatiiOnline.Models.DBObjects
{
    public partial class Produs
    {
        public Produs()
        {
            ImagineProdus = new HashSet<ImagineProdus>();
            Licitatii = new HashSet<Licitatii>();
        }

        public Guid IdProdus { get; set; }
        public string? NumeProdus { get; set; }
        public string? Descriere { get; set; }
        public string? Categorie { get; set; }
        public decimal? PretPornire { get; set; }
        public DateTime? DataIncheiereLicitatie { get; set; }
        public string? Utilizator_Ofertant_Curent { get; set; }
        public string? Stare { get; set; }

        public string? CaleImagine { get; set; }

        public virtual ICollection<ImagineProdus> ImagineProdus { get; set; }
        public virtual ICollection<Licitatii> Licitatii { get; set; }
    }
}
